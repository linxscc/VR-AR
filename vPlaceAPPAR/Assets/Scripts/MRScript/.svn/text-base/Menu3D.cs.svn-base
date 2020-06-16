/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlaceAR.LabelDatas;
using UnityEngine.UI;

namespace PlaceAR
{
    /// <summary>
    /// 当前状态
    /// </summary>
    public enum State
    {
        typeMenu,
        childMenu,
        none
    }
    public class Menu3D : MonoBehaviour
    {

        private bool onBecame;
        /// <summary>
        /// 是否在视野范围内
        /// </summary>
        public bool OnBecame
        {
            get { return onBecame; }
            set
            {
                onBecame = value;
            }
        }
        private State onState;
        public State OnState
        {
            get { return onState; }
            set
            {
                onState = value;
                switch (onState)
                {
                    case State.typeMenu:
                        back.gameObject.SetActive(false);
                        childMenu.gameObject.SetActive(false);
                        typeMenu.gameObject.SetActive(true);
                        pageControl.OnInit(pageType);
                        break;
                    case State.childMenu:
                        back.gameObject.SetActive(true);
                        childMenu.gameObject.SetActive(true);
                        typeMenu.gameObject.SetActive(false);

                        break;
                    case State.none:

                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 類型按鈕
        /// </summary>
        private GameObject typeButton;
        /// <summary>
        /// 模型按钮
        /// </summary>
        private GameObject childButton;
        private Transform typeMenu;
        public Transform childMenu;
        public PageControl pageControl;
        private Button back;
        /// <summary>
        /// 实例化出来的类型按钮
        /// </summary>
        private List<GameObject> type;
        /// <summary>
        /// 类型下面的模型按钮
        /// </summary>
        private List<GameObject> child;
        /// <summary>
        /// 对模型类型进行分页.
        /// </summary>
        private Dictionary<int, List<List<ChildItemData>>> allButtonName;
        private List<List<ChildItemData>> itemData;
        private int i;        
        /// <summary>
        /// 类型分页情况
        /// </summary>
        private PageControl.Page pageType;
        public static Menu3D menu3D;
        private void Awake()
        {
            menu3D = this;
            pageType = new PageControl.Page();
            typeButton = Resources.Load<GameObject>(Global.typeButton);
            childButton = Resources.Load<GameObject>(Global.childButton);
            typeMenu = transform.Find("TypeMenu3D");
            childMenu = transform.Find("ChlidMenu3D");
            pageControl = transform.Find("PageControl").GetComponent<PageControl>();
            back = transform.Find("Back").GetComponent<Button>();
            back.onClick.AddListener(BackButton);
            type = new List<GameObject>();
            child = new List<GameObject>();
            OnState = State.typeMenu;
        }
        private void BackButton()
        {
            OnState = State.typeMenu;
        }
        private void Next(int index)
        {
            //Index++;
            CreatTypeButton(allButtonName[index]);
        }
        private void Last(int index)
        {
            //Index--;
            CreatTypeButton(allButtonName[index]);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void OnInit()
        {
           
            i = 0;
            allButtonName = new Dictionary<int, List<List<ChildItemData>>>();
            foreach (KeyValuePair<int, List<ItemData>> item in Global.itemData)
            {

                if (i % 8 == 0)
                {

                    itemData = new List<List<ChildItemData>>();
                    //ChildItemData ci = new ChildItemData(itemData[i], null);
                    if (!allButtonName.ContainsKey(i / 8))
                        allButtonName.Add(i / 8, itemData);
                    else
                    {
                        Debug.Log(i / 8);
                    }
                }
                List<ChildItemData> childItemData = new List<ChildItemData>();
                for (int j = 0; j < item.Value.Count; j++)
                {
                    ChildItemData ci = new ChildItemData(item.Value[j], null);
                    childItemData.Add(ci);
                }
                itemData.Add(childItemData);
                i++;
            }
            CreatTypeButton(allButtonName[0]);
            pageType.count = allButtonName.Count;
            pageType.index = 0;
            pageType.next = Next;
            pageType.last = Last;
           // Index = 0;
        }
        /// <summary>
        /// 实例化模型按钮
        /// </summary>
        /// <param name="data"></param>
        public void CreatChildButton(List<ChildItemData> data)
        {

            OnState = State.childMenu;
            for (int i = 0; i < child.Count; i++)
            {
                ObjectBool.Return(child[i]);
            }
            child.Clear();
            for (int i = 0; i < data.Count; i++)
            {
                GameObject t = ObjectBool.Get(childButton);
                t.transform.parent = childMenu;
                t.transform.localPosition = Vector3.zero;
                t.transform.localRotation = Quaternion.identity;
                //t.transform.eulerAngles=Vector3.zero;
                t.transform.localScale = Vector3.one;
                t.GetComponent<ChildItem3D>().OnInit(data[i]);
                child.Add(t);
            }
        }
        /// <summary>
        /// 设置关闭所有的选择框
        /// </summary>
        public void CloseAllImage()
        {
            
            for (int i = 0; i < child.Count; i++)
            {
                child[i].GetComponent<ChildItem3D>().Pitchon = false;

            }
        }
        /// <summary>
        /// 实例化类型按钮
        /// </summary>
        /// <param name="data"></param>
        private void CreatTypeButton(List<List<ChildItemData>> data)
        {
            for (int i = 0; i < type.Count; i++)
            {
                ObjectBool.Return(type[i]);
            }
            type.Clear();
            for (int i = 0; i < data.Count; i++)
            {
                GameObject t = ObjectBool.Get(typeButton);
                t.transform.parent = typeMenu;
                t.transform.localPosition = Vector3.zero;
                t.transform.localRotation = Quaternion.identity;
                //t.transform.eulerAngles=Vector3.zero;
                t.transform.localScale = Vector3.one;
                t.GetComponent<Type3DUI>().OnInit(data[i], this);
                //t.GetComponentInChildren<Text>().text = data[i][0].catName;
                type.Add(t);
            }
        }
    }
}