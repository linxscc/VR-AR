using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using DeadMosquito.AndroidGoodies;
using System.IO;
using ARKit_T;
using UnityEngine.SceneManagement;
using PlaceAR;
using Tools_XYRF;
using vPlace_zpc;


namespace UI_XYRF
{
    public class Panel_Control : MonoBehaviour
    {
        /// <summary>
        /// 渲染图片的Image
        /// </summary>
        private Image darawTexgture;
        /// <summary>
        /// 按下UI时间
        /// </summary>
        private float onDownTime;
        /// <summary>
        /// 是否正在录屏
        /// </summary>
        private bool isReplayVideoing = false;
        /// <summary>
        /// 录屏 value 改变增量
        /// </summary>
        private float ReplayVideoingSliderValueDex;
        /// <summary>
        /// 选择的录屏 / 截屏 方式
        /// </summary>
        private int changeReplayWay;
        /// <summary>
        /// 录屏时长
        /// </summary>
        private float supportReplayVideoTime;


        /// <summary>
        /// 需要旋转的ui
        /// </summary>
        public List<GameObject> rotateUI = new List<GameObject>();
        /// <summary>
        /// 按钮事件
        /// </summary>
        public List<Button> button = new List<Button>();


        private void OnEnable()
        {
            ISN_ReplayKit.ActionRecordStarted += HandleActionRecordStarted;                 //开始录屏 回调
            ISN_ReplayKit.ActionRecordStoped += HandleActionRecordStoped;                   //结束录屏 回调
            ISN_ReplayKit.ActionRecordInterrupted += HandleActionRecordInterrupted;   //录屏中断 回调

            ISN_ReplayKit.ActionShareDialogFinished += HandleActionShareDialogFinished;   //分享窗口 调用完成
            ISN_ReplayKit.ActionRecorderDidChangeAvailability += HandleActionRecorderDidChangeAvailability;   //录屏可用性是否更改

            //    IOSNativePopUpManager.showMessage("欢迎", "欢迎使用RePlay录屏功能!");
            ISN_Logger.Log("ReplayKit初始化状态: " + ISN_ReplayKit.Instance.IsAvailable);
        }
        private void OnDisable()
        {

            ISN_ReplayKit.ActionRecordStarted -= HandleActionRecordStarted;
            ISN_ReplayKit.ActionRecordStoped -= HandleActionRecordStoped;
            ISN_ReplayKit.ActionRecordInterrupted -= HandleActionRecordInterrupted;
            ISN_ReplayKit.ActionShareDialogFinished -= HandleActionShareDialogFinished;
            ISN_ReplayKit.ActionRecorderDidChangeAvailability -= HandleActionRecorderDidChangeAvailability;
            for (int i = 0; i < rotateUI.Count; i++)
            {
                transform.parent.GetComponent<StartCanvas>().rotaterUI.Remove(rotateUI[i]);
            }

        }

        private void Awake()
        {
            if (Global.OperatorModel == OperatorMode.MRModel)
            {
                rotateUI[1].transform.localScale = Vector3.zero;
            }
            transform.parent.GetComponent<StartCanvas>().rotaterUI.AddRange(rotateUI);
            Button[] b = transform.GetComponentsInChildren<Button>();
            button.AddRange(b);
            for (int i = 0; i < button.Count; i++)
            {
                switch (button[i].name)
                {
                    case "AR_camera":
                        EventListener_UGUI_T.LoadEvent(button[i].gameObject).onDown = OnDown;
                        EventListener_UGUI_T.LoadEvent(button[i].gameObject).onUp = OnUp;
                        break;
                    case "Replaytens":
                        EventListener_UGUI_T.LoadEvent(button[i].gameObject).onEnter = OnEnter;
                        EventListener_UGUI_T.LoadEvent(button[i].gameObject).onExit = OnExit;
                        EventListener_UGUI_T.LoadEvent(button[i].gameObject).onClick = OnButtonClick;
                        break;
                    case "Replaythirty":
                        EventListener_UGUI_T.LoadEvent(button[i].gameObject).onEnter = OnEnter;
                        EventListener_UGUI_T.LoadEvent(button[i].gameObject).onExit = OnExit;
                        EventListener_UGUI_T.LoadEvent(button[i].gameObject).onClick = OnButtonClick;
                        break;
                    case "Replaysixty":
                        EventListener_UGUI_T.LoadEvent(button[i].gameObject).onEnter = OnEnter;
                        EventListener_UGUI_T.LoadEvent(button[i].gameObject).onExit = OnExit;
                        EventListener_UGUI_T.LoadEvent(button[i].gameObject).onClick = OnButtonClick;
                        break;
                    default:
                        EventListener_UGUI_T.LoadEvent(button[i].gameObject).onClick = OnButtonClick;
                        break;
                }
            }
        }

