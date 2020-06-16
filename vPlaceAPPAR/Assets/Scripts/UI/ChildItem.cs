/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PlaceAR.LabelDatas;
using ARKit_T;
using vPlace_zpc;
using System.Collections.Generic;

namespace PlaceAR
{
    /// <summary>
    /// 模型按钮
    /// </summary>
    public class ChildItem : MonoBehaviour
    {

        private int typeID;
        [SerializeField]
        private GameObject backGround;
        /// <summary>
        /// 默认图片
        /// </summary>
        private Sprite defSprite;
        private AssetBundle assetBundle;
        /// <summary>
        /// 所属类别按钮
        /// </summary>
        private ScrollGridItem scrollGridItem;
        /// <summary>
        /// 模型包含的数据
        /// </summary>
        private ChildItemData data;
        /// <summary>
        /// 是否是选中状态
        /// </summary>
        public bool Pitchon
        {
            set
            {
                if(data!=null)
                data.pitchon = value;
                backGround.SetActive(value);
            }

        }
        public void OnInit(int typeID, ChildItemData data, ScrollGridItem scrollGridItem)
        {
            // if(!pitchon)
            // Close();
            
            this.data = data;
            Pitchon = data.pitchon;
            this.scrollGridItem = scrollGridItem;
            if (data.texture != null)
            {
                Sprite sprite = Sprite.Create(data.texture, new Rect(0, 0, data.texture.width, data.texture.height), Vector2.zero);
                GetComponent<Image>().sprite = sprite;
            }
            else
                GetComponent<Image>().sprite = defSprite;
            this.typeID = typeID;
            //this.itemData = itemData;

            GetComponent<Button>().onClick.RemoveAllListeners();
            GetComponent<Button>().onClick.AddListener(Click);
            //if (sprite == null)
            //  sprite = GetComponent<Image>().sprite;
        }
        private void Awake()
        {
            // backGround = transform.Find("BackGround").gameObject;
            defSprite = GetComponent<Image>().sprite;
            backGround.SetActive(false);
        }
        //public void SetSprite(Texture2D texture)
        //{
        //    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        //    GetComponent<Image>().sprite = sprite;
        //}

        public void Click()
        {
           
            AssetBundle.UnloadAllAssetBundles(true);
            scrollGridItem.CloseAllImage();
            Pitchon = true;
            if (ScrollMenuControl.Singleton.insObj != null)
            {
                if (ScrollMenuControl.Singleton.insObj.name != data.item.idName)

                    DestroyImmediate(ScrollMenuControl.Singleton.insObj, true);
            }

            string url = string.Format("{0}{1}/{2}/{3}.unity3d", Global.LocalUrl, typeID, data.item.id, data.item.idName);
            assetBundle = AssetBundle.LoadFromFile(url);
            Object[] obj = assetBundle.LoadAllAssets();
            ScrollMenuControl.Singleton.insObj = Instantiate(obj[0] as GameObject);
            ScrollMenuControl.Singleton.insObj.name = obj[0].name;
            //assetBundle.Unload(false);
            LabelDataList labelDataList = ReadConfigSaveToGlobal();
            if (labelDataList != null)
            {
                ScrollMenuControl.Singleton.insObj.transform.localEulerAngles = labelDataList.transform.Rotation;
                ScrollMenuControl.Singleton.insObj.transform.localScale = labelDataList.transform.Scale;
            }
            // SetShaderValue(childMenu.insObj.transform);
            if (ProjectConstDefine.labelDataList != null)
                ScrollMenuControl.Singleton.insObj.transform.eulerAngles = ProjectConstDefine.labelDataList.transform.Rotation;

            if (Global.OperatorModel == OperatorMode.ARMode)
                ARKit_DetectionPanel.Instance.ARKit_ExempleMode(ScrollMenuControl.Singleton.insObj.transform);
            else
                ModelControl.GetInstance().LoadModel(ScrollMenuControl.Singleton.insObj, labelDataList);
        }

