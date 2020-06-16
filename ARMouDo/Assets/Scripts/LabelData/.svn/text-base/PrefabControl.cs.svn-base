/*
 *    日期:2017/7/7
 *    作者:
 *    标题:模型本身
 *    功能:
*/
using UnityEngine;
using System.Collections;
using PlaceAR.LabelDatas;
using System.Collections.Generic;
using System.Linq;

namespace PlaceAR
{
    /// <summary>
    /// 模型本身
    /// </summary>
	public class PrefabControl : MonoBehaviour
    {
        [HideInInspector]
        /// <summary>
        /// 中文名称
        /// </summary>
        public string chinaName;
        [HideInInspector]
        /// <summary>
        /// 模型类型
        /// </summary>
        public PrefabType prefabType;
        [HideInInspector]
        /// <summary>
        /// 模型控制类型
        /// </summary>
        public ControlType controlType;
        /// <summary>
        /// 子物体
        /// </summary>
        [HideInInspector]
        public List<PrefabChildControl> childData = new List<PrefabChildControl>();

         #region  老版本代码移植
        public void OnInit(List<LabelData> datas)
        {
            if (childData == null)
                childData = new List<PrefabChildControl>();
            else
                childData.Clear();

            var children = transform.GetComponentsInChildren<Transform>();

            foreach (var data in datas)
            {
                var child = children.FirstOrDefault(t => t.name == data.name);
                if (child)
                {
                    var label = child.GetComponent<PrefabChildControl>();
                    if (label == null)
                        label = child.gameObject.AddComponent<PrefabChildControl>();

                    label.OnInit(data);

                    childData.Add(label);
                }
            }
        }
        #endregion

        /// <summary>
        /// 数据
        /// </summary>
        public LabelDataList DataList
        {

            get
            {
                List<LabelData> list = new List<LabelData>();
                for (int i = 0; i < childData.Count; i++)
                {
                    childData[i].OnInitLater();
                    list.Add(childData[i].data);
                }
                return new LabelDataList() {transform=transform.Serialization(transform.name,chinaName), prefabType = (int)prefabType, controlType = (int)controlType,list=list };
               // dataList.list = list;
               // dataList.transform.Name = transform.name;
                //dataList.transform.ChinaName = chinaName;
                //dataList.prefabType = (int)prefabType;
               // dataList.controlType = (int)controlType;
                //return dataList;
            }
        }

        #region 编辑状态下功能
        public void OnInitEditor(string localName, PrefabType type,ControlType cType)
        {
            Debug.Log(cType);
            chinaName = localName;
            prefabType = type;
            controlType = cType;
            //DataList = new LabelDataList();
            // DataList.transform.Rotation = transform.eulerAngles;
            // print(transform.name);
            //  DataList.transform.Scale = transform.localScale;
            // DataList.transform.Name = transform.name;
            // DataList.transform.ChinaName = localName;
           // DataList.prefabType = (int)type;
            
           // DataList.controlType = (int)cType;
            //dataList= data;
        }
        /// <summary>
        /// 显示名称的点
        /// </summary>
        public GameObject titlePoint;
        public void OnInitEditor()
        {
            // Debug.Log("enter");

            //titlePoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            foreach (Transform  child in transform)
            {
                PrefabChildControl label = child.GetComponent<PrefabChildControl>();
                if (label == null)
                    label = child.gameObject.AddComponent<PrefabChildControl>();
                label.OnInit();
                //DataList.list = new List<LabelData>();
               // DataList.list.Add(label.data);
                childData.Add(label);
            }
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        public void OnUpdate(List<LabelData> list)
        {
            for (int i = 0; i < childData.Count; i++)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    if (childData[i].name == list[j].name)
                    {
                        childData[i].OnInit(list[j], false);
                    }
                }
            }
           // DataList = dataList;
        }
        /// <summary>
        /// 编辑下加载已经有的数据
        /// </summary>
        /// <param name="datas"></param>
        public void OnUpdate(List<LabelData> datas, bool isClose)
        {

            foreach (var label in childData)
            {
                for (int i = 0; i < datas.Count; i++)
                {
                    if (label.data.name == datas[i].name)
                        label.OnInit(datas[i], isClose);
                }
                //var data = datas.FirstOrDefault(d => d.name == label.Name);
                //if (data != null)
                //    label.OnInit(data, isClose, 1f);
            }


        }
        #endregion
    }
}
