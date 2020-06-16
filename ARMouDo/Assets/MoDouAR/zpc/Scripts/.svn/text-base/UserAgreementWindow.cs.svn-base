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
 
        // 文件名：UserAgreementWindow.cs
        // 文件功能描述：用户协议界面打开与关闭

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

public class UserAgreementWindow : Window<UserAgreementWindow>
{
    #region 重写父类方法
    public override int ID
    {
        get
        {
            return -6;
        }
    }

    public override string Name
    {
        get
        {
            return "UserAgreementWindow";
        }
    }

    public override string Path
    {
        get
        {
            return "UserAgreementWindow";
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
                backGround = WndObject.transform.Find("BackGround");
            return backGround;
        }
    }

    /// <summary>
    /// 返回按钮
    /// </summary>
    public Button btn_Return = null;

    public override void Awake()
    {
        base.Awake();
        btn_Return.onClick.AddListener(Close);

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
    /// 关闭回调
    /// </summary>
    private void Complete()
    {
        base.Close();
    }
}
