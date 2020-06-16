using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MoDouAR
{
    public class UIPro
    {
        /// <summary>
        /// UI显隐 处理
        /// </summary>
        /// <param name="tra_">对象</param>
        /// <param name="to">true=显示</param>
        public void UI_Show(Transform tra_, bool to)
        {
            tra_.gameObject.SetActive(to);
        }
        /// <summary>
        /// UI图片透明 处理
        /// </summary>
        /// <param name="tra_">U对象</param>
        /// <param name="toAlpha">期望alpha值 [0-1]</param>
        public void UI_ImageAlpha(Transform tra_, float toAlpha)
        {
            Color rgb = new Color(255, 255, 255, toAlpha);
            tra_.GetComponent<Image>().color = rgb;
        }
        /// <summary>
        /// UI Text透明 处理
        /// </summary>
        /// <param name="tra_">U对象</param>
        /// <param name="toAlpha">期望alpha值 [0-1]</param>
        public void UI_TextAlpha(Transform tra_, float toAlpha)
        {
            Color rgb = new Color(255, 255, 255, toAlpha);
            tra_.GetComponent<Text>().color = rgb;
        }
        /// <summary>
        /// UI Button可点击 处理
        /// </summary>
        /// <param name="tra_">U对象</param>
        /// <param name="to">true=可点击</param>
        public void UI_ButtonInteractable(Transform tra_, bool to)
        {
            tra_.GetComponent<Button>().interactable = to;
        }
        /// <summary>
        /// UI InputField清理 输入
        /// </summary>
        /// <param name="tra_">U对象</param>
        public void UI_InputField(Transform tra_)
        {
            tra_.GetComponent<InputField>().text = "";
        }
        /// <summary>
        /// UI Text文本输入
        /// </summary>
        /// <param name="tra_"></param>
        /// <param name="str"></param>
        public void UI_TextModify(Transform tra_, string str)
        {
            tra_.GetComponent<Text>().text = str;
        }
    }
}
