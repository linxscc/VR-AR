using System.Collections;
using System.Collections.Generic;
using Tools_XYRF;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.iOS;
using XYRF_FingerGesture;


namespace ARKit_T
{
    [RequireComponent(typeof(ARKiAnimation_T))]
    [RequireComponent(typeof(OnMouseEvent_T))]
    public class ScreenHaveMode : MonoBehaviour
    {
        private void Awake()
        {
            if (!transform.GetComponent<MeshRenderer>())
                gameObject.AddComponent<MeshRenderer>();
        }
        /// <summary>
        /// 可见
        /// </summary>
        private void OnBecameVisible()
        {
            ARKit_OnLineCacheData.Instance.isScreenObj = true;
        }
        /// <summary>
        /// 不可见
        /// </summary>
        private void OnBecameInvisible()
        {
            ARKit_OnLineCacheData.Instance.isScreenObj = false;
        }
    }
}
