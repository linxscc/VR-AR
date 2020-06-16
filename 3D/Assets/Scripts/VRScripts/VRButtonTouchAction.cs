using UnityEngine;
using System.Collections;
using System;

namespace HTCVIVE
{
    /// <summary>
    /// 方向
    /// </summary>
    public enum Direction
    {
        up, down, lift, right
    }
    /// <summary>
    /// 触摸板
    /// </summary>
    public struct ChickArg
    {
        /// <summary>
        /// 发射的手柄
        /// </summary>
        public GameObject obj;
        /// <summary>
        /// 滑动速度
        /// </summary>
        public float pad;
        /// <summary>
        /// 滑动方向
        /// </summary>
        public Direction padDirection;
    }
    public class VRButtonTouchAction : MonoBehaviour
    {
        private bool isTriggerPreaed = false;
        /// <summary>
        /// 是否按下扳机键
        /// </summary>
        public bool IsTriggerPreaed
        {
            get { return isTriggerPreaed; }
            set
            {
                if (isTriggerPreaed == value) return;
                isTriggerPreaed = value;
                if (IsTriggerPreaed)
                {
                    if (triggerClicked != null)
                        triggerClicked(gameObject);
                }
                else
                {
                    if (triggerUnclicked != null)
                        triggerUnclicked(gameObject);
                }
            }
        }
        /// <summary>
        /// 是否点击菜单键
        /// </summary>
        public bool menu = false;
        private bool isGripd = false;
        /// <summary>
        /// 是否抓取
        /// </summary>
        public bool IsGripd
        {
            get { return isGripd; }
            set
            {
                if (isGripd == value) return;
                if (gripped != null)
                    gripped(moveVector);
                isGripd = value;
            }
        }
        //X轴
        private readonly Vector2 mXAxis = new Vector2(1, 0);
        //Y轴
        private readonly Vector2 mYAxis = new Vector2(0, 1);
        //是否跟踪滑动
        private bool trackingSwipe = false;
        //是否检查滑动
        private bool checkSwipe = false;
        private bool grip = false;
        //记录开始位置和结束位置
        private Vector2 mStartPosition;
        private Vector2 endPosition;
        //滑动开始时间
        private float mSwipeStartTime;
        //消息索引
        private int mMessageIndex = 0;

        // 滑动的角度范围参考值
        private const float mAngleRange = 30;

        // 用户至少滑动多少像素才能被识别成滑动
        private const float mMinSwipeDist = 0.2f;

