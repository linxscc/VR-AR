/*
	   信息：序列化Transform 
       2017/6/29
*/
using UnityEngine;
using System.Collections;
using System;

namespace PlaceAR.LabelDatas
{
    [Serializable]
    public class SerializableTransform
    {
        /// <summary>
        /// 位置
        /// </summary>
        public SerializableVector3 Position { set; get; }
        /// <summary>
        /// 缩放
        /// </summary>

        public SerializableVector3 Scale { set; get; }
        /// <summary>
        /// 角度
        /// </summary>
        public SerializableVector3 Rotation { set; get; }
        /// <summary>
        /// 模型名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string ChinaName { set; get; }

        public static implicit operator SerializableTransform(Transform rValue)
        {
            return rValue.Serialization();
        }


        public override string ToString()
        {
            return string.Format("{{Position : {0}, Rotation : {1}, Scale : {2}}}", Position.ToString(), Rotation.ToString(), Scale.ToString());
        }
    }

}
