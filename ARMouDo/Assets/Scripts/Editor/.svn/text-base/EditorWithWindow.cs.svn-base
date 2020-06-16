/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using UnityEditor;
using PlaceAR.LabelDatas;
using System.Collections.Generic;
using System.IO;

namespace PlaceAR
{
    /// <summary>
    /// 编辑窗口
    /// </summary>
    public class EditorWithWindow : EditorWindow
    {
        /// <summary>
        /// 编辑的模型
        /// </summary>
        private GameObject prefab;
        /// <summary>
        /// 实例化出来的模型
        /// </summary>
        private GameObject prefabOnline;
        /// <summary>
        /// 模型名称
        /// </summary>
        private string prefabName;
        /// <summary>
        /// 模型管理
        /// </summary>
        private PrefabControl prefabControl;
        /// <summary>
        /// 模型类型
        /// </summary>
        private LabelDatas.PrefabType prefabType;
        /// <summary>
        /// 加载的数据
        /// </summary>
        private string labelText;
        private bool showline = false;
        public static string sourcePath;
        /// <summary>
        /// 模型控制类型
        /// </summary>
        private LabelDatas.ControlType controlType = ControlType.拆解模式;


        [MenuItem("Tools/编辑窗口")]
        private static void Open()
        {

            EditorWithWindow window = (EditorWithWindow)EditorWindow.GetWindow(typeof(EditorWithWindow));
            window.Show();
        }

