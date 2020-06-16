using ARKit_T;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.iOS;


namespace Tools_XYRF
{
    /// <summary>
    /// 检测屏幕前方 对象 or 选取对象
    /// </summary>
    public class DetectionScreenCenterTra
    {

        #region  检测 屏幕前方是否存在对象
        /// <summary>
        /// 检测屏幕前方是否存在 模型对象
        /// </summary>
        private void DetectionScreenObj()
        {
            if (ARKit_OnLineCacheData.Instance.isScreenObj)  //存在
                EventComeBack_T.ScreenHaveObjs();
            else   //不存在
                EventComeBack_T.ScreenNotHaveObjs();
        }
        #endregion //实时监测 屏幕前方是否存在对象结束...

        /// <summary>
        /// 帧/一次  检测
        /// </summary>
        public void DetectionScreenCenterTraUpdate()
        {
            DetectionScreenObj();                     // 检测屏幕前方 是否存在模型对象
        }

        private DetectionScreenCenterTra()
        {
        }
        private static DetectionScreenCenterTra instance;
        public static DetectionScreenCenterTra Instance
        {
            get
            {
                if (instance == null)
                    instance = new DetectionScreenCenterTra();
                return instance;
            }
            set { instance = value; }
        }
    }
}