        /// <summary>
        ///读取配置传入全局变量
        /// </summary>
        private LabelDataList ReadConfigSaveToGlobal()
        {
            string configPath = string.Format("{0}{1}/{2}/", Global.LocalUrl, typeID, data.item.id);
            string configName = data.item.idName + ".txt";
            if (FileTools.FileExist(configPath, configName))
            {
                LabelDataList labelDataList = FileTools.ReadText<LabelDataList>(configPath + configName);
                ProjectConstDefine.labelDataList = labelDataList;
                ProjectConstDefine.hasConfig = true;
                ProjectConstDefine.selectedModelName = data.item.name;
                ProjectConstDefine.selectedModelDescription = data.item.info;
                
                if (Global.OperatorModel == OperatorMode.BrowserMode)
                {
                    ModelBrowserControl.Singleton.modelBrowserView.ShowOrHideModelName(true);
                    ModelBrowserControl.Singleton.modelBrowserView.BtnUIControl(ProjectConstDefine.hasConfig);
                }
                return labelDataList;
            }
            else
            {
               
                Debug.Log("文件不存在： configPath + configName: " + configPath + configName);
                ProjectConstDefine.hasConfig = false;
                return null;
            }

        }

        private void OnDisable()
        {
            //if (childMenu.insObj != null)
            //Destroy(childMenu.insObj);
            Unload();
        }
        public void Unload()
        {
            //if (assetBundle != null)
            // assetBundle.Unload(true);
        }
        private void WWWBack(WWW www, string name)
        {
            // AssetBundle.UnloadAllAssetBundles(true);
            //Resources.UnloadUnusedAssets();
            // print(name);
            try
            {
                GameObject[] obj = www.assetBundle.LoadAllAssets<GameObject>();
                ScrollMenuControl.Singleton.insObj = Instantiate(obj[0]);
                //www.assetBundle.Unload(false);
            }
            catch (System.Exception)
            {
                //DontDestroyOnLoad(this)
                throw;
            }

            // childMenu.insObj = Instantiate(www.assetBundle.mainAsset as GameObject);


            //if (childMenu.insObj.GetComponent<Animation>())
            //{
            //    childMenu.insObj.GetComponent<Animation>().Play();
            //}
            //if (childMenu.insObj.GetComponent<Animator>())
            //{
            //    // childMenu.insObj.GetComponent<Animator>().cont;
            //    childMenu.insObj.GetComponent<Animator>().Play("Play");
            //}

            //Shader shad = childMenu.insObj.GetComponent<MeshRenderer>().material.shader;
            // childMenu.insObj.GetComponent<MeshRenderer>().material.shader = shad;
            // print(childMenu.insObj.GetComponent<MeshRenderer>().material.shader.name);

            List<Transform> childs = new List<Transform>();
            //SetShaderValue();
            //ReturnChild(childMenu.insObj.transform, ref childs);

            //ssif (childMenu.insObj.GetComponent<Animator>())
            //childMenu.insObj.GetComponent<Animator>().Play();
            // GameObject newobj = new GameObject();
            // childMenu.insObj.transform.parent = newobj.transform;
            ARKit_DetectionPanel.Instance.ARKit_ExempleMode(ScrollMenuControl.Singleton.insObj.transform);



            // if (ScrollMenuControl.Singleton.returnPrefab != null)
            // ScrollMenuControl.Singleton.returnPrefab(childMenu.insObj);

        }

        /// <summary>
        /// 重新设置shader
        /// </summary>
        /// <param name="tran"></param>
        private void SetShaderValue(Transform tran)
        {
            //List<Transform> childs = new List<Transform>();
            Material[] mater = new Material[] { };
            foreach (Transform item in tran)
            {
                if (item.GetComponent<MeshRenderer>() != null)
                {
                    mater = item.GetComponent<MeshRenderer>().materials;
                    for (int i = 0; i < mater.Length; i++)
                    {
                        mater[i].shader = Shader.Find(mater[i].shader.name);
                    }
                }
                //childs.Add(item);
                else if (item.GetComponent<SkinnedMeshRenderer>() != null)
                {
                    mater = item.GetComponent<SkinnedMeshRenderer>().materials;
                    for (int i = 0; i < mater.Length; i++)
                    {
                        mater[i].shader = Shader.Find(mater[i].shader.name);
                    }
                }
                SetShaderValue(item);
            }

        }
    }
}
