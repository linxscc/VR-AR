using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace XYRF_FingerGesture
{
    /// <summary>
    /// 手势 事件回调
    /// </summary>
    public class FingerEventComeBack_T : MonoBehaviour
    {
        /// <summary>
        /// 当手势状态 改变时
        /// </summary>
        static public event Action<FingerGestureResult_T> OnFingerGestureChange = delegate { };

        static public void OnFingerGestureChanges()
        {
            FingerGestureResult_T fingerGestureResult = new FingerGestureResult_T(FingerGestureVariable_T.Instance.fingerGestureEnum_T, FingerGestureVariable_T.Instance.touchTransform);
            OnFingerGestureChange(fingerGestureResult);
        }
    }
}