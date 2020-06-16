/*
 *    日期:2017/7/5
 *    作者:
 *    标题:
 *    功能:下载创建UI
*/
using UnityEngine;
using System.Collections;
using PlaceAR.LabelDatas;
using System.Collections.Generic;

namespace PlaceAR
{
    public class DownPrefab : MonoBehaviour
    {
        /// <summary>
        /// 下载模型按钮预制物
        /// </summary>
        private GameObject itemPrefab;
        /// <summary>
        /// 生成的按钮
        /// </summary>
        public Dictionary<int, ItemChild> buttonItem = new Dictionary<int, ItemChild>();
        /// <summary>
        /// 存放按钮
        /// </summary>
        private Transform grid;

        /// <summary>
        /// 配置名称
        /// </summary>
        private string configName = "config.txt";

        public static DownPrefab downPrefab;
        /// <summary>
        /// 生成对象完成
        /// </summary>
        public CallBack ItemLoad;
        void Awake()
        {
            downPrefab = this;
            grid = GameObject.FindGameObjectWithTag(Tag.mainUICanvas).transform.Find("BackGround/ScrollView/Grid");
            StartCoroutine(LoadText(Global.getModeTypeUrl));
            itemPrefab = Resources.Load<GameObject>(Global.itemChlid);
        }

        /// <summary>
        /// 停止下载
        /// </summary>
        public void StopLoad()
        {
            foreach (KeyValuePair<int, ItemChild> kvp in buttonItem)
            {
                kvp.Value.StopLoad();
            }
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
            // PrintMenuControl.Singleton.Open(url);
            WWW www = new WWW(url);
            yield return www;
            if (www.error == "")
            {
                //PrintMenuControl.Singleton.Open(www.text);
                ConfigDataList dataList = JsonFx.Json.JsonReader.Deserialize<ConfigDataList>(www.text);

                dataList.data.Sort(delegate (ConfigData x, ConfigData y)
                {
                    return x.orderNum.CompareTo(y.orderNum);
                }
               
                );
                string config = JsonFx.Json.JsonWriter.Serialize(dataList);
                FileTools.CreateFile(Global.LocalUrl , configName, config);
                //callBack(dataList);
                ReadSeverCallBack(dataList);

            }
            else
            {
                // PrintMenuControl.Singleton.Open(www.error);
#if UNITY_IPHONE
                IOSNativePopUpManager.showMessage("提示", "网络连接错误!", "确认");
#elif UNITY_ANDROID
                    PrintMenuControl.Singleton.Open("配置文件下载失败!");
#endif
                ConfigDataList configDataList = FileTools.ReadText<ConfigDataList>(Global.LocalUrl+ configName);
                //PrintMenuControl.Singleton.Open(":配置文件下载失败");
                ReadSeverCallBack(configDataList);

            }
        }
        /// <summary>
        /// 读取服务器配置回调
        /// </summary>
        /// <param name="config"></param>
        private void ReadSeverCallBack(ConfigDataList config)
        {
            for (int i = 0; i < config.data.Count; i++)
            {
                if (!buttonItem.ContainsKey(config.data[i].id)&& config.data[i].id!=60)
                {
                    //Debug.Log(config.list[i].type);
                    GameObject item = ObjectBool.Get(itemPrefab);
                    //item.name = config.data[i].id.ToString();
                    item.transform.parent = grid;
                    item.transform.localPosition = Vector3.zero;
                    item.transform.localRotation = Quaternion.identity;
                    item.transform.localScale = new Vector3(1f, 1f, 1f);
                    buttonItem.Add(config.data[i].id, item.GetComponent<ItemChild>());
                    if (!Global.itemData.ContainsKey(config.data[i].id))
                        Global.itemData.Add(config.data[i].id, item.GetComponent<ItemChild>().configLocal);
                    item.GetComponent<ItemChild>().OnInit(config.data[i], i);
                    if (config.data[i].picUrl != null)
                    {
                        
                        if (FileTools.FileExist(Global.LocalUrl + config.data[i].id, FileTools.ReturnNmae(config.data[i].picUrl)))
                        {
                            Texture2D texture = FileTools.ReadTexture(Global.LocalUrl + config.data[i].id, FileTools.ReturnNmae(config.data[i].picUrl));
                            item.GetComponent<ItemChild>().typeImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                        }
                        else
                            StartCoroutine(LoadPic(config.data[i].picUrl, item.GetComponent<ItemChild>(), config.data[i]));
                    }

                }
            }
            if (ItemLoad != null)
                ItemLoad();
        }
        private IEnumerator LoadPic(string url, ItemChild itemChild, ConfigData data)
        {
            WWW www = new WWW(url);
            yield return www;
            if (www.error == "")
            {

                Texture2D pic = new Texture2D(132, 132, TextureFormat.RGB24, false);
                pic = www.texture;
                Sprite sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), Vector2.zero);
                sprite.name = url;
                itemChild.typeImage.sprite = sprite;
                byte[] model = www.bytes;
                int length = model.Length;
                FileTools.CreateModelFile(Global.LocalUrl + data.id, FileTools.ReturnNmae(url), model, length);
                // www.texture
            }
            //Resources.UnloadUnusedAssets();
        }

    }
}
