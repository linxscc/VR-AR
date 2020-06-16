using ARKit_T;
using CacheDarta_T;
using System.Collections;
using System.Collections.Generic;
using Tools_XYRF;
using UI_XYRF;
using UnityEngine;


namespace InitARData_T
{
    /// <summary>
    /// 初始化程序
    /// </summary> 
    public class InitData_T : MonoBehaviour
    {
        private void Awake()
        {
           
        }
        public void OnInit()
        {
            Init_ARKitControl_T();
        }
        /// <summary>
        /// 初始化ARKit控制器
        /// </summary>
        private void Init_ARKitControl_T()
        {
            DeviceInfoCacheDarta.Instance.Init_DeviceInfoCacheDarta();  //检测设备信息记录
            DeviceInfoCacheDarta deviceInfoCacheDarta = DeviceInfoCacheDarta.Instance;

            //Test
            deviceInfoCacheDarta.isCanOpenAR = true;
            if (deviceInfoCacheDarta.isCanOpenAR)  //系统支持 开启AR功能? 
            {
                // IOSNativePopUpManager.showMessage("恭喜 ", "该设备支持AR功能");
                //UI初始化
                StartSceneControl.Singleton.Close();  //关闭模型浏览器界面

                if (Global.StartCanvas == null)
                    Global.StartCanvas = GameObject.FindWithTag(Global.startCanvas);
                Global.aRPanel_Control = ResourcesLod_T.ResourcesLodObj(Global.ARPanel_Control_TYZJname, Global.StartCanvas.transform);
                Global.aRPanel_ShowImage = ResourcesLod_T.ResourcesLodObj(Global.ARPanel_ShowImage_TYZJname, Global.StartCanvas.transform);
               // Global.aRKit_PlaneAnchor = ResourcesLod_T.ResourcesLodObj(Global.ARKit_PlaneAnchorYZJname);
               // Global.aRKit_PlaneAnchor.SetActive(false);
                Global.StartCanvas.transform.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
                Global.StartCanvas.AddComponent<ARKit_OnLineCacheData>();
                Global.StartCanvas.AddComponent<UI_Manager>();

                //if (Global.aRkit_Control_T == null)
                //    Global.aRkit_Control_T = ResourcesLod_T.ResourcesLodObj(Global.ARkit_Control_TYZJname);
                //else
                //    Global.aRkit_Control_T.AddComponent<ARKitControl_T>();


            }
            else  //设备不支持
            {
#if UNITY_IPHONE
                //提示升级系统版本...
                IOSNativePopUpManager.showMessage("抱歉 ", "该设备系统版本不支持AR功能,请升级IOS系统到11.0以上");

#elif UNITY_ANDROID

#endif
            }

            Destroy(this.gameObject);     //删除
        }

    }
}