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
using ARKit_T;
using UnityEngine.EventSystems;
using System;

namespace PlaceAR
{
    public class ChildMenu : MonoBehaviour
    {
        /// <summary>/// <summary>
        /// 滑动组件
        /// </summary>
        private ScrollRect scrollRect ;
        /// <summary>
        /// 排序组件
        /// </summary>
        private GridLayoutGroup gridLayout ;
        /// 父级
        /// </summary>
        private  Transform childParent = null;
        private RectTransform childParentRTrans = null;
        /// <summary>
        /// 尾部位置
        /// </summary>
        private int tail = 0;
        /// <summary>
        /// 滑动变化值
        /// </summary>
        private float scrollValue = 0;
        /// <summary>
        /// 第一次生成子级个数
        /// </summary>
        private int smoth = 10;
        /// <summary>
        /// 子级数量最小值
        /// </summary>
        private int minNum = 1;
        /// <summary>
        /// 子元素高度
        /// </summary>
        private float cellHeight = 0;
        private List<ItemData> itemData;
        /// <summary>
        /// 组所有存在的按钮
        /// </summary>
        private List<GameObject> itemObj = new List<GameObject>();
        private int typeId;
        /// <summary>
        /// 按钮预制物
        /// </summary>
        [HideInInspector]
        public GameObject btnPrefab;
        [HideInInspector]
        /// <summary>
        /// 生成的预制物
        /// </summary>
        public GameObject insObj;
        private ContentSizeFitter childParentContentSizeFitter = null;

        private void Awake()
        {
            scrollRect = GetComponent<ScrollRect>();
            childParent = scrollRect.content;
            gridLayout = childParent.GetComponent<GridLayoutGroup>();
            childParentContentSizeFitter = childParent.GetComponent<ContentSizeFitter>();
            childParentRTrans = childParent.GetComponent<RectTransform>();
        }
        public void Open(List<ItemData> itemData, int id)
        {
            Clear();
            typeId = id;
            RectTransform rect = transform.Find("Grid").GetComponent<RectTransform>();
            for (int i = 0; i < itemData.Count; i++)
            {
                GameObject childButton = ObjectBool.Get(btnPrefab);
                transform.parent.parent.GetComponent<StartCanvas>().rotaterUI.Add(childButton);
                childButton.transform.SetParent(rect);
                childButton.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                childButton.transform.localScale = Vector3.one;
                itemObj.Add(childButton);
                string url = "file://" + Global.LocalUrl + id + "/" + itemData[i].id;
                if (FileTools.FileExist(Global.LocalUrl + id + "/" + itemData[i].id, itemData[i].idName + ".png"))
                {
                    //print(i);
                    Texture2D texture = FileTools.ReadTexture(Global.LocalUrl + id + "/" + itemData[i].id, itemData[i].idName + ".png");
                }
            }
            SetAllImage();
        }

        public void Clear()
        {
            for (int i = 0; i < itemObj.Count; i++)
            {
                itemObj[i].GetComponent<ChildItem>().Unload();
                ObjectBool.Return(itemObj[i]);
                //EventTriggerListener.Get(itemObj[i]).onClick -= ChlidButton;
            }
            itemObj.Clear();
        }
        public void SetAllImage()
        {
            //BackGround
           // for (int i = 0; i < itemObj.Count; i++)
           // {
                //itemObj[i].GetComponent<ChildItem>().Close();
           // }
        }
        /// <summary>
        /// 动态生成模型选择按钮，减缓加载压力
        /// </summary>
        /// <param name="itemData"></param>
        /// <param name="id"></param>
        public void DynamicCreateChildItem(List<ItemData> itemData, int id, bool intial)
        {
            Clear();
            typeId = id;
            this.itemData = itemData;
            CreateEnoughItem(true, intial);
            SetAllImage();
        }

        /// <summary>
        /// 创建新的对象组
        /// </summary>
        private void CreateEnoughItem(bool mark, bool intial)
        {
            Vector3[] coner = new Vector3[4];
            scrollRect.viewport.GetLocalCorners(coner);
            cellHeight = gridLayout.cellSize.y;
            minNum = smoth;
            minNum = minNum < itemData.Count ? minNum : itemData.Count;
            CreatItems(minNum, mark, intial);
            scrollRect.onValueChanged.AddListener((x) => scrollValue = x.y);
       }
        /// <summary>
        /// 创建新的对象
        /// </summary>
        /// <param name="num"></param>
        /// <param name="mark"></param>
        private void CreatItems(int num, bool mark, bool intial)
        {
            if (mark)
            {
                tail = 0;
                childParentRTrans.offsetMax = Vector2.zero;
                childParentRTrans.offsetMin = Vector2.zero;
                if (num >= 7) childParentContentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                else childParentContentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
            }
            for (int i = 0; i < num; ++i)
            {
                GameObject childButton = ObjectBool.Get(btnPrefab);
                transform.parent.parent.GetComponent<StartCanvas>().rotaterUI.Add(childButton);
                //childButton.name = itemData[tail].id.ToString();
                childButton.transform.SetParent(childParent);
                childButton.transform.SetSiblingIndex(transform.parent.childCount - 1);
                childButton.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                childButton.transform.localScale = Vector3.one;
              //  childButton.GetComponent<ChildItem>().OnInit(typeId, itemData[tail], this);
               // childButton.GetComponent<ChildItem>().Close();
                if (intial && i == 0)
                {
                    childButton.GetComponent<ChildItem>().Click();
                }
                itemObj.Add(childButton);
                if (FileTools.FileExist(Global.LocalUrl + typeId + "/" + itemData[tail].id, itemData[tail].idName + ".png"))
                {
                    Texture2D texture = FileTools.ReadTexture(Global.LocalUrl + typeId + "/" + itemData[tail].id, itemData[tail].idName + ".png");
                  //  childButton.GetComponent<ChildItem>().SetSprite(texture);
                }
                tail++;
                scrollRect.content.localPosition -= (cellHeight + 24) * Vector3.up;
            }
        }
        ///// <summary>
        ///// 拖动滑动区域时触发
        ///// </summary>
        ///// <param name="eventData"></param>
        //public void OnEndDrag(PointerEventData eventData)
        //{
        //    if (scrollValue <= 0 && tail < itemData.Count)
        //    {
        //        int creatNum = 0;
        //        if (minNum + tail < itemData.Count)
        //            creatNum = minNum;
        //        else
        //            creatNum = itemData.Count - tail;
        //        CreatItems(creatNum, false, false);
        //    }
        //}
    }
}
