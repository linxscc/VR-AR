/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace PlaceAR
{
    public class ChildItem3D : MonoBehaviour
    {
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
                    data.pitchon = value;
                backGround.SetActive(value);
            }

        }
        public void OnInit(ChildItemData data)
        {
            this.data = data;
            if (data.texture == null)
            {
                string url  = Global.LocalUrl + data.item.catId + "/" + data.item.id;
                print(url);
                Texture2D texture = FileTools.ReadTexture(url, data.item.idName + ".png");
                data.texture = texture;

            }
            Sprite sprite = Sprite.Create(data.texture, new Rect(0, 0, data.texture.width, data.texture.height), Vector2.zero);
            GetComponent<Image>().sprite = sprite;
            GetComponent<Button>().onClick.RemoveAllListeners();
            GetComponent<Button>().onClick.AddListener(Click);
        }
        private void Click()
        {
            AssetBundle.UnloadAllAssetBundles(true);
            Menu3D.menu3D.CloseAllImage();
            Pitchon = true;
            string url = string.Format("{0}{1}/{2}/{3}.unity3d", Global.LocalUrl, data.item.catId, data.item.id, data.item.idName);
            print(url);
            AssetBundle assetBundle = AssetBundle.LoadFromFile(url);
            Object[] obj = assetBundle.LoadAllAssets();
            if(ScrollMenuControl.Singleton.insObj)
            DestroyImmediate(ScrollMenuControl.Singleton.insObj, true);
            ScrollMenuControl.Singleton.insObj = Instantiate(obj[0] as GameObject);

        }
        private void Awake()
        {
            backGround = transform.Find("BackGround").gameObject;
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