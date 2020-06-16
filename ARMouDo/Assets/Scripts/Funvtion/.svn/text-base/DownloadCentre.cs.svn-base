/*
	   信息：2017/7/5
*/
using UnityEngine;
using System.Collections;
using PlaceAR.LabelDatas;
using System.Collections.Generic;
using System.IO;
using System;

namespace PlaceAR
{
    public class DownloadCentre : MonoBehaviour
    {
        /// <summary>
        /// 存储完一个模型回调
        /// </summary>
        public CallBack<ConfigData> down;
        /// <summary>
        /// 进度条
        /// </summary>
        public CallBack<WWW, LabelDataList> progress;
        // public List<ConfigData> configData = new List<ConfigData>();
        /// <summary>
        /// 启动加载配置文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="callBack"></param>
        public void LoadServerText<T>(string url, CallBack<T> callBack)
        {
            StartCoroutine(LoadText<T>(url, callBack));
        }
        /// <summary>
        /// 读取服务器上文本
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">地址</param>
        /// <param name="callBack">回调函数</param>
        /// <returns></returns>
        private IEnumerator LoadText<T>(string url, CallBack<T> callBack)
        {
            // PrintMenuControl.Singleton.Open(url);
            WWW www = new WWW(url);
            yield return www;
            if (www.error == "")
            {
                //PrintMenuControl.Singleton.Open(www.text);
                T dataList = JsonFx.Json.JsonReader.Deserialize<T>(www.text);
                callBack(dataList);
            }
            else
            {
                // PrintMenuControl.Singleton.Open(www.error);
#if UNITY_IPHONE
                IOSNativePopUpManager.showMessage("提示", "配置文件下载失败!", "确认");
#elif UNITY_ANDROID
                    PrintMenuControl.Singleton.Open("配置文件下载失败!");
#endif
                callBack(default(T));

            }
        }
        void OnDisable()
        {
            DownloadControl.Single = null;
        }
    }
}
