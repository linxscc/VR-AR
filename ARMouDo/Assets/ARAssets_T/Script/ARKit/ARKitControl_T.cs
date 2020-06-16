using CacheDarta_T;
using Input_XYRF;
using PlaceAR;
using System.Collections;
using System.Collections.Generic;
using Tools_XYRF;
using UI_XYRF;
using UnityEngine;


namespace ARKit_T
{
    public class ARKitControl_T : MonoBehaviour
    {
        /// <summary>
        /// 平面识别
        /// </summary>
        private PointPanelforCenter pointPanelforCenter;
        //public Transform dlight;
        private void Start()
        {
            //if (dlight == null)
               // dlight = transform.Find("Directional Light");
           // dlight.gameObject.SetActive(true);
            //若开启平面识别 显示片 则打开
            if (ARKit_OnLineCacheData.Instance.isOpenPanelShows)
            {
                //Global.aRKit_PlaneAnchor.SetActive(true);
               // pointPanelforCenter = PointPanelforCenter.Instance(Global.aRKit_PlaneAnchor);
            }
            //初始化 对象生成.位置提示框
            if (ARKit_OnLineCacheData.Instance.isOpenPromptPanel)
                Global.promptPanelTra = ResourcesLod_T.ResourcesLodObj(Global.promptPanelTraYZJname);
            //初始化 点云识别功能
            ARKit_DetectionPanel.Instance.Init_DetectionPointClound();
           // GetComponentInChildren<MRControl>().OnInit();
        }
        private void OnEnable()
        {
            //注册事件
            EventComeBack_T.ScreenHaveObj += EventComeBack_T_ScreenHaveObj;
            EventComeBack_T.ScreenNotHaveObj += EventComeBack_T_ScreenNotHaveObj;
        }

        private void OnDisable()
        {
            //取消事件
            EventComeBack_T.ScreenHaveObj -= EventComeBack_T_ScreenHaveObj;
            EventComeBack_T.ScreenNotHaveObj -= EventComeBack_T_ScreenNotHaveObj;

            //单列清理
            if (pointPanelforCenter != null)
                pointPanelforCenter.PointPanelforCenter_OnDestroy();
            ARKit_DetectionPanel.Instance = null;
            DetectionScreenCenterTra.Instance = null;
            DeviceInfoCacheDarta.Instance = null;
        }

        private void Update()
        {
            //if (Global.operatorModel != OperatorMode.ARMode) return;
            ARKit_DetectionPanel.Instance.ARKit_DetectionPanelUpdate();   //ARKit 平面识别检测
            DetectionScreenCenterTra.Instance.DetectionScreenCenterTraUpdate();  //ARKit 平面触摸检测 射线检测        
        }
        private void LateUpdate()
        {
            //AR_InputControl.Instance.GetInputDetection();  //输入控制
           // UI_Manager.Instance.AlterScreenUI();  //改变手机方向UI
        }


        /// <summary>
        /// 当屏幕前方 存在对象
        /// </summary>
        private void EventComeBack_T_ScreenHaveObj()
        {
            Global.promptPanelTra.SetActive(false);
        }
        /// <summary>
        /// 当屏幕前方 不存在对象
        /// </summary>
        private void EventComeBack_T_ScreenNotHaveObj()
        {
            if (ARKit_OnLineCacheData.Instance.isOpenPromptPanelCurrent)                  // 可以开启提示框  显示
                Global.promptPanelTra.SetActive(true);
        }


    }
}
