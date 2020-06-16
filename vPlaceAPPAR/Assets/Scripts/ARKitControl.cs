/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using UnityEngine.XR.iOS;
using System.Collections.Generic;
using ARKit_T;
using UnityEngine.SceneManagement;
using HoloKit;

namespace PlaceAR
{
    public class ARKitControl : MonoBehaviour
    {
        private static ARKitControl instance;

        public static ARKitControl Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject obj = Resources.Load<GameObject>(Global.ARkit_Control_TYZJname);
                    obj= Instantiate(obj);
                    DontDestroyOnLoad(obj);
                    instance = obj.GetComponent<ARKitControl>();
                    //instance = FindObjectOfType<ARKitControl>();
                }

                return instance;
            }
        }
        /// <summary>
        /// 关闭指示框
        /// </summary>
        public bool closePanel
        {
            set
            {
                frame.Find("focus-0@2x").gameObject.SetActive(value);
            }
        }
        public Camera camL;
        public Camera camR;
        private Camera aRCamera;
        private BarrelDistortion BDL;
        private BarrelDistortion BDR;
        /// <summary>
        /// 眼镜中点
        /// </summary>
        public Transform eyePoint;
        /// <summary>
        /// 3dUI
        /// </summary>
        [HideInInspector]
        public GameObject canvas3D;
        /// <summary>
        /// 2DUI
        /// </summary>
        [SerializeField]
        private GameObject canvas2D;
        /// <summary>
        /// 位置提示框
        /// </summary>
        public Transform frame;
        [HideInInspector]
        public UnityARCameraManagerNearFar_T uARMF;
        private Menu3D menu3D;
        /// <summary>
        /// AR模式下灯光
        /// </summary>
        private Transform dlight;
        private bool isShowUI = false;
        private void Awake()
        {
            canvas3D = Resources.Load<GameObject>(Global.canvas3D);

            canvas3D = Instantiate<GameObject>(canvas3D);
            canvas3D.transform.SetParent(transform);
            menu3D = canvas3D.GetComponentInChildren<Menu3D>();
            canvas3D.transform.localScale = new Vector3(0.003f, 0.003f, 0.003f);
            uARMF =GetComponentInChildren<UnityARCameraManagerNearFar_T>();
            uARMF.cameraLocat += UpdateLocation;
            aRCamera = GetComponent<Camera>();
            camL.backgroundColor = Color.black;
            camR.backgroundColor = Color.black;
            BDL = camL.GetComponent<BarrelDistortion>();
            BDR = camR.GetComponent<BarrelDistortion>();
            BDL.BarrelDistortionFactor = 1.7f;
            BDR.BarrelDistortionFactor = 1.7f;
            if (dlight == null)
             dlight = transform.Find("Directional Light");
      
            OnInit();

        }
        public void OnInit()
        {
            switch (Global.OperatorModel)
            {
                case OperatorMode.SelectMode:
                    IntoOther();
                    break;
                case OperatorMode.BrowserMode:
                    IntoOther();
                    break;
                case OperatorMode.ARMode:
                    IntoAR();
                    break;
                case OperatorMode.MRModel:
                    IntoMR();
                    break;
                default:
                    break;
            }
               
        }
        public void IntoOther()
        {
            dlight.gameObject.SetActive(false);
            camL.enabled = false;
            camR.enabled = false;
            if (GetComponent<ARKitControl_T>())
                Destroy(GetComponent<ARKitControl_T>());
            canvas2D.SetActive(false);
            canvas3D.SetActive(false);
            frame.gameObject.SetActive(false);
        }
        public void IntoMR()
        {
            dlight.gameObject.SetActive(true);
            camL.enabled = true;
            camR.enabled = true;
            if(GetComponent<ARKitControl_T>())
            Destroy(GetComponent<ARKitControl_T>());
            canvas2D.SetActive(true);
            canvas3D.SetActive(true);
            frame.gameObject.SetActive(true);
        }
        public void IntoAR()
        {
            dlight.gameObject.SetActive(true);
            camL.enabled = false;
            camR.enabled = false;
            if (!GetComponent<ARKitControl_T>())
                gameObject.AddComponent<ARKitControl_T>();
            canvas2D.SetActive(false);
            canvas3D.SetActive(false);
            frame.gameObject.SetActive(false);

        }
        public void Return()
        {
            Global.OperatorModel = OperatorMode.SelectMode;
            
            //IntoOther();
        }
        private bool HitTestWithResultType(ARPoint point, ARHitTestResultType resultTypes)
        {
            List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface().HitTest(point, resultTypes);
            if (hitResults.Count > 0)
            {
                foreach (var hitResult in hitResults)
                {
                    Debug.Log("Got hit!");
                    frame.position = UnityARMatrixOps.GetPosition(hitResult.worldTransform);
                    frame.rotation = UnityARMatrixOps.GetRotation(hitResult.worldTransform);
                    //Debug.Log(string.Format("x:{0:0.######} y:{1:0.######} z:{2:0.######}", frame.position.x, frame.position.y, frame.position.z));
                    return true;
                }
            }
            return false;
        }

        private void UpdateLocation(Vector3 p, Vector3 e)
        {          
            if (Global.OperatorModel != OperatorMode.MRModel) return;
            UpdateFramt();
            if (canvas3D != null  )
            {
                if (!menu3D.OnBecame && e.y <= 50)
                {
                   // print("q");
                    isShowUI = false;
                    canvas3D.transform.localPosition = new Vector3(p.x, p.y - 4f, p.z);
                    canvas3D.transform.eulerAngles = new Vector3(90, e.y, 0);
                }
                else if(!isShowUI)
                {
                    isShowUI = true;
                    //print("qwe");
                    menu3D.OnInit();
                }
            }


        }
        // Update is called once per frame
        void UpdateFramt()
        {
            var screenPosition = Camera.main.ScreenToViewportPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
            ARPoint point = new ARPoint
            {
                x = screenPosition.x,
                y = screenPosition.y
            };

            // prioritize reults types
            ARHitTestResultType[] resultTypes = {
                        ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
                        // if you want to use infinite planes use this:
                        ARHitTestResultType.ARHitTestResultTypeExistingPlane,
                        ARHitTestResultType.ARHitTestResultTypeHorizontalPlane,
                        ARHitTestResultType.ARHitTestResultTypeFeaturePoint
                    };
            for (int i = 0; i < resultTypes.Length; i++)
            {
                if (HitTestWithResultType(point, resultTypes[i]))
                {
                    return;
                }
            }
        }
        void OnDestroy()
        {
            instance = null;
        }
    }
}