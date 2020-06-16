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
//            佛祖保佑       无BUG        不修改                   //
////////////////////////////////////////////////////////////////////
/*
		// Copyright (C) 
 
        // 文件名：ModelChildSelect.cs
        // 文件功能描述：控制浏览功能子模型按钮的背景框显示或隐藏

		// 作者: 
        // 创建标识：

        // 修改标识：
        // 修改描述：
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace vPlace_zpc
{
    public class ModelChildSelect : MonoBehaviour
    {

        public Label2DUIController uicontroller = null;

        private GameObject border = null;

        public void OnInit()
        {
            border = transform.Find("Border").gameObject;
            if (GetComponent<Button>() != null)
                GetComponent<Button>().onClick.AddListener(Show);
            else
                GetComponent<Toggle>().onValueChanged.AddListener(Show);
            border.SetActive(false);
        }


        public void Hide()
        {
            if (!border)
                border = transform.Find("Border").gameObject;
            border.SetActive(false);
        }

        public void Show()
        {
            //browserTypeItem.Hide();
            uicontroller.ClearModelChildSelectBtn();
            if (!border)
                border = transform.Find("Border").gameObject;
            border.SetActive(true);
        }

        public void Show(bool b)
        {
            uicontroller.ClearModelChildSelectBtn();
            border.SetActive(true);
        }
    }
}
