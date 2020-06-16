using UnityEngine;
using UnityEditor;

using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ModelViewerProject.Label3D
{

    public class LabelEditorWindow : EditorWindow
    {

        [MenuItem("Window/Label Editor")]
        static void Init()
        {
            LabelEditorWindow window = (LabelEditorWindow)EditorWindow.GetWindow(typeof(LabelEditorWindow));
            window.Show();
        }

        GameObject srcMeshObj;//modelObject;
        Label3DController labelController;
        /// <summary>
        /// 名称
        /// </summary>
        string prefabName;
        /// <summary>
        /// 模型名称
        /// </summary>
        string localName;
        GameObject sceneObj;
        AnimaiomType animaiomType;
        ControlType controlType;
        float speed;
        float x, y, z;
        /// <summary>
        /// 加载的数据
        /// </summary>
        string labelText;
        void OnInspectorUpdate()
        {
            Repaint();
        }

        void OnGUI()
        {
            EditorGUILayout.LabelField("是否在编译 ? ", EditorApplication.isCompiling ? "正在编译中..." : "编译完成!");

            GUILayout.Label("设置模型初始信息", EditorStyles.boldLabel);

            srcMeshObj = (GameObject)EditorGUILayout.ObjectField("编辑的模型", srcMeshObj, typeof(GameObject));

            if (!srcMeshObj)
                return;
  

            if (GUI.changed)
                OnInit();
            if(prefabName==null)
            prefabName = srcMeshObj.name;
            prefabName = EditorGUILayout.TextField("模型名称", prefabName);
            if (GUILayout.Button("重置"))
            {
                OnInit();
                EditorApplication.Beep();

            }


            if (GUILayout.Button("保存信息"))
            {
                OnSaveLabelText();
            }

            if (GUILayout.Button("加载信息"))
            {
                OnLoadLabelText();
            }
            if (labelText != null)
            {
                if (GUILayout.Button("分"))
                {
                    OnLoadLabelTextT(true);
                }
                if (GUILayout.Button("合"))
                {
                    OnLoadLabelTextT(false);
                }
            }
            if (GUILayout.Button("打包模型"))
            {
                OnSaveAssetBundle();
            }
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("选择界面模型动画类型:");

           // x = EditorGUILayout.FloatField("x:", x);
           // y = EditorGUILayout.FloatField("y:", y);
           // z = EditorGUILayout.FloatField("z:", z);
            animaiomType = (AnimaiomType)EditorGUILayout.EnumPopup(animaiomType);
            //speed =  EditorGUILayout.FloatField("                       动画速度:", speed);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("控制模式选择:");
            controlType = (ControlType)EditorGUILayout.EnumPopup(controlType);
            EditorGUILayout.EndHorizontal();
        }
        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="isClose"></param>
        void OnLoadLabelTextT(bool isClose)
        {
            var dataList = JsonFx.Json.JsonReader.Deserialize<LabelDataList>(labelText);
            labelController.OnUpdate(dataList.list, isClose);

        }
        void OnInit()
        {

            if (!srcMeshObj)
                return;

            if (sceneObj)
                DestroyImmediate(sceneObj);

            sceneObj = Instantiate(srcMeshObj);//, Vector3.zero, Quaternion.Euler(0, 180, 0)) as GameObject;

            labelController = sceneObj.AddComponent<Label3DController>();
            labelController.MyOnInit(sceneObj.transform, prefabName);
            labelText = null;
        }

        void OnSaveLabelText()
        {
            string path = EditorUtility.SaveFilePanel("保存数据文件", "", srcMeshObj.name, "txt");
            localName = srcMeshObj.name;
            //Debug.Log ( string.Format ( "Path: {0}", path ) );

            if (path.Length != 0)
            {
                //var creaseData = mCrease.Data;
                Debug.Log(labelController.DataList);
                labelController.OnInit(sceneObj.transform, prefabName,localName, animaiomType, controlType);
                var data = JsonFx.Json.JsonWriter.Serialize(labelController.DataList);
                // Debug.Log(labelController.DataList);
                Debug.Log("Data String : " + data);

                _InternalDataCodec.SaveString(path, data);
            }
        }

        void OnLoadLabelText()
        {
            string path = EditorUtility.OpenFilePanel("加载数据文件", "", "txt");

            if (path.Length != 0)
            {
                labelText = _InternalDataCodec.LoadString(path);
                //string labelText = _InternalDataCodec.LoadString(path);
                LabelDataList dataList = JsonFx.Json.JsonReader.Deserialize<LabelDataList>(_InternalDataCodec.LoadString(path));
                prefabName = dataList.transform.Name;
                labelController.OnUpdate(dataList.list);
            }
        }

        void OnSaveAssetBundle()
        {
            string path = EditorUtility.SaveFilePanel("保存打包文件", "", srcMeshObj.name, "unity3d");

            if (path.Length != 0)
            {
                List<Object> toinclude = new List<Object>();
                //Model

                GameObject clone = (GameObject)Object.Instantiate(srcMeshObj);
                var tempPrefabPath = "Assets/model.prefab";
                Object tempPrefab = EditorUtility.CreateEmptyPrefab(tempPrefabPath);
                tempPrefab = EditorUtility.ReplacePrefab(clone, tempPrefab);

                toinclude.Add(tempPrefab);
                Object.DestroyImmediate(clone);

                //Label Text
                var labelText = JsonFx.Json.JsonWriter.Serialize(labelController.DataList);
                var tempTextPath = "Assets/label.asset";

                StringHolder holder = ScriptableObject.CreateInstance<StringHolder>();
                holder.content = new string[] { labelText };
                AssetDatabase.CreateAsset(holder, tempTextPath);
                toinclude.Add(AssetDatabase.LoadAssetAtPath(tempTextPath, typeof(StringHolder)));

                //Build Assetbundle
                BuildPipeline.BuildAssetBundle(null, toinclude.ToArray(), path, BuildAssetBundleOptions.CollectDependencies);
                // BuildPipeline.BuildAssetBundles(path, 0, EditorUserBuildSettings.activeBuildTarget);
                //Delete Temp file
                AssetDatabase.DeleteAsset(tempTextPath);
                AssetDatabase.DeleteAsset(tempPrefabPath);

            }
        }

        static Object GetPrefab(GameObject go, string name)
        {
            GameObject clone = (GameObject)Object.Instantiate(go);
            Object tempPrefab = EditorUtility.CreateEmptyPrefab("Assets/" + name + ".prefab");
            tempPrefab = EditorUtility.ReplacePrefab(clone, tempPrefab);
            Object.DestroyImmediate(clone);

            return tempPrefab;
        }

    }
}

