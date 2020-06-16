/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using PlaceAR.LabelDatas;
using UnityEngine.UI;

namespace MoDouAR
{
    /// <summary>
    /// 下载状态
    /// </summary>
    public enum LoadState
    {
        /// <summary>
        /// 未下载
        /// </summary>
        down,
        /// <summary>
        /// 下载中
        /// </summary>
        download,
        /// <summary>
        /// 下载完成
        /// </summary>
        downOver
    }
    /// <summary>
    /// 模型下载
    /// </summary>
    public class ModelButton : MonoBehaviour
    {
        private ChildItemData data;
        private LoadState State
        {
            get { return data.state; }
            set
            {
                data.state = value;
                switch (value)
                {
                    case LoadState.down:
                        down.gameObject.SetActive(true);
                        downLoding.gameObject.SetActive(false);
                        break;
                    case LoadState.download:
                        down.gameObject.SetActive(false);
                        downLoding.gameObject.SetActive(true);
                        progress.fillAmount = 0;
                        break;
                    case LoadState.downOver:
                        down.gameObject.SetActive(false);
                        downLoding.gameObject.SetActive(false);
                        progress.fillAmount = 0;
                        break;
                    default:
                        break;
                }

            }
        }
        [SerializeField]
        private Text modelName;
        [SerializeField]
        private Button down;
        [SerializeField]
        private Button downLoding;
        [SerializeField]
        private Image progress;
        [SerializeField]
        private GameObject offcial;
        [SerializeField]
        private Image image;
        [SerializeField]
        private Button info;
        /// <summary>
        /// 此模型文件夹url
        /// </summary>
        private string url;
        /// <summary>
        /// 默认图片
        /// </summary>
        private Sprite defSprite;
        /// <summary>
        /// 所属类型按钮
        /// </summary>
        private TypeButton typeButton;
        private bool isLoad = false;
        public void OnInit(ChildItemData data, TypeButton typeButtonB)
        {
            this.data = data;
            typeButton = typeButtonB;
            url = Global.LocalUrl + data.item.catId + "/" + data.item.id;

            modelName.text = data.item.name;
            if (data.sprite != null)
            {
                //Sprite sprite = Sprite.Create(data.texture, new Rect(0, 0, data.texture.width, data.texture.height), Vector2.zero);
                image.GetComponent<Image>().sprite = data.sprite;
            }
            else if (data.item.picUrl != null)
            {
                if (!FileTools.FileExist(url, FileTools.ReturnNmae(data.item.picUrl)))
                {
                    StartCoroutine(LoadData(data.item.picUrl, LoadPicture));
                }
                else
                {
                    Texture2D texture = FileTools.ReadTexture(url, FileTools.ReturnNmae(data.item.picUrl));
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                    data.sprite = sprite;
                   // Sprite sprite = Sprite.Create(data.texture, new Rect(0, 0, data.texture.width, data.texture.height), Vector2.zero);
                    image.GetComponent<Image>().sprite = sprite;
                }
            }
            if (data.item.configFileUrl != null)
            {
                if (!FileTools.FileExist(url, FileTools.ReturnNmae(data.item.configFileUrl)))
                {
                    StartCoroutine(LoadData(data.item.configFileUrl, LoadConfigFile));
                }
            }
            if (data.item.fileUrl != null)
            {
                if (!FileTools.FileExist(url, FileTools.ReturnNmae(data.item.fileUrl)))
                {
                    State = LoadState.down;

                }
                else
                {
                    State = LoadState.downOver;
                    //down.gameObject.SetActive(false);
                    //downLoding.gameObject.SetActive(false);
                    //progress.fillAmount = 0;
                }
            }
        }
        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="www"></param>
        private void LoadPicture(WWW www)
        {
            if (www.error == "")
            {
                byte[] model = www.bytes;
                int length = model.Length;
                FileTools.CreateModelFile(url, FileTools.ReturnNmae(data.item.picUrl), model, length);
                Sprite sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), Vector2.zero);
                data.sprite = sprite;
               // Sprite sprite = Sprite.Create(data.texture, new Rect(0, 0, data.texture.width, data.texture.height), Vector2.zero);
                image.GetComponent<Image>().sprite = sprite;
                //image.sprite=
                data.item.picDown = 1;
            }
        }
        /// <summary>
        /// 下载配置文件
        /// </summary>
        /// <param name="www"></param>
        private void LoadConfigFile(WWW www)
        {
            if (www.error == "")
            {
                // LabelDataList label = JsonFx.Json.JsonReader.Deserialize<LabelDataList>(wwwtext.text);
                //label.description = data.info;
                //string JsonFx.Json.JsonWriter.Serialize(wwwtext.text);
                FileTools.CreateFile(url, FileTools.ReturnNmae(data.item.configFileUrl), www.text);
                data.item.configDown = 1;
            }
            else
            {
#if UNITY_IPHONE
                IOSNativePopUpManager.showMessage("提示", "配置下载失败", "确认");
#elif UNITY_ANDROID
                   //PrintMenuControl.Singleton.Open("配置下载失败");
#endif
            }
        }
       
        private IEnumerator LoadData(string url, CallBack<WWW> w)
        {
            WWW www = new WWW(url);
            yield return www;
            w(www);
        }
        private void Awake()
        {
            defSprite = GetComponent<Image>().sprite;
            down.onClick.AddListener(Down);
            downLoding.onClick.AddListener(DownLoding);
            info.onClick.AddListener(Info);
        }

        public void Close()
        {
            GetComponent<Image>().sprite = defSprite;
        }
        /// <summary>
        /// 下载按钮
        /// </summary>
        private void Down()
        {
            switch (Application.internetReachability)
            {
                case NetworkReachability.NotReachable:
#if UNITY_IPHONE
                    IOSNativePopUpManager.showMessage("提示", "当前网络不可用!", "确认");
#elif UNITY_ANDROID
                   // PrintMenuControl.Singleton.Open("当前网络：不可用!");
#endif
                    break;
                case NetworkReachability.ReachableViaCarrierDataNetwork:
                    if (!Global.Atwifi)
                    {
#if UNITY_IPHONE
                        IOSNativePopUpManager.showMessage("提示", "当前网络2G/3G/4G!请在设置中打开(2G/3G/4G网络下载)", "确认");
#elif UNITY_ANDROID
                    // PrintMenuControl.Singleton.Open("当前网络：3G/4G!请在设置中打开(2G/3G/4G网络下载)", 4);
#endif
                    }
                    else
                        BeginDown();
                    break;
                case NetworkReachability.ReachableViaLocalAreaNetwork:
                    BeginDown();
                    break;
                default:
                    break;
            }

           
  
        }
        private void BeginDown()
        {
            isLoad = false;
            State = LoadState.download;
            StartCoroutine(typeButton.DownModel(data));
            StartCoroutine(LoadProgress(data.www));
        }
        /// <summary>
        /// 停止按钮
        /// </summary>
        private void DownLoding()
        {
            StopCoroutine(typeButton.DownModel(data));
            State = LoadState.down;
        }
        /// <summary>
        /// 模型信息按钮
        /// </summary>
        private void Info()
        {
            ModelInfo.Instance.CreatWindow();
            ModelInfo.Instance.Open(data, Down, DownLoding);
        }
        private IEnumerator LoadProgress(WWW www)
        {

            while (!isLoad)
            {
                progress.fillAmount = www.progress;
                if (www.progress == 1)
                {
                    isLoad = true;
                    State = LoadState.downOver;
                }
                  
                yield return new WaitForFixedUpdate();
            }
        }
        private void OnDisable()
        {
           // image.GetComponent<Image>().sprite = defSprite;
            //print("dis");
        }
    }
}