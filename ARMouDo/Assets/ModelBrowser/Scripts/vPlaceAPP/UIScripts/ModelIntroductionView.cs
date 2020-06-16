/***
 * 
 *    Title: vPlaceAPP-ModelBrowserView
 *           主题: 模型信息简介界面
 *    Description: 
 *           功能: 通过一段文字信息来介绍模型
 *           1: 
 *           2: 
 *           3: 
 *           4: 
 *                          
 *    Date: 
 *    Version: 
 *	  Author: 
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using System.Collections;
using vPlace_FW;
using UnityEngine.UI;

namespace vPlace_zpc
{
    public class ModelIntroductionView : BaseUI
    {
        #region 字段
        /// <summary>
        /// 模型名称
        /// </summary>
        public Text modelName = null;

        /// <summary>
        /// 模型简介
        /// </summary>
        public Text modelIntroduction = null;
        public CallBack close;

        #endregion


        #region 方法
        private void Awake()
        {
            base.CurrentUIType.uiFormType = UIFormType.PopUp;
            base.CurrentUIType.uiFormShowMode = UIFormShowMode.Normal;
            base.CurrentUIType.uiFormLucencyType = UIFormLucencyType.Lucency;


            //EventTriggerListener.Get(transform.Find("BlankArea").gameObject).onClick += Close;
        }

        private void Close(GameObject obj)
        {
            CloseUIForm("ModelIntroductionView");
            //UIManager.GetInstance().ShowSelectModelBtn();
            if (close != null) close();
        }

        #endregion

        private void OnEnable()
        {
            ViewInit();
        }


        #region 初始化

        /// <summary>
        /// 判断是整体模型还是子模型，分别显示相关简介
        /// </summary>
        private void ViewInit()
        {
            if (!BrowserTypeViewContol.Instance.browserTypeView.isClose)
            {
                //modelName.text = ProjectConstDefine.selectedModelChildName;
               // modelIntroduction.text = ProjectConstDefine.selectedModelChildDescription;
            }
            else
            {
                modelName.text = ProjectConstDefine.selectedModelName;
                modelIntroduction.text = ProjectConstDefine.selectedModelDescription;
            }
        }

        #endregion

    }
}