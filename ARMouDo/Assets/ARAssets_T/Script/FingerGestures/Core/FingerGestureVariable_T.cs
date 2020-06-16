using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace XYRF_FingerGesture
{
    /// <summary>
    /// 手势 全局变量
    /// </summary>
    public class FingerGestureVariable_T
    {
        private FingerGestureVariable_T() { }
        static private FingerGestureVariable_T instance;
        static public FingerGestureVariable_T Instance
        {
            set { instance = value; }
            get
            {
                if (instance == null)
                    instance = new FingerGestureVariable_T();
                return instance;
            }
        }


        /// <summary>
        /// 手势类型
        /// </summary>
        public FingerGestureEnum_T fingerGestureEnum_T = FingerGestureEnum_T.None;
        /// <summary>
        /// 点击到的对象
        /// </summary>
        public Transform touchTransform = null;
        /// <summary>
        /// 当前是否点击到了 对象
        /// </summary>
        public bool isTouchMode = false;
        /// <summary>
        /// 上一次是否点击到 对象
        /// </summary>
        public bool isLateTouchMode = false;
        /// <summary>
        /// 两指间距离
        /// </summary>
        public float doubleFingerDistance = 0;
        /// <summary>
        /// 单指水平移动距离
        /// </summary>
        public float singleFingerHMoveDistance = 0;
        /// <summary>
        /// 单指垂直移动距离
        /// </summary>
        public float singleFingerVMoveDistance = 0;
    }
}
