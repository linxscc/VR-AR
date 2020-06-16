/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PureMVCDemo
{
    /// <summary>
    /// 二次确认菜单
    /// </summary>
    public class ConfirmMenuControl
    {
        /// <summary>
        /// UI根目录文件
        /// </summary>
        private GameObject canvas;
        /// <summary>
        /// 菜单
        /// </summary>
        private GameObject menu;
        /// <summary>
        /// 确定执行的按钮
        /// </summary>
        private CallBack callBack;
        private CallBack<bool> callBackParam;
        /// <summary>
        /// 是否开启
        /// </summary>
        private bool isOpen = false;
        private ConfirmMenu confirmMenu;

        private ConfirmMenuControl()
        {
            canvas = GameObject.FindGameObjectWithTag(Tag.mainUI);
            menu = Resources.Load<GameObject>(Global.confirmMenuControlUrl);
            menu = GameObject.Instantiate(menu);
            menu.transform.parent = canvas.transform;
            menu.transform.localPosition = Vector3.zero;
            menu.transform.localScale = Vector3.one;
            confirmMenu = menu.GetComponent<ConfirmMenu>();
            confirmMenu.OnInit();
            confirmMenu.confirm.onClick.AddListener(Confirm);
            confirmMenu.cancel.onClick.AddListener(Concel);
        }
        private static ConfirmMenuControl singleton;
        public static ConfirmMenuControl Singleton
        {
            get
            {
                if (singleton == null)
                    singleton = new ConfirmMenuControl();
                return singleton;
            }
            set
            {
                singleton = value;
            }
        }
        /// <summary>
        /// 确定按钮
        /// </summary>
        public void Confirm()
        {
            if (callBack != null)
                callBack();
            if (callBackParam != null)
                callBackParam(true);
            //Close();
        }
        /// <summary>
        /// 取消按钮
        /// </summary>
        public void Concel()
        {
            Close();
        }
        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="txt">显示的文字</param>
        /// <param name="callBack">需要确定执行的方法</param>
        /// <param name="complete">窗口动画回调</param>
        public void Open(string txt )
        {
            if (isOpen) return;
            isOpen = true;
            confirmMenu.Open(txt, null);
            //this.callBack = callBack;
            //this.callBackParam = callBackParam;
        }
        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="txt">显示的文字</param>
        /// <param name="callBack">需要确定执行的方法</param>
        /// <param name="complete">窗口动画回调</param>
        public void Open(string txt , CallBack callBack)
        {
            if (isOpen) return;
            isOpen = true;
            confirmMenu.Open(txt, null);
            this.callBack = callBack;
            //this.callBackParam = callBackParam;
        }
        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="txt">显示的文字</param>
        /// <param name="callBack">需要确定执行的方法</param>
        /// <param name="complete">窗口动画回调</param>
        public void Open(string txt ,  CallBack<bool> callBackParam , TweenCallback complete = null)
        {
            if (isOpen) return;
            isOpen = true;
            confirmMenu.Open(txt, complete);
           // this.callBack = callBack;
            this.callBackParam = callBackParam;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="complete">窗口关闭动画回调</param>
        public void Close(TweenCallback complete = null)
        {
  
            isOpen = false;
             confirmMenu.Close(complete);
            if (callBackParam != null)
                callBackParam(false);
        }
    }
}
