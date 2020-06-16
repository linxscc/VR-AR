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
 
        // 文件名：BottomMenu_ModelWindow.cs
        // 文件功能描述：模型按钮操作的主要实现脚本

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
using System;
using UnityEngine.UI;
using vPlace_zpc;

public class BottomMenu_ModelWindow : Window<BottomMenu_ModelWindow>
{
    #region 重写父类方法
    public override int ID
    {
        get
        {
            return -1;
        }
    }

    public override string Name
    {
        get
        {
            return "BottomMenu_ModelWindow";
        }
    }

    public override string Path
    {
        get
        {
            return "BottomMenu_ModelWindow";
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
    /// 当前模型图标
    /// </summary>
    public GameObject curentModel = null;
    /// <summary>
    /// 当前模型简介按钮
    /// </summary>
    public Button btn_ModelIntro = null;
    /// <summary>
    /// 模型浏览功能按钮
    /// </summary>
    public Button btn_BrowserMode = null;
    /// <summary>
    /// 模型复位按钮
    /// </summary>
    public Button btn_ResetModel = null;
    /// <summary>
    /// 全屏按钮
    /// </summary>
    public Button btn_FullScreen = null;
    /// <summary>
    /// 退出全屏按钮
    /// </summary>
    public Button btn_ExitFullScreen = null;
    /// <summary>
    /// 顶部UI
    /// </summary>
    public GameObject topUI = null;
    /// <summary>
    /// 模型控制
    /// </summary>
    private ModelControl modelController = null;
    /// <summary>
    /// 是否拆解
    /// </summary>
    private bool isAssemble = false;
    /// <summary>
    /// 是否截面
    /// </summary>
    private bool isSection = true;
    /// <summary>
    /// 当前模型数据
    /// </summary>
    private ChildItemData curModelData = null;
    /// <summary>
    /// 模型简介界面是否隐藏
    /// </summary>
    private bool introWindowIsHide = false;
    public override void Awake()
    {
        base.Awake();

        if (modelController == null)
            modelController = FindObjectOfType<ModelControl>();

        curentModel.GetComponent<Button>().onClick.AddListener(OpenModelInfo);
        btn_ModelIntro.onClick.AddListener(OpenModelIntroWindow);
        btn_BrowserMode.onClick.AddListener(OpenBottomMenu_BrowserWindow);
        btn_ResetModel.onClick.AddListener(BackHome);
        btn_FullScreen.onClick.AddListener(FullScreen);
        btn_ExitFullScreen.onClick.AddListener(ExitFullScreen);

        BackGround.localPosition = new Vector3(BackGround.localPosition.x, -Screen.height / 2, BackGround.localPosition.z);
    }
    /// <summary>
    /// 打开模型库内的模型信息界面
    /// </summary>
    private void OpenModelInfo()
    {
        ModelInfo.Instance.CreatWindow();
        ModelInfo.Instance.Open(curModelData);
        ModelInfo.Instance.downButton.gameObject.SetActive(false);
        ModelInfo.Instance.downLoding.gameObject.SetActive(false);
    }

    /// <summary>
    /// 改变当前展示模型图片
    /// </summary>
    /// <param name="data">当前模型数据</param>
    public void ChangeTheCurModelImage(ChildItemData data)
    {
        curModelData = data;
        curentModel.GetComponent<Image>().sprite = data.sprite;
    }

    /// <summary>
    /// 打开模型简介界面
    /// </summary>
    private void OpenModelIntroWindow()
    {
        ModelIntroWindow.Instance.CreatWindow();
        ModelIntroWindow.Instance.Open();
    }

    /// <summary>
    /// 1.实现各模型的浏览功能
    /// 2.打开模型浏览功能界面
    /// </summary>
    private void OpenBottomMenu_BrowserWindow()
    {
        //BottomMenu_BrowserWindow.Instance.CreatWindow();
        BottomMenu_BrowserWindow.Instance.OpenWindow();

        OnAssembleDis();
    }

    /// <summary>
    /// 预设拆解/逐层/剖面等按钮
    /// </summary>
    public void OnAssembleDis()
    {
        switch (ProjectConstDefine.labelDataList.controlType)
        {
            case 0:
                if (isAssemble)
                    modelController.OnAssemble();
                else
                    modelController.OnDisassemble();
                break;
            case 1:
                modelController.Profile();
                break;
            case 2:
                modelController.Layer();
                break;
            case 6:
                Section();
                BottomMenu_BrowserWindow.Instance.Section();
                modelController.Section();
                break;
            case 7:
                if (isAssemble)
                    modelController.OnAssemble();
                else
                    modelController.OnDisassemble();
                break;
            default: break;
        }

        isAssemble = !isAssemble;
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
    }

    /// <summary>
    /// 模型复位
    /// </summary>
    public void BackHome()
    {
        modelController.BackHome();
        modelController.OnAssemble();
        isAssemble = false;
        //labelUIs.ClearModelChildSelectBtn();
        isSection = true;
    }

    /// <summary>
    /// 全屏
    /// </summary>
    private void FullScreen()
    {
        btn_FullScreen.gameObject.SetActive(false);
        btn_ExitFullScreen.gameObject.SetActive(true);

        //隐藏当前所有UI操作
        topUI.SetActive(false);
        BackGround.gameObject.SetActive(false);
        TitleMenu.Instance.Close();
        BottomMenu.Instance.Close(true);
        if (ModelIntroWindow.Instance.gameObject.activeSelf)
        {
            ModelIntroWindow.Instance.Close();
            introWindowIsHide = true;//记录简介界面是否被关闭
        }
    }

    /// <summary>
    /// 退出全屏
    /// </summary>
    private void ExitFullScreen()
    {
        btn_ExitFullScreen.gameObject.SetActive(false);
        btn_FullScreen.gameObject.SetActive(true);

        //显示当前所有UI操作
        topUI.SetActive(true);
        BackGround.gameObject.SetActive(true);
        TitleMenu.Instance.Open();
        BottomMenu.Instance.Open();
        if (introWindowIsHide)
        {
            ModelIntroWindow.Instance.Open();
            introWindowIsHide = false;
        }
    }

    public override void Open()
    {
        base.Open();
        float x = Screen.height;
        if (Screen.height == 1080)
            x = 750;
        if (Screen.height == 1920)
            x = 1334;
        Tweener tweener = BackGround.DOLocalMoveY(-x / 2 + 44, .3f);//44为UI高度的一半
        tweener.OnComplete(BtnSetting);
        SectionHideSomeChild();
        isAssemble = false;
    }

    /// <summary>
    /// 按钮交互设置
    /// </summary>
    private void BtnSetting()
    {
        BottomMenu_BrowserWindow.Instance.Close();

        if (ProjectConstDefine.labelDataList != null)
        {
            if (ProjectConstDefine.labelDataList.controlType != 4)
                btn_BrowserMode.gameObject.SetActive(true) ;
            else
                btn_BrowserMode.gameObject.SetActive(false);
        }

        //若当前没有选择模型，复位按钮无效
        if (modelController.model == null)
            btn_ResetModel.interactable = false;
        else
            btn_ResetModel.interactable = true;
    }

    /// <summary>
    /// 截面模式-隐藏特殊的子模型
    /// </summary>
    private void SectionHideSomeChild()
    {
        if (ProjectConstDefine.labelDataList != null && ProjectConstDefine.labelDataList.controlType == 6)
        {
            foreach (var child in modelController.modelChildList)
            {
                if (child.isHide)
                    child.gameObject.SetActive(false);
            }
        }
    }

    public override void Close()
    {
        Tweener tweener = BackGround.DOLocalMoveY(-Screen.height / 2, .3f);
        tweener.OnComplete(Complete);
    }

    private void Complete()
    {
        base.Close();
    }
}
