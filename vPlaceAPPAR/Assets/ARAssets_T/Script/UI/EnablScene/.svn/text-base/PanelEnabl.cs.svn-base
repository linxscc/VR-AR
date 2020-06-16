using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.IO;
using UnityEngine.SceneManagement;


namespace UI_XYRF
{
    /// <summary>
    /// UI PanelEnabl
    /// </summary>
    public class PanelEnabl : MonoBehaviour
    {
        /// <summary>
        /// PanelEnabl 面板
        /// </summary>
        public List<Button> button = new List<Button>();
        /// <summary>
        /// Text启动页 最后一页 倒计时
        /// </summary>
        public Text textOverlap;

        /// <summary>
        /// 异步对象
        /// </summary>
        public AsyncOperation async;
        /// <summary>
        /// 滑动对象
        /// </summary>
        private GridPanelDrag_T gridPanelDrag_T;
        /// <summary>
        /// 滑动页面 最后一页 停留时间增量
        /// </summary>
        private float latePageWaitTimeDex;
        public bool isFistOpen = false;
       public Scene allScene;
        private void Awake()
        {
            EnableSceneInit();
        }
        /// <summary>
        /// APP启动初始化
        /// </summary>
        private void EnableSceneInit()
        {
            gridPanelDrag_T = transform.GetComponentInChildren<GridPanelDrag_T>();
            //判断是否存在配置 SendAppEnableName.txt   
            if (!File.Exists(Global.LocalUrl + Global.SendAppEnableName))
            {
                isFistOpen = true;
                //不存在则创建
                Global.enableData.broadcastNumber = 0;
                Global.enableData.pageData = new PageData[gridPanelDrag_T.transform.childCount];
                for (int i = 0; i < gridPanelDrag_T.transform.childCount; i++)
                {
                    Global.enableData.pageData[i] = new PageData();
                }
                Global.enableData.pageData[gridPanelDrag_T.transform.childCount - 1].pageWaitTime = 5f;
                FileTools.CreateFile(Global.LocalUrl, Global.SendAppEnableName, JsonFx.Json.JsonWriter.Serialize(Global.enableData));
            }
            else
            {
                //存在 则读取 并重写
                Global.enableData = FileTools.ReadText<EnableData>(Global.LocalUrl + Global.SendAppEnableName);
                if (Global.enableData.broadcastNumber > 0)  //减少播放次数 && 重写 数据
                {
                    isFistOpen = true;
                    Global.enableData.broadcastNumber -= 1;
                    FileTools.CreateFile(Global.LocalUrl, Global.SendAppEnableName, JsonFx.Json.JsonWriter.Serialize(Global.enableData));
                }
                else  //直接进入主场景
                {
                    isFistOpen = false;
                   // SceneManager.LoadScene(Global.startScene);
                    return;
                }
            }

            //UI初始化
            Button[] b = transform.GetComponentsInChildren<Button>();
            button.AddRange(b);
            foreach (var bu_ in button)
            {
                EventListener_UGUI_T.LoadEvent(bu_.gameObject).onClick = OnButtonClick;
            }
        }
        private void OnEnable()
        {
            GridPanelDrag_T.OnEnablGridLate += GridPanelDrag_T_OnEnablGridLate;
        }
        private void OnDisable()
        {
            GridPanelDrag_T.OnEnablGridLate -= GridPanelDrag_T_OnEnablGridLate;
        }

        private void Start()
        {
           // allScene = SceneManager.GetSceneByName("Start");
           // async = SceneManager.LoadSceneAsync(Global.startScene,LoadSceneMode.Additive);
           
            //  async = Application.LoadLevelAsync("Start");
              //async.allowSceneActivation = false;

            //信息初始化
            latePageWaitTimeDex = Global.enableData.pageData[gridPanelDrag_T.transform.childCount - 1].pageWaitTime + 1;
        }

        /// <summary>
        /// 当点击时触发
        /// </summary>
        /// <param name="go"></param>
        private void OnButtonClick(GameObject go)
        {
            switch (go.name)
            {
                case "ButtonOverlap":
                    // async.allowSceneActivation = true;
                    // Scene allScene = SceneManager.GetSceneByName(Global.startScene);
                    // StartSceneControl.Singleton.Open();
                    Global.OperatorModel = OperatorMode.SelectMode;
                    SceneManager.LoadScene(Global.startScene);
                    //跳过引导界面

                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 当引导页面 是最后一页时执行
        /// </summary>
        private void GridPanelDrag_T_OnEnablGridLate()
        {
            InvokeRepeating("DetectionEnablSchedule", 0, 1f);
        }
        public void DetectionEnablSchedule()
        {
            latePageWaitTimeDex -= 1f;
            if (latePageWaitTimeDex == 0)
            {
                Global.OperatorModel = OperatorMode.SelectMode;
                // StartSceneControl.Singleton.Open();
                SceneManager.LoadScene(Global.startScene);
                //CancelInvoke("DetectionEnablSchedule");
                //async.allowSceneActivation = true;
                return;
            }
            textOverlap.text = "立即体验 " + latePageWaitTimeDex.ToString() + "s";
        }

    }
}
