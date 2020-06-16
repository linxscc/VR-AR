/***
 * 
 *    Title: UI框架
 *           主题： 框架核心参数  
 *    Description: 
 *           功能：
 *           1： 系统常量
 *           2： 全局性方法
 *           3： 系统枚举
 *           4： 委托定义
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
using PlaceAR.LabelDatas;
using System.Collections.Generic;

namespace vPlace_FW
{
    #region 系统枚举类型

    /// <summary>
    /// UI窗体的位置类型
    /// </summary>
    public enum UIFormType
    {
        /// <summary>
        /// 普通窗体
        /// </summary>
        Normal,
        /// <summary>
        /// 固定窗体
        /// </summary>
        Fixed,
        /// <summary>
        /// 弹出窗体
        /// </summary>
        PopUp
    }

    /// <summary>
    /// 窗体的显示类型
    /// </summary>
    public enum UIFormShowMode
    {
        /// <summary>
        /// 普通显示
        /// </summary>
        Normal,
        /// <summary>
        /// 反向切换显示
        /// </summary>
        ReverseChange,
        /// <summary>
        /// 隐藏其他显示
        /// </summary>
        HideOther
    }

    /// <summary>
    /// UI窗体的透明度类型
    /// </summary>
    public enum UIFormLucencyType
    {
        /// <summary>
        /// 完全透明，不能穿透
        /// </summary>
        Lucency,
        /// <summary>
        /// 半透明，不能穿透
        /// </summary>
        Translucence,
        /// <summary>
        /// 低透明度，不能穿透
        /// </summary>
        ImPenetrable,
        /// <summary>
        /// 可以穿透
        /// </summary>
        Pentrate
    }

    /// <summary>
    /// UI按钮的状态
    /// </summary>
    public enum ButtonType
    {
        /// <summary>
        /// 未选中状态
        /// </summary>
        Normal,
        /// <summary>
        /// 选中状态
        /// </summary>
        Selected,
    }
    #endregion

    public class SystemDefine
    {
        /// <summary>
        /// UI根节点路径
        /// </summary>
        public const string SYS_PATH_CANVAS = "ModeBrowserCanvas";

        /// <summary>
        /// UI窗体配置文件路径
        /// </summary>
        public const string SYS_PATH_UIFORM_CONFIG_INFO = "UIFormsConfigInfo";

        /// <summary>
        /// 系统配置文件路径
        /// </summary>
        public const string SYS_PATH_CONFIG_INFO = "SysConfigInfo";

        /// <summary>
        /// 标签常量
        /// </summary>
        public const string SYS_TAG_CANVAS = "UICamera";

        /// <summary>
        /// UI正常显示节点常量
        /// </summary>
        public const string SYS_NORMAL_NODE = "Normal";

        /// <summary>
        /// UI固定显示节点常量
        /// </summary>
        public const string SYS_FIXED_NODE = "Fixed";

        /// <summary>
        /// UI弹窗显示节点常量
        /// </summary>
        public const string SYS_POPUP_NODE = "PopUp";

        /// <summary>
        /// 脚本显示节点常量
        /// </summary>
        public const string SYS_SCRIPTMANAGER_NODE = "ScriptMgr";

        /* 遮罩管理器中，透明度常量 */
        public const float SYS_UIMASK_LUCENCY_COLOR_RGB = 255 / 255F;
        public const float SYS_UIMASK_LUCENCY_COLOR_RGB_A = 0F / 255F;

        public const float SYS_UIMASK_TRANS_LUCENCY_COLOR_RGB = 220 / 255F;
        public const float SYS_UIMASK_TRANS_LUCENCY_COLOR_RGB_A = 50F / 255F;

        public const float SYS_UIMASK_IMPENETRABLE_COLOR_RGB = 50 / 255F;
        public const float SYS_UIMASK_IMPENETRABLE_COLOR_RGB_A = 200F / 255F;

        /* 摄像机层深的常量 */

        /* 全局性的方法 */
        //Todo...

        /* 委托的定义 */
        //Todo....
    }
}