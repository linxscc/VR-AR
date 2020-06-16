/*
		// Copyright (C) 
 
        // 文件名：
        // 文件功能描述：

		// 作者: 
        // 创建标识：

        // 修改标识：
        // 修改描述：
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlaceAR;

public class LabelUIEventArgs : EventArgs 
{
    public bool Used { set; get; }
    public Vector3 Direction { set; get; }
    public PrefabChildControl label3D { set; get; }
}
