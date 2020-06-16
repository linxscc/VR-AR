/*
		// Copyright (C) 
 
        // 文件名：Label2DUIController.cs
        // 文件功能描述：控制浏览功能UI中的子模型按钮的生成及其数据初始化

		// 作者: 
        // 创建标识：

        // 修改标识：
        // 修改描述：
*/
using PlaceAR;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using vPlace_FW;
using vPlace_zpc;

public class Label2DUIController : MonoBehaviour
{
    /// <summary>
    /// 所有按钮
    /// </summary>
    public Dictionary<string, List<BrowserTypeItem>> groupDic = new Dictionary<string, List<BrowserTypeItem>>();
    /// <summary>
    /// 一组的按钮
    /// </summary>
    public Dictionary<string, BrowserTypeItem> gridItemDic = new Dictionary<string, BrowserTypeItem>();
    /// <summary>
    /// 按钮父级
    /// </summary>
    public RectTransform modelChildGrid = null;
    /// <summary>
    /// ToggleGroup组件
    /// </summary>
    public ToggleGroup toggleGroup = null;
    public List<GameObject> hideObjList = new List<GameObject>();
    /// <summary>
    /// 模型分组按钮
    /// </summary>
    private GameObject itemObj = null;
    /// <summary>
    /// 组内子模型按钮
    /// </summary>
    private GameObject itemChild = null;
    /// <summary>
    /// 组模型按钮背景
    /// </summary>
    private List<ModelChildSelect> groupObjBtn = new List<ModelChildSelect>();
    /// <summary>
    /// 子模型按钮背景
    /// </summary>
    private List<ModelChildSelect> childObjBtn = new List<ModelChildSelect>();
    private void Awake()
    {
        if (!modelChildGrid)
            modelChildGrid = GetComponent<RectTransform>();
        if (!toggleGroup)
            toggleGroup = GetComponent<ToggleGroup>();
    }
    /// <summary>
    /// 加载模型子级
    /// </summary>
    public void LoadModelChild(List<PrefabChildControl> label3Ds, BottomMenu_BrowserWindow uiController)
    {
        if (ProjectConstDefine.hasConfig && ProjectConstDefine.labelDataList.controlType != 4)
        {
            //SortGridChild(label3Ds.Count);
            groupObjBtn.Clear();
            childObjBtn.Clear();
            groupDic.Clear();
            gridItemDic.Clear();
            hideObjList.Clear();
            if (!itemObj)
                itemObj = Resources.Load<GameObject>(ProjectConstDefine.MODELBROWSER_BROWSERTYPEITEM);
            if (!itemChild)
                itemChild = Resources.Load<GameObject>(ProjectConstDefine.MODELBROWSER_BUTTONCHILD);

            //注：不知道为什么删除不彻底，只能使用循环，进行彻底删除
            while (transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; ++i)
                {
                    DestroyImmediate(transform.GetChild(i).gameObject);
                }
            }

            for (int i = 0; i < label3Ds.Count; ++i)
            {
                if (ProjectConstDefine.labelDataList.controlType == 6 && i == label3Ds.Count - 1)
                    return;
                if (label3Ds[i].Group == null || label3Ds[i].Group.Equals("所属分组"))
                {
                    GameObject itemTemp1 = Instantiate(itemObj);
                    itemTemp1.name = ProjectConstDefine.labelDataList.list[i].name;

                    itemTemp1.transform.SetParent(transform);
                    itemTemp1.transform.localEulerAngles = Vector3.zero;
                    itemTemp1.transform.localPosition = Vector3.zero;
                    itemTemp1.transform.localScale = Vector3.one;
                    itemTemp1.transform.Find("Title/ItemButton").GetComponentInChildren<Text>().text = label3Ds[i].Title;
                    itemTemp1.transform.Find("Title/ItemButton").GetComponent<Toggle>().group = toggleGroup;
                    BrowserTypeItem itemScript = itemTemp1.GetComponent<BrowserTypeItem>();
                    itemScript.OnInit(label3Ds[i]);
                    itemScript.onClick += uiController.Label2DUI_OnClick;
                    ModelChildSelect modelChildSelect1 = itemTemp1.transform.Find("Title/ItemButton").GetComponent<ModelChildSelect>();
                    modelChildSelect1.uicontroller = this;
                    modelChildSelect1.OnInit();
                    groupObjBtn.Add(modelChildSelect1);
                    List<BrowserTypeItem> itemList1 = new List<BrowserTypeItem>();
                    itemList1.Add(itemScript);
                    if (!groupDic.ContainsKey(label3Ds[i].Title))
                        groupDic.Add(label3Ds[i].Title, itemList1);
                    if (ProjectConstDefine.labelDataList.controlType == 6)
                        if (label3Ds[i].isHide) //截面模式特殊处理
                            hideObjList.Add(itemTemp1);
                }
                else
                {
                    if (!groupDic.ContainsKey(label3Ds[i].Group))
                    {
                        GameObject itemTemp2 = Instantiate(itemObj);
                        itemTemp2.name = label3Ds[i].Group;
                        itemTemp2.transform.SetParent(transform);
                        itemTemp2.transform.localEulerAngles = Vector3.zero;
                        itemTemp2.transform.localPosition = Vector3.zero;
                        itemTemp2.transform.localScale = Vector3.one;
                        itemTemp2.transform.Find("Title/ItemButton").GetComponentInChildren<Text>().text = label3Ds[i].Group;
                        itemTemp2.transform.Find("Title/ItemButton").GetComponent<Toggle>().group = toggleGroup;
                        BrowserTypeItem itemTemp2Script = itemTemp2.GetComponent<BrowserTypeItem>();
                        gridItemDic.Add(label3Ds[i].Group, itemTemp2Script);
                        itemTemp2Script.OnInit(label3Ds[i]);
                        itemTemp2Script.onClick += uiController.Label2DUI_OnClick;
                        ModelChildSelect modelChildSelect2 = itemTemp2.transform.Find("Title/ItemButton").GetComponent<ModelChildSelect>();
                        modelChildSelect2.uicontroller = this;
                        modelChildSelect2.OnInit();
                        groupObjBtn.Add(modelChildSelect2);
                        List<BrowserTypeItem> g = new List<BrowserTypeItem>();
                        g.Add(itemTemp2Script);
                        groupDic.Add(label3Ds[i].Group, g);
                    }
                    GameObject objTemp = Instantiate(itemChild);
                    objTemp.transform.parent = transform.Find(label3Ds[i].Group + "/ItemChild");
                    objTemp.transform.localEulerAngles = Vector3.zero;
                    objTemp.transform.localPosition = Vector3.zero;
                    objTemp.transform.localScale = Vector3.one;
                    objTemp.GetComponentInChildren<Text>().text = label3Ds[i].Title;
                    objTemp.name = label3Ds[i].Title;
                    BrowserTypeItem itemTemp3Script = objTemp.GetComponent<BrowserTypeItem>();
                    itemTemp3Script.OnInit(label3Ds[i]);
                    itemTemp3Script.onClick += uiController.Label2DUI_OnClick;
                    ModelChildSelect modelChildSelect = objTemp.GetComponent<ModelChildSelect>();
                    modelChildSelect.uicontroller = this;
                    modelChildSelect.OnInit();
                    childObjBtn.Add(modelChildSelect);
                    groupDic[label3Ds[i].Group].Add(itemTemp3Script);
                    gridItemDic[label3Ds[i].Group].labelChild.Add(itemTemp3Script);
                }
            }
        }
    }

    /// <summary>
    /// 浏览功能子级按钮排版
    /// </summary>
    /// <param name="childCount">子级数量</param>
    private void SortGridChild(int childCount)
    {
        if (childCount >= 6)
        {
            modelChildGrid.pivot = new Vector2(.5f, .5f);
            modelChildGrid.localPosition = new Vector3(120, 0, 0);
        }
        else
        {
            modelChildGrid.pivot = new Vector2(0f, 1);
            modelChildGrid.localPosition = Vector3.zero;
        }
    }

    /// <summary>
    /// 清空组模型背景选中框
    /// </summary>
    public void ClearModelGroupSelectBtn()
    {
        for (int i = 0; i < groupObjBtn.Count; ++i)
        {
            groupObjBtn[i].GroupHide();
        }
    }

    /// <summary>
    /// 清空子模型背景选中框
    /// </summary>
    public void ClearModelChildSelectBtn()
    {
        for (int i = 0; i < childObjBtn.Count; ++i)
        {
            childObjBtn[i].ChildHide();
        }
    }
}
