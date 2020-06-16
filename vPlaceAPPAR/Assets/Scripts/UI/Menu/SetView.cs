/*
 *    日期:2017,7,26
 *    作者:
 *    标题:
 *    功能:设置菜单
 *   
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UI_XYRF;
using Tools_XYRF;

namespace PlaceAR
{
    public class SetView : MonoBehaviour
    {
        /// <summary>
        /// 网络下载设置
        /// </summary>
        [SerializeField]
        private Transform switchOn;
        /// <summary>
        /// 关于我们
        /// </summary>
        [SerializeField]
        private Transform aboutUs;
        /// <summary>
        /// 退出登录
        /// </summary>
        public Transform LoginBtn;

        private void Start()
        {
            switchOn.gameObject.SetActive(false);
            aboutUs.gameObject.SetActive(false);
        }
        public void Close()
        {
            transform.localScale = Vector3.zero;
        }
        /// <summary>
        /// 打开关于我们
        /// </summary>
        public void OpenAboutus()
        {
            aboutUs.gameObject.SetActive(true);
        }
        /// <summary>
        /// 关闭关于我们
        /// </summary>
        public void CloseAboutus()
        {
            aboutUs.gameObject.SetActive(false);
        }
        /// <summary>
        /// 网络下载设置
        /// </summary>
        public void NetSwitchOff()
        {

            if (Global.Atwifi)
                switchOn.gameObject.SetActive(false);
            else
                switchOn.gameObject.SetActive(true);
            Global.Atwifi = !Global.Atwifi;
            //    if (Application.internetReachability == NetworkReachability.NotReachable)
            // NetWorkTxt.text = "当前网络：不可用";
            // else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            // NetWorkTxt.text = "当前网络：3G/4G";
            //  else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            //  NetWorkTxt.text = "当前网络 : WIFI";

        }

        /// <summary>
        /// 我要吐槽
        /// </summary>
        public void RotateApp()
        {
            IOSNativeUtility.RedirectToAppStoreRatingPage(Global.AppleId);
        }

        /// <summary>
        ///  登录 / 退出登录
        /// </summary>
        public void QuitLogon()
        {
            if (Global.userLogonState)  //退出登录
            {
                LoginBtn.GetChild(0).GetComponent<Text>().text = "立即登录";
                Sprite spr_ = ResourcesLod_T.ResourcesLoad_SPR("ARTexture", "sidebar-btn-avatar-0@3x");
                FileTools.Delete(Global.LocalUrl, Global.SendTokenInfoName);   //删除本地信息
                Global.SendTokenInfo = null;
                Global.userLogonState = false;
                LogonApplication.Instance.loginBtnHead.sprite = spr_;
                LogonApplication.Instance.userAccount.text = "立即登录";
            }
            else  // 立即登录
            {
                SideBarView sideBarView = transform.GetComponentInParent<SideBarView>();
                sideBarView.OpenLoginView();   //打开登录菜单
                sideBarView.CloseSetView();     //关闭设置菜单

            }
        }
        void OnDisable()
        {
            SetViewControl.Singleton = null;
        }
        /// <summary>
        /// 打开用户协议
        /// </summary>
        public void OpenUserAgreement()
        {
            UserAgreementViewControl.Singleton.Open();
        }

        /// <summary>
        /// 关闭用户协议
        /// </summary>
        public void CloseUserAgreement()
        {
            UserAgreementViewControl.Singleton.Close();
        }
    }
}
