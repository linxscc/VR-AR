/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
namespace MoDouAR
{
    /// <summary>
    /// 窗体基类
    /// </summary>
    public class WindowsBase : MonoBehaviour
    {
        protected Wind windData;
        public virtual Wind WindData
        {
            set;
            get;
        }
        protected UIType currentUIType = new UIType();
        /// <summary>
        /// 当前UI窗体类型
        /// </summary>
        public virtual UIType CurrentUIType
        {
            get { return currentUIType; }
            set { currentUIType = value; }
        }

        protected GameObject mWndObject;
        public GameObject WndObject
        {
            get { return mWndObject; }
        }
        /// <summary>
        /// 打开
        /// </summary>
        public virtual void Open()
        {

            //SystemLog.LogClick(WindData, "Open");
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public virtual void Close()
        {
            UIManager.GetInstance().CloseUIForm(WindData.name);
            SystemLog.LogClick(WindData, "Close");
        }
        /// <summary>
        /// 窗体显示状态
        /// </summary>
        public virtual void Display()
        {
            this.gameObject.SetActive(true);
            //设置模态窗体调用(必须是弹出窗体)
            if (currentUIType.uiFormType == UIFormType.PopUp)
            {
                UIMaskManager.GetInstance().SetMaskWindow(this.gameObject, currentUIType.uiFormLucencyType);
            }
        }
        /// <summary>
        /// 窗体冻结状态（在"栈"集合中）
        /// </summary>
        public virtual void Freeze()
        {
            this.gameObject.SetActive(true);
        }
        /// <summary>
        /// 窗体隐藏状态（不在"栈"集合中)
        /// </summary>
        public virtual void Hiding()
        {
         
            //浏览功能UI界面不能隐藏，需要用到，故添加此判断
            //if (!gameObject.name.Equals("BottomMenu_BrowserWindow"))
            if(currentUIType.isHidden)
                this.gameObject.SetActive(false);
            //取消模态窗体调用
            if (currentUIType.uiFormType == UIFormType.PopUp)
            {
                UIMaskManager.GetInstance().CancelMaskWindow();
            }
        }
        /// <summary>
        /// 窗体重新显示状态
        /// </summary>
        public virtual void ReDisplay()
        {
            this.gameObject.SetActive(true);
            //设置模态窗体调用(必须是弹出窗体)
            if (currentUIType.uiFormType == UIFormType.PopUp)
            {
                UIMaskManager.GetInstance().SetMaskWindow(this.gameObject, currentUIType.uiFormLucencyType);
            }
        }
    }
}