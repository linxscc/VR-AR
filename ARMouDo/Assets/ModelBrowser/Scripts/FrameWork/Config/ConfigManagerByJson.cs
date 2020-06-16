/***
 * 
 *    Title: UI框架
 *           主题:  基于Json配置文件的“配置管理器”
 *    Description: 
 *           功能: 
 *           1: 
 *           2: 
 *           3: 
 *           4: 
 *                          
 *    Date: 
 *    Version: 
 *	  Author: 
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace vPlace_FW
{
    public class ConfigManagerByJson : IConfigManager
    {
        /// <summary>
        /// 保存（键值对）应用设置集合
        /// </summary>
        private static Dictionary<string, string> appSetting;

        /// <summary>
        /// 只读属性： 得到应用设置（键值对集合）
        /// </summary>
        public Dictionary<string, string> AppSetting
        {
            get
            {
                return appSetting;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="jsonPath">JSon配置文件路径</param>
        public ConfigManagerByJson(string jsonPath)
        {
            appSetting = new Dictionary<string, string>();
            //初始化解析Json数据，加载到（appSetting）
            InitAndAnalysisJson(jsonPath);
        }

        /// <summary>
        /// 初始化解析Json数据，加载到集合中
        /// </summary>
        /// <param name="jsonPath">Json配置文件路径</param>
        private void InitAndAnalysisJson(string jsonPath)
        {
            TextAsset configInfo = null;
            KeyValuesInfo keyValueInfoObj = null;

            if (string.IsNullOrEmpty(jsonPath)) return;
            try
            {
                configInfo = Resources.Load<TextAsset>(jsonPath);
                keyValueInfoObj = JsonUtility.FromJson<KeyValuesInfo>(configInfo.text);
            }
            catch
            {
                throw new JsonAnalysisIsException(GetType() + "/InitAndAnalysisJson()/Json Analysis Exception ! Parameter jsonPath =" + jsonPath);
            }

            foreach (KeyValuesNode nodeInfo in keyValueInfoObj.ConfigInfo)
                appSetting.Add(nodeInfo.Key, nodeInfo.Value);
        }


        /// <summary>
        /// 得到APPSetting的最大数值
        /// </summary>
        /// <returns></returns>
        public int GetAppSettingMaxNumber()
        {
            if (appSetting != null && appSetting.Count >= 1)
                return appSetting.Count;
            else
                return 0;
        }
    }
}