using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tools_XYRF
{
    /// <summary>
    /// 回调事件
    /// </summary>
    public class EventComeBack_T
    {
        /// <summary>
        /// 当用户 登录成功时 [登录状态]
        /// </summary>
        static public event Action OnUserLogoning = delegate { };
        /// <summary>
        /// 当用户 退出登录时 [登录状态]
        /// </summary>
        static public event Action OnUserQuit = delegate { };
        /// <summary>
        /// 当屏幕前方 存在对象时
        /// </summary>
        static public event Action ScreenHaveObj = delegate { };
        /// <summary>
        /// 当屏幕前方 不存在对象时
        /// </summary>
        static public event Action ScreenNotHaveObj = delegate { };


        static public void OnUserLogonings()
        {
            OnUserLogoning();
        }
        static public void OnUserQuits()
        {
            OnUserQuit();
        }
        static public void ScreenHaveObjs()
        {
            ScreenHaveObj();
        }
        static public void ScreenNotHaveObjs()
        {
            ScreenNotHaveObj();
        }


    }
}
