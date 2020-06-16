﻿/*
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
using vPlace_FW;
using PlaceAR.LabelDatas;
using System;
using UnityEngine.UI;
using PlaceAR;
using UnityEngine.EventSystems;

namespace vPlace_zpc
{
    public class BrowserTypeItem : MonoBehaviour
    {
        /// <summary>
        /// 子模型数据
        /// </summary>
        public PrefabChildControl labelData;
        
        public EventHandler onClick;
        /// <summary>
        ///所属子模型
        /// </summary>
        public List<BrowserTypeItem> labelChild = new List<BrowserTypeItem>();
        /// <summary>
        /// 模型名称
        /// </summary>
        public Text modelNameText = null;
        /// <summary>
        /// UI布局组件
        /// </summary>
        private LayoutElement layoutElement = null;

        /// <summary>
        /// 根据字数改变按钮大小
        /// </summary>
        private void Start()
        {
            layoutElement = GetComponent<LayoutElement>();

            if (modelNameText == null)
                modelNameText = GetComponentInChildren<Text>();

            layoutElement.minWidth = modelNameText.text.Length * modelNameText.fontSize;

            GetComponent<RectTransform>().sizeDelta = new Vector2(modelNameText.text.Length * modelNameText.fontSize, modelNameText.fontSize);
        }

        public void OnInit(PrefabChildControl labelData)
        {
            this.labelData = labelData;
            switch (ProjectConstDefine.labelDataList.controlType)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    {
                        //transform.Find("Title/ItemButton").GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
                        //EventTriggerListener.Get(transform.Find("Title/ItemButton").gameObject).onClick = OnClickBtn;
                        transform.Find("Title/ItemButton").GetComponent<Toggle>().onValueChanged.AddListener((value) => OnClickBtn(value));
                    }
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }

        public void OnClickBtn(bool value)
        {
            LabelUIEventArgs e = new LabelUIEventArgs() { Direction = labelData.LocalPosition, label3D = labelData };
            if (onClick != null)
                onClick(this, e);
        }

        public void OnClickBtn(GameObject obj)
        {
            LabelUIEventArgs e = new LabelUIEventArgs() { Direction = labelData.LocalPosition, label3D = labelData };
            if (onClick != null)
                onClick(this, e);
        }
    }
}