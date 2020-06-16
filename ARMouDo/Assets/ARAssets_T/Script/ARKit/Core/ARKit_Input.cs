using Consolation;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Tools_XYRF;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.iOS;
using XYRF_FingerGesture;


namespace ARKit_T
{
    /// <summary>
    /// 检测输入
    /// </summary>
    [RequireComponent(typeof(FingerGesture_T))]
    public class ARKit_Input : MonoBehaviour
    {

        #region AR场景 参数
        /// <summary>
        /// 模型移动 增量 差
        /// </summary>
        private Vector3 increment;
        /// <summary>
        /// 缩放倍率
        /// </summary>
        private Vector3 loatScale_ = Vector3.zero;
        /// <summary>
        /// 最小缩放比例
        /// </summary>
        private float minScale = 0.01f;
        /// <summary>
        /// 旋转速率
        /// </summary>
        private Vector3 loatRot = Vector3.zero;
        /// <summary>
        /// AR模型 移动速度
        /// </summary>
        private float ARModeMoveSpeed = 1f;
        /// <summary>
        /// AR模型 旋转速度
        /// </summary>
        private float ARModeRotSpeed = 7f;
        /// <summary>
        /// AR模型 移动 目标位置
        /// </summary>
        private Vector3 targetPos = Vector3.zero;
        /// <summary>
        /// 射线选取的 目标
        /// </summary>
        private Transform targetTra = null;
        #endregion

        #region 模型浏览器 私有参数

        /// <summary>
        /// 是否 按下 
        /// </summary>
        private bool isDown = false;
        /// <summary>
        /// 是否 检测到平面
        /// </summary>
        private bool isDetectionPanel = false;
        /// <summary>
        /// 模型浏览器 模型移动 增量
        /// </summary>
        private Vector3 distance;
        /// <summary>
        /// 单指水平移动距离
        /// </summary>
        float singleFingerHMoveDistance = 0;
        /// <summary>
        /// 单指垂直移动距离
        /// </summary>
        float singleFingerVMoveDistance = 0;
        /// <summary>
        /// 双指间距
        /// </summary>
        float doubleFingerDistance = 0;
        /// <summary>
        /// 模型世界坐标转为屏幕坐标
        /// </summary>
        Vector3 screenPoint;
        /// <summary>
        /// 模型世界坐标
        /// </summary>
        Vector3 worldPosition;

        #region  相机控制参数
        private Transform curCameraTrans;
        private Transform targetObject;
        private Vector3 targetOffset;
        private float averageDistance = 0f;
        private float maxDistance = 16;
        private float minDistance = .3f;
        private float xSpeed = 200.0f;
        private float ySpeed = 200.0f;
        private int yMinLimit = -10;
        private int yMaxLimit = 80;
        private int zoomSpeed = 40;
        private float zoomDampening = 5.0f;

        private float xDeg = 0.0f;
        private float yDeg = 0.0f;
        private float currentDistance;
        private float desiredDistance;
        private Quaternion currentRotation;
        private Quaternion desiredRotation;
        private Quaternion rotation;
        private Vector3 position;
        #endregion

        #endregion

