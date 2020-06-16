using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ARKit_T;


namespace XYRF_FingerGesture
{
    public class FingerGesture_T : MonoBehaviour
    {
        /// <summary>
        /// 可否单手 触屏?
        /// </summary>
        private bool OneTouchOk = true;
        /// <summary>
        /// 首次单手 触屏?
        /// </summary>
        private bool isOneFinger = true;
        /// <summary>
        /// 首次双手 触屏?
        /// </summary>
        private bool isTwoFinger = true;
        //上一帧触屏位置
        private Vector2 oldPosition0;
        private Vector2 oldPosition1;
        /// <summary>
        /// 当前帧触屏位置
        /// </summary>
        private Vector2 newPosition0;
        private Vector2 newPosition1;
        /// <summary>
        /// 手指角度
        /// </summary>
        private float fingerAngle0;
        private float fingerAngle1;
        /// <summary>
        /// 缩放速度
        /// </summary>
        private float scaleSpeed = 5f;
        /// <summary>
        /// 按下时间 单手指增量
        /// </summary>
        private float touchTimeDexOne = 0f;
        /// <summary>
        /// 按下时间 双手指增量
        /// </summary>
        private float touchTimeDexTwo = 0f;
        /// <summary>
        /// 手势输入
        /// </summary>
        private ARKit_Input arKitInput = null;

        private void Start()
        {
            arKitInput = GetComponent<ARKit_Input>();
        }
        private void Update()
        {
            DetectionFingerGestureState();
            //浏览场景相机移动差值计算
            arKitInput.CameraPositionControl();
        }
        private void OnDisable()
        {
            FingerGestureVariable_T.Instance = null;
        }
        #region 检测当前手势状态

        /// <summary>
        /// 检测 手势状态
        /// </summary>
        private void DetectionFingerGestureState()
        {
            if (Input.touchCount == 0 || Input.GetTouch(0).phase == TouchPhase.Ended)  //未触屏 || 松手
            {
                if (!isTwoFinger)      //双手触屏 抬起
                {
                    FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchUpTwo;
                    FingerEventComeBack_T.OnFingerGestureChanges();  //回调
                    if (touchTimeDexTwo < 0.25f) //双手触屏 点击了
                    {
                        FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchClickTwo;
                        FingerEventComeBack_T.OnFingerGestureChanges();  //回调
                    }
                }
                else if (!isOneFinger && !FingerGestureVariable_T.Instance.isTouchMode) //单手触屏 抬起
                {
                    FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchUpOne;
                    FingerEventComeBack_T.OnFingerGestureChanges();  //回调
                    if (touchTimeDexOne < 0.25f) //单手触屏 点击了
                    {
                        FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchClickOne;
                        FingerEventComeBack_T.OnFingerGestureChanges();  //回调
                    }
                }
                //信息重置
                CancelInvoke("CalculateTouchDownTimeTwo");
                touchTimeDexTwo = 0f;
                isTwoFinger = true;

                CancelInvoke("CalculateTouchDownTimeOne");
                touchTimeDexOne = 0f;
                isOneFinger = true;

                OneTouchOk = true;
                FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.None;
                //    FingerEventComeBack_T.OnFingerGestureChanges();  //回调
                return;
            }

            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))   //如果触摸到UI
            {
                touchTimeDexTwo = 10f;
                touchTimeDexOne = 10f;
                return;
            }

