using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace XYRF_FingerGesture
{
    //检测 触屏选中了对象?   使用条件: 对象必须添加Collider组件
    public class OnMouseEvent_T : MonoBehaviour
    {
        /// <summary>
        /// 按下时间 增量
        /// </summary>
        private float touchTimeDex = 0f;
        /// <summary>
        /// 上一次的Touch位置
        /// </summary>
        private Vector2 lateTouchPos;
        /// <summary>
        /// 当前的Touch位置
        /// </summary>
        private Vector2 newPosition0;
        /// <summary>
        /// 手指角度
        /// </summary>
        private float fingerAngle0;

        private void OnMouseDown()
        {
            if (Input.touchCount > 0)
                lateTouchPos = Input.GetTouch(0).position;
            FingerGestureVariable_T.Instance.touchTransform = transform;
            //     FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchModeDownOne;
            FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchDownOne;
            FingerGestureVariable_T.Instance.isTouchMode = true;
            InvokeRepeating("CalculateTouchDownTime", 0f, 0.02f);
            FingerEventComeBack_T.OnFingerGestureChanges();  //回调
        }
        private void OnMouseDrag()
        {
            if (Input.touchCount > 0)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))   //如果触摸到UI
                    return;
                newPosition0 = Input.GetTouch(0).position;
                //模型浏览界面使用数据，单指左右滑动距离（模型碰撞框过大，沾满屏幕时使用，如三星堆）
                FingerGestureVariable_T.Instance.singleFingerHMoveDistance = (Input.GetTouch(0).deltaPosition.x) / 30;
                FingerGestureVariable_T.Instance.singleFingerVMoveDistance = (Input.GetTouch(0).deltaPosition.y) / 30;
                if (Input.GetTouch(0).position != lateTouchPos && Input.touchCount == 1)
                {
                    float tox0 = newPosition0.x - lateTouchPos.x;
                    float toy0 = newPosition0.y - lateTouchPos.y;
                    //计算手指 角度值
                    Vector3 aPoint0 = (newPosition0 - lateTouchPos).normalized;
                    Vector3 bPoint0 = (new Vector2(lateTouchPos.x - 1, 0) - lateTouchPos).normalized;
                    float dot0 = Vector2.Dot(aPoint0, bPoint0);
                    fingerAngle0 = Mathf.Acos(dot0) * Mathf.Rad2Deg;
                    //角度返回结果
                    /*  上拖拽 返回180度  下拖拽返回0度
                     *   左右拖拽 返回90度
                     */
                    if (tox0 < 0 && fingerAngle0 >= 45 && fingerAngle0 <= 135)  //左拖拽
                        FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchLeftModeDragOne;
                    else if (tox0 > 0 && fingerAngle0 >= 45 && fingerAngle0 <= 135)  //右拖拽
                        FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchRightModeDragOne;
                    if (toy0 > 0 && fingerAngle0 > 135)  //上拖拽
                        FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchUpModeDragOne;
                    else if (toy0 < 0 && fingerAngle0 < 45)  //下拖拽
                        FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchDownModeDragOne;

                    FingerEventComeBack_T.OnFingerGestureChanges();  //回调                                                    
                                                                     //记录 上一帧的触摸位置  
                    lateTouchPos = Input.GetTouch(0).position;
                }
                else if (Input.GetTouch(0).position != lateTouchPos && Input.touchCount == 2)
                {
                    lateTouchPos = Input.GetTouch(0).position;
                    FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchModeDragTwo;
                    FingerEventComeBack_T.OnFingerGestureChanges();  //回调
                }
            }
        }
        private void OnMouseUp()
        {
            StartCoroutine(WaitOnMouseUpOver());
        }
        private IEnumerator WaitOnMouseUpOver()
        {
            yield return Input.touchCount == 0;
            //     FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchModeUpOne;
            FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchUpOne;
            FingerEventComeBack_T.OnFingerGestureChanges();  //回调
            if (touchTimeDex < 0.25f) //点击了 模型
            {
                FingerGestureVariable_T.Instance.fingerGestureEnum_T = FingerGestureEnum_T.TouchModeClickOne;
                FingerEventComeBack_T.OnFingerGestureChanges();  //回调
            }
            CancelInvoke("CalculateTouchDownTime");
            touchTimeDex = 0f;
            FingerGestureVariable_T.Instance.isTouchMode = false;
            StopCoroutine(WaitOnMouseUpOver());
        }

        /// <summary>
        /// 按下时间 判定
        /// </summary>
        private void CalculateTouchDownTime()
        {
            touchTimeDex += 0.02f;
        }

    }
}
