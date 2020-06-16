using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using Tools_XYRF;
using Screen_XYRF;
using System.Globalization;
using ARKit_T;
using Input_XYRF;
using DG.Tweening;


namespace UI_XYRF
{
    /// <summary>
    /// 管理器
    /// </summary>
    public class UI_Manager : MonoBehaviour
    {
        #region 系列参数
        //公开参数...

        //私有参数...
        /// <summary>
        /// 上一次记录的 屏幕方向
        /// </summary>
        private ScreenDirection lastScreenDirection = ScreenDirection.horizontalLeft;
        #endregion  //参数完毕...

        private static UI_Manager instance;
        public static UI_Manager Instance
        {
            get
            {
                return instance;
            }
        }
        private void Start()
        {
            instance = this;
            UI_Init_T();
        }

        /// <summary>
        /// 根据 手机方向 改变UI
        /// </summary>
        public void AlterScreenUI()
        {
            ScreenDirectionRotation.Instance.ChooseDirection();
            if (lastScreenDirection != Global.currentScreenDirection)
            {
                Vector3 rot = new Vector3(0, 0, 0);
                switch (Global.currentScreenDirection)
                {
                    case ScreenDirection.horizontalLeft:
                        rot = Vector3.zero;
                        break;
                    case ScreenDirection.horizontalRight:
                        rot = new Vector3(0, 0, 180);
                        break;
                    case ScreenDirection.verticalTo:
                        rot = new Vector3(0, 0, 90f);
                        break;
                    case ScreenDirection.verticalBack:
                        rot = new Vector3(0, 0, -90f);
                        break;
                }
                //  for (int i = 0; i < UI_CacheData.Instance.UI_TypeCache[UI_Type.Button].Count; i++)
                //   {

                //UI_CacheData.Instance.UI_TypeCache[UI_Type.Button][i].localEulerAngles = rot;
                //   Vector3 a = UI_CacheData.Instance.UI_TypeCache[UI_Type.Button][i].localEulerAngles;
                //  UI_CacheData.Instance.UI_TypeCache[UI_Type.Button][i].localEulerAngles = Vector3.Lerp(a, rot, 0.5f);
                // }
                lastScreenDirection = Global.currentScreenDirection;
            }

        }

        /// <summary>
        /// UI初始化
        /// </summary>
        public void UI_Init_T()
        {
            List<Transform> tra_chids = new List<Transform>();
            List<Transform> BB = new List<Transform>();
            List<Transform> SL = new List<Transform>();
            List<Transform> TO = new List<Transform>();
            List<Transform> DR = new List<Transform>();
            List<Transform> INP = new List<Transform>();
            List<Transform> TE = new List<Transform>();
            List<Transform> IMA = new List<Transform>();
            tra_chids.AddRange(transform.GetComponentsInChildren<Transform>());
            for (int i = 0; i < tra_chids.Count; i++)
            {

                if (tra_chids[i].GetComponent<Button>())
                    BB.Add(tra_chids[i]);
                else if (tra_chids[i].GetComponent<Slider>())
                    SL.Add(tra_chids[i]);
                else if (tra_chids[i].GetComponent<Toggle>())
                    TO.Add(tra_chids[i]);
                else if (tra_chids[i].GetComponent<Dropdown>())
                    DR.Add(tra_chids[i]);
                else if (tra_chids[i].GetComponent<InputField>())
                    INP.Add(tra_chids[i]);
                else if (tra_chids[i].GetComponent<Text>())
                    TE.Add(tra_chids[i]);
                else if (tra_chids[i].GetComponent<Image>())
                    IMA.Add(tra_chids[i]);

            }
            //信息缓存
            UI_CacheData single = UI_CacheData.Instance;
            single.UI_TypeCache.Add(UI_Type.Button, BB);
            single.UI_TypeCache.Add(UI_Type.Slider, SL);
            single.UI_TypeCache.Add(UI_Type.Toogle, TO);
            single.UI_TypeCache.Add(UI_Type.Dropdown, DR);
            single.UI_TypeCache.Add(UI_Type.InputField, INP);
            single.UI_TypeCache.Add(UI_Type.Text, TE);
            single.UI_TypeCache.Add(UI_Type.Image, IMA);

            for (int i = 0; i < transform.childCount; i++)
            {
                //UI面板缓存
                single.UI_Panel.Add(transform.GetChild(i).name, transform.GetChild(i));
            }
            //UI初始化
            UIProcessing.Instance.UI_ShowOrClose("Panel_Control", true);
            UIProcessing.Instance.UI_ShowOrClose("Panel_ShowImage", false);
            UIProcessing.Instance.UI_ShowOrClose(UI_Type.Image, "PanelReplayTime", false);
            UIProcessing.Instance.UI_ShowOrClose(UI_Type.Image, "RePlayKitSchedule", false);

            //ARkit 阴影面板生成
            if (Global.planeTerrainTra == null)
                Global.planeTerrainTra = ResourcesLod_T.ResourcesLodObj(Global.planeTerrainYZJname);
            else
                Global.planeTerrainTra.SetActive(true);
        }
        void OnDisable()
        {
            UI_CacheData.Instance = null;
            ARKit_DetectionPanel.Instance = null;
            ScreenDirectionRotation.Instance = null;
            UIProcessing.Instance = null;
            DetectionScreenCenterTra.Instance = null;
            AR_InputControl.Instance = null;
        }

    }
}
