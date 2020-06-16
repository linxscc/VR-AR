/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using PlaceAR.LabelDatas;
using System.Collections.Generic;
using UnityEngine.UI;

namespace MoDouAR
{
    /// <summary>
    /// 下载模型数据结构
    /// </summary>
    public class ChildItemData
    {
        public ChildItemData(ItemData item, Sprite sprite)
        {
            this.item = item;
            this.sprite = sprite;
        }
        /// <summary>
        /// 是否是默认模型
        /// </summary>
        public bool defaultModel = false;
        /// <summary>
        /// 选择状态
        /// </summary>
        public bool option=false;
        /// <summary>
        /// 模型下载状态
        /// </summary>
        public LoadState state;
        /// <summary>
        /// 下载信息
        /// </summary>
        public WWW www;
        /// <summary>
        /// 数据
        /// </summary>
        public ItemData item;
        /// <summary>
        /// 模型图片
        /// </summary>
        public Sprite sprite;
        /// <summary>
        /// 官方精选
        /// </summary>
        public bool offcial;
    }
    /// <summary>
    /// 模型库菜单
    /// </summary>
    public class ModelLibrary : Window<ModelLibrary>
    {
        public override int ID
        {
            get
            {
                return 2;
            }
        }

        public override string Name
        {
            get
            {
                return "ModelLibrary";
            }
        }

        public override string Path
        {
            get
            {
                return "UI/ModelLibrary";
            }
        }
        public override UIType CurrentUIType
        {
            get
            {
                currentUIType.isHidden = false;
                currentUIType.uiFormShowMode = UIFormShowMode.ReverseChange;
                currentUIType.uiFormType = UIFormType.PopUp;
                currentUIType.uiFormLucencyType = UIFormLucencyType.ImPenetrable;
               // currentUIType.isClearStack = true;
                return base.CurrentUIType;
            }

            set
            {
                base.CurrentUIType = value;
            }
        }
        [SerializeField]
        private Transform backGround;
        [SerializeField]
        private Transform close;
        /// <summary>
        /// 类型按钮组
        /// </summary>
        private Transform typeGrid;
        /// <summary>
        /// 模型组
        /// </summary>
        public Transform childGrid;
        private GameObject typeButtonP;
        /// <summary>
        /// 类型按钮预制物
        /// </summary>
        private GameObject TypeButtonP
        {
            get
            {
                if (typeButtonP == null)
                    typeButtonP = Resources.Load<GameObject>(SystemDefineTag.typeButtonURL);
                return typeButtonP;
            }
        }
        private GameObject modelButtonP;
        /// <summary>
        /// 类型按钮预制物
        /// </summary>
        public GameObject ModelButtonP
        {
            get
            {
                if (modelButtonP == null)
                    modelButtonP = Resources.Load<GameObject>(SystemDefineTag.modelButtonURL);
                return modelButtonP;
            }
        }
        /// <summary>
        /// 所有类型按钮
        /// </summary>
        private Dictionary<int, TypeButton> typeButton = new Dictionary<int, TypeButton>();
        public override void Awake()
        {

            base.Awake();
            typeGrid = backGround.Find("ScrollType/Grid");
            childGrid = backGround.Find("ScrollChild/Grid");
            backGround.localPosition = new Vector3(backGround.localPosition.x, -1334, backGround.localPosition.z);
        }
        public override void Start()
        {
            base.Start();

            IsOpen = false;
        }
        public override void Open()
        {
            base.Open();
            close.gameObject.SetActive(true);
            Tweener tw = backGround.DOLocalMoveY(-60, 0.3f);
            tw.OnComplete(CompleteOpen);
       
        }
        /// <summary>
        /// 打开回调
        /// </summary>
        private void CompleteOpen()
        {
            List<ConfigData> data = new List<ConfigData>();
            data.AddRange(LoadData.Instance.configData.data);
            StartCoroutine(CreatTypeButton(data));
        }
        private IEnumerator CreatTypeButton(List<ConfigData> data)
        {
            if (data.Count > 0)
            {

                if (!typeButton.ContainsKey(data[0].id))
                {
                    GameObject item = ObjectBool.Get(TypeButtonP);
                    yield return item;
                    item.transform.parent = typeGrid;
                    item.transform.localPosition = Vector3.zero;
                    item.transform.localRotation = Quaternion.identity;
                    item.transform.localScale = new Vector3(1f, 1f, 1f);
                    item.GetComponent<TypeButton>().OnInit(data[0]);
                    if (typeButton.Count == 0)
                    {
                        item.GetComponent<TypeButton>().Click();
                    }
                    typeButton.Add(data[0].id, item.GetComponent<TypeButton>());
                    data.Remove(data[0]);
                    if(data.Count>0)
                        StartCoroutine(CreatTypeButton(data));
                }              
            }
            
            typeGrid.parent.GetComponent<ScrollRect>().horizontalNormalizedPosition = 0;
        }
        /// <summary>
        /// 取消所有按钮的选择
        /// </summary>
        public void CloseAllChild()
        {
            foreach (KeyValuePair<int, TypeButton> kv in typeButton)
            {
                kv.Value.Close();
            }
        }
        public override void Close()
        {
            Tweener tw = backGround.DOLocalMoveY(-1334, 0.3f);
            tw.OnComplete(Complete);

        }
        /// <summary>
        /// 动画回调
        /// </summary>
        private void Complete()
        {
            base.Close();
            close.gameObject.SetActive(false);
           // UIMaskManager.GetInstance().CancelMaskWindow();
        }
    }
}