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
 
        // 文件名：SetViewWindow.cs
        // 文件功能描述：APP设置界面相关功能及打开与关闭

		// 作者: zpc
        // 创建标识：

        // 修改标识：
        // 修改描述：
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoDouAR;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class SetViewWindow : Window<SetViewWindow>
{
    #region 重写父类方法
    public override int ID
    {
        get
        {
            return -4;
        }
    }

    public override string Name
    {
        get
        {
            return "SetViewWindow";
        }
    }

    public override string Path
    {
        get
        {
            return "SetViewWindow";
        }
    }

    public override UIType CurrentUIType
    {
        get
        {
            currentUIType.uiFormType = UIFormType.PopUp;
            currentUIType.uiFormShowMode = UIFormShowMode.ReverseChange;
            currentUIType.uiFormLucencyType = UIFormLucencyType.Lucency;//
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
    public Button btn_Return = null;
    /// <summary>
    /// 网络开关按钮
    /// </summary>
    public GameObject btn_NetWork = null;
    /// <summary>
    /// 相机开关按钮
    /// </summary>
    public GameObject btn_Camera = null;
    /// <summary>
    /// 打开关于界面按钮
    /// </summary>
    public GameObject btn_AboutApp = null;
    /// <summary>
    /// 检查更新按钮
    /// </summary>
    public GameObject btn_CheckVersion = null;
    /// <summary>
    /// App评分按钮
    /// </summary>
    public GameObject btn_AppMark = null;
    public override void Awake()
    {
        base.Awake();
        btn_Return.onClick.AddListener(Close);
        EventTriggerListener.Get(btn_NetWork).onClick = Btn_NetWork;
        EventTriggerListener.Get(btn_Camera).onClick = Btn_Camera;
        EventTriggerListener.Get(btn_AboutApp).onClick = Btn_AboutApp;
        EventTriggerListener.Get(btn_CheckVersion).onClick = Btn_GoAppStore;
        EventTriggerListener.Get(btn_AppMark).onClick = Btn_GoAppStore;

        BackGround.localPosition = new Vector3(BackGround.localPosition.x, -Screen.height, BackGround.localPosition.z);
    }

    public override void Open()
    {
        base.Open();
        BackGround.DOLocalMoveY(0, .3f);
    }


    public override void Close()
    {
       
        Tweener tweener = BackGround.DOLocalMoveY(-Screen.height, .3f);
        tweener.OnComplete(Complete);
    }

    /// <summary>
    /// 关闭动画完成回调
    /// </summary>
    private void Complete()
    {
        base.Close();
       
    }
    /// <summary>
    /// 网络开关
    /// </summary>
    /// <param name="obj"></param>
    private void Btn_NetWork(GameObject obj)
    {
        GameObject switchObj = obj.transform.Find("Image_On").gameObject;

        if (Global.Atwifi)
            switchObj.SetActive(false);
        else
            switchObj.SetActive(true);
        Global.Atwifi = !Global.Atwifi;
    }

    /// <summary>
    /// 相机开关
    /// </summary>
    /// <param name="obj"></param>
    private void Btn_Camera(GameObject obj)
    {
        GameObject switchObj = obj.transform.Find("Image_On").gameObject;

        if (!switchObj.activeSelf)
            switchObj.SetActive(true);
        else
            switchObj.SetActive(false);
    }

    /// <summary>
    /// 打开关于App界面
    /// </summary>
    /// <param name="obj"></param>
    private void Btn_AboutApp(GameObject obj)
    {
        AboutAppWindow.Instance.CreatWindow();
        AboutAppWindow.Instance.Open();
    }

    /// <summary>
    /// 更新及评分
    /// </summary>
    /// <param name="obj"></param>
    private void Btn_GoAppStore(GameObject obj)
    {
        IOSNativeUtility.RedirectToAppStoreRatingPage(Global.AppleId);
    }
}
