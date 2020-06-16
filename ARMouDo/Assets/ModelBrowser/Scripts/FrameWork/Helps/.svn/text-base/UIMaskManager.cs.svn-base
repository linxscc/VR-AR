/***
 * 
 *    Title: UI框架
 *           主题: UI遮罩管理器 
 *    Description: 
 *           功能: 负责“弹出窗体”模态显示实现
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
using UnityEngine.UI;

namespace MoDouAR
{
    public class UIMaskManager : MonoBehaviour
    {
        #region 字段

        private static UIMaskManager instance = null;

        private GameObject canvasRoot = null;

       // private Transform TransUIScriptsNode = null;

        private GameObject topPanel;

        private GameObject maskPanel;

        #endregion


        #region 方法

        public static UIMaskManager GetInstance()
        {
            if (instance == null)
                instance = new GameObject("UIMaskManager").AddComponent<UIMaskManager>();
            return instance;
        }

        private void Awake()
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
            //得到UI根节点对象、脚本节点对象
            canvasRoot = GameObject.FindGameObjectWithTag(SystemDefineTag.UICanvas2D);
            // TransUIScriptsNode = UnityHelper.FindTheChildNode(canvasRoot, SystemDefine.SYS_SCRIPTMANAGER_NODE);
            //把本脚本实例，作为“脚本节点对象”的子节点。
            //UnityHelper.AddChildNodeToParentNode(canvasRoot.transform, this.gameObject.transform);
            //得到“顶层面板”、“遮罩面板”
           
            topPanel = gameObject;
            maskPanel = transform.Find("MaskPlane").gameObject;
            maskPanel.SetActive(false);
            // maskPanel = UnityHelper.FindTheChildNode(gameObject, "MaskPlane").gameObject;
        }
        /// <summary>
        /// 设置遮罩状态
        /// </summary>
        /// <param name="goDisplayUIForms">需要显示的UI窗体</param>
        /// <param name="lucenyType">显示透明度属性</param>
        public void SetMaskWindow(GameObject goDisplayUIForms, UIFormLucencyType lucenyType = UIFormLucencyType.Lucency)
        {
            //顶层窗体下移
            topPanel.transform.SetParent(goDisplayUIForms.transform.parent);
            topPanel.transform.SetAsLastSibling();
            //启用遮罩窗体以及设置透明度
            switch (lucenyType)
            {
                //完全透明，不能穿透
                case UIFormLucencyType.Lucency:
                   // print("完全透明");
                    maskPanel.SetActive(true);
                    Color newColor1 = new Color(0, 0, 0, 0F / 255F);
                    maskPanel.GetComponent<Image>().color = newColor1;
                    break;
                //半透明，不能穿透
                case UIFormLucencyType.Translucence:
                    //print("半透明");
                    maskPanel.SetActive(true);
                    Color newColor2 = new Color(0, 0, 0, 50 / 255F);
                    maskPanel.GetComponent<Image>().color = newColor2;
                    break;
                //低透明，不能穿透
                case UIFormLucencyType.ImPenetrable:
                    //print("低透明");
                    maskPanel.SetActive(true);
                    Color newColor3 = new Color(0, 0, 0, 200F / 255F);
                    maskPanel.GetComponent<Image>().color = newColor3;
                    break;
                //可以穿透
                case UIFormLucencyType.Pentrate:
                   // print("允许穿透");
                    if (maskPanel.activeInHierarchy)
                    {
                        maskPanel.SetActive(false);
                    }
                    break;

                default:
                    break;
            }
            //遮罩窗体下移
            maskPanel.transform.SetAsLastSibling();
            //显示窗体的下移
            goDisplayUIForms.transform.SetAsLastSibling();
        }
        /// <summary>
        /// 取消遮罩状态
        /// </summary>
	    public void CancelMaskWindow()
        {
            //顶层窗体上移
            topPanel.transform.SetAsFirstSibling();
            //禁用遮罩窗体
          //  if (maskPanel.activeInHierarchy)
           // {
                //隐藏
                maskPanel.SetActive(false);
           // }
        }
        #endregion
    }
}