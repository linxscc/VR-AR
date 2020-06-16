/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PlaceAR.LabelDatas;
using System.Collections.Generic;

namespace PlaceAR
{
    /// <summary>
    /// 3dUI类型按钮
    /// </summary>
    public class Type3DUI : MonoBehaviour
    {
        private List<ChildItemData> data;
        private Dictionary<int, List<ChildItemData>> itemData;
        private List<ChildItemData> page;
        /// <summary>
        /// 初始化
        /// </summary>
        public void OnInit(List<ChildItemData> data, Menu3D menu3D)
        {
            this.data = data;
            GetComponentInChildren<Text>().text = data[0].item.catName;
            itemData = new Dictionary<int, List<ChildItemData>>();
            for (int i = 0; i < data.Count; i++)
            {
                if (i % 8 == 0)
                {
                    page = new List<ChildItemData>();
                    itemData.Add(i / 8, page);
                }
                page.Add(data[i]);
            }
        }

        void OnClick()
        {
            PageControl.Page pageType = new PageControl.Page();
            pageType.count = itemData.Count;
            pageType.index = 0;
            pageType.next = Next;
            pageType.last = Last;
            Menu3D.menu3D.CreatChildButton(itemData[0]);
            Menu3D.menu3D.pageControl.OnInit(pageType);

        }
        private void Next(int index)
        {
            Menu3D.menu3D.CreatChildButton(itemData[index]);
        }
        private void Last(int index)
        {
            Menu3D.menu3D.CreatChildButton(itemData[index]);
        }
        void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
            itemData = new Dictionary<int, List<ChildItemData>>();
        }
    }
}