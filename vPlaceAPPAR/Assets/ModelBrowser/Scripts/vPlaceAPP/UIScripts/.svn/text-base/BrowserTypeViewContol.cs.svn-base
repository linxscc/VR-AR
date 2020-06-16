/*
		// Copyright (C) 
 
        // 文件名：BrowserTypeViewContol.cs
        // 文件功能描述：控制模型浏览功能界面打开关闭等功能

		// 作者: 
        // 创建标识：

        // 修改标识：
        // 修改描述：
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vPlace_zpc
{
    public class BrowserTypeViewContol
    {
        /// <summary>
        /// 模型浏览实体
        /// </summary>
        public GameObject browserTypeViewObj = null;

        /// <summary>
        /// 模型浏览对象
        /// </summary>
        public BrowserTypeView browserTypeView = null;


        private BrowserTypeViewContol()
        {
            if (!browserTypeViewObj)
            {
                browserTypeViewObj = Resources.Load<GameObject>(ProjectConstDefine.MODELBROWSER_BROWSERTYPEVIEW);
                browserTypeViewObj = GameObject.Instantiate(browserTypeViewObj);
            }
            //browserTypeViewObj.SetActive(false);
            browserTypeView = browserTypeViewObj.GetComponent<BrowserTypeView>();
            browserTypeViewObj.transform.SetParent(GameObject.Find("StartCanvas").transform);
            browserTypeViewObj.transform.GetComponent<RectTransform>().sizeDelta = Vector2.one;
            browserTypeViewObj.transform.localPosition = Vector3.zero;
            browserTypeViewObj.transform.localEulerAngles = Vector3.zero;
            browserTypeViewObj.transform.localScale = new Vector3(1, 0, 1);
        }

        private static BrowserTypeViewContol instance = null;
        public static BrowserTypeViewContol Instance
        {
            get
            {
                if (instance == null)
                    instance = new BrowserTypeViewContol();
                return instance;
            }
        }

        public void Open()
        {
            browserTypeView.Open();
        }

        public void Close()
        {
            browserTypeView.Close();
        }
    }
}