        private void OnEnable()
        {
            FingerEventComeBack_T.OnFingerGestureChange += FingerEventComeBack_T_OnFingerGestureChange;
        }
        private void OnDisable()
        {
            FingerEventComeBack_T.OnFingerGestureChange -= FingerEventComeBack_T_OnFingerGestureChange;
        }
        private void Start()
        {
            Init();
            float loat_ = (transform.localScale.x / 100f) * 12f;
            loatScale_ = new Vector3(loat_, loat_, loat_);
            loatRot = new Vector3(0, 16f, 0);
            minScale *= 0.06f;
        }
        public void UpdataData()
        {

            isDetectionPanel = false;
            isDown = false;
            if (ARKit_OnLineCacheData.Instance != null)
                ARKit_OnLineCacheData.Instance.callBack = CallBack;
        }
        private void CallBack(bool b)
        {
            if (b)
            {
                isDetectionPanel = true;
            }
        }
        private void FingerEventComeBack_T_OnFingerGestureChange(FingerGestureResult_T obj)
        {
            //数据赋值
            singleFingerHMoveDistance = FingerGestureVariable_T.Instance.singleFingerHMoveDistance;
            singleFingerVMoveDistance = FingerGestureVariable_T.Instance.singleFingerVMoveDistance;
            doubleFingerDistance = FingerGestureVariable_T.Instance.doubleFingerDistance;


            //DebugConsole.Log("手势状态:" + obj.GetFingerGestureState.ToString(), "warning");  //打印 测试
            //DebugConsole.Log("双指距离:" + doubleFingerDistance, "error");  //打印 测试

            switch (obj.GetFingerGestureState)
            {
                case FingerGestureEnum_T.None:  //无手势
                    break;
                case FingerGestureEnum_T.TouchClickOne:   // 单手指 单击
                    if (Global.OperatorModel == OperatorMode.ARMode)
                        ARKitModeMoves();
                    break;
                case FingerGestureEnum_T.TouchUpDragOne:  //单手指 上拖拽
                    if (Global.OperatorModel == OperatorMode.ARMode)
                    {
                        switch (Global.currentScreenDirection)
                        {
                            case UI_XYRF.ScreenDirection.verticalTo:
                                Vector3 riEuler = transform.localEulerAngles - loatRot;
                                transform.DORotate(riEuler, 0.3f).SetEase(Ease.OutQuart);
                                break;
                            case UI_XYRF.ScreenDirection.verticalBack:
                                Vector3 leEuler = transform.localEulerAngles + loatRot;
                                transform.DORotate(leEuler, 0.3f).SetEase(Ease.OutQuart);
                                break;
                        }
                    }
                    else
                        CameraRotateControl();
                    break;
                case FingerGestureEnum_T.TouchDownDragOne:  //单手指 下拖拽
                    if (Global.OperatorModel == OperatorMode.ARMode)
                    {
                        switch (Global.currentScreenDirection)
                        {
                            case UI_XYRF.ScreenDirection.verticalTo:
                                Vector3 leEuler = transform.localEulerAngles + loatRot;
                                transform.DORotate(leEuler, 0.3f).SetEase(Ease.OutQuart);
                                break;
                            case UI_XYRF.ScreenDirection.verticalBack:
                                Vector3 riEuler = transform.localEulerAngles - loatRot;
                                transform.DORotate(riEuler, 0.3f).SetEase(Ease.OutQuart);
                                break;
                        }
                    }
                    else
                        CameraRotateControl();
                    break;
                case FingerGestureEnum_T.TouchLeftDragOne:  //单手指 左拖拽
                    if (Global.OperatorModel == OperatorMode.ARMode)
                    {
                        switch (Global.currentScreenDirection)
                        {
                            case UI_XYRF.ScreenDirection.horizontalLeft:
                                Vector3 leEuler = transform.localEulerAngles + loatRot;
                                transform.DORotate(leEuler, 0.3f).SetEase(Ease.OutQuart);
                                break;
                            case UI_XYRF.ScreenDirection.horizontalRight:
                                Vector3 riEuler = transform.localEulerAngles - loatRot;
                                transform.DORotate(riEuler, 0.3f).SetEase(Ease.OutQuart);
                                break;
                        }
                    }
                    else
                        CameraRotateControl();
                    break;
                case FingerGestureEnum_T.TouchRightDragOne:  //单手指 右拖拽
                    if (Global.OperatorModel == OperatorMode.ARMode)
                    {
                        switch (Global.currentScreenDirection)
                        {
                            case UI_XYRF.ScreenDirection.horizontalLeft:
                                Vector3 riEuler = transform.localEulerAngles - loatRot;
                                transform.DORotate(riEuler, 0.3f).SetEase(Ease.OutQuart);
                                break;
                            case UI_XYRF.ScreenDirection.horizontalRight:
                                Vector3 leEuler = transform.localEulerAngles + loatRot;
                                transform.DORotate(leEuler, 0.3f).SetEase(Ease.OutQuart);
                                break;
                        }
                    }
                    else
                        CameraRotateControl();

                    break;
                case FingerGestureEnum_T.TouchDrawOutTwo:  //双手指 分散
                    if (Global.OperatorModel == OperatorMode.ARMode)
                    {
                        loatScale_ = new Vector3(transform.localScale.x / 10, transform.localScale.y / 10, transform.localScale.z / 10);
                        Vector3 lox = transform.localScale + loatScale_;
                        transform.DOScale(lox, 0.4f).SetEase(Ease.OutQuart);
                        //计算 增量
                        ARDragDown();
                    }
                   else
                        CameraZoomInControl();
                    break;
                case FingerGestureEnum_T.TouchDrawInTwo:  //双手指 靠近
                    if (Global.OperatorModel == OperatorMode.ARMode)
                    {
                        loatScale_ = new Vector3(transform.localScale.x / 10, transform.localScale.y / 10, transform.localScale.z / 10);
                        Vector3 lom = transform.localScale - loatScale_;
                        if (lom.x <= minScale)
                            lom = new Vector3(minScale, minScale, minScale);
                        transform.DOScale(lom, 0.4f).SetEase(Ease.OutQuart);

                        ARDragDown();
                    }
                    else
                        CameraZoomOutControl();
                    break;
                case FingerGestureEnum_T.TouchDownTwo:  //双手指 按下
                                                        //计算 增量
                    if (Global.OperatorModel == OperatorMode.ARMode)
                        ARDragDown();
                    else
                        prefabDown();
                    break;
                case FingerGestureEnum_T.TouchDragTwo:  //双手指 拖拽
                    if (Global.OperatorModel == OperatorMode.ARMode)
                        ARDragMove();
                    else
                        PrefabMove();
                    break;
                case FingerGestureEnum_T.TouchModeClickOne:   //单手指 点击对象
                    if (Global.OperatorModel == OperatorMode.ARMode)
                        ARKiAnimation_T.Instance.PlayModeAnimation(ARAnimaitonType.Random);
                    break;
                case FingerGestureEnum_T.TouchModeDownOne:  //单手指 按下对象
                    break;
                case FingerGestureEnum_T.TouchModeDragTwo:   //双手指 拖拽对象
                    if (Global.OperatorModel == OperatorMode.ARMode)
                        ARDragMove();
                    break;
                case FingerGestureEnum_T.TouchLeftModeDragOne:
                    if (Global.OperatorModel == OperatorMode.ARMode)
                    {
                        Vector3 leModeEuler = transform.localEulerAngles + loatRot;
                        transform.DORotate(leModeEuler, 0.3f).SetEase(Ease.OutQuart);
                    }
                    else
                        CameraRotateControl();

                    break;
                case FingerGestureEnum_T.TouchRightModeDragOne:
                    if (Global.OperatorModel == OperatorMode.ARMode)
                    {
                        Vector3 riModeEuler = transform.localEulerAngles - loatRot;
                        transform.DORotate(riModeEuler, 0.3f).SetEase(Ease.OutQuart);
                    }
                    else
                        CameraRotateControl();

                    break;
                case FingerGestureEnum_T.TouchUpModeDragOne:
                case FingerGestureEnum_T.TouchDownModeDragOne:
                    if (Global.OperatorModel == OperatorMode.BrowserMode)
                        CameraRotateControl();

                    break;
            }
        }
        #region AR场景 模型移动算法[X,Z]
        /// <summary>
        /// 模型拖拽移动 按下 计算增量
        /// </summary>
        private void ARDragDown()
        {
            targetTra = RaySystemTarget.GEtRayTargetTra(Input.mousePosition, Global.planeTerrainTag, Global.planeTerrainLayerMask);
            if (targetTra != null)  //存在地形
            {
                targetPos = RaySystemTarget.GetRayTargetPoint(Input.mousePosition, Global.planeTerrainTag, Global.planeTerrainLayerMask);
                increment = transform.position - targetPos;
            }
        }
        /// <summary>
        /// 模型移动
        /// </summary>
        private void ARDragMove()
        {
            targetTra = RaySystemTarget.GEtRayTargetTra(Input.mousePosition, Global.planeTerrainTag, Global.planeTerrainLayerMask);
            if (targetTra != null)  //存在地形
            {
                targetPos = RaySystemTarget.GetRayTargetPoint(Input.mousePosition, Global.planeTerrainTag, Global.planeTerrainLayerMask);
                transform.DOMove(targetPos + increment, 0.2f).SetEase(Ease.OutQuart);
            }
        }


