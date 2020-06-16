using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using ARKit_T;
using PlaceAR;

namespace UI_XYRF
{
    public class Panel_ShowImage : MonoBehaviour
    {
        public List<Button> button = new List<Button>();
        /// <summary>
        /// 渲染图片的Image
        /// </summary>
        private Image darawTexgture;
        /// <summary>
        /// 需要旋转的UI
        /// </summary>
        public List<GameObject> rotUI = new List<GameObject>();
        private void Awake()
        {
            transform.parent.GetComponent<StartCanvas>().rotaterUI.AddRange(rotUI);
            Button[] b = transform.GetComponentsInChildren<Button>();
            button.AddRange(b);
            foreach (var bu in button)
            {
                EventListener_UGUI_T.LoadEvent(bu.gameObject).onClick = OnButtonClick;
            }
            
        }
        private void OnDisable()
        {
            for (int i = 0; i < rotUI.Count; i++)
            {
                transform.parent.GetComponent<StartCanvas>().rotaterUI.Remove(rotUI[i]);
            }
        }
        private void OnButtonClick(GameObject go)
        {
            switch (go.name)
            {
                case "close_ShowImage":  //关闭图面查看界面
                    //开启面板  对象
                    UIProcessing.Instance.UI_ShowOrClose(UI_Type.Button, "AR_close", true);
                    UIProcessing.Instance.UI_ShowOrClose(UI_Type.Button, "AR_camera", true);
                    UIProcessing.Instance.UI_ShowOrClose(UI_Type.Button, "AR_modes", true);
                    UIProcessing.Instance.UI_ShowOrClose("Panel_ShowImage", false);
                    break;
                case "Pick_Save":  //保存到相册 然后退出
                    SaveTextureToGayyery();
                    UIProcessing.Instance.UI_ShowOrClose("Panel_ShowImage", false);
                    //开启面板  对象
                    UIProcessing.Instance.UI_ShowOrClose(UI_Type.Image, "AR_close", true);
                    UIProcessing.Instance.UI_ShowOrClose(UI_Type.Button, "AR_camera", true);
                    UIProcessing.Instance.UI_ShowOrClose(UI_Type.Button, "AR_modes", true);
                    break;
                case "Pcik_Share":  //分享
                    IOSSocialManager.Instance.ShareMedia(ARKit_OnLineCacheData.Instance.shareTextureText, UI_CacheData.Instance.currentSelectTexture2dFromGalley);
                    SaveTextureToGayyery();
                    break;
                case "Open_Gallery":  //打开相册
                    OpenGallery();
                    break;
            }
        }

        /// <summary>
        /// 保存截图到相册
        /// </summary>
        private void SaveTextureToGayyery()
        {

#if UNITY_ANDROID
//var imageTitle = "Screenshot-" + System.DateTime.Now.ToString("yy-MM-dd-hh-mm-ss") + ".png";
//const string folderName = "vPlaceAR";
//AGFileUtils.SaveImageToGallery(_lastTakenScreenshot, imageTitle, folderName, ImageFormat.PNG);
//AGUIMisc.ShowToast(" 截图成功");
#elif UNITY_IPHONE
            IOSCamera.OnImageSaved += OnImageSaved;
            IOSCamera.Instance.SaveTextureToCameraRoll(UI_CacheData.Instance.currentSelectTexture2dFromGalley);
            //  var imageTitle = "Screenshot-" + System.DateTime.Now.ToString("yy-MM-dd-hh-mm-ss") + ".png";
            //   string path = Application.persistentDataPath + "/" + imageTitle;
            //   byte[] bytes = _lastTakenScreenshot.EncodeToPNG();
            //  File.WriteAllBytes(path, bytes);
#endif

        }
        /// <summary>
        /// IOS截图 回调
        /// </summary>
        /// <param name="result"></param>
        private void OnImageSaved(SA.Common.Models.Result result)
        {
            IOSCamera.OnImageSaved -= OnImageSaved;
            //if (result.IsSucceeded)
            //    IOSMessage.Create("成功", "保存成功");
            //   else
            //      IOSMessage.Create("失败", "保存失败");
        }

        #region 打开相册功能 
        /// <summary>
        /// 打开相册
        /// </summary>
        private void OpenGallery()
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                IOSCamera.OnImagePicked += OnImage;
                IOSCamera.Instance.PickImage(ISN_ImageSource.Album);
            }
            //else if (Application.platform == RuntimePlatform.Android)
            //{
            //    UIProcessing.Instance.UI_ShowOrClose("Panel_ShowImage", true);
            //    darawTexgture = UIProcessing.Instance.GetUI_Transform(UI_Type.Image, "Show_Image").GetComponent<Image>();
            //    var imageResultSize = ImageResultSize.Original;
            //    AGGallery.PickImageFromGallery(
            //        selectedImage =>
            //        {
            //            var imageTexture2D = selectedImage.LoadTexture2D();

            //            string msg = string.Format("{0} was loaded from gallery with size {1}x{2}",
            //                selectedImage.OriginalPath, imageTexture2D.width, imageTexture2D.height);
            //        //  AGUIMisc.ShowToast(msg);
            //        darawTexgture.sprite = SpriteFromTex2D(imageTexture2D);
            //        // Clean up
            //        Resources.UnloadUnusedAssets();
            //        }, errorMessage =>
            //        {
            //            AGUIMisc.ShowToast("没有选择任何图片");
            //            UIProcessing.Instance.UI_ShowOrClose("Panel_ShowImage", false);
            //        }, imageResultSize, true);
            //}
        }
        /// <summary>
        /// IOS选择图片回调
        /// </summary>
        /// <param name="result"></param>
        private void OnImage(IOSImagePickResult result)
        {
            if (result.IsSucceeded)   //成功
            {
                darawTexgture = UIProcessing.Instance.GetUI_Transform(UI_Type.Image, "Show_Image").GetComponent<Image>();

                UI_CacheData.Instance.currentSelectTexture2dFromGalley = result.Image;  // 分享图片
                darawTexgture.sprite = SpriteFromTex2D(result.Image);

            }
            else  //失败
            { }
            IOSCamera.OnImagePicked -= OnImage;
        }
        #endregion //....



        /// <summary>
        /// Texture2D → Sprite
        /// </summary>
        /// <param name="texture"></param>
        /// <returns></returns>
        private Sprite SpriteFromTex2D(Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }

    }
}
