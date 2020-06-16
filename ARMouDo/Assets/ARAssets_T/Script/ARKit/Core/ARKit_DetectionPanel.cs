using CacheDarta_T;
using System;
using System.Collections;
using System.Collections.Generic;
using Tools_XYRF;
using UnityEngine;
using UnityEngine.XR.iOS;
using XYRF_FingerGesture;

namespace ARKit_T
{
    public class OnLineCacheData
    {
        public Vector3 position;
        public Vector3 rotation;
    }
    /// <summary>
    /// ARKit 输入
    /// </summary>
    public class ARKit_DetectionPanel
    {
        /// <summary>
        /// 是否刷新 帧/此
        /// </summary>
        private bool frameUpdated = false;
        /// <summary>
        /// 粒子特效 点云
        /// </summary>
        private ParticleSystem currentPS;

        #region 私有参数
        private Vector3 ScreenCenter = Vector3.zero;
        private ARPoint point;
        private ARHitTestResultType[] resultTypes = new ARHitTestResultType[4];
        #endregion //结束...


        public CallBack<Vector3> pointBack;
        #region 实例对象到ARKit场景
        /// <summary>
        /// 实例模型
        /// </summary>
        /// <param name="goo">已经实例到场景的 模型</param>
        public void ARKit_ExempleMode(Transform goo)
        {
            Global.currentSelectObjcetChild = goo.gameObject;
            GameObject obj = new GameObject();
            obj.transform.position = goo.position;
            goo.parent = obj.transform;
            goo.localPosition = Vector3.zero;
            //goo.localEulerAngles = Vector3.zero;
           // if (ARKit_OnLineCacheData.Instance.isOpenMuchTra)  //可生成对个对象
           // {
               // if (!ARKit_OnLineCacheData.Instance.ARKit_Transform.ContainsKey(obj.name))
               // {
                   // obj.transform.position = ARKit_OnLineCacheData.Instance.promptPanelPos;
                   // ARKit_OnLineCacheData.Instance.ARKit_Transform.Add(obj.name, obj.gameObject);
               // }
               // else
                  //  ARKit_OnLineCacheData.Instance.ARKit_Transform[obj.name].transform.position = ARKit_OnLineCacheData.Instance.promptPanelPos;
               // Global.currentSelectObjectFather = obj.transform.gameObject;
           // }
           // else
          //  {
                //阴影面板 初始化
                if (Global.planeShadowTra != null)
                    Global.planeShadowTra.transform.parent = null;
                if (Global.currentSelectObjectFather != obj.gameObject)
                    UnityEngine.GameObject.Destroy(Global.currentSelectObjectFather);
                Global.currentSelectObjectFather = obj;
                //obj.transform.position = ARKit_OnLineCacheData.Instance.promptPanelPos;
                //    goo.rotation = ARKit_OnLineCacheData.Instance.promptPaneQua;  //尝试 设置方向
                //设置 模型初始朝向
               // obj.transform.LookAt(Camera.main.transform);
               // obj.transform.localEulerAngles = new Vector3(0f, obj.transform.localEulerAngles.y - 180, obj.transform.localEulerAngles.z);

                ARKit_ExempleMode_Init(obj.transform);   //初始化模型信息
          //  }

            EventComeBack_T.ScreenHaveObjs();
        }
        /// <summary>
        /// 初始化 实例的模型
        /// </summary>
        /// <param name="goo"></param>
        private void ARKit_ExempleMode_Init(Transform goo)
        {
            //添加标签和层
            //   goo.gameObject.layer = Global.modelLayerMask;
            goo.gameObject.tag = Global.modelTag;

            //添加 模型是否在屏幕前面 检测
           // if (!goo.GetComponent<Collider>() && !goo.GetChild(0).GetComponent<ScreenHaveMode>())
               // goo.GetChild(0).gameObject.AddComponent<ScreenHaveMode>();
          //  else
                //goo.gameObject.AddComponent<ScreenHaveMode>();
            //输入检测
            if (!goo.GetComponent<ARKit_Input>())
                goo.gameObject.AddComponent<ARKit_Input>().UpdataData();
            else
                goo.gameObject.GetComponent<ARKit_Input>().UpdataData();

            //地形面板位置 初始化
            if (Global.planeTerrainTra == null)
                Global.planeTerrainTra = ResourcesLod_T.ResourcesLodObj(Global.planeTerrainYZJname);
            else
                Global.planeTerrainTra.transform.position = goo.position;
            //阴影面板 初始化
            if (Global.planeShadowTra == null)
            {
                Global.planeShadowTra = ResourcesLod_T.ResourcesLodObj(Global.planeShadowYZJname, goo);
                Global.planeShadowTra.transform.localPosition = Vector3.zero;
            }
            else
            {
                Global.planeShadowTra.transform.parent = Global.currentSelectObjectFather.transform;
                Global.planeShadowTra.transform.localPosition = Vector3.zero;
                Global.planeShadowTra.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }

        /// <summary>
        /// 模型浏览器 → AR场景
        /// </summary>
        /// <param name="goo">已经实例到场景的 模型</param>
        public void ARKit_ModelBrowserToARScene(Transform goo)
        {
            if (goo != null)
            {
                Global.currentSelectObjcetChild = goo.gameObject;
                if (goo.GetComponent<ARKit_Input>())
                    GameObject.Destroy(goo.GetComponent<ARKit_Input>());
                if (goo.GetComponent<FingerGesture_T>())
                    GameObject.Destroy(goo.GetComponent<FingerGesture_T>());

                GameObject targetPar = new GameObject();
                //父级 至空
                goo.parent = null;
                targetPar.transform.position = goo.position;
                targetPar.transform.localEulerAngles = goo.localEulerAngles;

                goo.parent = targetPar.transform;
                goo.localPosition = Vector3.zero;
                goo.localEulerAngles = Vector3.zero;

                Global.currentSelectObjectFather = targetPar;
                ARKit_ExempleMode_Init(targetPar.transform);
            }
        }
        #endregion //实例对象到ARKit场景结束...

        #region 平面识别...

        /// <summary>
        /// 平面 识别 (识别到了 则生成提示框)
        /// </summary>
        private void DetectionPanel(Vector3 ScreenCenter)
        {
            var screenPosition = Camera.main.ScreenToViewportPoint(ScreenCenter);
            point = new ARPoint
            {
                x = screenPosition.x,
                y = screenPosition.y
            };
            for (int i = 0; i < resultTypes.Length; i++)
            {
                if (HitTestWithResultType(point, resultTypes[i]))
                {
                    return;
                }
            }
        }
        private bool HitTestWithResultType(ARPoint point, ARHitTestResultType resultType)
        {
            List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface().HitTest(point, resultType);
            if (hitResults.Count > 0)   //识别到 平面了
            {
                if (ARKit_OnLineCacheData.Instance.callBack != null)
                    ARKit_OnLineCacheData.Instance.callBack(true);   // 识别到平面了

                //1.随机数 不遍历
                //int range = UnityEngine.Random.Range(0, hitResults.Count - 1);  //随机数
                //if (pointBack != null)
                //    pointBack(UnityARMatrixOps.GetPosition(hitResults[range].worldTransform));
                //ARKit_OnLineCacheData.Instance.promptPanelPos = UnityARMatrixOps.GetPosition(hitResults[range].worldTransform);  //获取 位置
                //ARKit_OnLineCacheData.Instance.promptPaneQua = UnityARMatrixOps.GetRotation(hitResults[range].worldTransform);  //获取 方向
                ////显示 提示框
                //ARKit_OnLineCacheData.Instance.promptPanelTra.SetActive(ARKit_OnLineCacheData.Instance.isOpenPromptPanel);
                ////匹配 位置
                //ARKit_OnLineCacheData.Instance.promptPanelTra.transform.position = ARKit_OnLineCacheData.Instance.promptPanelPos;

                //2.遍历处理
                for (int i = 0; i < hitResults.Count; i++)
                {

                    if (pointBack != null)
                        pointBack(UnityARMatrixOps.GetPosition(hitResults[i].worldTransform));
                    ARKit_OnLineCacheData.Instance.promptPanelPos = UnityARMatrixOps.GetPosition(hitResults[i].worldTransform);  //获取 位置
                    ARKit_OnLineCacheData.Instance.promptPaneQua = UnityARMatrixOps.GetRotation(hitResults[i].worldTransform);  //获取 方向

                    //显示 提示框
                    Global.promptPanelTra.SetActive(ARKit_OnLineCacheData.Instance.isOpenPromptPanelCurrent);
                    //匹配 位置
                    Global.promptPanelTra.transform.position = ARKit_OnLineCacheData.Instance.promptPanelPos;
                    return true;
                }
            }
            else
            {
                // if (ARKit_OnLineCacheData.Instance.callBack != null)
                //   ARKit_OnLineCacheData.Instance.callBack(false);
            }
            return false;
        }



        #endregion //平面识别结束...

        #region 点云识别...
        /// <summary>
        /// 初始化 点云识别
        /// </summary>
        public void Init_DetectionPointClound()
        {
            UnityARSessionNativeInterface.ARFrameUpdatedEvent += ARFrameUpdated;
            currentPS = ResourcesLod_T.ResourcesLodPar(Global.pointCloudParticlePrefabYZJPath, "ParticlePrefab");
            Global.particlePrefab = currentPS.gameObject;
            if (!ARKit_OnLineCacheData.Instance.isOpenPointClound)
                Global.particlePrefab.SetActive(false);
            frameUpdated = false;
        }
        private void ARFrameUpdated(UnityARCamera camera)
        {
            ARKit_OnLineCacheData.Instance.m_PointCloudData = camera.pointCloudData;
            frameUpdated = true;
        }
        /// <summary>
        /// 点云 识别
        /// </summary>
        private void DetectionPointClound()
        {
            if (frameUpdated && ARKit_OnLineCacheData.Instance.isOpenPointClound)
            {
                if (ARKit_OnLineCacheData.Instance.m_PointCloudData != null && ARKit_OnLineCacheData.Instance.m_PointCloudData.Length > 0)
                {
                    int numParticles = Mathf.Min(ARKit_OnLineCacheData.Instance.m_PointCloudData.Length, 10000);
                    ParticleSystem.Particle[] particles = new ParticleSystem.Particle[numParticles];
                    int index = 0;
                    foreach (Vector3 currentPoint in ARKit_OnLineCacheData.Instance.m_PointCloudData)
                    {
                        particles[index].position = currentPoint;
                        particles[index].startColor = new Color(1.0f, 1.0f, 1.0f);
                        particles[index].startSize = 0.01f;
                        index++;
                    }
                    currentPS.SetParticles(particles, numParticles);
                }
                else
                {
                    ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1];
                    particles[0].startSize = 0.0f;
                    currentPS.SetParticles(particles, 1);
                }
                frameUpdated = false;
            }
        }
        #endregion //点云识别结束...



        /// <summary>
        /// ARKit 检测(帧/一次)
        /// </summary>
        public void ARKit_DetectionPanelUpdate()
        {
            DetectionPanel(ScreenCenter);  //检测平面
            DetectionPointClound();  //点云检测 显示...           
        }

        private ARKit_DetectionPanel()
        {
            ScreenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
            resultTypes[0] = ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent;  /// **结果类型与现有飞机锚点相交，考虑到飞机的程度。
            resultTypes[2] = ARHitTestResultType.ARHitTestResultTypeHorizontalPlane;   /// **结果类型从检测和交叉新的水平面。
            resultTypes[3] = ARHitTestResultType.ARHitTestResultTypeExistingPlane;   /// **结果类型与现有平面锚相交。
            resultTypes[1] = ARHitTestResultType.ARHitTestResultTypeVerticalPlane;  /// **结果类型从检测和相交新的垂直平面。
                                                                                    ///  ARHitTestResultTypeFeaturePoint  **结果类型与最近的特征点相交。
        }
        private static ARKit_DetectionPanel instance;
        public static ARKit_DetectionPanel Instance
        {
            get
            {
                if (instance == null)
                    instance = new ARKit_DetectionPanel();
                return instance;
            }
            set { instance = value; }
        }
    }
}
