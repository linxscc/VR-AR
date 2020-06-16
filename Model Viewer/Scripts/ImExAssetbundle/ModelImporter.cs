using UnityEngine;
using System.Runtime.InteropServices;

using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections;

namespace ModelViewerProject.Model
{

    using ZERO.Utilities;

    using Label3D;

    public class ModelImporter : MonoBehaviour
    {
        //public static event EventHandler OnShowCursor;

        //public static event EventHandler OnHideCursor;

        void Awake()
        {
#if UNITY_EDITOR
            Cursor.visible = true;
#elif UNITY_STANDALONE
                        Cursor.visible = false;
#endif
            OnInIt(Global.modelName);
        }
        /// <summary>
        /// 进入操作场景
        /// </summary>
        /// <param name="modelName"></param>
        private void OnInIt(string modelName)
        {
            SetMenuControl.Singleton.Close();
            StartCoroutine(LoadModel(modelName));
        }
        private IEnumerator LoadModel(string modelName)
        {
            WWW www = new WWW(string.Format("{0}/Resources/{1}/{2}.unity3d", Global.Url, modelName, modelName));
            yield return www;
            if (www.error == null)
            {

                GameObject obj = Instantiate(www.assetBundle.LoadAsset<GameObject>("model"));
                obj.name = modelName;

                HandleOnInitModel(obj, Global.labelDataList);
            }
            else
            {
                Debug.Log(www.error);
            }

        }
        void OnEnable()
        {
            _InputManager.OnControlOpen += _InputManager_OnControlOpen;
        }

        private void _InputManager_OnControlOpen(object sender, EventArgs e)
        {
            InputEventArgs iea = e as InputEventArgs;

            //if ( OnShowCursor != null )
            //    OnShowCursor ( this, EventArgs.Empty );
#if UNITY_STANDALONE
            Cursor.visible = true;
#endif

            OpenLocalDirectory();

            iea.Used = true;
        }

        void OnDisable()
        {
            _InputManager.OnControlOpen -= _InputManager_OnControlOpen;
        }

        IEnumerator DownloadAssetbundle(string url, params Action[] callbacks)
        {
            yield return new _WWWProxy(
                url,
                www =>
                {
                    foreach (var n in www.assetBundle.GetAllAssetNames())
                    {
                        Debug.Log(n);
                    }

                    //labelRequest = www.assetBundle.LoadAssetAsync ( "label", typeof ( StringHolder ) );

                    var labelAsset = www.assetBundle.LoadAsset("label") as StringHolder;
                    var modelAsset = www.assetBundle.LoadAsset("model") as GameObject;
                    //var materialAsset = www.assetBundle.LoadAsset("material") as Material;

                    var modelObj = GameObject.Instantiate(modelAsset);

                    var labelList = JsonFx.Json.JsonReader.Deserialize<LabelDataList>(labelAsset.content[0]);

                    HandleOnInitModel(modelObj, labelList);
                    //Debug.Log ( labelAsset.content [ 0 ] );

#if UNITY_STANDALONE
                    Cursor.visible = false;
#endif

                    if (callbacks != null)
                    {
                        foreach (var feedback in callbacks)
                        {
                            if (feedback != null)
                                feedback();
                        }
                    }

                }).WaitForFinished();

            yield return null;
        }

        void HandleOnInitModel(GameObject modelObj, LabelDataList labels)
        {
            var modelController = GetComponent<ModelController>();

            if (!modelController)
                Debug.Log("Model Controller is null!");

            modelController.OnInit(modelObj, labels);

        }

        void OpenLocalDirectory()
        {
            OpenFileName ofn = new OpenFileName();
            ofn.structSize = Marshal.SizeOf(typeof(OpenFileName));
            ofn.filter = "资源文件(*.unity3d)\0*.unity3d";
            ofn.file = new string(new char[128]);
            ofn.maxFile = 128;
            ofn.title = "Load Assetbundle File";
            ofn.defExt = "unity3d";
            ofn.flags = (int)(OpenSaveFileDialgueFlags.OFN_EXPLORER | OpenSaveFileDialgueFlags.OFN_FILEMUSTEXIST | OpenSaveFileDialgueFlags.OFN_PATHMUSTEXIST |
                OpenSaveFileDialgueFlags.OFN_FILEMUSTEXIST |
                OpenSaveFileDialgueFlags.OFN_ALLOWMULTISELECT | OpenSaveFileDialgueFlags.OFN_NOCHANGEDIR);

            if (WindowDll.GetOpenFileName(ofn))
            {

                StartCoroutine(DownloadAssetbundle("file://" + ofn.file));

            }
        }

    }
}
