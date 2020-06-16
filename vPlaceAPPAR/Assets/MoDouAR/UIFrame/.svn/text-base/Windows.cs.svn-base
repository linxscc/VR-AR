/*
 *    日期:2017,10,13
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

namespace MoDouAR
{
  
    /// <summary>
    /// 所有窗口父类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Window<T> : WindowsBase where T : MonoBehaviour, new()
    {
        /// <summary>
        /// 窗体id
        /// </summary>
        public abstract int ID { get; }
        /// <summary>
        /// 窗体名称
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// 预制物路径
        /// </summary>
        public abstract string Path { get; }

        public override Wind WindData
        {
            get
            {
                if (windData == null)
                {
                    windData = new Wind();
                    windData.id = ID;
                    windData.name = Name;
                    windData.path = Path;
                }
                return windData;
            }

            set
            {               
                windData = value;
            }
        }
        public static T instance;
        /// <summary>
        /// 实例
        /// </summary>
        public static T Instance
        {
            get
            {

               return instance ?? (instance = new T());
            }
            set { instance = value; }
        }

        protected GameObject mWndObject = null;
        /// <summary>
        /// gameobject
        /// </summary>
        public GameObject WndObject { get { return mWndObject; } }

        public bool isOpen = false;

        //UITweener tweener;

        public override void Open()
        {
               
            if (mWndObject == null)
                mWndObject = UIManager.GetInstance().CreatWindows(WindData);
            if (!isOpen)
            {
                isOpen = true;
               // b();
                //IsTweenerRun(tweener, "ShowWnd", true);
            }
            base.Open();
        }

        public override void Close()
        {
            base.Close();
            if (isOpen)
            {
                isOpen = false;
                //b();
            }
            // IsTweenerRun(tweener, "CloseWnd", false);
        }


        public virtual void Awake()
        {
            instance = GetComponent<T>();
           
        }

        public virtual void OnEnable()
        {

        }

        public virtual void Start()
        {
            mWndObject = gameObject;
      
            // tweener = gameObject.GetComponent<UITweener>();

        }

        public virtual void OnDisable()
        {

        }

        public virtual void OnDestroy()
        {
            mWndObject = null;
            instance = null;

        }



        //void IsTweenerRun(UITweener tweener, string func, bool IsForward)
        //{
        //    if (tweener)
        //    {
        //        tweener.eventReceiver = gameObject;
        //        tweener.callWhenFinished = func;
        //        tweener.Play(IsForward);
        //    }
        //    else
        //    {
        //        Invoke(func, 0);
        //    }
        //}
    }
}