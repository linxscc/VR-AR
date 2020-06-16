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
 
        // 文件名：ChangeBtnSize.cs
        // 文件功能描述：改变浏览功能UI的模型与子模型的按钮大小，与字体相适应

		// 作者: zpc
        // 创建标识：

        // 修改标识：
        // 修改描述：
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 根据字体数量改变UI大小
/// </summary>
public class ChangeBtnSize : MonoBehaviour 
{
    private Text wordText = null;
    private LayoutElement layoutElement = null;
    private void Start()
    {
        layoutElement = GetComponent<LayoutElement>();
        wordText = GetComponentInChildren<Text>();
        layoutElement.minWidth = wordText.text.Length * wordText.fontSize;
        GetComponent<RectTransform>().sizeDelta = new Vector2(wordText.text.Length * wordText.fontSize, wordText.fontSize);
    }
}
