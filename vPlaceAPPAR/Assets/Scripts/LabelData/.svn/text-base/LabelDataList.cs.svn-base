/*
	   信息：模型数据结构
       2017/6/29
*/
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace PlaceAR.LabelDatas
{
    /// <summary>
    /// 配置文件结构
    /// </summary>
    [Serializable]
    public class ConfigDataList
    {
        public int code { set; get; }
        public List<ConfigData> data { set; get; }
    }
    [Serializable]
    public class ConfigData
    {
        public int id { set; get; }
        public int parentId { set; get; }
        public string parentName { set; get; }
        public string name { set; get; }
        public string picUrl { set; get; }
        public string icon { set; get; }
        public int orderNum { set; get; }
        public string updateUserId { set; get; }
        public string updateTime { set; get; }
        public string createUserId { set; get; }
        public string createTime { set; get; }
        public string info { set; get; }
        public string open { set; get; }
        public string list { set; get; }
    }
    /// <summary>
    /// 类文件结构
    /// </summary>
    [Serializable]
    public class ItemDataList
    {
        public int code { set; get; }
        public List<ItemData> data { set; get; }
    }
    [Serializable]
    public class ItemData
    {

        public int id { set; get; }
        public int catId { set; get; }
        public string catName { set; get; }
        public string name { set; get; }
        public string picUrl { set; get; }
        public int picDown = 0;
        public string fileUrl { set; get; }
        public int fileDown = 0;
        public string configFileUrl { set; get; }
        public int configDown = 0;
        public string fileSize { set; get; }
        public string fileSizeInfo { set; get; }
        public int orderNum { set; get; }
        public string info { set; get; }
        public string updateUserId { set; get; }
        public string updateTime { set; get; }
        public int createUserId { set; get; }
        public string createTime { set; get; }
        public string idName { set; get; }
       // public Texture2D texture { set; get; }
    }
    /// <summary>
    /// 模型数据结构
    /// </summary>
    [Serializable]
    public class LabelDataList
    {
        /// <summary>
        /// 自身数据
        /// </summary>
        public SerializableTransform transform { set; get; }
        /// <summary>
        /// 子模型数据
        /// </summary>
        public List<LabelData> list { set; get; }
        /// <summary>
        /// 模型类型
        /// </summary>
        public int prefabType { set; get; }
        /// <summary>
        /// 模型控制类型
        /// 拆解模式 = 0, 剖面模式 = 1, 逐层模式 = 2, 显微镜 = 3, 其他 = 4, 高亮模式 = 5, 截面模式 = 6, 分解模式 = 7
        /// </summary>
        public int controlType { set; get; }
        /// <summary>
        /// 模型介绍
        /// </summary>
        public string description { set; get; }
    }
    [Serializable]
    public class LabelData
    {
        public LabelData()
        {

        }
        public LabelData(string name, string title, string group, string description, SerializableVector3 localPosition, SerializableVector3 initialPosition)
        {
            this.group = group;
            this.name = name;
            this.title = title;
            this.description = description;
            this.localPosition = localPosition;
            this.initialPosition = initialPosition;
        }
        public int layer { set; get; }
        /// <summary>
        /// 所属分组
        /// </summary>
        public string group { set; get; }
        /// <summary>
        /// 模型unity中名称
        /// </summary>
        public string name { set; get; }
        //public string text { set; get; }
        /// <summary>
        /// 模型名称
        /// </summary>
        public string title { set; get; }
        /// <summary>
        /// 模型是否隐藏
        /// </summary>
        public bool isHide { set; get; }
        /// <summary>
        /// 详细信息
        /// </summary>
        public string description { set; get; }
        /// <summary>
        /// 拆分坐标
        /// </summary>
        public SerializableVector3 localPosition { set; get; }
        /// <summary>
        /// 原始坐标
        /// </summary>
        public SerializableVector3 initialPosition { set; get; }
        // public List<LabelData> list { set; get; }

    }
    /// <summary>
    /// 模型类型
    /// </summary>
    public enum PrefabType
    {
        动物 = 0, 植物 = 1, 建筑 = 2, 机械 = 3, 其他 = 4, 星体 = 5
    }
    /// <summary>
    /// 模型操作模式
    /// </summary>
    public enum ControlType
    {
        拆解模式 = 0, 剖面模式 = 1, 逐层模式 = 2, 显微镜 = 3, 其他 = 4, 高亮模式 = 5, 截面模式 = 6, 分解模式 = 7
    }
}
