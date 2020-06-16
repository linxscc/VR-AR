////////////////////////////////////////////////////////////////////
//                            _ooOoo_                             //
//                           o8888888o                            //
//                           88" . "88                            //
//                           (| ^_^ |)                            //
//                           O\  =  /O                            //
//                        ____/`---'\____                         //
//                      .'  \\|     |//  `.                       //
//                     /  \\|||  :  |||//  \                      //
//                    /  _||||| -:- |||||-  \                     //
//                    |   | \\\  -  /// |   |                     //
//                    | \_|  ''\---/''  |   |                     //
//                    \  .-\__  `-`  ___/-. /                     //
//                  ___`. .'  /--.--\  `. . ___                   //
//                ."" '<  `.___\_<|>_/___.'  >'"".                //
//              | | :  `- \`.;`\ _ /`;.`/ - ` : | |               //
//              \  \ `-.   \_ __\ /__ _/   .-` /  /               //
//        ========`-.____`-.___\_____/___.-`____.-'========       //
//                             `=---='                            //
//        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^      //
//                  佛祖镇楼            BUG辟易                    //
////////////////////////////////////////////////////////////////////
/*
		// Copyright (C) 
 
        // 文件名：BottomMenu_BrowserWindow.cs
        // 文件功能描述：浏览功能UI的实现及打开与关闭

		// 作者: zpc
        // 创建标识：

        // 修改标识：
        // 修改描述：
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoDouAR;
using DG.Tweening;
using UnityEngine.UI;
using vPlace_zpc;

public class BottomMenu_BrowserWindow : Window<BottomMenu_BrowserWindow>
{
                                                                                                                     #region 重写父类方法
    public override int ID
    {
        get
        {
            return -2;
        }
    }

    public override string Name
    {
        get
        {
            return "BottomMenu_BrowserWindow";
        }
    }

    public override string Path
    {
        get
        {
            return "BottomMenu_BrowserWindow";
        }
    }

    public override UIType CurrentUIType
    {
        get
        {
            currentUIType.isHidden = false;
            return base.CurrentUIType;
        }
    }

    #endregion

    private Transform backGround;
    public Transform BackGround
    {
        get
        {
            if (backGround == null)
                backGround = WndObject.transform.Find("BackGround");
            return backGround;
        }
    }

    /// <summary>
    /// 返回按钮
    /// </summary>
    public GameObject btn_Return = null;
    /// <summary>
    /// 浏览功能UI控制
    /// </summary>
    public Label2DUIController labelUIs = null;
    /// <summary>
    /// 模型控制
    /// </summary>
    private ModelControl modelController = null;
    /// <summary>
    /// 是否截面
    /// </summary>
    private bool isSection = true;
    public override void Awake()
    {
        base.Awake();
        EventTriggerListener.Get(btn_Return).onClick = CloseUIAndOnAssembleDis;
        //btnWidth = browserTypeBtn.GetComponent<RectTransform>().sizeDelta.y;
        if (labelUIs == null)
            labelUIs = GetComponent<Label2DUIController>();
        if (modelController == null)
            modelController = FindObjectOfType<ModelControl>();
        //此UI需提前加载且不能隐藏，故将其移动到最大屏幕像素外 - 1920
        BackGround.localPosition = new Vector3(1920, BackGround.localPosition.y, BackGround.localPosition.z);
    }

    /// <summary>
    /// 关闭UI、退出浏览功能、透明材质还原、按钮背景清空、子模型按钮还原（Toggle）、简介还原
    /// </summary>
    private void CloseUIAndOnAssembleDis(GameObject obj)
    {
        Close();
        BottomMenu_ModelWindow.Instance.OnAssembleDis();
        ModelControl.GetInstance().MaterialReset();
        labelUIs.ClearModelGroupSelectBtn();
        ModelIntroWindow.Instance.ModelInfoUpdate(modelController.data.item.name, modelController.data.item.info);
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
        for (int i = 0; i < labelUIs.hideObjList.Count; ++i)
            labelUIs.hideObjList[i].SetActive(b);
        //隐藏的UI顶格显示(知了AR竖式排版所用)
        //labelUIs.modelChildGrid.anchoredPosition3D -= btnWidth * labelUIs.hideObjList.Count * Vector3.right;
    }

    /// <summary>
    /// 模型浏览功能实现逻辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void Label2DUI_OnClick(object sender, System.EventArgs e)
    {
        LabelUIEventArgs lue = e as LabelUIEventArgs;
        //ProjectConstDefine.selectedModelName = lue.label3D.Title;
        //ProjectConstDefine.selectedModelDescription = lue.label3D.Description;
        //if (!ModelIntroWindow.instance)
        //ModelIntroWindow.Instance.CreatWindow();
        ModelIntroWindow.Instance.ModelInfoUpdate(lue.label3D.Title, lue.label3D.Description);

        int count = (sender as BrowserTypeItem).labelChild.Count;

        switch (ProjectConstDefine.labelDataList.controlType)
        {

            case 0:
                //if (lue.label3D.Group.Equals(labelUIs.modelChildGrid.GetChild(0).name)) //ScrollViewUI排版
                //labelUIs.modelChildGrid.anchoredPosition3D -= btnHeight * count * Vector3.up;
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
                    GameObject obj = modelController.model.transform.GetChild(modelController.model.transform.childCount - 1).gameObject;
                    if (obj.activeSelf)
                        obj.SetActive(false);
                }
                else
                    modelController.HideOthersBut(lue.label3D, sender as BrowserTypeItem);
                break;
            default:
                break;
        }

        //lue.Used = true;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        modelController.OnInitModel += Model_OnInitModel;
    }

    /// <summary>
    /// 浏览功能UI载入逻辑
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void Model_OnInitModel(object sender, System.EventArgs e)
    {
        labelUIs.LoadModelChild(modelController.Label3Ds.childData, this);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        modelController.OnInitModel -= Model_OnInitModel;
    }

    public override void Open()
    {
        base.Open();
    }

    /// <summary>
    /// 打开动画及层次调整
    /// </summary>
    public void OpenWindow()
    {
        base.Open();
        transform.SetAsLastSibling();
        BackGround.DOLocalMoveX(0, .3f);
    }

    public override void Close()
    {
        Tweener tweener = BackGround.DOLocalMoveX(1920, .3f);
        tweener.OnComplete(Complete);
    }

    private void Complete()
    {
        base.Close();
    }
}
