using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;


namespace ARKit_T
{
    /// <summary>
    /// 平面识别 → 显现 
    /// </summary>
    public class PointPanelforCenter
    {
        public UnityARAnchorManager unityARAnchorManager;
        private PointPanelforCenter(GameObject go)
        {
            unityARAnchorManager = new UnityARAnchorManager();
            UnityARUtility.InitializePlanePrefab(go);
        }
        private static PointPanelforCenter instance;
        public static PointPanelforCenter Instance(GameObject go)
        {
            if (instance == null)
                instance = new PointPanelforCenter(go);
            return instance;
        }
        public void PointPanelforCenter_OnDestroy()
        {
            unityARAnchorManager.Destroy();
        }

    }
}
