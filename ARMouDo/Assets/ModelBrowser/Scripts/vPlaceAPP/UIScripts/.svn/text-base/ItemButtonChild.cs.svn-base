/*
		// Copyright (C) 
 
        // 文件名：ItemButtonChild.cs
        // 文件功能描述：控制子模型分组的显隐

		// 作者: 
        // 创建标识：

        // 修改标识：
        // 修改描述：
*/
using PlaceAR;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using vPlace_FW;
using vPlace_zpc;

public class ItemButtonChild : MonoBehaviour
{
    public GameObject itemChild = null;
    private bool isShowItemChild = true;

    public void ShowOrHIdeItemChild()
    {
        if (isShowItemChild && itemChild.transform.childCount > 0)
        {
            itemChild.SetActive(true);
        }
        else
        {
            itemChild.SetActive(false);
        }
        isShowItemChild = !isShowItemChild;
    }

}
