using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace UI_XYRF
{
    /// <summary>
    /// UI面板 拖拽滑动
    /// </summary>
    public class GridPanelDrag_T : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        #region 滑动回调
        /// <summary>
        /// 当在最后一页时
        /// </summary>
        static public event Action OnEnablGridLate = delegate { };
        static public void OnEnablGridLates()
        {
            OnEnablGridLate();
        }
        #endregion


        public float 拖拽速度 = 30;
        public float 滑动速度阈值 = 5;
        public int 页码;
        public float 滑动持续时间 = 0.5f;
        public AnimationCurve 动画曲线;

        //开始拖拽位置
        private Vector2 beginPoint;
        private Vector2 beginPos, endPos;
        private float x = 1;

        /// <summary>
        /// Grid 子物体
        /// </summary>
        private Transform[] childArrayTF;

        private void Start()
        {
            //存储所有 Grid子对象
            childArrayTF = new Transform[transform.childCount];
            for (int i = 0; i < childArrayTF.Length; i++)
            {
                childArrayTF[i] = transform.GetChild(i);
            }
        }


        /// <summary>
        /// 当开始 拖拽
        /// </summary>
        /// <param name="eventData"></param>
        public void OnBeginDrag(PointerEventData eventData)
        {
            beginPoint = eventData.position;
        }
        /// <summary>
        /// 当拖拽中
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.delta.x < 0 && 页码 < childArrayTF.Length - 1)
                transform.Translate(eventData.delta.x * Time.deltaTime * 拖拽速度, 0, 0);
        }
        /// <summary>
        /// 当结束拖拽
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData)
        {
            Vector2 dragOffset = eventData.position - beginPoint;
            //更换页面条件：光标移动距离、速度
            if (dragOffset.magnitude > Screen.width / 3 || Mathf.Abs(eventData.delta.x) > 滑动速度阈值)
            {
                //如果光标向左移动 则页码增加
                if (dragOffset.x < 0)
                {
                    //计算页码
                    页码++;
                    if (页码 == (childArrayTF.Length - 1))  //如果是最后一页
                        OnEnablGridLates();
                }
                //else
                //  {
                // 页码--;
                //  }

                //限制页码范围
                页码 = Mathf.Clamp(页码, 0, transform.childCount - 1);
            }
            //transform.position = endPos;
            beginPos = transform.position;
            //公式：父UI位置 - 需要呈现页面位置 + 当前物体位置
            endPos = transform.parent.position - childArrayTF[页码].position + transform.position;
            x = 0;
        }
        private void Update()
        {
            if (x < 1)
            {
                x += Time.deltaTime / 滑动持续时间;
                //起点固定   终点固定  比例变化
                transform.position = Vector3.Lerp(beginPos, endPos, 动画曲线.Evaluate(x));
            }
        }




    }
}