        /// <summary>
        /// 模型 点击移动
        /// </summary>
        private void ARKitModeMoves()
        {
            targetTra = RaySystemTarget.GEtRayTargetTra(Input.mousePosition, Global.planeTerrainTag, Global.planeTerrainLayerMask);
            if (targetTra != null)  //选中地形
            {
                //计算目标点
                targetPos = RaySystemTarget.GetRayTargetPoint(Input.mousePosition, Global.planeTerrainTag, Global.planeTerrainLayerMask);
                if (ARKiAnimation_T.Instance.JudeModeAnimation(ARAnimaitonType.Move))  //存在动画 才移动
                {
                    Global.currentSelectObjcetChild.transform.parent = null;
                    Global.planeShadowTra.transform.parent = Global.currentSelectObjcetChild.transform;
                    CancelInvoke("ARKitModeMove");
                    ARKiAnimation_T.Instance.PlayModeAnimation(ARAnimaitonType.Move);
                    InvokeRepeating("ARKitModeMove", 0f, 0.02f);

                }
            }
        }
        private void ARKitModeMove()
        {
            Vector3 targetDir = targetPos - Global.currentSelectObjcetChild.transform.position;

            Vector3 newDir = Vector3.RotateTowards(Global.currentSelectObjcetChild.transform.forward, targetDir, ARModeRotSpeed * Time.deltaTime, 0.0f);
            Global.currentSelectObjcetChild.transform.rotation = Quaternion.LookRotation(newDir);
            Global.currentSelectObjcetChild.transform.position = Vector3.MoveTowards(Global.currentSelectObjcetChild.transform.position, targetPos, transform.localScale.x * Time.deltaTime);
            if (Vector3.Distance(Global.currentSelectObjcetChild.transform.position, targetPos) <= 0f)
            {
                ARKiAnimation_T.Instance.PlayModeAnimation(ARAnimaitonType.Idle);
                Global.currentSelectObjectFather.transform.position = Global.currentSelectObjcetChild.transform.position;
                Global.currentSelectObjcetChild.transform.parent = Global.currentSelectObjectFather.transform;
                Global.currentSelectObjcetChild.transform.localPosition = Vector3.zero;
                Global.planeShadowTra.transform.parent = Global.currentSelectObjectFather.transform;
                Global.planeShadowTra.transform.localPosition = Vector3.zero;
                CancelInvoke("ARKitModeMove");
            }
        }

