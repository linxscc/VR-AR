/***
 * 
 *    Title: UI框架
 *           主题: 通用配置管理器接口
 *    Description: 
 *           功能: 基于"键值对"配置文件的通用解析
 *           1: 
 *           2: 
 *           3: 
 *           4: 
 *                          
 *    Date: 2017/07
 *    Version: 0.1
 *	  Author: 
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace vPlace_FW
{
    public interface IConfigManager
    {
        /// <summary>
        /// 只读属性： 应用设置
        /// 功能： 得到键值对集合数据
        /// </summary>
        Dictionary<string, string> AppSetting { get; }

        /// <summary>
        /// 得到配置文件（AppSetting）最大的数量
        /// </summary>
        /// <returns></returns>
        int GetAppSettingMaxNumber();

    }

    [Serializable]
    internal class KeyValuesInfo
    {
        /// <summary>
        /// 配置信息
        /// </summary>
        public List<KeyValuesNode> ConfigInfo = null;
    }


    [Serializable]
    internal class KeyValuesNode
    {
        /// <summary>
        /// 键
        /// </summary>
        public string Key = null;

        /// <summary>
        /// 值
        /// </summary>
        public string Value = null;
    }
}
