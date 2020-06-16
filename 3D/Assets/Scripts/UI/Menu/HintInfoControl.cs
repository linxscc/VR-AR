/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PureMVCDemo
{
    /// <summary>
    /// 提示信息控制
    /// </summary>
    public class HintInfoControl
    {
        /// <summary>
        /// UI根目录文件
        /// </summary>
        private GameObject canvas;
        /// <summary>
        /// 菜单
        /// </summary>
        private GameObject menu;
        private HintInfo hintInfo;
        /// <summary>
        /// 初始化
        /// </summary>
        public HintInfoControl()
        {
            canvas = GameObject.FindGameObjectWithTag(Tag.mainUI);
            menu = Resources.Load<GameObject>(Global.hintInfoUrl);
            menu = GameObject.Instantiate(menu);
            menu.transform.parent = canvas.transform;
            hintInfo = menu.GetComponent<HintInfo>();
            menu.transform.localPosition = Vector3.zero;
            menu.transform.localScale = new Vector3(.1f, .1f, .1f);

        }
        private static HintInfoControl singleton;
        public static HintInfoControl Singleton
        {
            get
            {
                if (singleton == null)
                    singleton = new HintInfoControl();
                return singleton;
            }
            set
            {
                singleton = value;
            }
        }
        /// <summary>
        /// 开启提示
        /// </summary>
        /// <param name="txt">显示的文字</param>
        /// <param name="x">位置x</param>
        /// <param name="y">位置y</param>
        /// <param name="z">位置z</param>
        public void Open(string txt,float x=0,float y=0,float z=0)
        {
            hintInfo.Open(txt,new Vector3(x,y,z-0.1f));
        }
        /// <summary>
        /// 关闭提示
        /// </summary>
        public void Close()
        {
            hintInfo.Close();
        }
    }
}
