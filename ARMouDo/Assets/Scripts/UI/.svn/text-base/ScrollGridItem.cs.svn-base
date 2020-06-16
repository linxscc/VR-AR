/*
 *    日期:2017,8,2
 *    作者:
 *    标题:
 *    功能:类型按钮
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlaceAR.LabelDatas;
using UnityEngine.UI;

namespace PlaceAR
{
    public class ChildItemData
    {
        public ChildItemData(ItemData item, Texture2D texture)
        {
            this.item = item;
            this.texture = texture;
        }
        public bool pitchon;
        public ItemData item;
        public Texture2D texture;
    }
    /// <summary>
    /// 类型按钮
    /// </summary>
	public class ScrollGridItem : MonoBehaviour
    {

        /// <summary>
        /// 是否需要初始化模型
        /// </summary>
        private bool isInitiPrefab = false;
        /// <summary>
        /// 包含的模型
        /// </summary>
        private List<ChildItemData> childItem=new List<ChildItemData>();
        public  int typeID;
        /// <summary>
        /// 背景圆圈
        /// </summary>
        private GameObject backGround;
        public void OnInit(List<ItemData> itemData, int id)
        {
            //itemData.Sort(delegate (ItemData x)
            //{
            //    return x.orderNum.CompareTo(y.orderNum);
            //}
            //     );
            backGround = transform.Find("BackGround").gameObject;
            typeID = id;
            childItem.Clear();
            for (int i = 0; i < itemData.Count; i++)
            {
                ChildItemData ci = new ChildItemData(itemData[i],null);
                childItem.Add(ci);
            }
            //this.itemData = itemData;
            GetComponent<Button>().onClick.AddListener(Click);
            Close();
        }
        /// <summary>
        /// 关闭类型
        /// </summary>
        public void Close()
        {          
            if (backGround==null)
                backGround = transform.Find("BackGround").gameObject;
            backGround.SetActive(false);
        }
        /// <summary>
        /// 打开类型
        /// </summary>
        public void Open(bool intial)
        {
            isInitiPrefab = intial;
            //关闭所有按钮
            ScrollMenuControl.Singleton.ChildOnClick();
            CloseAllImage();
           // ScrollMenuControl.Singleton.childGrid.onInitializeItem = OnInitializeItem;
           // ScrollMenuControl.Singleton.childGrid.OnInit(childItem.Count);
            ScrollMenuControl.Singleton.typeID = typeID;
           // ScrollMenuControl.Singleton.childGrid.DynamicCreateChildItem(itemData, typeID, intial);
            backGround.SetActive(true);
        }
        /// <summary>
        /// 设置关闭所有的选择框
        /// </summary>
        public void CloseAllImage()
        {
           // List<Transform> tran = ScrollMenuControl.Singleton.childGrid.mChild;
           // for (int i = 0; i < tran.Count; i++)
           // {
               // tran[i].GetComponent<ChildItem>().Pitchon = false;

           // }
        }
        /// <summary>
        /// 滑动回调
        /// </summary>
        /// <param name="item"></param>
        /// <param name="realIndex"></param>
        private void OnInitializeItem(Transform item, int realIndex)
        {     
            if (childItem[realIndex].texture == null)
            {
                Texture2D texture = FileTools.ReadTexture(Global.LocalUrl + typeID + "/" + childItem[realIndex].item.id, childItem[realIndex].item.idName + ".png");
                childItem[realIndex].texture = texture;
            }
            item.GetComponent<ChildItem>().OnInit(typeID, childItem[realIndex],this);
            if (isInitiPrefab && realIndex == 0)
            {
                item.GetComponent<ChildItem>().Click();
            }
        }
        private void Click()
        {

            Open(false);
        }
    }
}
