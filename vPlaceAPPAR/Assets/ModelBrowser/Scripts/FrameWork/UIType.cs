/***
 * 
 *    Title: UI框架
 *           主题: UI窗体类型
 *    Description: 
 *           功能: 引用SystemDefine.cs中的三个核心枚举，方便使用
 *           1: 
 *           2: 
 *           3: 
 *           4: 
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

namespace vPlace_FW
{
    public class UIType : MonoBehaviour
    {
        /// <summary>
        /// 是否清空"栈集合"，适用反向切换窗体，多弹窗UI
        /// </summary>
        public bool isClearStack = false;

        /// <summary>
        /// UI窗体的位置类型
        /// </summary>
        public UIFormType uiFormType = UIFormType.Normal;

        /// <summary>
        /// UI窗体的显示类型
        /// </summary>
        public UIFormShowMode uiFormShowMode = UIFormShowMode.Normal;

        /// <summary>
        /// UI窗体的透明度类型
        /// </summary>
        public UIFormLucencyType uiFormLucencyType = UIFormLucencyType.Lucency;
    }
}