        /// <summary>
        /// 获取模型 转向角度
        /// </summary>
        /// <param name="tra">待 移动的Transform</param>
        /// <param name="targetPos">目标点</param>
        /// <returns>旋转角度</returns>
        private Vector3 GetLookAtEuler(Transform tra, Vector3 targetPos)
        {
            //计算物体在 朝向某个向量后的正前方
            Vector3 forwardDir = targetPos - tra.position;
            //计算朝向这个正前方时的物体四元素值
            Quaternion lookAtRot = Quaternion.LookRotation(forwardDir);
            //把四元素值 转换成 角度
            Vector3 resultEuler = lookAtRot.eulerAngles;
            return resultEuler;
        }
        #endregion

        #region 模型浏览器 移动[X,Y]
        /// <summary>
        /// 按下时 计算坐标增量
        /// </summary>
        private void prefabDown()
        {
            screenPoint = Global.camera.WorldToScreenPoint(transform.position);
            worldPosition = Global.camera.ScreenToWorldPoint(new Vector3(Input.GetTouch(1).position.x, Input.GetTouch(1).position.y, screenPoint.z));
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(1).fingerId))
                distance = transform.position - worldPosition;
        }
        /// <summary>
        /// 拖拽时 移动
        /// </summary>
        private void PrefabMove()
        {
            worldPosition = Global.camera.ScreenToWorldPoint(new Vector3(Input.GetTouch(1).position.x, Input.GetTouch(1).position.y, screenPoint.z));
            Vector3 curPosition = worldPosition + distance;
            //移动范围判定
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(1).fingerId))
                //if (((Mathf.Abs(transform.position.x) > Mathf.Abs(averageDistance / 2)) || (Mathf.Abs(transform.position.y) > Mathf.Abs(averageDistance / 2)))
                //    && ((Mathf.Abs(worldPosition.x + transform.position.x) > Mathf.Abs(averageDistance / 2)) || (Mathf.Abs(worldPosition.y + transform.position.y) > Mathf.Abs(averageDistance / 2))))
                //    return;
                //else
                //    //transform.position = worldPosition + distance;
                transform.DOMove(curPosition, .3f).SetEase(Ease.OutQuart);
        }

        /// <summary>
        /// 相机初始化
        /// </summary>
        public void Init()
        {
            if (Global.OperatorModel == OperatorMode.BrowserMode)
            {
                curCameraTrans = Global.camera.transform;
                averageDistance = vPlace_zpc.ModelControl.GetInstance().model.transform.localPosition.z;
                maxDistance = averageDistance;
                targetObject = vPlace_zpc.ModelControl.GetInstance().modelParent.transform;
                targetOffset = new Vector3(targetObject.localPosition.x, targetObject.localPosition.y, 0);

                if (!targetObject)
                {
                    GameObject go = new GameObject("Cam Target");
                    go.transform.position = curCameraTrans.position + (curCameraTrans.forward * averageDistance);
                    targetObject = go.transform;
                }
                currentDistance = averageDistance;
                desiredDistance = averageDistance;

                position = curCameraTrans.position;
                rotation = curCameraTrans.rotation;
                currentRotation = curCameraTrans.rotation;
                desiredRotation = curCameraTrans.rotation;

                xDeg = Vector3.Angle(Vector3.right, curCameraTrans.right);
                yDeg = Vector3.Angle(Vector3.up, curCameraTrans.up);
                position = targetObject.position - (rotation * Vector3.forward * currentDistance + targetOffset);
            }
        }

        /// <summary>
        ///相机旋转控制
        /// </summary>
        private void CameraRotateControl()
        {
            //PC端
            //xDeg += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            //yDeg -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            //yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit);

            //手机端
            xDeg += singleFingerHMoveDistance * xSpeed * 0.02f;
            yDeg -= singleFingerVMoveDistance * ySpeed * 0.02f;
            yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit);

            desiredRotation = Quaternion.Euler(yDeg, xDeg, 0);

            //公转
            //xDeg = singleFingerHMoveDistance * xSpeed * 0.02f;
            //yDeg = singleFingerVMoveDistance * ySpeed * 0.02f;
            //yDegChange += yDeg;
            //if (yDegChange < -80)
            //    yDegChange = -80;
            //if (yDegChange > 80)
            //    yDegChange = 80;

            //curCameraTrans.transform.RotateAround(targetObject.localPosition, curCameraTrans.transform.up, xDeg);
            //if (yDegChange > -80 && yDegChange < 80)
            //    curCameraTrans.transform.RotateAround(targetObject.localPosition, -curCameraTrans.transform.right, yDeg);
        }

        /// <summary>
        /// 相机拉近
        /// </summary>
        private void CameraZoomInControl()
        {
            //desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * 0.02f * zoomSpeed * Mathf.Abs(desiredDistance);
            desiredDistance -= doubleFingerDistance * 0.02f * zoomSpeed * Mathf.Abs(desiredDistance);
        }

        /// <summary>
        /// 相机拉远
        /// </summary>
        private void CameraZoomOutControl()
        {
            //desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * 0.02f * zoomSpeed * Mathf.Abs(desiredDistance);
            desiredDistance += doubleFingerDistance * 0.02f * zoomSpeed * Mathf.Abs(desiredDistance);
        }

        /// <summary>
        /// 相机位置及旋转差值计算
        /// </summary>
        public void CameraPositionControl()
        {
            if (Global.OperatorModel == OperatorMode.BrowserMode)
            {
                currentRotation = Global.camera.transform.rotation;
                rotation = Quaternion.Lerp(currentRotation, desiredRotation, 0.02f * zoomDampening);
                curCameraTrans.rotation = rotation;

                desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
                currentDistance = Mathf.Lerp(currentDistance, desiredDistance, 0.02f * zoomDampening);
                position = targetObject.position - (rotation * Vector3.forward * currentDistance + targetOffset);
                curCameraTrans.position = position;
            }
        }

        /// <summary>
        /// 角度转换
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360)
                angle += 360;
            if (angle > 360)
                angle -= 360;
            return Mathf.Clamp(angle, min, max);
        }


        private void ModelXRotate(float value)
        {
            Vector3 rotationXEulerangles = transform.localEulerAngles - new Vector3(value, 0, 0);
            if (rotationXEulerangles.x < -360)
                rotationXEulerangles.x += 360;
            if (rotationXEulerangles.x > 360)
                rotationXEulerangles.x -= 360;
            if (rotationXEulerangles.x < 80 && rotationXEulerangles.x > -10)
            {
                transform.DOLocalRotate(rotationXEulerangles, .4f).SetEase(Ease.OutQuart);
            }
        }

        private void ModelYRotate(float value)
        {
            Vector3 rotationYEulerangles = transform.localEulerAngles - new Vector3(0, value, 0);

            transform.DOLocalRotate(rotationYEulerangles, .4f).SetEase(Ease.OutQuart);
        }

        private void ModelScale()
        {

        }
        #endregion
    }

    #region ARKit 空间移动算法[弃用, 坐标点点不全]
    ///// <summary>
    ///// 平面识别 点获取
    ///// </summary>
    //private void DetectionInptuTouch(ARkit_MoveWay way)
    //{
    //    if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))  //未触摸UI
    //    {
    //        //   拖拽移动 
    //        var touch = Input.GetTouch(0);
    //        // 触屏未移动 || 触屏移动
    //        if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
    //        {
    //            var screenPosition = Global.camera.ScreenToViewportPoint(touch.position);
    //            ARPoint point = new ARPoint
    //            {
    //                x = screenPosition.x,
    //                y = screenPosition.y
    //            };
    //            ARHitTestResultType[] resultTypes = {
    //                 ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
    //                // if you want to use infinite planes use this:
    //                //ARHitTestResultType.ARHitTestResultTypeExistingPlane,
    //                ARHitTestResultType.ARHitTestResultTypeHorizontalPlane,
    //                ARHitTestResultType.ARHitTestResultTypeFeaturePoint
    //                };
    //            foreach (ARHitTestResultType resultType in resultTypes)
    //            {
    //                if (CalculateModeMovePos(point, resultType, way))
    //                {
    //                    return;
    //                }
    //            }
    //        }
    //    }
    //}
    ///// <summary>
    ///// 计算模型 移动
    ///// </summary>
    //private bool CalculateModeMovePos(ARPoint point, ARHitTestResultType resultType, ARkit_MoveWay way)
    //{
    //    List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface().HitTest(point, resultType);
    //    if (hitResults.Count > 0)
    //    {
    //        foreach (var hitResult in hitResults)
    //        {
    //            aRkit_Vector3.ARkit_DetectionPanelPos = UnityARMatrixOps.GetPosition(hitResult.worldTransform);
    //            switch (way)
    //            {
    //                case ARkit_MoveWay.DragMove:    //触屏 拖拽移动
    //                                                // 触屏开始  计算坐标增量
    //                    if (Input.GetTouch(0).phase == TouchPhase.Began)
    //                        increment = UnityARMatrixOps.GetPosition(hitResult.worldTransform) - transform.position;
    //                    if (Input.GetTouch(0).phase == TouchPhase.Moved)
    //                        transform.position = UnityARMatrixOps.GetPosition(hitResult.worldTransform) - increment;
    //                    break;
    //                case ARkit_MoveWay.ClickMove:    //触屏 点击移动

    //                    break;
    //            }
    //            return true;
    //        }
    //    }
    //    return false;
    //}
    #endregion
}
