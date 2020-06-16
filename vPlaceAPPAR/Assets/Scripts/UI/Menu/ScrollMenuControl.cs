/*
 *    日期:2017,8,1
 *    作者:
 *    标题:
 *    功能:模型选择菜单
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlaceAR.LabelDatas;
using UnityEngine.UI;
using vPlace_FW;
using vPlace_zpc;

namespace PlaceAR
{
    /// <summary>
    /// 模型选择菜单
    /// </summary>
    public class ScrollMenuControl
    {
        /// <summary>
        /// 生成模型的回调
        /// </summary>
        public CallBack<GameObject> returnPrefab;
        private ScrollMenu scrollMenu;
        /// <summary>
        /// 菜单模型
        /// </summary>
        public GameObject scrollMenuObj;
        /// <summary>
        /// 类型按钮
        /// </summary>
        private GameObject scrollGridItem;
        /// <summary>
        /// 模型按钮
        /// </summary>
        private GameObject childItem;
        public List<ScrollGridItem> ltemList = new List<ScrollGridItem>();
        /// <summary>
        /// 打开回调
        /// </summary>
        public CallBack open;
        /// <summary>
        /// 关闭回调
        /// </summary>
        public CallBack close;
        // public CallBack<GameObject> obj;
        /// <summary>
        /// 类型按钮grid
        /// </summary>
        public GameObject typeGrid;
        /// <summary>
        /// 类型子集grid
        /// </summary>
        public GridAndLoop childGrid;
        /// <summary>
        /// 是否已经打开
        /// </summary>
        private bool isOpen = false;
        /// <summary>
        /// 排序控制组件
        /// </summary>
        private ContentSizeFitter typeGridContentSizeFitter = null;
        /// <summary>
        /// 记录已下载的类目数量
        /// </summary>
        private int downloadCount = 0;

        /// <summary>
        /// 选择模型的图片
        /// </summary>
        //  public Image btnImage;
        private Transform canvas;
        /// <summary>
        /// 当前选择类别id
        /// </summary>
        public int typeID = 0;
        /// <summary>
        /// 实例化出来的模型
        /// </summary>
        public GameObject insObj;

        private bool isFirstOpenAR = false;
        private ScrollMenuControl()
        {

            scrollMenuObj = Resources.Load<GameObject>(Global.scrollMenu);
            scrollGridItem = Resources.Load<GameObject>(Global.scrollGridItem);
            childItem = Resources.Load<GameObject>(Global.childItem);
            scrollMenuObj = GameObject.Instantiate(scrollMenuObj);
            canvas = GameObject.FindGameObjectWithTag(Tag.mainUICanvas).transform;
            scrollMenuObj.transform.parent = canvas;
            scrollMenuObj.transform.GetComponent<RectTransform>().sizeDelta = Vector2.one;
            scrollMenuObj.transform.localPosition = Vector3.zero;
            scrollMenuObj.transform.localEulerAngles = Vector3.zero;
            scrollMenuObj.transform.localScale = Vector3.one;

            //scrollMenuObj.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            //scrollMenuObj.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            scrollMenu = scrollMenuObj.GetComponent<ScrollMenu>();
            typeGrid = scrollMenu.typeMenu;
            typeGridContentSizeFitter = scrollMenu.typeMenu.GetComponent<ContentSizeFitter>();
            childGrid = scrollMenu.childGrid.GetComponent<GridAndLoop>();
            //childGrid.btnPrefab = childItem;
            EventTriggerListener.Get(scrollMenuObj.transform.Find("Close").gameObject).onClick += Close;
        }
        private static ScrollMenuControl singleton;
        public static ScrollMenuControl Singleton
        {
            get
            {
                if (singleton == null)
                    singleton = new ScrollMenuControl();
                return singleton;
            }

        }
        public void Open()
        {
            //  childGrid.OnInit(Global.itemData[typeID].Count);
            if (Global.OperatorModel == OperatorMode.ARMode && !isFirstOpenAR)
            {
                OnInit(typeID, false);
                isFirstOpenAR = true;
            }
            scrollMenu.Open();
            //if (open != null) open();
        }
        public void OpenIntial(int typeID)
        {
            this.typeID = typeID;
            //Debug.Log(scrollMenu.grid.GetComponent<RectTransform>().sizeDelta);
            // scrollMenuObj.SetActive(true);
            //this.btnImage = btnImage;
            // BrowserTypeViewContol.Instance.Close();
            scrollMenu.Open();
            OnInit(typeID, true);

            if (open != null) open();
        }

        public void Close(GameObject obj)
        {
            scrollMenu.Close();
            if (Global.OperatorModel == OperatorMode.BrowserMode)
                UIManager.GetInstance().ShowSelectModelBtn();
            //Clean();
            if (close != null) close();
        }
        /// <summary>
        /// 子按钮点击
        /// </summary>
        public void ChildOnClick()
        {
            for (int i = 0; i < ltemList.Count; i++)
            {
                ltemList[i].Close();
            }
        }
        /// <summary>
        /// 清理
        /// </summary>
        private void Clean()
        {
            for (int i = 0; i < ltemList.Count; i++)
            {
                ObjectBool.Return(ltemList[i].gameObject);
            }
            ltemList.Clear();
            downloadCount = 0;
        }
        public void OnInit(int typeID, bool intial)
        {
            Clean();
            // if (isOpen) return;
            //isOpen = true;
            foreach (KeyValuePair<int, List<ItemData>> item in Global.itemData)
            {
                if (item.Value.Count > 0)
                {
                    downloadCount++;

                    if (Global.OperatorModel == OperatorMode.ARMode)
                    {
                        GameObject childButton = ObjectBool.Get(scrollGridItem);
                        childButton.transform.parent = typeGrid.transform;
                        canvas.GetComponent<StartCanvas>().rotaterUI.Add(childButton);
                        childButton.GetComponentInChildren<Text>().text = item.Value[0].catName;
                        ScrollGridItem gridItem = childButton.GetComponent<ScrollGridItem>();
                        ltemList.Add(gridItem);
                        gridItem.OnInit(item.Value, item.Key);
                        if (typeID == gridItem.typeID)
                        {
                            Debug.Log(typeID);
                            gridItem.Open(intial);
                        }
                        RectTransform rect = scrollMenu.typeMenu.GetComponent<RectTransform>();
                        childButton.transform.localPosition = Vector3.zero;
                        childButton.transform.localEulerAngles = Vector3.zero;
                        childButton.transform.localScale = Vector3.one;
                    }
                    else
                    {
                        if (!item.Value[0].catName.Equals("天气系统"))
                        {
                            GameObject childButton = ObjectBool.Get(scrollGridItem);
                            childButton.transform.parent = typeGrid.transform;
                            canvas.GetComponent<StartCanvas>().rotaterUI.Add(childButton);
                            childButton.GetComponentInChildren<Text>().text = item.Value[0].catName;
                            ScrollGridItem gridItem = childButton.GetComponent<ScrollGridItem>();
                            ltemList.Add(gridItem);

                            gridItem.OnInit(item.Value, item.Key);
                            if (typeID == gridItem.typeID)
                            {
                                gridItem.Open(intial);
                            }
                            RectTransform rect = scrollMenu.typeMenu.GetComponent<RectTransform>();
                            childButton.transform.localPosition = Vector3.zero;
                            childButton.transform.localEulerAngles = Vector3.zero;
                            childButton.transform.localScale = Vector3.one;
                        }
                    }
                    if (downloadCount >= 7)
                        typeGridContentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                    else
                        typeGridContentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
                }
            }
        }
    }
}
