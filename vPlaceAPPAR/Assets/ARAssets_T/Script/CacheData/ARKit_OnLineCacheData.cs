using InitARData_T;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;


namespace ARKit_T
{
    public class ARKit_OnLineCacheData : MonoBehaviour
    {
        /// <summary>
        ///  开启对象放置位置提示框[一级权限]
        /// </summary>
        [Tooltip("开启对象放置位置→提示框")]
        public bool isOpenPromptPanel = true;
        /// <summary>
        /// 当前是否开启  对象放置位置提示框[临时权限]
        /// </summary>
        [HideInInspector]
        public bool isOpenPromptPanelCurrent = true;
        /// <summary>
        /// 开启识别到平面显示框
        /// </summary>
        [Tooltip("开启识别到平面→显示框")]
        public bool isOpenPanelShows = false;
        /// <summary>
        /// 开启 点云显示?
        /// </summary>
        [Tooltip("开启点云 显示?")]
        public bool isOpenPointClound = false;
        /// <summary>
        /// 可生成多个对象?
        /// </summary>
        [Tooltip("可生成多个对象?")]
        public bool isOpenMuchTra = false;
        /// <summary>
        /// ARKit 生成的相关对象
        /// </summary>
        [Tooltip("ARKit 生成的相关对象")]
        public Dictionary<string, GameObject> ARKit_Transform = new Dictionary<string, GameObject>();


        /// <summary>
        /// ARKit 对象生成位置
        /// </summary>
        [HideInInspector]
        public Vector3 promptPanelPos = Vector3.zero;
        /// <summary>
        /// ARKit 对象生成方向
        /// </summary>
        [HideInInspector]
        public Quaternion promptPaneQua;
        /// <summary>
        /// 屏幕前方是否存在对象
        /// </summary>
        [HideInInspector]
        public bool isScreenObj = false;
        /// <summary>
        /// 点云位置 坐标数组
        /// </summary>
        [HideInInspector]
        public Vector3[] m_PointCloudData;
        /// <summary>
        /// 是否 识别到平面
        /// </summary>
        [HideInInspector]
        public CallBack<bool> callBack;




        /// <summary>
        /// 分享图片 附带文本
        /// </summary>
        [HideInInspector]
        public readonly string shareTextureText = "发现一个好玩的App";


        /// <summary>
        /// 初始化 信息缓存类
        /// </summary>
        private void Init_ARKit_OnLineCacheData()
        {
            Global.modelLayerMask = LayerMask.NameToLayer(Global.modelLayerMaskName);
            Global.planeTerrainLayerMask = LayerMask.NameToLayer(Global.planeTerrainLayerMaskName);
        }
        private static ARKit_OnLineCacheData instance;
        public static ARKit_OnLineCacheData Instance
        {
            get { return instance; }
        }
        private void Awake()
        {
            instance = this;
            Init_ARKit_OnLineCacheData();
        }
    }
}
