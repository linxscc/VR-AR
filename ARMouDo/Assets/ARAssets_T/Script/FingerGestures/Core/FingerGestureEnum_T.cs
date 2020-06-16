using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace XYRF_FingerGesture
{
    /// <summary>
    /// Touch手势类型
    /// </summary>
    public enum FingerGestureEnum_T
    {
        /// <summary>
        /// null 无手势
        /// </summary>
        None,

        /// <summary>
        /// 单手指 按下
        /// </summary>
        TouchDownOne,
        /// <summary>
        /// 单手指 点击
        /// </summary>
        TouchClickOne,
        /// <summary>
        /// 单手指 左拖拽
        /// </summary>
        TouchLeftDragOne,
        /// <summary>
        /// 单手指 右拖拽
        /// </summary>
        TouchRightDragOne,
        /// <summary>
        /// 单手指 上拖拽
        /// </summary>
        TouchUpDragOne,
        /// <summary>
        /// 单手指 下拖拽
        /// </summary>
        TouchDownDragOne,
        /// <summary>
        /// 单手指 抬起
        /// </summary>
        TouchUpOne,
        /// <summary>
        /// 单手指 按下对象
        /// </summary>
        TouchModeDownOne,
        /// <summary>
        /// 单手指 点击对象
        /// </summary>
        TouchModeClickOne,
        /// <summary>
        /// 单手指 左拖拽对象
        /// </summary>
        TouchLeftModeDragOne,
        /// <summary>
        /// 单手指 右拖拽对象
        /// </summary>
        TouchRightModeDragOne,
        /// <summary>
        /// 单手指 上拖拽对象
        /// </summary>
        TouchUpModeDragOne,
        /// <summary>
        /// 单手指 下拖拽对象
        /// </summary>
        TouchDownModeDragOne,
        /// <summary>
        /// 双手指 拖拽对象
        /// </summary>
        TouchModeDragTwo,
        /// <summary>
        /// 单手指 抬起对象
        /// </summary>
        TouchModeUpOne,
        /// <summary>
        /// 双手指 按下
        /// </summary>
        TouchDownTwo,
        /// <summary>
        /// 双手指 点击
        /// </summary>
        TouchClickTwo,
        /// <summary>
        /// 双手指 拖拽 
        /// </summary>
        TouchDragTwo,
        /// <summary>
        /// 双手指 扩大拉伸 →向外
        /// </summary>
        TouchDrawOutTwo,
        /// <summary>
        /// 双手指 缩小拉伸 →向里
        /// </summary>
        TouchDrawInTwo,
        /// <summary>
        /// 双手指 抬起
        /// </summary>
        TouchUpTwo,
    }
}
