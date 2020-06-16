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
        public LabelData data = new LabelData();

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
        /// <summary>
        /// 编辑名称显示的点
        /// </summary>
        public GameObject anchor;
        /// <summary>
        /// 连接的线
        /// </summary>
        public GameObject line;
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
        /// <summary>
        /// 更新连线位置长度
        /// </summary>
        public void UpdateLine()
        {
            if (anchor == null)
            {
                GameObject o = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                o.transform.position = transform.position;
                o.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                o.name = name + "_point";
                o.transform.parent = transform.parent;
                anchor = o;
            }
            else
                anchor.SetActive(true);
            if (line == null)
            {
                line = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Material my = new Material(Shader.Find("Standard"));
                my.color = Color.black;
                line.GetComponent<MeshRenderer>().material = my;
                line.name = name + "_line";
                line.transform.parent = transform.parent;
            }
            else
                line.SetActive(true);
            Vector3 tempPos = (transform.position + anchor.transform.position) / 2;//计算两个点的中点坐标，
            line.transform.position = tempPos;
            line.transform.right = (transform.position - anchor.transform.position).normalized;//改变线条的朝向
            float distance = Vector3.Distance(transform.position, anchor.transform.position);
            line.transform.localScale = new Vector3(distance, 0.01f, 0.01f);
        }
        /// <summary>
        /// 关闭连线
        /// </summary>
        public void CloseLine()
        {
            if (anchor)
                anchor.SetActive(false);
            if (line)
                line.SetActive(false);
        }
        /// <summary>
        /// 复位连线位置
        /// </summary>
        public void ResetLine()
        {
            anchor.transform.position = transform.position;
        }
    }
}
