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
    public abstract class Window<T> : WindowsBase where T : WindowsBase, new()
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
            get { return instance ?? (instance = new T()); }
            set { instance = value; }
        }
        protected bool isOpen = false;

        public virtual bool IsOpen
        {
            get
            {
                return isOpen;
            }
            set
            {
                isOpen = value;
            }
        }
        /// <summary>
        /// 销毁窗口
        /// </summary>
        public virtual void DestroyWindow()
        {
            if (mWndObject != null)
                Destroy(gameObject);
        }
        /// <summary>
        /// 创建窗口
        /// </summary>
        public virtual void CreatWindow()
        {
            if (mWndObject == null)
            {
                mWndObject = UIManager.GetInstance().CreatWindows(WindData.path, WindData.name);

            }           
        }
        /// <summary>
        /// 开启窗口
        /// </summary>
        public override void Open()
        {
            if (mWndObject == null)
            {
                CreatWindow();
                return;
            }
            else
            {
                if (!isOpen)
                {
                    isOpen = true;
                }
                UIManager.GetInstance().ShowUIForm(WindData);
            }
        }

        public override void Close()
        {
            base.Close();
            if (isOpen)
            {
                isOpen = false;
            }
        }
        public virtual void Awake()
        {
            mWndObject = gameObject;
            instance = mWndObject.GetComponent<T>();        
        }
        public virtual void OnEnable()
        {
        }
        public virtual void Start()
        {          
        }
        public virtual void OnDisable()
        {
        }
        public virtual void OnDestroy()
        {
            //print("dis==" + gameObject.name);
            mWndObject = null;
            instance = null;
        }
    }
}