/*
 *    日期:2017,7,26
 *    作者:
 *    标题:
 *    功能:侧边功能
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlaceAR
{
    public class SideBarViewControl
    {

        public SideBarView sideBarView;
        private GameObject sideBarViewObj;
        private SideBarViewControl()
        {
            sideBarViewObj = Resources.Load<GameObject>(Global.sideBarView);
            sideBarViewObj = GameObject.Instantiate(sideBarViewObj);
            sideBarViewObj.transform.parent = GameObject.FindGameObjectWithTag(Tag.mainUICanvas).transform;
            sideBarViewObj.transform.localPosition = new Vector3(0, 0, 0);
            sideBarViewObj.transform.localScale = Vector3.one;
            sideBarView = sideBarViewObj.GetComponent<SideBarView>();
            sideBarView.OnInit();
        }
        private static SideBarViewControl singleton;
        public static SideBarViewControl Singleton
        {
            get
            {
                if (singleton == null)
                    singleton = new SideBarViewControl();
                return singleton;
            }
            set
            {
                singleton = value;
            }
        }
        /// <summary>
        /// 登录
        /// </summary>
        public void LogIn()
        {

        }

        /// <summary>
        /// 退出登录
        /// </summary>
        public void LogOut()
        {

        }
        /// <summary>
        /// 打开菜单
        /// </summary>
        public void Open(Dictionary<int, ItemChild> buttonIte)
        {
            sideBarView.Open(buttonIte);
        }
        /// <summary>
        /// 关闭菜单
        /// </summary>
        public void Close()
        {
            sideBarView.Close();
        }
    }
}
