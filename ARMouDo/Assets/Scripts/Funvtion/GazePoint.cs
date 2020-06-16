/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using DG.Tweening;
using MoDouAR;

namespace PlaceAR
{
    /// <summary>
    /// 焦点事件结构体
    /// </summary>
    public struct PointArgs
    {
        //标记
        public uint flags;
        //距离
        public float distance;
        //目标
        public Transform target;
    }
    /// <summary>
    /// 注视点
    /// </summary>
    public class GazePoint : MonoBehaviour
    {
        private static GazePoint instance;
        public static GazePoint Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GazePoint>();
                }

                return instance;
            }
        }

        RaycastHit hit;
        /// <summary>
        /// 作用的层
        /// </summary>
        public LayerMask layer;

        public float rayDistance = 1000;
        /// <summary>
        /// 点击的obj
        /// </summary>
        private Transform point;
        public Image image;
        /// <summary>
        /// 间隔时间
        /// </summary>
        public float intervalTime = 0.6f;
        private float time;
        private float pointz;
        public CallBack<PointArgs> eyePointBegin;
        public CallBack<PointArgs> eyePointEnd;
        public CallBack<PointArgs> eyePointOut;
        // Use this for initialization
        void Start()
        {
            //point.transform.position = Vector3.zero;
        }
        void Update()
        {
            if (Global.OperatorModel != OperatorMode.MRModel) return;
            Ray raycast = new Ray(ARKitControl.Instance.eyePoint.position, ARKitControl.Instance.eyePoint.forward);
            bool bHit = Physics.Raycast(raycast, out hit, rayDistance, layer);

            if (point && point != hit.transform)
            {
                PointArgs args = new PointArgs();
                args.distance = 0f;
                args.flags = 0;
                args.target = point;
                OnPointerOut(args);
                point = null;
            }
            if (bHit )
            {
                Debug.DrawLine(ARKitControl.Instance.eyePoint.position, hit.point, Color.red);
                if (hit.transform.GetComponent<Button>())
                {
                    if (point != hit.transform)
                    {
                        PointArgs argsIn = new PointArgs();
                        argsIn.distance = hit.distance;
                        argsIn.flags = 0;
                        argsIn.target = hit.transform;
                        time = intervalTime;
                        image.fillAmount = 0;
                        OnPointerIn(argsIn);
                        point = hit.transform;
                    }
                    if (time >= 0)
                    {
                        image.fillAmount += 1 / (intervalTime * 50);
                        time -= 0.02f;
                        if (time <= 0)
                        {
                            PointArgs argsClick = new PointArgs();
                            argsClick.distance = hit.distance;
                            argsClick.flags = 0;
                            argsClick.target = hit.transform;
                            OnPointerClick(argsClick);                   
                        }
                    }
                }
                else
                {
                    time = intervalTime;
                    image.fillAmount = 0;
                    point = null;
                }
                transform.position = hit.point;
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 0.01f);
                transform.localScale = new Vector3(0.1f * hit.distance, 0.1f * hit.distance, 1);
                ARKitControl.Instance.closePanel = false;
            }
            else
            {
                transform.position = Vector3.zero;
                transform.localScale = Vector3.zero;
                //ARKitControl.Instance.closePanel = true;
                point = null;
            }

        }
        /// <summary>
        /// 引发光标进入事件
        /// </summary>
        /// <param name="p"></param>
        private void OnPointerIn(PointArgs p)
        {
            ExecuteEvents.Execute<IPointerEnterHandler>(p.target.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            if (eyePointBegin != null)
                eyePointBegin(p);
           // pointz=
            p.target.DOLocalMoveZ(p.target.localPosition.z - 100, 0.1f);
        }
        /// <summary>
        /// 引发光标离开事件
        /// </summary>
        /// <param name="p"></param>
        private void OnPointerOut(PointArgs p)
        {
            ExecuteEvents.Execute<IPointerExitHandler>(p.target.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
            if (eyePointOut != null)
                eyePointOut(p);
            p.target.DOLocalMoveZ(0, 0.1f);
        }
        /// <summary>
        /// 引发光标计时结束
        /// </summary>
        /// <param name="p"></param>
        private void OnPointerClick(PointArgs p)
        {
            ExecuteEvents.Execute<IPointerClickHandler>(p.target.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
        }
        private void MyRay()
        {
            Ray raycast = new Ray(ARKitControl.Instance.eyePoint.position, ARKitControl.Instance.eyePoint.forward);

            if (Physics.Raycast(raycast, out hit, rayDistance, layer))
            {
                Debug.DrawLine(ARKitControl.Instance.eyePoint.position, hit.point, Color.red);
                //transform.position = hit.point;
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 0.01f);
                transform.localScale = new Vector3(0.1f * hit.distance, 0.1f * hit.distance, 1);

                // ExecuteEvents.Execute<IPointerExitHandler>(hit.transform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                if (hit.transform.GetComponent<Button>())
                {

                    if (point != hit.transform)
                    {
                        time = intervalTime;
                        image.fillAmount = 0;
                        point = hit.transform;
                        print("enter");
                        ExecuteEvents.Execute<IPointerEnterHandler>(hit.transform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
                       // if (eyePointBegin != null)
                            //eyePointBegin(hit);

                    }
                    if (time >= 0)
                    {
                        image.fillAmount += 1 / (intervalTime * 50);
                        time -= 0.02f;
                        if (time <= 0)
                        {
                            ExecuteEvents.Execute<IPointerClickHandler>(hit.transform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                            //  print(hit.transform.name);
                            //Application.LoadLevel("");
                            // SceneManager.LoadScene(Global.enablScene);
                           // if (eyePointEnd != null)
                              //  eyePointEnd(hit);

                        }
                    }
                }
                else
                {
                    time = intervalTime;
                    image.fillAmount = 0;
                    point = hit.transform;
                    print("out");
                }

                ARKitControl.Instance.closePanel = false;
            }

            else if (point && point != hit.transform)
            {
                print("out2");
                point = null;
                transform.position = Vector3.zero;
                transform.localScale = Vector3.zero;
                ARKitControl.Instance.closePanel = true;
            }
        }
        protected  void OnDestroy()
        {
            
            instance = null;
        }


    }
}