using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;


namespace MoDouAR
{
    public class PanelSetupEntrance : Window<PanelSetupEntrance>
    {

        #region  继承方法
        public override int ID
        {
            get
            {
                return 0;
            }
        }

        public override string Name
        {
            get
            {
                return "PanelSetupEntrance";
            }
        }

        public override string Path
        {
            get
            {
                return "UItcw/PanelSetupEntrance";
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
        /// 区域页面
        /// </summary>
        public GameObject[] traPage;

        /// <summary>
        /// 相册面板
        /// </summary>
        public Transform panelPhotoAndVideo;
        /// <summary>
        /// 照片:文本
        /// </summary>
        public Transform textPhoto;

        /// <summary>
        /// Grid
        /// </summary>
        public Transform gridSwitch;
        /// <summary>
        /// PanelGrid
        /// </summary>
        public Transform panelGrid;


        /// <summary>
        /// 相册页面页码
        /// </summary>
        private int 页码;
        /// <summary>
        /// 相册Grid面板 0页码位置
        /// </summary>
        private Vector3 gridSwitchPos0;

        public override void OnEnable()
        {
            base.OnEnable();
            ComeBack.OnEnablGridChange += PanelGrid_OnEnablGridChange;
            ComeBack.OnButtonPhotoOrVideo += ComeBack_OnButtonPhotoOrVideo;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            ComeBack.OnEnablGridChange -= PanelGrid_OnEnablGridChange;
            ComeBack.OnButtonPhotoOrVideo -= ComeBack_OnButtonPhotoOrVideo;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            ComeBack.OnEnablGridChange -= PanelGrid_OnEnablGridChange;
            ComeBack.OnButtonPhotoOrVideo -= ComeBack_OnButtonPhotoOrVideo;
        }

        public override void Start()
        {
            base.Start();
            button[0].onClick.AddListener(ButtonComeback);
            button[1].onClick.AddListener(ButtonSetup);
            button[2].onClick.AddListener(ButtonSwitch);
            button[3].onClick.AddListener(ButtonPhoto);
            button[4].onClick.AddListener(ButtonVideo);

        }
        public override void Open()
        {
            base.Open();
            StartCoroutine(PanelSetupEntranceInitData());
        }

        /// <summary>
        /// 数据刷新-调用(每次都得调用)
        /// </summary>
        public IEnumerator PanelSetupEntranceInitData()
        {
            transform.localPosition = new Vector3(-Screen.width, 0, 0);
            float yPos = -1;
            switch (Screen.orientation)
            {
                case ScreenOrientation.Portrait:
                    yPos = -60f;
                    break;
                case ScreenOrientation.PortraitUpsideDown:
                    yPos = -60f;
                    break;
                case ScreenOrientation.LandscapeLeft:
                    yPos = -352f;
                    break;
                case ScreenOrientation.LandscapeRight:
                    yPos = -352f;
                    break;
            }
            panelGrid.localPosition = new Vector3(panelGrid.localPosition.x, yPos, 0);
            panelPhotoAndVideo.GetComponent<ScrollRect>().enabled = false;
            gridSwitchPos0 = gridSwitch.localPosition;
            transform.DOLocalMove(Vector3.zero, 0.3f);
            yield return new WaitUntil(() => (transform.localPosition - Vector3.zero).magnitude <= 1f);
            //检测是否登录了账号
            DecitionToken();
            //相册刷新
            RefreshPhotos();
        }
        public void DecitionToken()
        {
            Singleton<LogonServerCodeFun>.Instance.UserTokenCodeing(
                delegate
                {
                    //已登录
                    traPage[0].SetActive(true);
                    traPage[1].SetActive(false);
                    traPage[2].transform.GetComponent<Text>().text = LogonCacheData.serverReturnData.user.mobile;
                    LogonCacheData.logonState = true;
                },
                delegate
                {
                    //未登录
                    traPage[0].SetActive(false);
                    traPage[1].SetActive(true);
                    LogonCacheData.logonState = false;
                });
        }

        /// <summary>
        /// 按钮 视频Grid
        /// </summary>
        private void ButtonVideo()
        {

        }

        /// <summary>
        /// 按钮 相册Grid
        /// </summary>
        private void ButtonPhoto()
        {
        }

        /// <summary>
        /// 切换账号-立即登录
        /// </summary>
        public void ButtonSwitch()
        {
            LogonApp.Instance.CreatWindow();
            LogonApp.Instance.Open();
        }

        /// <summary>
        /// 打开设置
        /// </summary>
        private void ButtonSetup()
        {
            SetViewWindow.Instance.CreatWindow();
            SetViewWindow.Instance.Open();
        }

        /// <summary>
        /// 返回上一级页面
        /// </summary>
        private void ButtonComeback()
        {
            if (页码 == 0)
            {
                TCoroutine.Instance.TStopAllCoroutine();
                Vector3 endPos = new Vector3(transform.localPosition.x - Screen.width, transform.localPosition.y, 0);
                transform.DOLocalMove(endPos, 0.3f).OnKill(
                    delegate
                    {
                        Close();
                    });
            }
            else if (页码 == 1)
            {
                //相册→初始位置
                StartCoroutine(gridSwitch_Init());
            }
        }
        private IEnumerator gridSwitch_Init()
        {
            gridSwitch.DOLocalMove(new Vector3(gridSwitchPos0.x, gridSwitchPos0.y, 0), 0.3f);
            yield return new WaitUntil(() => Math.Abs(Math.Abs(gridSwitch.localPosition.y) - Math.Abs(gridSwitchPos0.y)) <= 1f);
            PanelGrid.Instance.ChangePage(0);
            StopCoroutine(gridSwitch_Init());
        }


        /// <summary>
        /// 刷新相册 Grid
        /// </summary>
        public void RefreshPhotos()
        {
            Singleton<ScreenShotFun>.Instance.RefreshPhoto(gridSwitch, OnSuccessLoadScreenShotPhoto, OnLostLoadScreenShotPhoto);
        }


        /// <summary>
        /// 刷新视频 Grid
        /// </summary>
        private void RefreshVideos()
        {

        }

        /// <summary>
        /// 退出登录
        /// </summary>
        public void QuteLogon()
        {
            if (FileFolders.FileExist(LogonCacheData.GetScreenShotConfigPath, LogonCacheData.GetUserConfig))
                FileFolders.Delete(LogonCacheData.GetScreenShotConfigPath, LogonCacheData.GetUserConfig);
            traPage[0].SetActive(false);
            traPage[1].SetActive(true);
            LogonCacheData.logonState = false;
        }

        #region  回调...

        /// <summary>
        /// 刷新相册 成功
        /// </summary>
        private void OnSuccessLoadScreenShotPhoto()
        {
            Debug.Log("刷新相册 成功!");
        }
        /// <summary>
        /// 刷新相册 失败
        /// </summary>
        private void OnLostLoadScreenShotPhoto(string error)
        {
            Debug.Log("刷新相册 失败:" + error);
        }

        /// <summary>
        /// 图片点击
        /// </summary>
        /// <param name="obj">对象</param>
        private void ComeBack_OnButtonPhotoOrVideo(PanelShowPhotoOrVideoResult obj)
        {
            // Debug.Log(obj.GetButtonGameObject.name);
            ButtonPhotoOrVideo button = obj.GetButtonGameObject.GetComponent<ButtonPhotoOrVideo>();
            StartCoroutine(OnButtonPhotoOrVideo(button));
        }
        private IEnumerator OnButtonPhotoOrVideo(ButtonPhotoOrVideo button)
        {
            Texture2D te = ScreenShotCacheData.shotCachTexture[button.textureId];
            if (!ScreenShotCacheData.shotCachSprite.ContainsKey(button.textureId))
            {
                ScreenShotCacheData.shotCachSprite.Add(button.textureId, FileTexture.SpriteFromTex2D(te));
                yield return new WaitUntil(() => ScreenShotCacheData.shotCachSprite.ContainsKey(button.textureId));
            }
            PanelShowPhoto.Instance.CreatWindow();
            PanelShowPhoto.Instance.Open();
            PanelShowPhoto panelShowPhoto = PanelShowPhoto.Instance;
            panelShowPhoto.imagePhoto.sprite = ScreenShotCacheData.shotCachSprite[button.textureId];
            panelShowPhoto.beginPos = button.transform.position;
            panelShowPhoto.buttonPhoto = button.gameObject;

            //横/竖屏 适配Show图片

            var rt = panelShowPhoto.imagePhoto.transform.GetComponent<RectTransform>();
            float width_ = 750f;
            float height_ = 1334f;
            if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
            {
                if (te.height < te.width)
                    height_ = 584f;
            }
            else if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
            {
                if (te.height >= te.width)
                {
                    width_ = 584f;
                    height_ = 750f;
                }
                else
                {
                    width_ = 1334f;
                    height_ = 750f;
                }
            }
            rt.DOSizeDelta(new Vector2(width_, height_), 0.3f);

            panelShowPhoto.imagePhoto.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
            panelShowPhoto.imagePhoto.transform.position = button.transform.position;
            for (int i = 0; i < panelShowPhoto.button.Length; i++)
            {
                panelShowPhoto.button[i].gameObject.SetActive(false);
            }
            Tweener t = panelShowPhoto.imagePhoto.transform.DOScale(new Vector3(1.03f, 1.03f, 1.03f), 0.3f);
            t.OnComplete(delegate
            {
                //结束时
                panelShowPhoto.imagePhoto.transform.DOScale(Vector3.one, 0.5f);
                for (int i = 0; i < panelShowPhoto.button.Length; i++)
                {
                    panelShowPhoto.button[i].gameObject.SetActive(true);
                }
            });

            Vector3 endPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            panelShowPhoto.imagePhoto.transform.DOMove(endPos, 0.3f);
            panelShowPhoto.imagePhoto.color = new Color(255, 255, 255, 0.1f);
            panelShowPhoto.imagePhoto.DOColor(new Color(255, 255, 255, 1), 0.3f);
        }


        /// <summary>
        /// 滑动回调
        /// </summary>
        /// <param name="obj"></param>
        private void PanelGrid_OnEnablGridChange(PanelGridResult obj)
        {
            页码 = obj.GetPageNumber;
            switch (obj.GetTonStart)
            {
                case TonStart.Start:
                    if (页码 == 1)
                    {
                        panelPhotoAndVideo.GetComponent<ScrollRect>().enabled = true;
                        button[0].transform.DOLocalRotate(Vector3.zero, 0.1f);
                        textPhoto.gameObject.SetActive(true);
                        button[1].gameObject.SetActive(false);
                    }
                    if (页码 == 0)
                    {
                        panelPhotoAndVideo.GetComponent<ScrollRect>().enabled = false;
                        button[0].transform.DOLocalRotate(new Vector3(0, 0, -90), 0.1f);
                        textPhoto.gameObject.SetActive(false);
                        button[1].gameObject.SetActive(true);
                    }
                    break;
                case TonStart.Centering:
                    break;
                case TonStart.End:
                    break;
            }
        }

        #endregion  //...

    }
}
