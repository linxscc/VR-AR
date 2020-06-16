/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using PlaceAR.LabelDatas;
using System.Collections.Generic;
using System.IO;

namespace MoDouAR
{
    /// <summary>
    /// 加载数据
    /// </summary>
    public class LoadData : MonoBehaviour
    {
        private static LoadData instance;
        public static LoadData Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject obj = new GameObject("LoadData");
                    instance = obj.AddComponent<LoadData>();
                }
                return instance;
            }
        }
        /// <summary>
        /// 配置名称
        /// </summary>
        private string configName = "config.txt";
        /// <summary>
        /// 配置数据
        /// </summary>
        public ConfigDataList configData;
        private string downloadDataName = "download.txt";
        /// <summary>
        /// 已经下载文件配置
        /// </summary>
        public ItemDataList downloadData;
        public List<ChildItemData> childItem = new List<ChildItemData>();
        private void Awake()
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        public void Open()
        {
            ReadDownloadConfig();
            StartCoroutine(LoadText(Global.getModeTypeUrl));

        }
        /// <summary>
        /// 读取下载配置
        /// </summary>
        private void ReadDownloadConfig()
        {
            if (FileTools.FileExist(Global.LocalUrl, downloadDataName))
            {
                downloadData = FileTools.ReadText<ItemDataList>(Global.LocalUrl + downloadDataName);
                for (int i = 0; i < downloadData.data.Count; i++)
                {
                    string url = Global.LocalUrl + downloadData.data[i].catId + "/" + downloadData.data[i].id;
                    Texture2D texture = FileTools.ReadTexture(url, FileTools.ReturnNmae(downloadData.data[i].picUrl));
                    Sprite sprite = null;
                    if (texture != null)
                        sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                    ChildItemData d = new ChildItemData(downloadData.data[i], sprite);
                    d.state = LoadState.downOver;
                    childItem.Add(d);
                }
            }

            else
            {
                downloadData = new ItemDataList();

                List<string> path = new List<string>();
                path.Add("/DefaultModel/60/");
                StartCoroutine(LoadDefaultModel(path));
            }

        }
        /// <summary>
        /// 把默认的模型转移到本地沙盒中
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private IEnumerator LoadDefaultModel(List<string> path)
        {
            if (path.Count > 0)
            {
                string url = Application.streamingAssetsPath + path[0];
                ItemDataList data = FileTools.ReadText<ItemDataList>(url + "webData.txt");

                for (int i = 0; i < data.data.Count; i++)
                {
                    if (data.data[i].id==199|| data.data[i].id == 200|| data.data[i].id == 201)
                    {
                        WWW ww = new WWW("file://" + url + data.data[i].id + "/" + FileTools.ReturnNmae(data.data[i].configFileUrl));
                        yield return ww;
                        Save(ww, data.data[i], FileTools.ReturnNmae(data.data[i].configFileUrl));
                        WWW wwPic = new WWW("file://" + url + data.data[i].id + "/" + FileTools.ReturnNmae(data.data[i].picUrl));
                        yield return wwPic;
                        Save(wwPic, data.data[i], FileTools.ReturnNmae(data.data[i].picUrl));
                        WWW wwFile = new WWW("file://" + url + data.data[i].id + "/" + FileTools.ReturnNmae(data.data[i].fileUrl));
                        yield return wwFile;
                        Texture2D texture = wwPic.texture;
                        Sprite sprite = null;
                        if (texture != null)
                            sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                        ChildItemData d = new ChildItemData(data.data[i], sprite);
                        d.state = LoadState.downOver;
                        childItem.Add(d);
                        Save(wwFile, data.data[i], FileTools.ReturnNmae(data.data[i].fileUrl));
                        downloadData.data.Add(data.data[i]);
                    }
                }
                string config = JsonFx.Json.JsonWriter.Serialize(downloadData);
                FileTools.CreateFile(Global.LocalUrl, downloadDataName, config);
                path.Remove(path[0]);
                if (path.Count > 0)
                    LoadDefaultModel(path);
            }
        }
        private bool Save(WWW ww, ItemData data, string urlName)
        {
            if (string.IsNullOrEmpty(ww.error))
            {
                byte[] model = ww.bytes;
                int length = model.Length;
                FileTools.CreateModelFile(Global.LocalUrl + data.catId + "/" + data.id, urlName, model, length);
                return true;
            }
            else return false;
        }
        /// <summary>
        /// 读取服务器上文本
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">地址</param>
        /// <param name="callBack">回调函数</param>
        /// <returns></returns>
        private IEnumerator LoadText(string url)
        {
            WWW www = new WWW(url);
            yield return www;
            if (www.error == "")
            {
                configData = JsonFx.Json.JsonReader.Deserialize<ConfigDataList>(www.text);

                configData.data.Sort(delegate (ConfigData x, ConfigData y)
                {
                    return x.orderNum.CompareTo(y.orderNum);
                });
                string config = JsonFx.Json.JsonWriter.Serialize(configData);
                FileTools.CreateFile(Global.LocalUrl, configName, config);
                //callBack(dataList);
                //ReadSeverCallBack(dataList);

            }
            else
            {
                // PrintMenuControl.Singleton.Open(www.error);
#if UNITY_IPHONE
                IOSNativePopUpManager.showMessage("提示", "网络连接错误!", "确认");
#elif UNITY_ANDROID
                    PrintMenuControl.Singleton.Open("配置文件下载失败!");
#endif
                configData = FileTools.ReadText<ConfigDataList>(Global.LocalUrl + configName);
                //PrintMenuControl.Singleton.Open(":配置文件下载失败");
                // ReadSeverCallBack(configDataList);

            }
        }
        private void OnDestroy()
        {
            instance = null;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}