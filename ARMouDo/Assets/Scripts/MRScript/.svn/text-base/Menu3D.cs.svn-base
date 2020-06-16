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

namespace MoDouAR
{
    /// <summary>
    /// 3D菜单
    /// </summary>
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
        /// <summary>
        /// 模型按钮
        /// </summary>
        private GameObject childItem3D;
        public Transform childMenu;
        /// <summary>
        /// 所有下载的数据
        /// </summary>
        private List<ChildItemData> childItem = new List<ChildItemData>();
        /// <summary>
        /// 实例化出来的按钮
        /// </summary>
        private List<GameObject> buttonList;
        /// <summary>
        /// 对模型类型进行分页.
        /// </summary>
        private Dictionary<int, List<ChildItemData>> allButtonName;
        public Text page;
        public static Menu3D instance;

        private int index;
        private int Index
        {
            get { return index; }
            set
            {
                index = value;
                if (index <= 0)
                    index = 0;
                if (index >= allButtonName.Count - 1)
                    index = allButtonName.Count - 1;
            }
        }
        private void Awake()
        {
            instance = this;
            childItem3D = Resources.Load<GameObject>(SystemDefineTag.childItem3D);
            childMenu = transform.Find("ChlidMenu3D");
            buttonList = new List<GameObject>();
        }
        public void Next()
        {
           
            Index--;
            CreatTypeButton(allButtonName[Index]);
            page.text = Index+1 + "/" + allButtonName.Count;
        }
        public void Last()
        {

            Index++;
            CreatTypeButton(allButtonName[Index]);
            page.text = Index + 1 + "/" + allButtonName.Count;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void OnInit()
        {
            childItem= LoadData.Instance.childItem;
            allButtonName = new Dictionary<int, List<ChildItemData>>();
            List<ChildItemData> item=new List<ChildItemData>();
            for (int i = 0; i < childItem.Count; i++)
            {
                if (i % 8 == 0)
                {
                    item = new List<ChildItemData>();
                    if (!allButtonName.ContainsKey(i / 8))
                        allButtonName.Add(i / 8, item);
                }
                item.Add(childItem[i]);

            }
            page.text = 1 + "/" + allButtonName.Count;
            CreatTypeButton(allButtonName[0]);
        }
        /// <summary>
        /// 设置关闭所有的选择框
        /// </summary>
        public void CloseAllImage()
        {
            
            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].GetComponent<ChildItem3D>().Pitchon = false;

            }
            for (int i = 0; i < childItem.Count; i++)
            {
                childItem[i].option = false;
            }
        }
        /// <summary>
        /// 实例化类型按钮
        /// </summary>
        /// <param name="data"></param>
        private void CreatTypeButton(List<ChildItemData> data)
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                ObjectBool.Return(buttonList[i]);
            }
            buttonList.Clear();
            for (int i = 0; i < data.Count; i++)
            {
                GameObject t = ObjectBool.Get(childItem3D);
                t.transform.parent = childMenu;
                t.transform.localPosition = Vector3.zero;
                t.transform.localRotation = Quaternion.identity;
                //t.transform.eulerAngles=Vector3.zero;
                t.transform.localScale = Vector3.one;
                t.GetComponent<ChildItem3D>().OnInit(data[i]);
                //t.GetComponentInChildren<Text>().text = data[i][0].catName;
                buttonList.Add(t);
            }
        }
    }
}