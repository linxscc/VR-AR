using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;


// 注意 因为 Windows 的路径分隔符为 '/', Mac OS 系统的路径分隔符号为 '\' 大坑
// 当时是在 Mac 上写的打包方法， 没注意到这个大坑， 后来在 Windows 上使用时发现此坑
// 如果发现打包不成功，可以打印一下代码中有关路径的地方，看看是不是有问题
// 大致就是使用如   pathReplace = path.Replace('\\', '/');   pathReplace = path.Replace('/', '\\');
// 转换下即可
public class BuildAssetBundle : Editor {

    //需要打包的路径
    private static string assetPath = "AllAssets";

    //导出包路径
    private static string AssetBundleOutPsth = "Assets/StreamingAssets";

    //保存需要打包的资源路径
    private static List<string> assetPathList = new List<string>();

    //需要打包的资源后缀
    private static Dictionary<string, string> asExtensionDic = new Dictionary<string, string> ();

    [MenuItem("Assets/BuildAssetBundle")]
    private static void BuildAssetBundleSource()
    {
        assetPathList.Clear ();

        SetASExtensionDic();
        //根据不同平台拼接不同平台导出路径
        string outPsth = Path.Combine (AssetBundleOutPsth, Plathform.GetPlatformFolder(EditorUserBuildSettings.activeBuildTarget));

        GetDirs(Application.dataPath + "/" + assetPath);

        BuildAsset (outPsth);
    }

    //添加需要打包资源的后缀
    private static void SetASExtensionDic ()
    {
        asExtensionDic.Clear ();

        asExtensionDic.Add (".prefab", ".unity3d");
        asExtensionDic.Add (".mat", ".unity3d");
        asExtensionDic.Add (".png", ".unity3d");
    }

    //遍历制定文件夹获取需要打包的资源路径
    private static void GetDirs(string dirPath)
    {
        foreach (string path in Directory.GetFiles(dirPath))
        {
            // 通过资源后缀判断资源是否为需要打包的资源
            if (asExtensionDic.ContainsKey(System.IO.Path.GetExtension(path)))
            {
                string pathReplace = "";

                // Windows 平台分隔符为 '/', OS 平台 路径分隔符为 '\'， 此处是一个大坑
                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    pathReplace = path.Replace('\\', '/');
                }

                //将需要打包的资源路径添加到打包路劲中
                assetPathList.Add(pathReplace);
            }
        }

        if (Directory.GetDirectories(dirPath).Length > 0)  //遍历所有文件夹
        {
            foreach (string path in Directory.GetDirectories(dirPath))
            {
                //使用递归方法遍历所有文件夹
                GetDirs(path);
            }
        }
    }

    //清除已经打包的资源 AssetBundleNames
    private static void ClearAssetBundlesName()
    {
        int length = AssetDatabase.GetAllAssetBundleNames ().Length;
        Debug.Log (length);
        string[] oldAssetBundleNames = new string[length];
        for (int i = 0; i < length; i++) 
        {
            oldAssetBundleNames[i] = AssetDatabase.GetAllAssetBundleNames()[i];
        }

        for (int j = 0; j < oldAssetBundleNames.Length; j++) 
        {
            AssetDatabase.RemoveAssetBundleName(oldAssetBundleNames[j],true);
        }
    }

    //打AS包
    private static void BuildAsset(string outPath)
    {
        //遍历获取到的打包资源路径
        for (int i = 0; i < assetPathList.Count; i ++) 
        {
            string asPath = assetPathList[i];


            // 在Windows 平台 要从 Assets/ 开始，和 Mac 上不同，此处也是一个大坑
            string path = "";
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                path = asPath.Substring(asPath.IndexOf("Assets/"));
            }

            //通过资源路径来获取需要打包的资源
            AssetImporter assetImporter = AssetImporter.GetAtPath(path);
            if (assetImporter == null)
            {
                Debug.LogWarning("null  " + path);
                continue;
            }

            // 从此处(assetPath = "AllAssets")截取路径  
            string assetName = asPath.Substring(asPath.IndexOf(assetPath));
            //替换后缀名
            assetName = assetName.Replace(Path.GetExtension(assetName), ".unity3d");
            //设置打包资源的名字包括后缀
            assetImporter.assetBundleName = assetName;
        }

        //如果不存在到处路径文件，创建一个
        if (!Directory.Exists (outPath)) {
            Directory.CreateDirectory(outPath);
        }

        //打包
        BuildPipeline.BuildAssetBundles (outPath, 0, EditorUserBuildSettings.activeBuildTarget);

        //刷新资源路径,避免生成的文件不显示
        AssetDatabase.Refresh ();
    }
}


//根据切换的平台返回相应的导出路径
public class Plathform
{
    public static string GetPlatformFolder(BuildTarget target)
    {
        switch (target)
        {
        case BuildTarget.Android:   //Android平台导出到 Android文件夹中
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