/*
		// Copyright (C) 
 
        // 文件名：
        // 文件功能描述：

		// 作者: 
        // 创建标识：

        // 修改标识：
        // 修改描述：
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserAgreementViewControl
{
    public UserAgreementView userAgreementView = null;
    private GameObject userAgreementViewObj = null;
    private UserAgreementViewControl()
    {
        userAgreementViewObj = Resources.Load<GameObject>(Global.userAgreementView);
        userAgreementViewObj = GameObject.Instantiate(userAgreementViewObj);
        userAgreementView = userAgreementViewObj.GetComponent<UserAgreementView>();
    }
    private static UserAgreementViewControl singleton;
    public static UserAgreementViewControl Singleton
    {
        get
        {
            if (singleton == null)
                singleton = new UserAgreementViewControl();
            return singleton;
        }
    }

    public void Open(Transform trans = null)
    {
        userAgreementView.Open(trans);
    }

    public void Close()
    {
        userAgreementView.Close();
    }
}
