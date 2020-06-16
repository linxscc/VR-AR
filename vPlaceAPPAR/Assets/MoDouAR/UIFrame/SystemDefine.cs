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
    public class UIType
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
    public class Wind
    {
        /// <summary>
        /// 窗体id
        /// </summary>
        public int id;
        /// <summary>
        /// 窗体名称
        /// </summary>
        public string name;
        /// <summary>
        /// 预制物路径
        /// </summary>
        public string path;
    }
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
    public class SystemDefineTag 
    {

        /// <summary>
        /// 标签常量
        /// </summary>
        public const string UICanvas2D = "UICamera";
        public const string NormalPath = "Norma";
        public const string FixedPath = "Fixed";
        public const string PopPath = "Pop";
    }
}