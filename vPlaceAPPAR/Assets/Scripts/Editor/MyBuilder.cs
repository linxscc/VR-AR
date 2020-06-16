using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using PlaceAR.LabelDatas;
using System.Collections.Generic;
using PlaceAR;
using System.Text;
using System.Runtime.Serialization;
/// <summary>  
/// 把Resource下的资源打包成.unity3d 到StreamingAssets目录下  
/// </summary>  
public class MyBuilder : Editor
{
    public static string sourcePath = Application.dataPath + "/Prefabs/Builder";
    private const string AssetBundlesOutputPath = "Assets/Prefabs/Builder";
   // [MenuItem("Tools/全部打包")]
    public static void BuildAssetBundle()
    {
        ClearAssetBundlesName();

        Pack(sourcePath);

        string outputPath = System.IO.Path.Combine(AssetBundlesOutputPath, Platform.GetPlatformFolder(EditorUserBuildSettings.activeBuildTarget));
        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }

        //根据BuildSetting里面所激活的平台进行打包  
        BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);

        AssetDatabase.Refresh();

        Debug.Log("打包完成");

    }
    private void SaveText()
    {

    }
    /// <summary>  
    /// 清除之前设置过的AssetBundleName，避免产生不必要的资源也打包  
    /// 之前说过，只要设置了AssetBundleName的，都会进行打包，不论在什么目录下  
    /// </summary>  
    static void ClearAssetBundlesName()
    {
        int length = AssetDatabase.GetAllAssetBundleNames().Length;
        Debug.Log(length);
        string[] oldAssetBundleNames = new string[length];
        for (int i = 0; i < length; i++)
        {
            oldAssetBundleNames[i] = AssetDatabase.GetAllAssetBundleNames()[i];
        }

        for (int j = 0; j < oldAssetBundleNames.Length; j++)
        {
            AssetDatabase.RemoveAssetBundleName(oldAssetBundleNames[j], true);
        }
        length = AssetDatabase.GetAllAssetBundleNames().Length;
        Debug.Log(length);
    }

    static void Pack(string source)
    {
        DirectoryInfo folder = new DirectoryInfo(source);
        FileSystemInfo[] files = folder.GetFileSystemInfos();
        int length = files.Length;
        for (int i = 0; i < length; i++)
        {
            if (files[i] is DirectoryInfo)
            {
                Pack(files[i].FullName);
            }
            else
            {
                if (!files[i].Name.EndsWith(".meta"))
                {
                    file(files[i].FullName);
                }
            }
        }
    }

    static void file(string source)
    {
        string _source = Replace(source);
        string _assetPath = "Assets" + _source.Substring(Application.dataPath.Length);
        string _assetPath2 = _source.Substring(Application.dataPath.Length + 1);
        //Debug.Log (_assetPath);  

        //在代码中给资源设置AssetBundleName  
        AssetImporter assetImporter = AssetImporter.GetAtPath(_assetPath);
        string assetName = _assetPath2.Substring(_assetPath2.IndexOf("/") + 1);
        assetName = assetName.Replace(System.IO.Path.GetExtension(assetName), ".unity3d");
        //Debug.Log (assetName);  
        assetImporter.assetBundleName = assetName;
    }

    static string Replace(string s)
    {
        return s.Replace("\\", "/");
    }

   // [MenuItem("Tools/生成子物体的预制物")]
    public static void BatchPrefab()
    {

        Transform tParent = ((GameObject)Selection.activeObject).transform;
        Object tempPrefab;
        List<LabelData> datas = new List<LabelData>();
        //int i = 0;
        for (int i = 0; i < tParent.childCount; i++)
        {
            tempPrefab = PrefabUtility.CreateEmptyPrefab("Assets/Prefabs/Builder/" + tParent.GetChild(i).name + ".prefab");

            tempPrefab = PrefabUtility.ReplacePrefab(tParent.GetChild(i).gameObject, tempPrefab);
            LabelData labelData = new LabelData(tParent.GetChild(i).name,null, null, null, tParent.GetChild(i).transform.position, tParent.GetChild(i).transform.eulerAngles);
            datas.Add(labelData);
        }
        LabelDataList dataList = new LabelDataList() { transform = tParent.Serialization(tParent.name, tParent.name), list = datas };
        string data = JsonFx.Json.JsonWriter.Serialize(dataList);
        Debug.Log(AssetBundlesOutputPath);
        SaveString(AssetBundlesOutputPath, data, tParent.name);
        Debug.Log(dataList.ToString());


        //foreach (Transform t in tParent)
        //{

        //    tempPrefab = PrefabUtility.CreateEmptyPrefab("Assets/Resources/prefab" + i + ".prefab");

        //    tempPrefab = PrefabUtility.ReplacePrefab(t.gameObject, tempPrefab);
        //    i++;
        //}

    }
    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="localPath"></param>
    /// <param name="file"></param>
    private static void SaveString(string localPath, string file,string dataName)
    {
        try
        {
            string url = sourcePath +"/"+ dataName + ".txt";
            FileStream aFile = new FileStream(@"" + url, FileMode.OpenOrCreate);
            StreamWriter outfile = new StreamWriter(aFile);

            byte[] b = Encoding.UTF8.GetBytes(file);
            string c = Encoding.UTF8.GetString(b);
            outfile.Write(c);
            outfile.Close();
            outfile.Dispose();
            Debug.Log(file + "\n导出成功");


        }
        catch (SerializationException e)
        {
            Debug.Log(e.Message);
            throw;
        }
    }
   // [MenuItem("Tools/打包场景")]
    static void CreateSceneALL()
    {
        //取得当前选择场景文件路径
        string myPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        //去掉路径前部分得到文件名
        string path = myPath.Replace("Assets/", "");
        //替换文件扩展名
        path = path.Replace("unity", "unity3d");
        //清楚缓存
        Caching.ClearCache();
        //拼接出打包之后文件存放路径（和场景文件同目录下）
        string Path = Application.dataPath + "/" + path;
        string[] array = path.Split('.');
        for (int i = 0; i < array.Length; i++)
        {
            Debug.Log(array[i]);
        }
        string[] levels = { myPath };
        //打包场景
        BuildPipeline.BuildPlayer(levels, Path, BuildTarget.Android, BuildOptions.BuildAdditionalStreamedScenes);
        //刷新编辑器
        AssetDatabase.Refresh();
    }
}

public class Platform
{
    public static string GetPlatformFolder(BuildTarget target)
    {
        switch (target)
        {
            case BuildTarget.Android:
                return "Android";
            case BuildTarget.iOS:
                return "IOS";
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                return "Windows";
            case BuildTarget.StandaloneOSXIntel:
            case BuildTarget.StandaloneOSXIntel64:
            case BuildTarget.StandaloneOSX:
                return "OSX";
            default:
                return null;
        }
    }
}



