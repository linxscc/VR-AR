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
using PlaceAR;
using DG.Tweening;

public class UserAgreementView : MonoBehaviour
{

    public void Open(Transform trans)
    {
        if (trans == null)
            transform.SetParent(GameObject.FindGameObjectWithTag(Tag.mainUICanvas).transform);
        else
            transform.SetParent(trans);
        transform.GetComponent<RectTransform>().sizeDelta = Vector2.one;
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        transform.localScale = Vector3.one;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        //transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

}