        void OnInspectorUpdate()
        {
            if (showline)
            {
                List<PrefabChildControl> childData = prefabControl.childData;
                for (int i = 0; i < childData.Count; i++)
                {
                    childData[i].UpdateLine();
                }
            }
            //Debug.Log("qwe");
            Repaint();
        }
        void OnGUI()
        {
            EditorGUI.LabelField(new Rect(0, 0, 500, 20), "是否在编译 ? ", EditorApplication.isCompiling ? "正在编译中..." : "编译完成!");
            GUI.Label(new Rect(0, 20, 500, 20), "设置模型初始信息", EditorStyles.boldLabel);
            prefab = (GameObject)EditorGUI.ObjectField(new Rect(0, 40, 500, 20), "编辑的模型", prefab, typeof(GameObject));
            if (!prefab) return;
            if (GUI.changed) OnInit();
            if (prefabName == null)
                prefabName = prefab.name;
            prefabName = EditorGUI.TextField(new Rect(0, 60, 482, 20), "模型名称", prefabName);
            if (GUI.Button(new Rect(10, 90, 120, 20), "重置"))
            {
                OnInit();
                EditorApplication.Beep();
            }
            if (GUI.Button(new Rect(150, 90, 120, 20), "保存信息"))
            {
                OnSaveLabelText();
            }
            if (GUI.Button(new Rect(290, 90, 120, 20), "加载信息"))
            {
                OnLoadLabelText();
            }
            if (GUI.Button(new Rect(430, 90, 120, 20), "打包模型"))
            {
                //SaveAssetBundle();
                OnSaveAssetBundle();
            }
            GUI.Label(new Rect(0, 120, 60, 20), "模型类型:");
            prefabType = (LabelDatas.PrefabType)EditorGUI.EnumPopup(new Rect(60, 120, 120, 20), prefabType);
            GUI.Label(new Rect(200, 120, 60, 20), "控制模式选择:");
            controlType = (ControlType)EditorGUI.EnumPopup(new Rect(270, 120, 120, 20), controlType);
            GUI.Label(new Rect(0, 140, 100, 20), "模型3D名称编辑:");
            if (GUI.Button(new Rect(10, 170, 120, 20), "显示名称锚点"))
            {
                showline = true;
                ShowAnchor();
            }
            if (GUI.Button(new Rect(150, 170, 120, 20), "关闭名称锚点"))
            {
                showline = false;
                ShowAnchor();
                // OnLoadLabelTextT(false);
            }
            if (GUI.Button(new Rect(290, 170, 120, 20), "复位名称锚点"))
            {
                List<PrefabChildControl> childData = prefabControl.childData;
                for (int i = 0; i < childData.Count; i++)
                {
                    childData[i].ResetLine();
                }
            }
            if (!string.IsNullOrEmpty(labelText))
            {
                if (GUI.Button(new Rect(10, 200, 120, 20), "分"))
                {
                    OnLoadLabelTextT(true);
                }
                if (GUI.Button(new Rect(150, 200, 120, 20), "合"))
                {
                    OnLoadLabelTextT(false);
                }

            }
        }
        private void ShowAnchor()
        {
            if (showline)
            {
                List<PrefabChildControl> childData = prefabControl.childData;
                for (int i = 0; i < childData.Count; i++)
                {
                    childData[i].UpdateLine();
                }
            }
            else
            {
                List<PrefabChildControl> childData = prefabControl.childData;
                for (int i = 0; i < childData.Count; i++)
                {
                    childData[i].CloseLine();
                }
            }
        }
        private void OnInit()
        {
            if (!prefab)
                return;

            if (prefabOnline)
                DestroyImmediate(prefabOnline);

            prefabOnline = Instantiate(prefab);//, Vector3.zero, Quaternion.Euler(0, 180, 0)) as GameObject;
            prefabOnline.transform.position = prefab.transform.position;
            prefabOnline.name = prefab.name;
            prefabControl = prefabOnline.AddComponent<PrefabControl>();
            prefabControl.OnInitEditor();
            labelText = null;
        }
        void OnSaveLabelText()
        {
            string path = EditorUtility.SaveFilePanel("保存数据文件", "", prefab.name, "txt");
            if (path.Length != 0)
            {
                prefabControl.OnInitEditor(prefabName, prefabType, controlType);
                labelText = JsonFx.Json.JsonWriter.Serialize(prefabControl.DataList);
                Debug.Log("Data String : " + labelText);

                InternalDataCodec.SaveString(path, labelText);
            }
        }
        void SaveAssetBundle()
        {
            sourcePath = Application.dataPath + "/Prefabs/Builder/";
            string prefabName = prefab.name;
            string path = Application.dataPath;// EditorUtility.SaveFolderPanel("保存打包文件", prefabName, "");
            if (!Directory.Exists(sourcePath))
            {
                Directory.CreateDirectory(sourcePath);
            }
            DeletePrefab(sourcePath);
            ClearAssetBundlesName();
            Object tempPrefab = PrefabUtility.CreateEmptyPrefab("Assets/Prefabs/Builder/" + prefab.name + ".prefab");
            tempPrefab = PrefabUtility.ReplacePrefab(prefab, tempPrefab);

            Pack(sourcePath);
            // Debug.Log(path);
            BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.iOS);
            // Object.DestroyImmediate(tempPrefab);
            AssetDatabase.Refresh();
        }
        void OnSaveAssetBundle()
        {

            string prefabName = prefab.name;
            string path = EditorUtility.SaveFilePanel("保存打包文件", "", prefabName, "unity3d");


            // List<Object> toinclude = new List<Object>();
            //Model

            GameObject clone = (GameObject)Object.Instantiate(prefab.gameObject);
            clone.name = prefab.name;
            string tempPrefabPath = "Assets/" + prefab.name + ".prefab";
            Object tempPrefab = PrefabUtility.CreateEmptyPrefab(tempPrefabPath);
            tempPrefab = PrefabUtility.ReplacePrefab(clone, tempPrefab);
            path = path.Replace(prefabName + ".unity3d", "");
            AssetBundleBuild[] assetBundle = new AssetBundleBuild[1];
            assetBundle[0].assetBundleName = prefab.name + ".unity3d";
            assetBundle[0].assetNames = new string[] { tempPrefabPath };
            BuildPipeline.BuildAssetBundles(path, assetBundle, BuildAssetBundleOptions.None, BuildTarget.iOS);
            // BuildPipeline.BuildAssetBundles(path, assetBundle);
            Object.DestroyImmediate(clone);
            var labelText = JsonFx.Json.JsonWriter.Serialize(prefabControl.DataList);
            //var tempTextPath = "Assets/label.asset";

            StringHolder holder = ScriptableObject.CreateInstance<StringHolder>();
            holder.content = new string[] { labelText };
            // AssetDatabase.CreateAsset(holder, tempTextPath);
            // AssetDatabase.DeleteAsset(tempTextPath);
            AssetDatabase.DeleteAsset(tempPrefabPath);
            AssetDatabase.Refresh();
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
        static void DeletePrefab(string source)
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
                        File.Delete(files[i].FullName);
                        //FileTools.Delete(files[i].FullName);
                        // file(files[i].FullName);
                    }
                }
            }
        }
        static void file(string source)
        {
            Debug.Log(source);
            string _source = Replace(source);
            string _assetPath = "Assets" + _source.Substring(Application.dataPath.Length);
            string _assetPath2 = _source.Substring(Application.dataPath.Length + 1);
            //Debug.Log (_assetPath);  

            //在代码中给资源设置AssetBundleName  
            AssetImporter assetImporter = AssetImporter.GetAtPath(_assetPath);
            string assetName = _assetPath2.Substring(_assetPath2.IndexOf("/") + 1);
            Debug.Log(assetName);
            assetName = assetName.Replace(System.IO.Path.GetExtension(assetName), ".unity3d");
            Debug.Log(assetName);
            //Debug.Log (assetName);  
            assetImporter.assetBundleName = assetName;
        }
        static string Replace(string s)
        {
            return s.Replace("\\", "/");
        }
        /// <summary>  
        /// 清除之前设置过的AssetBundleName，避免产生不必要的资源也打包  
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
        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="isClose"></param>
        void OnLoadLabelTextT(bool isClose)
        {
            var dataList = JsonFx.Json.JsonReader.Deserialize<LabelDataList>(labelText);
            prefabControl.OnUpdate(dataList.list, isClose);

        }
        void OnLoadLabelText()
        {
            string path = EditorUtility.OpenFilePanel("加载数据文件", "", "txt");

            if (path.Length != 0)
            {
                labelText = InternalDataCodec.LoadString(path);
                //string labelText = _InternalDataCodec.LoadString(path);
                LabelDataList dataList = JsonFx.Json.JsonReader.Deserialize<LabelDataList>(labelText);
                prefabName = dataList.transform.ChinaName;
                prefabControl.name = dataList.transform.Name;
                prefabType = (LabelDatas.PrefabType)dataList.prefabType;
                controlType = (ControlType)dataList.controlType;
                prefabControl.OnUpdate(dataList.list);
            }
        }
    }
}
