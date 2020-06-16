/***
 * 
 *    Title: vPlaceAPP-ModelBrowserView
 *           主题:  浏览类型选择界面
 *    Description: 
 *           功能: 选择对模型适用哪一种模式浏览
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
using System.Collections.Generic;
using PlaceAR.LabelDatas;
using PlaceAR;
using System.Linq;
using DG.Tweening;
using UnityEditor;

namespace vPlace_zpc
{
    public class BrowserTypeView : MonoBehaviour
    {
        #region 字段
        /// <summary>
        /// 浏览功能实现按钮
        /// </summary>
        public GameObject browserTypeBtn = null;
        ///// <summary>
        ///// 复位按钮
        ///// </summary>
        //public GameObject resetBtn = null;
        /// <summary>
        /// 子模型点击逻辑实现
        /// </summary>
        public Label2DUIController labelUIs = null;
        /// <summary>
        /// 浏览窗口是否关闭
        /// </summary>
        public bool isClose = false;

        /// <summary>
        /// 模型控制
        /// </summary>
        private ModelControl modelController = null;
        /// <summary>
        /// 浏览功能文本
        /// </summary>
        private Text browserTypeText = null;
        /// <summary>
        /// 是否拆解
        /// </summary>
        private bool isAssemble = false;
        /// <summary>
        /// 是否截面
        /// </summary>
        private bool isSection = true;
        /// <summary>
        /// 子模型按钮高度
        /// </summary>
        private float btnHeight = 0f;
        #endregion

        #region 初始化

        private void Awake()
        {
            modelController = FindObjectOfType<ModelControl>();
            browserTypeText = browserTypeBtn.GetComponentInChildren<Text>();
            btnHeight = browserTypeBtn.GetComponent<RectTransform>().sizeDelta.y;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 浏览方式文字显示
        /// </summary>
        public void BrowserTypeShowText()
        {
            if (ProjectConstDefine.hasConfig)
            {
                browserTypeBtn.GetComponent<Button>().interactable = true;

                switch (ProjectConstDefine.labelDataList.controlType)
                {
                    case 0:
                        browserTypeText.text = "预设拆解(合)";
                        break;
                    case 1:
                        browserTypeText.text = "预设剖面";
                        break;
                    case 2:
                        browserTypeText.text = "预设逐层";
                        break;
                    case 3:
                        browserTypeText.text = "预设显微";
                        break;
                    case 4:
                        browserTypeText.text = "其他模式";
                        break;
                    case 5:
                        browserTypeText.text = "高亮模式";
                        browserTypeBtn.GetComponent<Button>().interactable = false;
                        break;
                    case 6:
                        browserTypeText.text = "预设截面";
                        break;
                    case 7:
                        browserTypeText.text = "预设分解(合)";
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 预设拆解/逐层/剖面按钮
        /// </summary>
        public void OnAssembleDis()
        {
            switch (ProjectConstDefine.labelDataList.controlType)
            {
                case 0:
                    if (isAssemble)
                    {
                        modelController.OnAssemble();
                        browserTypeText.text = "预设拆解(合)";
                    }
                    else
                    {
                        modelController.OnDisassemble();
                        browserTypeText.text = "预设拆解(分)";
                    }
                    break;
                case 1:
                    modelController.Profile();
                    break;
                case 2:
                    modelController.Layer();
                    break;
                case 6:
                    Section();
                    modelController.Section();
                    break;
                case 7:
                    if (isAssemble)
                    {
                        modelController.OnAssemble();
                        browserTypeText.text = "预设分解(合)";
                    }
                    else
                    {
                        modelController.OnDisassemble();
                        browserTypeText.text = "预设分解(分)";
                    }
                    break;
                default: break;
            }

            isAssemble = !isAssemble;
        }

        /// <summary>
        /// 复位
        /// </summary>
        public void BackHome()
        {
            modelController.BackHome();
            modelController.OnAssemble();
            isAssemble = false;
            labelUIs.ClearModelChildSelectBtn();
            isSection = true;
        }

        /// <summary>
        /// 截面模式中子模型的显隐
        /// </summary>
        public void Section()
        {
            if (isSection)
                SectionModelChildShowOrHide(isSection);
            else
                SectionModelChildShowOrHide(isSection);

            isSection = !isSection;
        }

        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="b"></param>
        private void SectionModelChildShowOrHide(bool b)
        {
            for (int i = 0; i < modelController.modelChildList.Count; ++i)
                if (modelController.modelChildList[i].isHide)
                    modelController.modelChildList[i].gameObject.SetActive(b);
            for (int i = 0; i < labelUIs.hideObjList.Count; ++i)
                labelUIs.hideObjList[i].SetActive(b);
            //隐藏的UI顶格显示
            labelUIs.modelChildGrid.anchoredPosition3D -= btnHeight * labelUIs.hideObjList.Count * Vector3.up;
        }

        /// <summary>
        /// 模型浏览功能实现逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Label2DUI_OnClick(object sender, System.EventArgs e)
        {
            LabelUIEventArgs lue = e as LabelUIEventArgs;
            ProjectConstDefine.selectedModelChildName = lue.label3D.Title;
            ProjectConstDefine.selectedModelChildDescription = lue.label3D.Description;

            int count = (sender as BrowserTypeItem).labelChild.Count;

            switch (ProjectConstDefine.labelDataList.controlType)
            {

                case 0:
                    if (lue.label3D.Group.Equals(labelUIs.modelChildGrid.GetChild(0).name)) //ScrollViewUI排版
                        labelUIs.modelChildGrid.anchoredPosition3D -= btnHeight * count * Vector3.up;
                    if (count == 0)
                        modelController.HideOthersButOne(lue.label3D, null);
                    else
                        modelController.HideOthersBut(lue.label3D, sender as BrowserTypeItem);
                    break;
                case 1:
                    modelController.Profile(lue.label3D);
                    break;
                case 2:
                    modelController.Layer(lue.label3D);
                    break;
                case 5:
                case 7:
                    if (count == 0)
                        modelController.HideOthersButOne(lue.label3D);
                    else
                        modelController.HideOthersBut(lue.label3D, sender as BrowserTypeItem);
                    break;
                case 6:
                    if (count == 0)
                    {
                        modelController.HideOthersButOne(lue.label3D);
                        if (modelController.model.transform.GetChild(modelController.model.transform.childCount - 1).gameObject.activeSelf)
                            modelController.model.transform.GetChild(modelController.model.transform.childCount - 1).gameObject.SetActive(false);
                    }
                    else
                        modelController.HideOthersBut(lue.label3D, sender as BrowserTypeItem);
                    break;
                default:
                    break;
            }

            //lue.Used = true;
        }
        #endregion


        void OnEnable()
        {
            modelController.OnInitModel += Model_OnInitModel;
        }

        public void Model_OnInitModel(object sender, System.EventArgs e)
        {
            labelUIs.LoadModelChild(modelController.Label3Ds.childData, this);
        }

        void OnDisable()
        {
            modelController.OnInitModel -= Model_OnInitModel;
        }

        /// <summary>
        /// 浏览功能界面打开
        /// </summary>
        public void Open()
        {
            if (ProjectConstDefine.labelDataList.controlType == 6)
                foreach (var child in modelController.modelChildList)
                {
                    if (child.isHide)
                        child.gameObject.SetActive(false);
                }
            gameObject.SetActive(true);
            BrowserTypeShowText();
            transform.DOScaleY(1, .5f);
            isClose = false;
            isAssemble = false;
        }

        /// <summary>
        /// 浏览功能界面关闭
        /// </summary>
        public void Close()
        {
            transform.DOScaleY(0, .05f);
            isClose = true;
        }

    }
}
