/***
 * 
 *    Title: UI框架
 *           主题： 消息（传递）中心
 *    Description: 
 *           功能： 负责UI框架中，所有UI窗体中间的数据传值。
 *                  
 *    Date: 2017/07
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vPlace_FW
{
	public class MessageCenter {
        //委托：消息传递
	    public delegate void DelegateMessageDelivery(KeyValuesUpdate kv);

        //消息中心缓存集合
        //<string : 数据大的分类，DelMessageDelivery 数据执行委托>
	    public static Dictionary<string, DelegateMessageDelivery> dicMessages = new Dictionary<string, DelegateMessageDelivery>();

        /// <summary>
        /// 增加消息的监听。
        /// </summary>
        /// <param name="messageType">消息分类</param>
        /// <param name="handler">消息委托</param>
	    public static void AddMessageListener(string messageType,DelegateMessageDelivery handler)
	    {
            if (!dicMessages.ContainsKey(messageType))
	        {
                dicMessages.Add(messageType,null);
            }
	        dicMessages[messageType] += handler;
	    }

        /// <summary>
        /// 取消消息的监听
        /// </summary>
        /// <param name="messageType">消息分类</param>
        /// <param name="handele">消息委托</param>
	    public static void RemoveMessageListener(string messageType,DelegateMessageDelivery handele)
	    {
            if (dicMessages.ContainsKey(messageType))
            {
                dicMessages[messageType] -= handele;
            }

	    }

        /// <summary>
        /// 取消所有指定消息的监听
        /// </summary>
	    public static void ClearALLMessageListener()
	    {
	        if (dicMessages!=null)
	        {
	            dicMessages.Clear();
            }
	    }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="messageType">消息的分类</param>
        /// <param name="kv">键值对(对象)</param>
	    public static void SendMessage(string messageType,KeyValuesUpdate kv)
	    {
	        DelegateMessageDelivery del;                         //委托

	        if (dicMessages.TryGetValue(messageType,out del))
	        {
	            if (del!=null)
	            {
                    //调用委托
	                del(kv);
	            }
	        }
	    }


	}

    /// <summary>
    /// 键值更新对
    /// 功能： 配合委托，实现委托数据传递
    /// </summary>
    public class KeyValuesUpdate
    {   //键
        private string key;
        //值
        private object values;

        /*  只读属性  */

        public string Key
        {
            get { return key; }
        }
        public object Values
        {
            get { return values; }
        }

        public KeyValuesUpdate(string key, object valueObj)
        {
            this.key = key;
            values = valueObj;
        }
    }


}