        /// <summary>
        /// 悬浮 触发
        /// </summary>
        /// <param name="go"></param>
        private void OnEnter(GameObject go)
        {
            switch (go.name)
            {
                case "Replaytens":
                    UIProcessing.Instance.UI_ShowOrClose(UI_Type.Image, "ImagetensBlue", true);
                    changeReplayWay = 1;
                    break;
                case "Replaythirty":
                    UIProcessing.Instance.UI_ShowOrClose(UI_Type.Image, "ImagethirtyBlue", true);
                    changeReplayWay = 2;
                    break;
                case "Replaysixty":
                    UIProcessing.Instance.UI_ShowOrClose(UI_Type.Image, "ImagesixtyBlue", true);
                    changeReplayWay = 3;
                    break;
            }
        }
        /// <summary>
        /// 离开 触发
        /// </summary>
        /// <param name="go"></param>
        private void OnExit(GameObject go)
        {
            switch (go.name)
            {
                case "Replaytens":
                    UIProcessing.Instance.UI_ShowOrClose(UI_Type.Image, "ImagetensBlue", false);
                    changeReplayWay = 0;
                    break;
                case "Replaythirty":
                    UIProcessing.Instance.UI_ShowOrClose(UI_Type.Image, "ImagethirtyBlue", false);
                    changeReplayWay = 0;
                    break;
                case "Replaysixty":
                    UIProcessing.Instance.UI_ShowOrClose(UI_Type.Image, "ImagesixtyBlue", false);
                    changeReplayWay = 0;
                    break;
            }
        }
        private void OnDown(GameObject go)
        {
            switch (go.name)
            {
                case "AR_camera":  //截图/录屏 保存
                    isReplayVideoing = false;
                    onDownTime = 0;
                    changeReplayWay = 0;
                   // InvokeRepeating("OnDownTimeAdd", 0.02f, 0.02f);
                    break;
            }
        }
        private void OnUp(GameObject go)
        {
            switch (go.name)
            {
                case "AR_camera":  //截图保存
                    switch (changeReplayWay)
                    {
                        case 0:  //截图
                            if (onDownTime < 0.5f)  //截屏
                            {
                                Debug.Log("开始截屏");
                                OnClickAR_CameraSaveScreenTexture();
                               // CancelInvoke("OnDownTimeAdd");
                            }
                            else
                            {
                                //超时 未选择 录屏时间 取消录屏
                                // 关闭 选择录屏时间 UI 并初始化
                                UIProcessing.Instance.UI_ShowOrClose(UI_Type.Image, "PanelReplayTime", false);
                                //显示UI信息
                                UIProcessing.Instance.UI_ShowOrClose(UI_Type.Button, "AR_close", true);
                                UIProcessing.Instance.UI_ShowOrClose(UI_Type.Button, "AR_modes", true);
                                UIProcessing.Instance.UI_TransformScale(UIProcessing.Instance.GetUI_Transform(UI_Type.Button, "AR_camera").GetChild(0), new Vector3(1f, 1f, 1f), 1f);
                                UIProcessing.Instance.UI_TransformScale(UIProcessing.Instance.GetUI_Transform(UI_Type.Button, "AR_camera").GetChild(1), new Vector3(1f, 1f, 1f), 1f);
                                ARKit_OnLineCacheData.Instance.isOpenPromptPanelCurrent = true;   // 开启 ARKit 中心提示框 显示
                            }
                            break;
                        case 1:  //录屏10s
                            supportReplayVideoTime = 10f;
                            OnUpReplayProssingUI();
                            break;
                        case 2:   //录屏30s
                            supportReplayVideoTime = 30f;
                            OnUpReplayProssingUI();
                            break;
                        case 3:   //录屏60s
                            supportReplayVideoTime = 60f;
                            OnUpReplayProssingUI();
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// 点击触发
        /// </summary>
        /// <param name="go"></param>
        private void OnButtonClick(GameObject go)
        {
            switch (go.name)
            {
                case "AR_close":
                   // Global.aRkit_Control_T.GetComponent<ARKitControl_T>().dlight.gameObject.SetActive(false);
                    if (ScrollMenuControl.Singleton != null)
                        ScrollMenuControl.Singleton.close = null;//退出
                    UIProcessing.Instance.OPenOrCloseARCanvasUI(false);  //关闭AR模式
                    Global.OperatorModel = OperatorMode.BrowserMode;
                    //if (Global.isStartSceneEnterAR)   //返回 主界面
                    //{
                    //    StartSceneControl.Singleton.Open();
                    //}
                    //else   //返回 模型浏览器
                    //{
                    //    StartSceneControl.Singleton.Close(true);
                    //    ModelBrowserControl.Singleton.Open();
                    //}
                    break;
                case "AR_modes":   //选择模型
                    Global.OperatorModel = OperatorMode.ARMode;
                    if (ScrollMenuControl.Singleton != null)
                        ScrollMenuControl.Singleton.close = Open;
                    Close();
                    //ScrollMenuControl.Singleton.OnInit();
                    ScrollMenuControl.Singleton.Open();
                    break;
            }
        }

        public void Open()
        {
            // UI显示
            string[] names = { "AR_close", "AR_camera", "AR_modes" };
            UIProcessing.Instance.UI_ChildShowOrClose("Panel_Control", true, names);
        }
        public void Close()
        {

            foreach (Transform item in transform)
            {
                item.gameObject.SetActive(false);
            }

        }

        /// <summary>
        /// 截图保存
        /// </summary>
        private void OnClickAR_CameraSaveScreenTexture()
        {
            ARKit_OnLineCacheData.Instance.isOpenPromptPanelCurrent = false;               //关闭ARKit 中心提示框
            Global.promptPanelTra.SetActive(ARKit_OnLineCacheData.Instance.isOpenPromptPanelCurrent);
            //隐藏面板  对象
            UIProcessing.Instance.UI_ChildShowOrClose("Panel_Control", false);

            StartCoroutine(TakeScreenshot(Screen.width, Screen.height));
        }
        /// <summary>
        /// 截图 并保持到相册
        /// </summary>
        public IEnumerator TakeScreenshot(int width, int height)
        {
            yield return new WaitForEndOfFrame();
            var texture = new Texture2D(width, height, TextureFormat.RGB24, false);
            texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            texture.Apply();
            Texture2D _lastTakenScreenshot = texture;
            //截图成功 渲染显示
            UIProcessing.Instance.UI_ShowOrClose("Panel_ShowImage", true);
            darawTexgture = UIProcessing.Instance.GetUI_Transform(UI_Type.Image, "Show_Image").GetComponent<Image>();
            UI_CacheData.Instance.currentSelectTexture2dFromGalley = _lastTakenScreenshot;    //记录 当前截屏 图片
            darawTexgture.sprite = SpriteFromTex2D(_lastTakenScreenshot);

            ARKit_OnLineCacheData.Instance.isOpenPromptPanelCurrent = true;   // 开启 ARKit 中心提示框 显示
            //  Destroy(_lastTakenScreenshot);
        }



        /// <summary>
        /// Texture2D → Sprite
        /// </summary>
        /// <param name="texture"></param>
        /// <returns></returns>
        private Sprite SpriteFromTex2D(Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }

        #region 录屏处理 开始

        /// <summary>
        /// 当按下时 记录按住时间
        /// </summary>
        private void OnDownTimeAdd()
        {
            print(onDownTime);
            onDownTime += 0.02f;
            if (onDownTime >= 0.5f)  //开始录屏
            {
                //关闭ARKit 中心提示框
                ARKit_OnLineCacheData.Instance.isOpenPromptPanelCurrent = false;
                Global.promptPanelTra.SetActive(ARKit_OnLineCacheData.Instance.isOpenPromptPanelCurrent);

                // 打开 选择录屏时间 UI 并初始化
                UIProcessing.Instance.UI_ShowOrClose(UI_Type.Image, "PanelReplayTime", true);

                string[] panelReplayTimeNamesFalse = { "ImagetensBlue", "ImagethirtyBlue", "ImagesixtyBlue" };
                for (int c = 0; c < panelReplayTimeNamesFalse.Length; c++)
                {
                    UIProcessing.Instance.UI_ShowOrClose(UI_Type.Image, panelReplayTimeNamesFalse[c], false);
                }
                //隐藏UI信息
                UIProcessing.Instance.UI_ShowOrClose(UI_Type.Button, "AR_close", false);
                UIProcessing.Instance.UI_ShowOrClose(UI_Type.Button, "AR_modes", false);
                UIProcessing.Instance.UI_TransformScale(UIProcessing.Instance.GetUI_Transform(UI_Type.Button, "AR_camera").GetChild(0), new Vector3(1.1f, 1.1f, 1.1f), 0.2f);
                UIProcessing.Instance.UI_TransformScale(UIProcessing.Instance.GetUI_Transform(UI_Type.Button, "AR_camera").GetChild(1), new Vector3(0.8f, 0.8f, 0.8f), 0.2f);
                //  UIProcessing.Instance.UI_TransformAlpha(UIProcessing.Instance.GetUI_Transform(UI_Type.Button, "AR_camera"), 0f, 0.1f, false);

                CancelInvoke("OnDownTimeAdd");
            }
        }
        /// <summary>
        /// 开始录屏前处理 UI信息
        /// </summary>
        private void OnUpReplayProssingUI()
        {
            ///正在录屏了 ...
            isReplayVideoing = true;

            //关闭 录屏时间选择面板
            UIProcessing.Instance.UI_ShowOrClose(UI_Type.Image, "PanelReplayTime", false);
            //开启进度条 面板
            UIProcessing.Instance.UI_ShowOrClose(UI_Type.Image, "RePlayKitSchedule", true);
            UIProcessing.Instance.GetUI_Transform(UI_Type.Slider, "ScheduleSlider").GetComponent<Slider>().value = 0;
            //关闭 AR_camera面板
            UIProcessing.Instance.UI_ShowOrClose(UI_Type.Button, "AR_camera", false);

            //计算 录屏改变增量 帧/一次
            ReplayVideoingSliderValueDex = (1f / supportReplayVideoTime) / 50f;
            //开始录屏
            ISN_ReplayKit.Instance.StartRecording();
            InvokeRepeating("InvokeingReplayKitSchedule", 0, 0.02f);
        }

        private void InvokeingReplayKitSchedule()
        {
            UIProcessing.Instance.GetUI_Transform(UI_Type.Slider, "ScheduleSlider").GetComponent<Slider>().value += ReplayVideoingSliderValueDex;
            //录屏结束...
            if (UIProcessing.Instance.GetUI_Transform(UI_Type.Slider, "ScheduleSlider").GetComponent<Slider>().value > 0.98f)
                ReplayVideoBad();

        }
        /// <summary>
        /// 录屏 终止方法
        /// </summary>
        private void ReplayVideoBad()
        {
            //停止录屏
            ISN_ReplayKit.Instance.StopRecording();
            //  Debug.Log("停止录屏");
            //关闭进度条 面板
            UIProcessing.Instance.UI_ShowOrClose(UI_Type.Image, "RePlayKitSchedule", false);
            isReplayVideoing = false;
            //显示UI信息
            UIProcessing.Instance.UI_ShowOrClose(UI_Type.Button, "AR_close", true);
            UIProcessing.Instance.UI_ShowOrClose(UI_Type.Button, "AR_modes", true);
            UIProcessing.Instance.UI_TransformScale(UIProcessing.Instance.GetUI_Transform(UI_Type.Button, "AR_camera").GetChild(0), new Vector3(1f, 1f, 1f), 1f);
            UIProcessing.Instance.UI_TransformScale(UIProcessing.Instance.GetUI_Transform(UI_Type.Button, "AR_camera").GetChild(1), new Vector3(1f, 1f, 1f), 1f);
            //   UIProcessing.Instance.UI_TransformAlpha(UIProcessing.Instance.GetUI_Transform(UI_Type.Button, "AR_camera"), 1f);
            //开启 AR_camera面板
            UIProcessing.Instance.UI_ShowOrClose(UI_Type.Button, "AR_camera", true);
            onDownTime = 0;
            ARKit_OnLineCacheData.Instance.isOpenPromptPanelCurrent = true;   // 开启 ARKit 中心提示框 显示
            CancelInvoke("InvokeingReplayKitSchedule");
        }


        #region Replay录屏相关功能回调
        /// <summary>
        /// 录屏出错回调
        /// </summary>
        /// <param name="error"></param>
        private void HandleActionRecordInterrupted(SA.Common.Models.Error error)
        {
            //   IOSNativePopUpManager.showMessage("录屏出错: ", " " + error.Message);
            IOSNativePopUpManager.showMessage("失败: ", "录屏出错...");
            //即时关闭 UI相关
            ReplayVideoBad();
        }
        /// <summary>
        /// 停止录屏 回调
        /// </summary>
        /// <param name="res"></param>
        private void HandleActionRecordStoped(SA.Common.Models.Result res)
        {
            if (res.IsSucceeded)  //录屏成功
            {
                ISN_ReplayKit.Instance.ShowVideoShareDialog();  //分享  录屏结束后 最好按钮点击 弹出共享窗口
            }
            else  //录屏失败
                IOSNativePopUpManager.showMessage("失败", "录屏出错... ");
            // IOSNativePopUpManager.showMessage("失败", "原因: " + res.Error.Message);
        }
        /// <summary>
        /// 分享回调
        /// </summary>
        /// <param name="res"></param>
        private void HandleActionShareDialogFinished(ReplayKitVideoShareResult res)
        {
            if (res.Sources.Length > 0)
            {
                foreach (string source in res.Sources)
                {
                    //  IOSNativePopUpManager.showMessage("成功", "分享视频" + source);
                    //  IOSNativePopUpManager.showMessage("成功", "分享视频");
                }
            }
            else
            {
                //  IOSNativePopUpManager.showMessage("失败", "未分享视频");

            }
        }
        /// <summary>
        /// 开始录屏回调
        /// </summary>
        /// <param name="res"></param>
        private void HandleActionRecordStarted(SA.Common.Models.Result res)
        {
            if (res.IsSucceeded)  // 开始录屏 成功
            {
                //  IOSNativePopUpManager.showMessage("成功", "录屏启动");

                ISN_ReplayKit.Instance.DiscardRecording();  //丢弃 缓存记录
            }
            else //开始录屏 失败
            {
                ISN_Logger.Log("录屏启动失败: " + res.Error.Message);
                IOSNativePopUpManager.showMessage("录屏启动失败", "原因: " + res.Error.Message);
            }
            ISN_ReplayKit.ActionRecordStarted -= HandleActionRecordStarted;
        }
        /// <summary>
        /// 录屏可用性是否 更改
        /// </summary>
        /// <param name="IsRecordingAvaliable"></param>
        private void HandleActionRecorderDidChangeAvailability(bool IsRecordingAvaliable)
        {
            ISN_Logger.Log("录屏是否可用: " + IsRecordingAvaliable);
            ISN_Logger.Log("麦克风是否可用: " + ISN_ReplayKit.Instance.IsMicEnabled);

            ISN_ReplayKit.ActionRecordDiscard += HandleActionRecordDiscard;   //清除 录屏缓存
            ISN_ReplayKit.Instance.DiscardRecording();  //丢弃 缓存记录
        }
        private void HandleActionRecordDiscard()
        {
            ISN_Logger.Log("缓存删除");
            ISN_ReplayKit.ActionRecordDiscard -= HandleActionRecordDiscard;
        }
        #endregion //Replay录屏相关功能回调 结束...

        #endregion //录屏处理 结束...

    }
}