using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace MoDouAR
{
    /// <summary>
    /// 自动匹配初始位置
    /// </summary>
    public class PanelShowPhoto : Window<PanelShowPhoto>
    {

        #region  继承方法
        public override int ID
        {
            get
            {
                return 1;
            }
        }

        public override string Name
        {
            get
            {
                return "PanelShowPhoto";
            }
        }

        public override string Path
        {
            get
            {
                return "UItcw/PanelShowPhoto";
            }
        }
        public override UIType CurrentUIType
        {
            get
            {
                currentUIType.uiFormType = UIFormType.PopUp;
                return base.CurrentUIType;
            }
            set
            {
                base.CurrentUIType = value;
            }
        }
        #endregion //结束...


        /// <summary>
        /// 功能按钮
        /// </summary>
        public Button[] button;
        /// <summary>
        /// 开始位置
        /// </summary>
        public Vector3 beginPos = Vector3.zero;
        /// <summary>
        /// Show图片面板
        /// </summary>
        public Image imagePhoto;
        /// <summary>
        /// 点击的图片对象
        /// </summary>
        public GameObject buttonPhoto;

        public override void Start()
        {
            base.Start();
            button[0].onClick.AddListener(ButtonComeback);
            button[1].onClick.AddListener(ButtonDelete);
            button[2].onClick.AddListener(ButtonSave);
            button[3].onClick.AddListener(ButtonShare);
        }

        /// <summary>
        /// 分享图片→社交平台
        /// </summary>
        private void ButtonShare()
        {
            IOSSocialManager.Instance.ShareMedia("发现一个好玩的App", imagePhoto.sprite.texture);
        }


        /// <summary>
        /// 保存图片→相册 [IOS:注意Xcode相机权限] 
        /// </summary>
        private void ButtonSave()
        {
#if UNITY_IPHONE

            IOSCamera.OnImageSaved += delegate { IOSCamera.OnImageSaved -= delegate { }; };
            IOSCamera.Instance.SaveTextureToCameraRoll(imagePhoto.sprite.texture);

#elif UNITY_ANDROID

//var imageTitle = "Screenshot-" + System.DateTime.Now.ToString("yy-MM-dd-hh-mm-ss") + ".png";
//const string folderName = "vPlaceAR";
//AGFileUtils.SaveImageToGallery(_lastTakenScreenshot, imageTitle, folderName, ImageFormat.PNG);
//AGUIMisc.ShowToast(" 截图成功");

#endif
        }


        /// <summary>
        /// 删除图片
        /// </summary>
        private void ButtonDelete()
        {
            int id = buttonPhoto.GetComponent<ButtonPhotoOrVideo>().textureId;
            //删除信息
            Singleton<ScreenShotFun>.Instance.DeletePhoto(id, TextureSuffixs.JPG, OnSuccessDeleteTexture, OnLostDeleteTexture);
        }


        /// <summary>
        /// 返回上一级页面
        /// </summary>
        public void ButtonComeback()
        {
            Vector3 endScale = new Vector3(0.05f, 0.05f, 0.05f);
            imagePhoto.transform.DOScale(endScale, 0.3f).OnComplete(CloseButtonComeback);
            imagePhoto.transform.DOMove(beginPos, 0.3f);
            imagePhoto.DOColor(new Color(255, 255, 255, 0), 0.3f);
            for (int i = 0; i < button.Length; i++)
            {
                button[i].gameObject.SetActive(false);
            }
        }
        private void CloseButtonComeback()
        {
            Close();
        }


        #region  回调...

        /// <summary>
        /// 图片信息删除 成功
        /// </summary>
        private void OnSuccessDeleteTexture()
        {
            //刷新相册
            Transform gridSwitch = Window<PanelSetupEntrance>.Instance.WndObject.
                    GetComponent<PanelSetupEntrance>().gridSwitch;

            ScreenShotCacheData.shotCachTexture.Clear();
            ScreenShotCacheData.shotCachSprite.Clear();
            Window<PanelSetupEntrance>.Instance.WndObject.GetComponent<PanelSetupEntrance>().RefreshPhotos();

            //关闭当前页面
            CloseButtonComeback();
            Debug.Log("图片信息 删除成功!");
        }
        /// <summary>
        /// 图片信息删除 失败
        /// </summary>
        private void OnLostDeleteTexture(string error)
        {
            Debug.Log("图片信息删除 失败:" + error);
        }

        #endregion  //...


    }
}
