/*
 *    日期:2017/7/7
 *    作者:
 *    标题:模型子物体
 *    功能:
*/
using UnityEngine;
using System.Collections;
using PlaceAR.LabelDatas;

namespace PlaceAR
{
    /// <summary>
    /// 模型子物体
    /// </summary>
	public class PrefabChildControl : MonoBehaviour 
	{
        public LabelData data=new LabelData();

#region 老版本代码移植
        /// <summary>
        /// 名称
        /// </summary>
        public string Title { set; get; }

        /// <summary>
        /// 详细信息
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        /// 逐层模式中所属的层级
        /// </summary>
        public int Layer { set; get; }

        /// <summary>
        /// 预设分解位置
        /// </summary>
        public Vector3 LocalPosition { set; get; }

        /// <summary>
        /// 初始位置
        /// </summary>
        public Vector3 InitialPosition { set; get; }

        /// <summary>
        /// 所属组
        /// </summary>
        public string Group { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool isHide { set; get; }

        public void OnInit(LabelData data, float alpha = 0)
        {
           // gameObject.layer = LayerMask.NameToLayer("model");
            transform.tag = Tag.prefab;
            LocalPosition = data.localPosition;

            InitialPosition = data.initialPosition;
            Name = data.name;
            isHide = data.isHide;
            Title = data.title;
            Description = data.description;
            Layer = data.layer;
            Group = data.group;
        }
        #endregion
        
        /// <summary>
        /// 初始化
        /// </summary>
        public void OnInit()
        {
            data.initialPosition = transform.localPosition;
            data.name = transform.name;
            data.title = transform.name;
            data.group = "所属分组";
            data.description = "详细信息";
        }
        public void OnInit(LabelData datas, bool isClose)
        {
            gameObject.layer = LayerMask.NameToLayer("model");
            transform.tag = Tag.prefab;
            data = datas;
            if (isClose)
                transform.localPosition = data.localPosition;
            else
                transform.localPosition = data.initialPosition;
        }
        /// <summary>
        /// 保存最后的值
        /// </summary>
        /// <param name="description"></param>
        /// <param name="title"></param>
        public void OnInitLater()
        {
            data.localPosition = transform.localPosition;
        }

	}
}
