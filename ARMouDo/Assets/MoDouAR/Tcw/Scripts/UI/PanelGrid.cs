using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace MoDouAR
{
    /// <summary>
    /// 页面滑动
    /// </summary>
    public class PanelGrid : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        static private PanelGrid instance = null;
        static public PanelGrid Instance
        {
            get
            {
                return instance;
            }
        }

        [Header("PanelGrid")]
        public float 拖拽速度 = 30;
        public float 滑动速度阈值 = 5;
        public float 滑动持续时间 = 0.5f;
        public int 页码;
        public AnimationCurve 动画曲线;

        //开始拖拽位置
        private Vector2 beginPoint;
        private Vector2 beginPos;
        private Vector2 endPos;
        //拖拽的方向
        private TouchDirection touchDirection;

        private void Start()
        {
            instance = this;
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
            if (eventData.delta.y > 0)
                touchDirection = TouchDirection.Up;
            else
                touchDirection = TouchDirection.Down;
            transform.Translate(0, eventData.delta.y / 10 * Time.deltaTime * 拖拽速度, 0);
        }

        /// <summary>
        /// 当结束拖拽
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData)
        {
            Vector2 dragOffset = eventData.position - beginPoint;
            //更换页面条件：光标移动距离、速度
            if (ScreenShotCacheData.shotCachData == null || ScreenShotCacheData.shotCachData.dataPhoto.Count < 15)
                ToZero();
            else if (dragOffset.magnitude > Screen.height / 4 || Mathf.Abs(eventData.delta.y) > 滑动速度阈值)
            {
                //如果光标向上移动 则页码增加
                if (dragOffset.y > Screen.height / 12)
                {
                    //计算页码
                    页码++;
                }
                else
                {
                    页码--;
                }

                //限制页码范围
                页码 = Mathf.Clamp(页码, 0, transform.childCount - 1);
                //transform.position = endPos;
                //公式：父UI位置 - 需要呈现页面位置 + 当前物体位置
                //      endPos = transform.parent.position - childArrayTF[页码].position + transform.position;

                //方向判别
                switch (touchDirection)
                {
                    case TouchDirection.Up:
                        //       endPos = new Vector2(transform.position.x, 854);
                        endPos = new Vector2(transform.localPosition.x, 181f);
                        break;
                    case TouchDirection.Down:
                        //   endPos = new Vector2(transform.position.x, 613);
                        endPos = new Vector2(transform.localPosition.x, -60f);
                        break;
                    case TouchDirection.Left:
                        break;
                    case TouchDirection.Right:
                        break;
                    default:
                        break;
                }
                //  transform.DOMove(endPos, 0.3f).SetEase(动画曲线).OnStart(AAStart).OnKill(BBEnd);
                transform.DOLocalMove(endPos, 0.3f).SetEase(动画曲线).OnStart(AAStart).OnKill(BBEnd);
            }
            else  //回到点击区域
                ToZero();
        }
        /// <summary>
        /// 复位endPos
        /// </summary>
        private void ToZero()
        {
            float yPos = -1;
            switch (Screen.orientation)
            {
                case ScreenOrientation.Portrait:
                    yPos = -60f;
                    break;
                case ScreenOrientation.PortraitUpsideDown:
                    yPos = -60f;
                    break;
                case ScreenOrientation.LandscapeLeft:
                    yPos = -352f;
                    break;
                case ScreenOrientation.LandscapeRight:
                    yPos = -352f;
                    break;
            }
            endPos = new Vector3(transform.localPosition.x, yPos, 0);
            transform.DOLocalMove(endPos, 0.3f).SetEase(动画曲线).OnStart(AAStart).OnKill(BBEnd);
        }
        /// <summary>
        /// 开始改变页码时
        /// </summary>
        private void AAStart()
        {
            ComeBack.OnEnablGridChanges(页码, TonStart.Start);
        }
        /// <summary>
        /// 改变页码结束时
        /// </summary>
        private void BBEnd()
        {
            ComeBack.OnEnablGridChanges(页码, TonStart.End);
        }

        /// <summary>
        /// 改变页面-调用
        /// </summary>
        /// <param name="targetPage">目标页码</param>
        public void ChangePage(int targetPage)
        {
            switch (targetPage)
            {
                case 0:
                    endPos = new Vector2(transform.localPosition.x, -60f);
                    页码 = 0;
                    break;
                case 1:
                    endPos = new Vector2(transform.localPosition.x, 241f);
                    页码 = 1;
                    break;
                default:
                    break;
            }
            transform.DOLocalMove(endPos, 0.3f).SetEase(动画曲线).OnStart(AAStart).OnKill(BBEnd);
        }

    }
    /// <summary>
    /// 滑动方向
    /// </summary>
    public enum TouchDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}
