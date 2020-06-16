using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
namespace HTCVIVE
{
    /// <summary>
    /// 滑动检测
    /// </summary>
    public class ViveSwipeDetector : NetworkBehaviour
    {
        [SerializeField]
        SteamVR_TrackedObject trackedObj;
        private const int mMessageWidth = 200;
        private const int mMessageHeight = 64;

        //X轴
        private readonly Vector2 mXAxis = new Vector2(1, 0);
        //Y轴
        private readonly Vector2 mYAxis = new Vector2(0, 1);
        //是否跟踪滑动
        private bool trackingSwipe = false;
        //是否检查滑动
        private bool checkSwipe = false;

        //消息数组,可以考虑用枚举enum
        private readonly string[] mMessage = {
                "",
                "Swipe Left",//左滑
                "Swipe Right",//右滑
                "Swipe Top",//上滑
                "Swipe Bottom"//下滑
        };

        //消息索引
        private int mMessageIndex = 0;

        // 滑动的角度范围参考值
        private const float mAngleRange = 30;

        // 用户至少滑动多少像素才能被识别成滑动
        private const float mMinSwipeDist = 0.2f;

        //最小的滑动速度,低于该速度则无法识别为滑动
        private const float mMinVelocity = 4.0f;

        //记录开始位置和结束位置
        private Vector2 mStartPosition;
        private Vector2 endPosition;

        //滑动开始时间
        private float mSwipeStartTime;

        void Update()
        {
            //获取手柄设备
            var device = SteamVR_Controller.Input((int)trackedObj.index);
            // GetTouchDown时可能会滑动
            if ((int)trackedObj.index != -1 && device.GetTouchDown(Valve.VR.EVRButtonId.k_EButton_Axis0))
            {
                trackingSwipe = true;
                // 记录开始时间和位置
                mStartPosition = new Vector2(device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x,
                        device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y);
                mSwipeStartTime = Time.time;
            }
            // GetTouchUp时可能会滑动
            else if (device.GetTouchUp(Valve.VR.EVRButtonId.k_EButton_Axis0))
            {
                trackingSwipe = false;
                trackingSwipe = true;
                checkSwipe = true;
                Debug.Log("追踪结束" + Time.time);
            }
            else if (trackingSwipe)
            {
                //结束位置
                endPosition = new Vector2(device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x,
                                              device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y);

            }

            if (checkSwipe)
            {
                checkSwipe = false;
                //滑动时间
                float deltaTime = Time.time - mSwipeStartTime;
                //滑动向量
                Vector2 swipeVector = endPosition - mStartPosition;
                //滑动速度
                float velocity = swipeVector.magnitude / deltaTime;
                Debug.Log("滑动的速度为:" + velocity + "--" + "滑动时间为:" + deltaTime);
                if (velocity > mMinVelocity &&
                                    swipeVector.magnitude > mMinSwipeDist)
                {
                    // 如果滑动满足检测要求


                    swipeVector.Normalize();
                    //滑动的角度
                    float angleOfSwipe = Vector2.Dot(swipeVector, mXAxis);
                    angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;

                    // 检测是左滑还是右滑
                    if (angleOfSwipe < mAngleRange)
                    {
                        OnSwipeRight();
                    }
                    else if ((180.0f - angleOfSwipe) < mAngleRange)
                    {
                        OnSwipeLeft();
                    }
                    else
                    {
                        //检测上下滑动 
                        angleOfSwipe = Vector2.Dot(swipeVector, mYAxis);
                        angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;
                        if (angleOfSwipe < mAngleRange)
                        {
                            OnSwipeTop();
                        }
                        else if ((180.0f - angleOfSwipe) < mAngleRange)
                        {
                            OnSwipeBottom();
                        }
                        else
                        {
                            mMessageIndex = 0;
                        }
                    }
                }
            }

        }

        void OnGUI()
        {
            // 显示消息,方便在眼镜里面观测
            GUI.Label(new Rect((Screen.width - mMessageWidth) / 2,
                    (Screen.height - mMessageHeight) / 2,
                    mMessageWidth, mMessageHeight),
                    mMessage[mMessageIndex]);
        }

        private void OnSwipeLeft()
        {
            Debug.Log("Swipe Left 左滑");
            mMessageIndex = 1;
        }

        private void OnSwipeRight()
        {
            Debug.Log("Swipe right 右滑");
            mMessageIndex = 2;
        }

        private void OnSwipeTop()
        {
            Debug.Log("Swipe Top 上滑");
            mMessageIndex = 3;
        }

        private void OnSwipeBottom()
        {
            Debug.Log("Swipe Bottom 下滑");
            mMessageIndex = 4;
        }
    }
}