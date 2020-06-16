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
 
        // 文件名：ModelIntroWindow.cs
        // 文件功能描述：模型简介页面内容实时更新，打开即关闭

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

public class ModelIntroWindow : Window<ModelIntroWindow>
{
    #region 重写父类方法
    public override int ID
    {
        get
        {
            return -3;
        }
    }

    public override string Name
    {
        get
        {
            return "ModelIntroWindow";
        }
    }

    public override string Path
    {
        get
        {
            return "ModelIntroWindow";
        }
    }

    public override UIType CurrentUIType
    {
        get
        {
            currentUIType.uiFormType = UIFormType.Fixed;
            currentUIType.uiFormShowMode = UIFormShowMode.Normal;
            currentUIType.uiFormLucencyType = UIFormLucencyType.ImPenetrable;
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
    /// 模型名称
    /// </summary>
    public Text modelName = null;
    /// <summary>
    /// 模型简介
    /// </summary>
    public Text modelIntro = null;
    /// <summary>
    /// 返回按钮
    /// </summary>
    public Button btn_Return = null;
    /// <summary>
    /// 简介窗口DoTween移动数值
    /// </summary>
    private float moveX = 340;

    public override void Awake()
    {
        base.Awake();
        btn_Return.onClick.AddListener(Close);

        BackGround.localPosition = new Vector3(-moveX, BackGround.localPosition.y, BackGround.localPosition.z);
    }

    /// <summary>
    /// 模型相关简介
    /// </summary>
    /// <param name="selectedModelName">模型名称</param>
    /// <param name="selectedModelDescription">模型简介</param>
    public void ModelInfoUpdate(string selectedModelName, string selectedModelDescription)
    {
        modelName.text = selectedModelName;
        modelIntro.text = selectedModelDescription;
    }

    public override void Open()
    {
        base.Open();
        BackGround.DOLocalMoveX(0, .3f);
    }

    public override void Close()
    {
        Tweener tweener = BackGround.DOLocalMoveX(-moveX, .3f);
        tweener.OnComplete(Complete);
    }

    private void Complete()
    {
        base.Close();
    }

}