            if (Input.touchCount == 2)
            {
                if (isTwoFinger)
                {
                    FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchDownTwo;
                    InvokeRepeating("CalculateTouchDownTimeTwo", 0f, 0.02f);
                    FingerEventComeBack_T.OnFingerGestureChanges();  //回调
                    isTwoFinger = false;
                }
                // 手指0 移动 && 手指1 移动
                else if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
                {
                    ModeScale();
                    OneTouchOk = false;
                }
            }
            else if (Input.touchCount == 1 && OneTouchOk && !FingerGestureVariable_T.Instance.isTouchMode)
            {
                newPosition0 = Input.GetTouch(0).position;
                //首次单手触屏 || 触屏位置移动<标量 默认为按下
                if (isOneFinger)
                {
                    FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchDownOne;
                    InvokeRepeating("CalculateTouchDownTimeOne", 0f, 0.02f);
                    FingerEventComeBack_T.OnFingerGestureChanges();  //回调
                    isOneFinger = false;
                    //记录 上一帧的触摸位置  
                    oldPosition0 = newPosition0;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    float tox0 = newPosition0.x - oldPosition0.x;
                    float toy0 = newPosition0.y - oldPosition0.y;
                    //模型浏览界面使用数据，单指左右滑动距离
                    FingerGestureVariable_T.Instance.singleFingerHMoveDistance = (Input.GetTouch(0).deltaPosition.x) / 30;
                    FingerGestureVariable_T.Instance.singleFingerVMoveDistance = (Input.GetTouch(0).deltaPosition.y) / 30;
                    //计算手指 角度值
                    Vector3 aPoint0 = (newPosition0 - oldPosition0).normalized;
                    Vector3 bPoint0 = (new Vector2(oldPosition0.x - 1, 0) - oldPosition0).normalized;
                    float dot0 = Vector2.Dot(aPoint0, bPoint0);
                    fingerAngle0 = Mathf.Acos(dot0) * Mathf.Rad2Deg;
                    //角度返回结果
                    /*  上拖拽 返回180度  下拖拽返回0度
                     *   左右拖拽 返回90度
                     */
                    if (tox0 < 0 && fingerAngle0 >= 45 && fingerAngle0 <= 135)  //左拖拽
                        FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchLeftDragOne;
                    else if (tox0 > 0 && fingerAngle0 >= 45 && fingerAngle0 <= 135)  //右拖拽
                        FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchRightDragOne;
                    if (toy0 > 0 && fingerAngle0 > 135)  //上拖拽
                        FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchUpDragOne;
                    else if (toy0 < 0 && fingerAngle0 < 45)  //下拖拽
                        FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchDownDragOne;

                    FingerEventComeBack_T.OnFingerGestureChanges();  //回调
                                                                     //记录 上一帧的触摸位置  
                    oldPosition0 = newPosition0;
                }
            }
            else
            {
                FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.None;
                //    FingerEventComeBack_T.OnFingerGestureChanges();  //回调
            }
        }
        /// <summary>
        /// 按下时间 增量计算 单手指
        /// </summary>
        private void CalculateTouchDownTimeOne()
        {
            touchTimeDexOne += 0.02f;
        }
        /// <summary>
        /// 按下时间 增量计算 双手指
        /// </summary>
        private void CalculateTouchDownTimeTwo()
        {
            touchTimeDexTwo += 0.02f;
        }
        #region 检测双指 拉伸 / 拖拽手势
        /// <summary>
        /// 模型缩放 / 旋转 
        /// </summary>
        private void ModeScale()
        {
            //计算
            Enlarge();
            //记录 上一帧的触摸位置  
            oldPosition0 = newPosition0;
            oldPosition1 = newPosition1;

            //模型浏览界面使用数据，双指移动距离，这个数据更平滑（相比较下面的leng0 - leng1）
            FingerGestureVariable_T.Instance.doubleFingerDistance = (oldPosition0 - oldPosition1).magnitude / 10000;
        }

        /// <summary>
        /// 判断 2指触屏 缩放 / 旋转
        /// </summary>
        private void Enlarge()
        {
            var touch0 = Input.GetTouch(0);
            var touch1 = Input.GetTouch(1);
            //触摸位置  
            newPosition0 = touch0.position;
            newPosition1 = touch1.position;
            float tox0 = newPosition0.x - oldPosition0.x;
            float tox1 = newPosition1.x - oldPosition1.x;
            float toy0 = newPosition0.y - oldPosition0.y;
            float toy1 = newPosition1.y - oldPosition1.y;
            float leng0 = Mathf.Sqrt((oldPosition0.x - oldPosition1.x) * (oldPosition0.x - oldPosition1.x) + (oldPosition0.y - oldPosition1.y) * (oldPosition0.y - oldPosition1.y));                   //前一帧 2手指间距
            float leng1 = Mathf.Sqrt((newPosition0.x - newPosition1.x) * (newPosition0.x - newPosition1.x) + (newPosition0.y - newPosition1.y) * (newPosition0.y - newPosition1.y));   //当前    2手指间距
            
            if (((touch0.deltaPosition.x < 0 && touch1.deltaPosition.x < 0) ||
               (touch0.deltaPosition.x > 0 && touch1.deltaPosition.x > 0) ||
               (touch0.deltaPosition.y < 0 && touch1.deltaPosition.y < 0) ||
               (touch0.deltaPosition.y > 0 && touch1.deltaPosition.y > 0)) && leng1 <= 480f)
            {
                FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchDragTwo;
            }
            else if ((touch0.deltaPosition.x < 0 && touch1.deltaPosition.x > 0) ||
                (touch0.deltaPosition.x > 0 && touch1.deltaPosition.x < 0) ||
                (touch0.deltaPosition.y < 0 && touch1.deltaPosition.y > 0) ||
                    (touch0.deltaPosition.y > 0 && touch1.deltaPosition.y < 0))
            {
                if (leng0 < leng1)
                    FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchDrawOutTwo;
                else
                    FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchDrawInTwo;
            }
            //if ((tox0 < 0 && tox1 > 0) || (tox0 > 0 && tox1 < 0) || (toy0 < 0 && toy1 > 0) || (toy0 > 0 && toy0 < 0))  //缩放
            //{
            //    if (leng0 < leng1)
            //        FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchDrawOutTwo;
            //    else
            //        FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchDrawInTwo;
            //}
            //else   //双手按下拖拽
            //    FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchDragTwo;
            FingerEventComeBack_T.OnFingerGestureChanges();  //回调
        }
        #endregion
        #endregion
    }
}
