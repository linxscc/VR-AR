/***
 * 
 *    Title: UI框架
 *           主题: UI窗体的父类 
 *    Description: 
 *           功能: 定义所有窗体的父类，定义四个生命周期
 *           1: Dispaly 显示状态
 *           2: Hiding 隐藏状态
 *           3: ReDisplay 再显示状态
 *           4: Freeze 冻结状态
 *                          
 *    Date: 2017/07
 *    Version: 0.1
 *	  Author: 
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using System.Collections;
using PlaceAR;

namespace vPlace_FW
{
    public class BaseUI : MonoBehaviour
    {
        #region 字段

        private UIType currentUIType = new UIType();
        #endregion


        #region 属性

        /// <summary>
        /// 当前UI窗体类型
        /// </summary>
        public UIType CurrentUIType
        {
            get { return currentUIType; }
            set { currentUIType = value; }
        }
        #endregion


        #region 窗体的四种生命周期（状态）

        /// <summary>
        /// 窗体显示状态
        /// </summary>
        public virtual void Display()
        {
            this.gameObject.SetActive(true);
            //设置模态窗体调用(必须是弹出窗体)
            if (currentUIType.uiFormType == UIFormType.PopUp)
            {
                //UIMaskManager.GetInstance().SetMaskWindow(this.gameObject, currentUIType.uiFormLucencyType);
            }
        }

        /// <summary>
        /// 窗体隐藏状态（不在"栈"集合中)
        /// </summary>
        public virtual void Hiding()
        {
            this.gameObject.SetActive(false);
            //取消模态窗体调用
            if (currentUIType.uiFormType == UIFormType.PopUp)
            {
                //UIMaskManager.GetInstance().CancelMaskWindow();
            }
        }
        #endregion

        /// <summary>
        /// 窗体重新显示状态
        /// </summary>
        public virtual void ReDisplay()
        {
            this.gameObject.SetActive(true);
            //设置模态窗体调用(必须是弹出窗体)
            if (currentUIType.uiFormType == UIFormType.PopUp)
            {
                //UIMaskManager.GetInstance().SetMaskWindow(this.gameObject, currentUIType.uiFormLucencyType);
            }
        }

        /// <summary>
        /// 窗体冻结状态（在"栈"集合中）
        /// </summary>
        public virtual void Freeze()
        {
            this.gameObject.SetActive(true);
        }

        #region 封装子类常用的方法

        /// <summary>
        /// 注册按钮事件
        /// </summary>
        /// <param name="buttonName">按钮节点名称</param>
        /// <param name="delHandle">委托：需要注册的方法</param>
	    protected void RigisterButtonObjectEvent(string buttonName, EventTriggerListener.VoidDelegate delHandle)
        {
            GameObject goButton = UnityHelper.FindTheChildNode(this.gameObject, buttonName).gameObject;
            //给按钮注册事件方法
            if (goButton != null)
            {
                EventTriggerListener.Get(goButton).onClick = delHandle;
            }
        }

        /// <summary>
        /// 打开UI窗体
        /// </summary>
        /// <param name="uiFormName"></param>
	    protected void OpenUIForm(string uiFormName)
        {
            //ScrollMenuControl.Singleton.OnInit();
            //ScrollMenuControl.Singleton.Open();
            
            UIManager.GetInstance().ShowUIForm(uiFormName);
        }

        /// <summary>
        /// 关闭UI窗体
        /// </summary>
	    protected void CloseUIForm(string strUIFromName)
        {
            //ScrollMenuControl.Singleton.Close(null);
            //string strUIFromName = string.Empty;            //处理后的UIFrom 名称
            //int intPosition = -1;

            //strUIFromName = GetType().ToString();             //命名空间+类名
            //intPosition = strUIFromName.IndexOf('.');
            //if (intPosition != -1)
            //{
            //    //剪切字符串中“.”之间的部分
            //    strUIFromName = strUIFromName.Substring(intPosition + 1);
            //}

            UIManager.GetInstance().CloseUIForm(strUIFromName);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msgType">消息的类型</param>
        /// <param name="msgName">消息名称</param>
        /// <param name="msgContent">消息内容</param>
	    protected void SendMessage(string msgType, string msgName, object msgContent)
        {
            KeyValuesUpdate kvs = new KeyValuesUpdate(msgName, msgContent);
            MessageCenter.SendMessage(msgType, kvs);
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="messagType">消息分类</param>
        /// <param name="handler">消息委托</param>
	    public void ReceiveMessage(string messagType, MessageCenter.DelegateMessageDelivery handler)
        {
            MessageCenter.AddMessageListener(messagType, handler);
        }
        #endregion


        #region
        #endregion
    }
}
