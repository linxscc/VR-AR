/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace MoDouAR
{
    public class ChildItem3D : MonoBehaviour
    {
        [SerializeField]
        private Image image;
        [SerializeField]
        private Text text;
        private GameObject backGround;
        private ChildItemData data;
        /// <summary>
        /// 默认图片
        /// </summary>
        private Sprite defSprite;
        /// <summary>
        /// 是否是选中状态
        /// </summary>
        public bool Pitchon
        {
            set
            {
                if (data != null)
                    data.option = value;
                backGround.SetActive(value);
            }

        }
        public void OnInit(ChildItemData data)
        {
            this.data = data;
            text.text = data.item.name;
            Pitchon = data.option;
            if (data.sprite == null)
            {
                string url  = Global.LocalUrl + data.item.catId + "/" + data.item.id;
                print(url);
                Texture2D texture = FileTools.ReadTexture(url, FileTools.ReturnNmae(data.item.picUrl));
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                data.sprite = sprite;

            }
            //Sprite sprite = Sprite.Create(data.texture, new Rect(0, 0, data.texture.width, data.texture.height), Vector2.zero);
            image.sprite = data.sprite;
            GetComponent<Button>().onClick.RemoveAllListeners();
            GetComponent<Button>().onClick.AddListener(Click);
        }
        private void Click()
        {
            AssetBundle.UnloadAllAssetBundles(true);
            Menu3D.instance.CloseAllImage();
            Pitchon = true;
            string url = string.Format("{0}{1}/{2}/{3}", Global.LocalUrl, data.item.catId, data.item.id, FileTexts.ReturnNmae(data.item.fileUrl));
            print(url);
            AssetBundle assetBundle = AssetBundle.LoadFromFile(url);
            Object[] obj = assetBundle.LoadAllAssets();
            ARKitControl.Instance.obj= Instantiate(obj[0] as GameObject);
            ARKitControl.Instance.obj.transform.position = ARKitControl.Instance.frame.transform.position;
            ARKitControl.Instance.closePanel = false;
            // if(ScrollMenuControl.Singleton.insObj)
            // DestroyImmediate(ScrollMenuControl.Singleton.insObj, true);
            //ScrollMenuControl.Singleton.insObj = Instantiate(obj[0] as GameObject);

        }
        private void Awake()
        {
            backGround = transform.Find("Shade").gameObject;
            defSprite = GetComponent<Image>().sprite;
            backGround.SetActive(false);
        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}