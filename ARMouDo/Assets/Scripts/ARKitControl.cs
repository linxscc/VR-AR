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
using DG.Tweening;
using UnityEngine.SceneManagement;
using HoloKit;
using MoDouAR;
using PlaceAR;

namespace MoDouAR
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
                    GameObject obj = Resources.Load<GameObject>(SystemDefineTag.arKitControlURL);
                    obj = Instantiate(obj);
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
                //print(value);
                frame.Find("qq").gameObject.SetActive(value);
            }
        }
        public Camera camL;
        public Camera camR;
        private Camera aRCamera;
        private BarrelDistortion BDL;
        private BarrelDistortion BDR;
        /// <summary>
        /// 进入mr模式前的模式
        /// </summary>
        public OperatorMode lastOperator;
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
        /// <summary>
        /// 模型浏览相机
        /// </summary>
        [SerializeField]
        private Camera defaultCamera;
        [HideInInspector]
        public UnityARCameraManager uARMF;
        private Menu3D menu3D;
        /// <summary>
        /// AR模式下灯光
        /// </summary>
        public Light arlight;
        /// <summary>
        /// 阴影灯光
        /// </summary>
        public Light shadelight;
        [SerializeField]
        private GameObject backGround;
        private bool isShowUI = false;
        private bool pointShow = false;
        public Dictionary<string, bool> onBecame = new Dictionary<string, bool>();
        /// <summary>
        /// mr模式下实例化出来的模型
        /// </summary>
        public GameObject obj;
        private void Awake()
        {
            canvas3D = Resources.Load<GameObject>(SystemDefineTag.canvas3D);

            canvas3D = Instantiate<GameObject>(canvas3D);
            canvas3D.transform.SetParent(transform);
            menu3D = canvas3D.GetComponentInChildren<Menu3D>();
            canvas3D.transform.localScale = new Vector3(0.003f, 0.003f, 0.003f);
            uARMF = GetComponentInChildren<UnityARCameraManager>();
            uARMF.cameraLocat += UpdateLocation;
            aRCamera = transform.Find("ARCamera").GetComponent<Camera>();
            camL.backgroundColor = Color.black;
            camR.backgroundColor = Color.black;
            BDL = camL.GetComponent<BarrelDistortion>();
            BDR = camR.GetComponent<BarrelDistortion>();
            BDL.BarrelDistortionFactor = 1.7f;
            BDR.BarrelDistortionFactor = 1.7f;

            OnInit();

        }
        /// <summary>
        /// 设置标记点是否显示
        /// </summary>
        public void SetPoint(string na, bool on)
        {
           // print(on);
            if (onBecame.ContainsKey(na))
                onBecame[na] = on;
            else
                onBecame.Add(na, on);
            if (on)
                closePanel = false;
            else
            {
                foreach (KeyValuePair<string, bool> item in onBecame)
                {
                    if (item.Value)
                    {
                        closePanel = false;
                        return;
                    }
                    closePanel = true;
                }
            }
        }
        public void OnInit()
        {
            switch (Global.OperatorModel)
            {
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
            Screen.orientation = ScreenOrientation.AutoRotation;
            arlight.gameObject.SetActive(false);
            camL.enabled = false;
            camR.enabled = false;
            canvas2D.SetActive(false);
            canvas3D.SetActive(false);
            defaultCamera.gameObject.SetActive(true);
            frame.gameObject.SetActive(false);
            shadelight.gameObject.SetActive(true);
            backGround.SetActive(true);
            TitleMenu.Instance.Open();
            BottomMenu.Instance.Open();
            UnityARSessionNativeInterface.GetARSessionNativeInterface().Pause();
            aRCamera.enabled = false;
        }
        public void IntoMR()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            arlight.gameObject.SetActive(true);
            camL.enabled = true;
            camR.enabled = true;
            canvas2D.SetActive(true);
            canvas3D.SetActive(true);
            defaultCamera.gameObject.SetActive(false);
            frame.gameObject.SetActive(true);
            shadelight.gameObject.SetActive(false);
            backGround.SetActive(true);
            TitleMenu.Instance.Close();
            BottomMenu.Instance.Close();
            aRCamera.enabled = true;
            UnityARSessionNativeInterface.GetARSessionNativeInterface().Run();
            // lastOperator = Global.OperatorModel;
        }
        public void IntoAR()
        {
            Screen.orientation = ScreenOrientation.AutoRotation;
            backGround.SetActive(false);
            arlight.gameObject.SetActive(true);
            shadelight.gameObject.SetActive(false);
            camL.enabled = false;
            camR.enabled = false;
            defaultCamera.gameObject.SetActive(false);
            canvas2D.SetActive(false);
            canvas3D.SetActive(false);
            frame.gameObject.SetActive(true);
            TitleMenu.Instance.Open();
            BottomMenu.Instance.Open();
            aRCamera.enabled = true;
            UnityARSessionNativeInterface.GetARSessionNativeInterface().Run();
        }
        public void Return()
        {
            Global.OperatorModel = lastOperator;
            Destroy(obj);
            //IntoOther();
        }
        private bool HitTestWithResultType(ARPoint point, ARHitTestResultType resultTypes)
        {
            List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface().HitTest(point, resultTypes);
            if (hitResults.Count > 0 && Global.OperatorModel != OperatorMode.MRModel)
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
            //if (Global.OperatorModel != OperatorMode.MRModel) return;
            UpdateFramt();
            if (canvas3D != null)
            {
                if (!menu3D.OnBecame && e.y <= 50)
                {
                    // print("q");
                    isShowUI = false;
                    canvas3D.transform.localPosition = new Vector3(p.x, p.y - 8f, p.z);
                    canvas3D.transform.eulerAngles = new Vector3(90, e.y, 0);
                }
                else if (!isShowUI)
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
            var screenPosition = aRCamera.ScreenToViewportPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
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
                    if (pointShow)
                    {
                        pointShow = false;
                        Tween t = frame.DOScale(0.6f, 0.5f).SetLoops(1);
                    }
                        
                    return;
                    
                }
                else
                {

                    frame.position = aRCamera.transform.TransformPoint(0, 0, 0.7f);
                    if (!pointShow)
                    {
                        Tween t = frame.DOScale(0.6f, 0.5f).SetLoops(-1, LoopType.Yoyo);
                        pointShow = true;
                    }
                   
                    //t.Loops();
                }
            }
        }
        void OnDestroy()
        {
            instance = null;
        }
    }
}