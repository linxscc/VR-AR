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
 
        // 文件名：AboutAppWindow.cs
        // 文件功能描述：关于APP内容UI界面的打开与关闭

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
using DG.Tweening;
using UnityEngine.UI;

public class AboutAppWindow : Window<AboutAppWindow>
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
            return "AboutAppWindow";
        }
    }

    public override string Path
    {
        get
        {
            return "AboutAppWindow";
        }
    }

    public override UIType CurrentUIType
    {
        get
        {
            currentUIType.uiFormType = UIFormType.PopUp;
            currentUIType.uiFormShowMode = UIFormShowMode.ReverseChange;
            currentUIType.uiFormLucencyType = UIFormLucencyType.Lucency;
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
                backGround = mWndObject.transform.Find("BackGround");
            return backGround;
        }
    }

    /// <summary>
    /// 打开用户协议界面按钮
    /// </summary>
    public GameObject UserAgreement = null;
    /// <summary>
    /// 退出返回按钮
    /// </summary>
    public Button btn_Return = null;

    public override void Awake()
    {
        base.Awake();
        btn_Return.onClick.AddListener(Close);
        EventTriggerListener.Get(UserAgreement).onClick = OpenUserAgreementWindow;

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
    /// 关闭动画
    /// </summary>
    private void Complete()
    {
        base.Close();
    }

    /// <summary>
    /// 打开用户协议界面
    /// </summary>
    /// <param name="obj"></param>
    private void OpenUserAgreementWindow(GameObject obj)
    {
        UserAgreementWindow.Instance.CreatWindow();
        UserAgreementWindow.Instance.Open();
    }
}
