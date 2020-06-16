using UnityEngine;
using System.Collections.Generic;
using System;

namespace ModelViewerProject.Label3D
{
    
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
        /// 动画类型
        /// </summary>
        public int animationType { set; get; }
        /// <summary>
        /// 操作模式
        /// </summary>
        public int controlType { set; get; }
    }

    [Serializable]
    public class LabelData
    {
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
        /// 详细信息
        /// </summary>
        public string description { set; get; }
        /// <summary>
        /// 所属分组
        /// </summary>
        public string group { set; get; }
        /// <summary>
        /// 逐层中的所属层级
        /// </summary>
        public float layer { set; get; }
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


}
