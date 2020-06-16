/*
 *    日期:2017/7/5
 *    作者:
 *    标题:
 *    功能:显示进度值
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PlaceAR.LabelDatas;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

namespace PlaceAR
{
    public class DownCalBack
    {
        public WWW www;
        public List<ItemData> downLoad;
        public bool isLoad;
    }
    public class ItemChild : MonoBehaviour
    {
        ItemDataList itemDataList;
        public ConfigData data;

        /// <summary>
        /// 在服务器上这个类型包含的所有模型
        /// </summary>
        [HideInInspector]
        public List<ItemData> configServer = new List<ItemData>();

        /// <summary>
        /// 本地这个类型包含的所有模型
        /// </summary>
        [HideInInspector]
        public List<ItemData> configLocal = new List<ItemData>();

        /// <summary>
        /// 等待下载的模型
        /// </summary>
        [HideInInspector]
        public List<ItemData> configLoad = new List<ItemData>();
        /// <summary>
        /// 进度条
        /// </summary>
        [SerializeField]
        private Image progress;
        /// <summary>
        /// 下载按钮
        /// </summary>  
        [SerializeField]
        private Button downBtn;
        /// <summary>
        /// 组名称
        /// </summary>
        [SerializeField]
        public Text title;
        /// <summary>
        /// 下载个数
        /// </summary>
        [SerializeField]
        private Text downCount;
        /// <summary>
        /// 组介绍
        /// </summary>
        [SerializeField]
        private Text introduce;
        /// <summary>
        /// 查看模型
        /// </summary>
        [SerializeField]
        private GameObject openModel;
        /// <summary>
        /// 类型图标
        /// </summary>
        public Image typeImage;
        /// <summary>
        /// 所在位置
        /// </summary>
        private int index;
        /// <summary>
        /// 模型是否已经下载在本地
        /// </summary>
        private bool downLoad = false;
        private string itemDataName = "itemData.txt";
        /// <summary>
        /// 模型是否已经存在本地
        /// </summary>
        public bool DownLoad
        {

            get { return downLoad; }
            set
            {
                downLoad = value;
                if (downLoad)
                {
                    if (configLocal.Count > 0)
                        openModel.SetActive(true);
                    else
                    {
                        openModel.SetActive(true);
                        openModel.GetComponent<Button>().interactable = false;
                        openModel.GetComponentInChildren<Text>().text = "没有模型";
                    }
                    progress.fillAmount = 1;
                    downBtn.interactable = false;
                }
                else
                {
                    if (configLocal.Count > 0)
                    {
                        downCount.text = string.Format("更新模型");
                    }
                    else
                        downCount.text = string.Format("点击下载");
                    openModel.SetActive(false);
                    downBtn.interactable = true;
                }

                //delBtn.interactable = downLoad;
                //downBtn.interactable = !downLoad;
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void OnInit(ConfigData data, int index)
        {
            this.index = index;
            openModel.SetActive(false);
            openModel.GetComponent<Button>().onClick.AddListener(OpenModel);
            this.data = data;
            title.text = data.name;
            introduce.text = data.info;
            ItemDataList localData = FileTools.ReadText<ItemDataList>(Global.LocalUrl + data.id + "/" + data.id + ".txt");
            StartCoroutine(LoadItemList(localData));
        }
        /// <summary>
        /// 验证删除
        /// </summary>
        private void DelOnLocal(ref ItemDataList localData, Dictionary<int, ItemData> severrdata)
        {
            for (int i = 0; i < localData.data.Count; i++)
            {
                if (!severrdata.ContainsKey(localData.data[i].id))
                {
                    print("删除" + localData.data[i].id);
                    FileTools.DeleteFolder(Global.LocalUrl + localData.data[i].catId + "/" + localData.data[i].id);

                    localData.data.Remove(localData.data[i]);
                    itemDataList.data = localData.data;
                    string config = JsonFx.Json.JsonWriter.Serialize(itemDataList);
                    FileTools.CreateFile(Global.LocalUrl + data.id, data.id + ".txt", config);
                }
            }
        }

        private IEnumerator LoadItemList(ItemDataList localData)
        {
            //string url = "";
            //if (!File.Exists(Global.LocalUrl + Global.mainConfigName))
            //    url = Global.getModelList + data.id;
            //else
            //    url = Global.LocalUrl + data.id + "/" + data.id + ".txt";
            //WWW www = new WWW(url);
            WWW www = new WWW(Global.getModelList + data.id);
            yield return www;
            if (www.error == "")
            {
                itemDataList = JsonFx.Json.JsonReader.Deserialize<ItemDataList>(www.text);

                itemDataList.data.Sort(delegate (ItemData p1, ItemData p2)
                {
                    return p1.orderNum.CompareTo(p2.orderNum);//升序
                });
                string config = JsonFx.Json.JsonWriter.Serialize(itemDataList);

                FileTools.CreateFile(Global.LocalUrl+data.id+"/", itemDataName, config);

            }
            else
            {
                itemDataList = FileTools.ReadText<ItemDataList>(Global.LocalUrl + data.id+ "/"+ itemDataName);
                //if (localData == null) gameObject.SetActive(false);
                //else
                // {

                // }
            }
            ReadBack(localData);
            // downCount.text = string.Format("点击下载");

        }
        private void ReadBack( ItemDataList localData)
        {
            Dictionary<int, ItemData> itemDic = new Dictionary<int, ItemData>();
            //DownloadControl.Single.ReadConfig(data.id);
            if (localData != null)                                                                                                     // configServer = itemDataList.data;
                localData.data.Sort(delegate (ItemData p1, ItemData p2)
                {
                    return p1.orderNum.CompareTo(p2.orderNum);//升序
                }
                    );

            configServer.AddRange(itemDataList.data);

            configLoad.AddRange(itemDataList.data);
            for (int i = 0; i < itemDataList.data.Count; i++)
            {
                itemDic.Add(itemDataList.data[i].id, itemDataList.data[i]);
            }

            if (localData != null)
            {
                DelOnLocal(ref localData, itemDic);
                foreach (ItemData item in localData.data)
                {

                    configLocal.Add(item);
                }
                for (int i = 0; i < localData.data.Count; i++)
                {
                    for (int j = 0; j < configLoad.Count; j++)
                    {
                        if (configLoad[j].id == configLocal[i].id)
                        {
                            //print(configLoad[j].id);
                            configLoad.Remove(configLoad[j]);
                        }
                    }
                }

            }
            progress.fillAmount = 0;
            if (configServer.Count <= configLocal.Count)
            {
                //if (data.id == 62)
                //  print(11);
                DownLoad = true;
                progress.fillAmount = 1;
            }
            else
            {
                DownLoad = false;
            }
        }
        /// <summary>
        /// 模型数据
        /// </summary>
       // private ConfigData data;
        private void Start()
        {
            // ProgressValue = progress.offsetMax.x;
            downBtn.onClick.AddListener(DownBtn);
            // delBtn.onClick.AddListener(DeleteBtn);

            //SetTextValue("变形金刚下载：" + GetComponent<Slider>().value * 100 + "%");
        }
        /// <summary>
        /// 下载完一个模型包回调
        /// </summary>
        /// <param name="www"></param>
        /// <param name="data"></param>
        private void Progress(WWW down)
        {
            // print(down.downLoad[0]);
            StartCoroutine(LoadProgress(down));
        }
        private IEnumerator LoadProgress(WWW www)
        {
            bool isLoad = false;
            while (!isLoad)
            {
                progress.fillAmount = www.progress;
                // print((1 / configServer.Count) * www.progress);
                if (www.progress == 1)
                    isLoad = true;
                yield return new WaitForFixedUpdate();
            }


        }
        /// <summary>
        /// 查看模型
        /// </summary>
        public void OpenModel()
        {
            if (data.name == "天气系统")
            {
                downCount.text = "不支持此模式";
                return;
            }
            // Global.
            if (configLocal.Count <= 0)
                PrintMenuControl.Singleton.Open("此类型没有模型！");
            else
            {
                Global.OperatorModel = OperatorMode.BrowserMode;
                StartSceneControl.Singleton.Close(true);
                ModelBrowserControl.Singleton.Open();
                ScrollMenuControl.Singleton.OpenIntial(data.id);
                //ScrollMenuControl.Singleton.OnInit();
                //AsyncOperation async = SceneManager.LoadSceneAsync(Global.sceneBrowse);
            }
        }
        /// <summary>
        /// 下载按钮
        /// </summary>
        public void DownBtn()
        {
            switch (Application.internetReachability)
            {
                case NetworkReachability.NotReachable:
#if UNITY_IPHONE
                    IOSNativePopUpManager.showMessage("提示", "当前网络不可用!", "确认");
#elif UNITY_ANDROID
                    PrintMenuControl.Singleton.Open("当前网络：不可用!");
#endif

                    break;
                case NetworkReachability.ReachableViaCarrierDataNetwork:
                    if (!Global.Atwifi)
                    {
#if UNITY_IPHONE
                        IOSNativePopUpManager.showMessage("提示", "当前网络2G/3G/4G!请在设置中打开(2G/3G/4G网络下载)", "确认");
#elif UNITY_ANDROID
                     PrintMenuControl.Singleton.Open("当前网络：3G/4G!请在设置中打开(2G/3G/4G网络下载)", 4);
#endif
                    }
                    else
                        LoadBack();
                    break;
                case NetworkReachability.ReachableViaLocalAreaNetwork:
                    LoadBack();
                    //if (!Global.Atwifi)
                    //    PrintMenuControl.Singleton.Open("当前网络：3G/4G!请在设置中打开(2G/3G/4G网络下载)", 4);
                    //else
                    //    LoadBack();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 下载
        /// </summary>
        public void LoadBack()
        {
            downCount.text = string.Format("{0}/{1}", configLocal.Count, configServer.Count);
            downBtn.interactable = false;
            StartCoroutine(Load());
        }
        /// <summary>
        /// 停止下载
        /// </summary>
        public void StopLoad()
        {
            StopAllCoroutines();
            if (configLocal.Count < configServer.Count)
                DownLoad = false;
        }
        /// <summary>
        /// 下载
        /// </summary>
        /// <returns></returns>
        private IEnumerator Load()
        {
            if (configLoad.Count > 0)
            {
                string url = Global.LocalUrl + data.id + "/" + configLoad[0].id;
                if (!Directory.Exists(url))
                {
                    Directory.CreateDirectory(url);
                }
                if (!FileTools.FileExist(url, configLoad[0].idName + ".txt"))
                {
                    if (configLoad[0].configFileUrl != null)
                    {
                        WWW wwwtext = new WWW(configLoad[0].configFileUrl);
                        Progress(wwwtext);
                        yield return wwwtext;
                        if (wwwtext.error == "")
                        {
                            // LabelDataList label = JsonFx.Json.JsonReader.Deserialize<LabelDataList>(wwwtext.text);
                            //label.description = data.info;
                            //string JsonFx.Json.JsonWriter.Serialize(wwwtext.text);
                            FileTools.CreateFile(url, configLoad[0].idName + ".txt", wwwtext.text);
                            configLoad[0].configDown = 1;
                        }
                        else
                        {
#if UNITY_IPHONE
                            IOSNativePopUpManager.showMessage("提示", "配置下载失败", "确认");
#elif UNITY_ANDROID
                   PrintMenuControl.Singleton.Open("配置下载失败");
#endif

                        }
                    }
                    else
                        configLoad[0].configDown = 2;
                }
                else
                {
                    configLoad[0].configDown = 1;
                }
                if (!FileTools.FileExist(url, configLoad[0].idName + ".png"))
                {
                    if (configLoad[0].picUrl != null)
                    {
                        WWW wwwPic = new WWW(configLoad[0].picUrl);
                        Progress(wwwPic);
                        yield return wwwPic;
                        if (wwwPic.error == "")
                        {
                            byte[] model = wwwPic.bytes;
                            int length = model.Length;
                            FileTools.CreateModelFile(url, configLoad[0].idName + ".png", model, length);
                            configLoad[0].picDown = 1;
                        }
                        else
                        {
#if UNITY_IPHONE
                            IOSNativePopUpManager.showMessage("提示", "图片下载失败", "确认");
#elif UNITY_ANDROID
                   PrintMenuControl.Singleton.Open("图片下载失败");
#endif

                        }
                    }
                    else
                        configLoad[0].picDown = 2;
                }
                else
                {
                    configLoad[0].picDown = 1;
                }
                if (!FileTools.FileExist(url, configLoad[0].idName + ".unity3d"))
                {
                    if (configLoad[0].fileUrl != null)
                    {
                        WWW wwwModel = new WWW(configLoad[0].fileUrl);
                        Progress(wwwModel);
                        yield return wwwModel;
                        if (wwwModel.error == "")
                        {
                            byte[] model = wwwModel.bytes;
                            int length = model.Length;
                            FileTools.CreateModelFile(url, configLoad[0].idName + ".unity3d", model, length);
                            configLoad[0].fileDown = 1;

                        }
                        else
                        {
#if UNITY_IPHONE
                            IOSNativePopUpManager.showMessage("提示", "模型下载失败", "确认");
#elif UNITY_ANDROID
                   PrintMenuControl.Singleton.Open("模型下载失败");
#endif

                            // PrintMenuControl.Singleton.Open("模型下载失败");
                        }
                    }
                    else
                        configLoad[0].fileDown = 2;
                }
                else
                {
                    configLoad[0].fileDown = 1;
                }
                configLocal.Add(configLoad[0]);
                configLoad.Remove(configLoad[0]);
                downCount.text = string.Format("{0}/{1}", configLocal.Count, configServer.Count);
                AssetBundle.UnloadAllAssetBundles(false);
                if (configLoad.Count > 0)
                {
                    StartCoroutine(Load());
                }
                else
                {
                    DownLoad = true;
                    // data. = configLocal;
                    itemDataList.data = configLocal;
                    string config = JsonFx.Json.JsonWriter.Serialize(itemDataList);
                    FileTools.CreateFile(Global.LocalUrl + data.id, data.id + ".txt", config);
                  
                }
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        public void UpdataData()
        {
            configLocal.Clear();
            StopLoad();
            progress.fillAmount = 0;
            for (int i = 0; i < configServer.Count; i++)
            {
                configLoad.Add(configServer[i]);
            }
            //  configLoad = configServer;
        }

    }
}
