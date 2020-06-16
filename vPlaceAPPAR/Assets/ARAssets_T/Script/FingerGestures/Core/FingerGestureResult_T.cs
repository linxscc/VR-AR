using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace XYRF_FingerGesture
{
    /// <summary>
    /// 手势 类型结果
    /// </summary>
    public class FingerGestureResult_T
    {
        protected FingerGestureEnum_T FingerGestureEnum_T_ = FingerGestureEnum_T.None;
        protected Transform tra_ = null;
        private FingerGestureResult_T() { }
        public FingerGestureResult_T(FingerGestureEnum_T fingerGestureEnum_T, Transform tra_)
        {
            FingerGestureEnum_T_ = fingerGestureEnum_T;
            this.tra_ = tra_;
        }

        /// <summary>
        /// 得到 手势状态
        /// </summary>
        public FingerGestureEnum_T GetFingerGestureState
        {
            get
            {
                return FingerGestureEnum_T_;
            }
        }
        /// <summary>
        /// 得到 Transform
        /// </summary>
        public Transform GetTouchTransform
        {
            get
            {
                return tra_;
            }
        }
    }
}