        //最小的滑动速度,低于该速度则无法识别为滑动
        private const float mMinVelocity = 4.0f;
        //手柄  
        SteamVR_TrackedObject trackdeObjec;
        /// <summary>
        /// 扣动扳机
        /// </summary>
        public event CallBack<GameObject> triggerClicked;
        /// <summary>
        /// 弹起扳机
        /// </summary>
        public event CallBack<GameObject> triggerUnclicked;
        /// <summary>
        /// 菜单点击
        /// </summary>
        public event CallBack<GameObject> menuButtonClicked;
        /// <summary>
        /// 触摸板按下
        /// </summary>
        public event CallBack<ChickArg> padClicked;
        /// <summary>
        /// 触摸板弹起
        /// </summary>
        public event CallBack<GameObject> PadUnclicked;
        /// <summary>
        /// 触摸板触摸
        /// </summary>
        public event CallBack<Vector2> padPress;
        /// <summary>
        /// 抓取键
        /// </summary>
        public event CallBack<Vector2> gripped;
        public event CallBack<GameObject> triggerPress;
        SteamVR_Controller.Device device;
        /// <summary>
        /// 结束位置
        /// </summary>
        private Vector2 endPos;
        /// <summary>
        /// 移动方向和速度
        /// </summary>
        public Vector2 moveVector;
        void Awake()
        {

            // device = SteamVR_Controller.Input((int)trackdeObjec.index);
            //获取手柄上的这个组件  
            trackdeObjec = GetComponent<SteamVR_TrackedObject>();
        }
        // Use this for initialization  
        void Start()
        {
        }
        void LateUpdate()
        {   //获取手柄输入  

            device = SteamVR_Controller.Input((int)trackdeObjec.index);
            //扳机键
            if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                // print("trigger");
                IsTriggerPreaed = true;
            }
            //松开扳机
            else if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                IsTriggerPreaed = false;
                //print("trigger");
            }
           // if()
            //ApplicationMenu键 带菜单标志的那个按键（在方向圆盘上面）  
            if (device.GetTouchDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
            {
                if (menu) return;
                if (menuButtonClicked != null)
                    menuButtonClicked(gameObject);
                menu = true;
            }
            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.ApplicationMenu))
            {
                menu = false;
            }
            //Grip键 两侧的键，每个手柄左右各一功能相同，同一手柄两个键是一个键。  
            if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Grip))
            {

                if (grip) return;
                IsGripd = !IsGripd;
                grip = true;
            }
            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Grip))
            {
                if (!grip) return;
                grip = false;
            }
            if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
            {
                //print("press");
                if (triggerPress != null)
                    triggerPress(gameObject);
            }
            moveVector = new Vector2((float)(Math.Round(Convert.ToDouble(endPos.x - transform.position.x), 3)), (float)(Math.Round(Convert.ToDouble(endPos.y - transform.position.y), 3))); //endPos - new Vector2(transform.position.x,transform.position.y);
                                                                                                                                                                                             //float speed = dir.magnitude / Time.deltaTime;
            endPos = new Vector2(transform.position.x, transform.position.y);
            if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
            {
               // Vector2 dir = new Vector2((float)(Math.Round(Convert.ToDouble(endPos.x - transform.position.x),3)), (float)(Math.Round(Convert.ToDouble(endPos.y - transform.position.y), 3))); //endPos - new Vector2(transform.position.x,transform.position.y);
                //float speed = dir.magnitude / Time.deltaTime;
               // endPos = new Vector2(transform.position.x, transform.position.y);
                //print(dir.x + "\n" + dir.y);
                // ChickArg c = new ChickArg();
                // Vector2 cc = device.GetAxis();
                if (padPress != null)
                    padPress(moveVector);
            }
            else
            {
               // endPos = transform.position;
            }
           
           // Touch(device);
            //按动触发扳  
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
            {
                ChickArg c = new ChickArg();
                Vector2 cc = device.GetAxis();
                float jiaodu = VectorAngle(new Vector2(1, 0), cc);
                //下  
                if (jiaodu > 45 && jiaodu < 135)
                {
                    c.padDirection = Direction.down;
                }
                //上  
                if (jiaodu < -45 && jiaodu > -135)
                {
                    c.padDirection = Direction.up;
                }
                //左  
                if ((jiaodu < 180 && jiaodu > 135) || (jiaodu < -135 && jiaodu > -180))
                {
                    c.padDirection = Direction.lift;
                }
                //右  
                if ((jiaodu > 0 && jiaodu < 45) || (jiaodu > -45 && jiaodu < 0))
                {
                    c.padDirection = Direction.right;
                }
                if (padClicked != null)
                    padClicked(c);
            }
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                if (PadUnclicked != null)
                    PadUnclicked(gameObject);
            }
           
        }
        private void Touch(SteamVR_Controller.Device device)
        {
            if ((int)trackdeObjec.index != -1 && device.GetTouchDown(Valve.VR.EVRButtonId.k_EButton_Axis0))
            {
                trackingSwipe = true;
                // 记录开始时间和位置
                mStartPosition = new Vector2(device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x,
                        device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y);
                mSwipeStartTime = Time.time;
            }
            // GetTouchUp时可能会滑动
            else if (device.GetTouch(Valve.VR.EVRButtonId.k_EButton_Axis0))
            {
                trackingSwipe = false;
                trackingSwipe = true;
                //checkSwipe = true;
                // if (checkSwipe)
                // {
                //  checkSwipe = false;
                //滑动时间
                float deltaTime = Time.time - mSwipeStartTime;
                //滑动向量
                Vector2 swipeVector = endPosition - mStartPosition;
                //滑动速度
                float velocity = swipeVector.magnitude / deltaTime;
                // Debug.Log("滑动的速度为:" + velocity + "--" + "滑动时间为:" + deltaTime);
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
                // }
            }
            else if (trackingSwipe)
            {
               // print("41");
                //结束位置
                endPosition = new Vector2(device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x,
                                              device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y);

            }


        }
        private void OnSwipeLeft()
        {
            //Debug.Log("Swipe Left 左滑");
            mMessageIndex = 1;
        }

        private void OnSwipeRight()
        {
            // Debug.Log("Swipe right 右滑");
            mMessageIndex = 2;
        }

        private void OnSwipeTop()
        {
            //  Debug.Log("Swipe Top 上滑");
            mMessageIndex = 3;
        }

        private void OnSwipeBottom()
        {
            //  Debug.Log("Swipe Bottom 下滑");
            mMessageIndex = 4;
        }
        //方向圆盘最好配合这个使用 圆盘的.GetAxis()会检测返回一个二位向量，可用角度划分圆盘按键数量  
        //这个函数输入两个二维向量会返回一个夹角 180 到 -180  
        float VectorAngle(Vector2 from, Vector2 to)
        {
            float angle;
            Vector3 cross = Vector3.Cross(from, to);
            angle = Vector2.Angle(from, to);
            return cross.z > 0 ? -angle : angle;
        }
    }
}

