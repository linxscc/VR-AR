/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Apple.ReplayKit;


namespace MoDouAR
{
    public class TakePhoto : Window<TakePhoto>
    {
        public override int ID
        {
            get
            {
                return 5;
            }
        }

        public override string Name
        {
            get
            {
                return "TakePhoto";
            }
        }

        public override string Path
        {
            get
            {
                return "UI/TakePhoto";
            }
        }
        [SerializeField]
        private Transform photoVideo;
        [SerializeField]
        private Transform video;
        [SerializeField]
        private Transform photo;
        [SerializeField]
        private Transform back;
        [SerializeField]
        private Transform pause;
        [SerializeField]
        private Transform showPhoto;
        [SerializeField]
        private Image picture;
        //public Dictionary<WindowsBase, bool> menu = new Dictionary<WindowsBase, bool>();
        /// <summary>
        /// 截图
        /// </summary>
        public void Photo()
        {
            photoVideo.gameObject.SetActive(false);

            back.gameObject.SetActive(false);

            TitleMenu.Instance.Close();
            StartCoroutine(Screenshot(new Rect(0, 0, Screen.width, Screen.height)));
        }
        private IEnumerator Screenshot(Rect rect)
        {
            ARKitControl.Instance.frame.gameObject.SetActive(false);
            yield return new WaitForEndOfFrame();

            Texture2D texture = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
            texture.ReadPixels(rect, 0, 0);
            texture.Apply();
            ScreenShotCacheData.lastTexture = texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            picture.gameObject.SetActive(true);
            picture.sprite = sprite;
            showPhoto.gameObject.SetActive(true);
            ARKitControl.Instance.frame.gameObject.SetActive(true);
        }
        /// <summary>
        /// 开始录像
        /// </summary>
        public void BeginVideo()
        {
            //判断是否支持录屏功能
            if (ReplayKit.APIAvailable)
            {
                try
                {
                    ReplayKit.Discard();

                    photoVideo.gameObject.SetActive(false);
                    back.gameObject.SetActive(false);

                    //pause.gameObject.SetActive(true);
                    TitleMenu.Instance.Close();
                    //    ISN_ReplayKit.Instance.StartRecording();
                    ReplayKit.StartRecording(true);
                    TIOSNative.CreateIOSButton(StopVideo);
                }
                catch (Exception e)
                {
                    IOSMessage.Create("提示", "初始化失败,请重试", "好的");
                }
            }
            else
                IOSMessage.Create("提示", "设备不支持该功能", "好的");
        }

        /// <summary>
        /// 停止录像
        /// </summary>
        public void StopVideo(string n)
        {
            // pause.gameObject.SetActive(false);
            //   ISN_ReplayKit.Instance.StopRecording();
            //    ISN_ReplayKit.Instance.ShowVideoShareDialog();
            ReplayKit.StopRecording();
            ReplayKit.Preview();

            showPhoto.gameObject.SetActive(true);
            //   ReturnPhoto();
            //  ISN_ReplayKit.Instance.DiscardRecording();
        }
        /// <summary>
        /// 保存到app
        /// </summary>
        public void Save()
        {
            Singleton<ScreenShotFun>.Instance.ScreenShotSave(delegate { Debug.Log("保存成功!"); }, delegate { Debug.Log("保存失败!"); });
            ReturnPhoto();
        }
        /// <summary>
        /// 保存到相机
        /// </summary>
        public void SaveAs()
        {
            IOSCamera.OnImageSaved += delegate { IOSCamera.OnImageSaved -= delegate { }; };
            IOSCamera.Instance.SaveTextureToCameraRoll(picture.sprite.texture);
            ReturnPhoto();
        }
        /// <summary>
        /// 删除
        /// </summary>
        public void Delete()
        {
            ReturnPhoto();
        }
        public void Back()
        {
            BottomMenu.Instance.Open();

            Close();
        }
        private void ReturnPhoto()
        {
            showPhoto.gameObject.SetActive(false);
            photoVideo.gameObject.SetActive(true);
            back.gameObject.SetActive(true);
            picture.gameObject.SetActive(false);
            TitleMenu.Instance.Open();
        }
        /// <summary>
        /// 选择拍照
        /// </summary>
        public void OpenPhoto()
        {
            picture.gameObject.SetActive(false);
            photoVideo.DOLocalMoveX(0, 0.2f);
            photo.GetComponent<Image>().DOColor(new Color(1, 1, 1, 1f), 0.2f);
            video.GetComponent<Button>().interactable = false;
            Tweener d = video.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0f), 0.2f);
            d.OnComplete(delegate { photo.GetComponent<Button>().interactable = true; });
            video.GetComponentInChildren<Text>().color = new Color(1, 1, 1, 0.5f);
            photo.GetComponentInChildren<Text>().color = new Color(1, 1, 1, 1f);
        }
        /// <summary>
        /// 选择录像
        /// </summary>
        public void OpenVideo()
        {
            photoVideo.DOLocalMoveX(-120, 0.2f);
            photo.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0f), 0.2f);
            photo.GetComponent<Button>().interactable = false;
            Tweener d = video.GetComponent<Image>().DOColor(new Color(1, 1, 1, 1f), 0.2f);
            d.OnComplete(delegate { video.GetComponent<Button>().interactable = true; });
            photo.GetComponentInChildren<Text>().color = new Color(1, 1, 1, 0.5f);
            video.GetComponentInChildren<Text>().color = new Color(1, 1, 1, 1f);
        }

        public override void Open()
        {
            base.Open();
            BottomMenu.Instance.Close();
            // menu.Add(BottomMenu.Instance, false);
            // BottomMenu_ModelWindow.Instance.CreatWindow();
            // if (BottomMenu_ModelWindow.Instance.IsOpen)
            // {
            // if (!menu.ContainsKey(BottomMenu_ModelWindow.Instance))
            // menu.Add(BottomMenu_ModelWindow.Instance, false);
            //}
            // BottomMenu_ModelWindow.Instance.Close();
            OpenPhoto();
        }
        public override void Close()
        {
            //BottomMenu.Instance.Open();
            // menu.Clear();
            base.Close();

        }
    }
}