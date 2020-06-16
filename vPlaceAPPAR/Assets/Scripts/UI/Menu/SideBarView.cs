/*
 *    日期:2017、7、/26
 *    作者:
 *    标题:
 *    功能:侧边功能菜单
*/
using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;

namespace PlaceAR
{
    public class SideBarView : MonoBehaviour
    {
        /// <summary>
        /// 关闭按钮
        /// </summary>
        [SerializeField]
        private Button closeButton;
        /// <summary>
        /// 主菜单
        /// </summary>
        [SerializeField]
        private Transform mainMenu;
        /// <summary>
        /// 登录菜单
        /// </summary>
        [SerializeField]
        private Transform loginView;
        /// <summary>
        /// 设置按钮
        /// </summary>
        [SerializeField]
        private Transform setMenu;
        /// <summary>
        /// 下载菜单
        /// </summary>
        [SerializeField]
        private Transform downLoadView;
        /// <summary>
        /// 相册
        /// </summary>
        [SerializeField]
        private Transform albumbg;
        private Dictionary<int, ItemChild> buttonIte;
        private float time = 0.3f;

        /// <summary>
        /// 关闭按钮
        /// </summary>
        public void CloseButton()
        {
            Close();

        }
        public void OnInit()
        {
            EventTriggerListener.Get(downLoadView.gameObject).onEnter += EnterButton;
            EventTriggerListener.Get(albumbg.gameObject).onEnter += EnterButton;
            EventTriggerListener.Get(setMenu.gameObject).onEnter += EnterButton;
            EventTriggerListener.Get(downLoadView.gameObject).onExit += OutButton;
            EventTriggerListener.Get(albumbg.gameObject).onExit += OutButton;
            EventTriggerListener.Get(setMenu.gameObject).onExit += OutButton;
            //    loginView.localScale = Vector3.zero;
            CloseDownLoadView();
            // CloseSetView();
            closeButton.onClick.AddListener(CloseButton);
            mainMenu.localPosition = new Vector3(930, 0, 0);

        }
        private void EnterButton(GameObject obj)
        {
            obj.transform.Find("SiderHglight").gameObject.SetActive(true);
        }
        private void OutButton(GameObject obj)
        {
            obj.transform.Find("SiderHglight").gameObject.SetActive(false);
        }
        /// <summary>
        /// 打开相册
        /// </summary>
        public void OpenAlbumBG()
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                IOSCamera.Instance.PickImage(ISN_ImageSource.Album);
            }
        }
        /// <summary>
        /// 打开设置菜单
        /// </summary>
        public void OpenSetView()
        {
            SetViewControl.Singleton.Open(transform);
        }
        /// <summary>
        /// 关闭设置菜单
        /// </summary>
        public void CloseSetView()
        {
            SetViewControl.Singleton.Close();
        }
        /// <summary>
        /// 打开下载菜单
        /// </summary>
        public void OpenDownLoadView()
        {
            DownLoadViewControl.Singleton.Open(buttonIte);
        }
        /// <summary>
        /// 关闭下载菜单
        /// </summary>
        public void CloseDownLoadView()
        {
            //print(11);
            if (DownLoadViewControl.Singleton != null)
                DownLoadViewControl.Singleton.Close();
        }

        /// <summary>
        /// 打开登录菜单
        /// </summary>
        public void OpenLoginView()
        {
            if (!Global.userLogonState)  //未登录时打开设置菜单
            {
                //    loginView.localScale = Vector3.zero;
                loginView.gameObject.SetActive(true);
                //   loginView.DOScale(Vector3.one, time);
                loginView.localScale = Vector3.one;
            }
        }
        /// <summary>
        /// 关闭登录菜单
        /// </summary>
        public void CloseLoginView()
        {
            //  loginView.DOScale(Vector3.zero, time);
            loginView.gameObject.SetActive(false);
        }
        public void Open(Dictionary<int, ItemChild> buttonIte)
        {
            this.buttonIte = buttonIte;
            closeButton.gameObject.SetActive(true);
            Tweener t = mainMenu.DOLocalMoveX(413, time);
        }
        public void Close()
        {
            Tweener t = mainMenu.DOLocalMoveX(930, time);
            t.OnComplete(CloseComplete);
        }
        private void CloseComplete()
        {
            closeButton.gameObject.SetActive(false);
        }
        void OnDisable()
        {
            SideBarViewControl.Singleton = null;
        }
    }
}
