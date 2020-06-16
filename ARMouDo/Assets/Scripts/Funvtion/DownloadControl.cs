/*
 *    日期:2017/7/5
 *    作者:
 *    标题:
 *    功能:下载器
*/
using UnityEngine;
using System.Collections;
using PlaceAR.LabelDatas;
using System.IO;
using System.Collections.Generic;
using System;

namespace PlaceAR
{
    /// <summary>
    /// 下载管理
    /// </summary>
    public class DownloadControl
    {
        private GameObject menu;
        public DownloadCentre downloadCentre;
       // public ConfigDataList localConfig;
        private DownloadControl()
        {
            menu = new GameObject("DownLoadCentre");
            downloadCentre = menu.AddComponent<DownloadCentre>();
           //downloadCentre.down += DownLoad;
        }
        private static DownloadControl single;
        public static DownloadControl Single
        {
            get
            {
                if (single == null)
                    single = new DownloadControl();
                return single;
            }
            set
            {
                single = null;
            }
        }
        /// <summary>
        /// 下载服务器配置和本地配置对比
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="callback"></param>
        public void DownServer<T>(string url,CallBack<T> callback)
        {
            downloadCentre.LoadServerText<T>(Global.getModeTypeUrl, callback);
        }
        /// <summary>
        /// 删除模型
        /// </summary>
        /// <param name="configData">文件的信息</param>
        public void DeleteFile( ConfigData configData)
        {
          //  File.Delete(Global.LocalUrl + configData.name + "//" + configData.name);
           
          //  localConfig.data.Remove(configData);
          //  string data = JsonFx.Json.JsonWriter.Serialize(localConfig);
           // CreateFile(Global.LocalUrl + "config", "config.txt", data);
        }
    }
}
