using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using PlaceAR;

public class ScrollRectHelper : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
  
    private float smooting = 10;                          //滑动速度
    public List<GameObject> listItem;                   //scrollview item 
    private int pageCount = 1;                           //每页显示的项目

    ScrollRect srect;
    float pageIndex;                                    //总页数
    bool isDrag = false;                                //是否拖拽结束
    List<float> listPageValue = new List<float> { 0 };  //总页数索引比列 0-1
    float targetPos = 0;                                //滑动的目标位置
    float nowindex = 0;                                 //当前位置索引
    private float beginPosition = 0;
    private float endPosition = 0;
    void Start()
    {
        DownPrefab.downPrefab.ItemLoad += ItemLoad;
        srect = GetComponent<ScrollRect>();
    }
    private void ItemLoad()
    {
        OnInit();
    }
    public void OnInit()
    {
        foreach (Transform item in transform.Find("Grid"))
        {
            listItem.Add(item.gameObject);
        }
        
        ListPageValueInit();
    }

    //每页比例
    void ListPageValueInit()
    {
        pageIndex = (listItem.Count / pageCount)-1;
        if (listItem != null && listItem.Count != 0)
        {
            for (float i = 1; i <= pageIndex; i++)
            {
                listPageValue.Add((i / pageIndex));
            }
        }
    }

    void Update()
    {
        if (!isDrag)
            srect.horizontalNormalizedPosition = Mathf.Lerp(srect.horizontalNormalizedPosition, targetPos, Time.deltaTime * smooting);
      //  print(srect.horizontalNormalizedPosition+"=="+ targetPos);
    }
    /// <summary>
    /// 拖动开始
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        beginPosition = srect.horizontalNormalizedPosition;
        //print(beginPosition);
        isDrag = true;
    }
    /// <summary>
    /// 拖拽结束
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        endPosition = srect.horizontalNormalizedPosition; //获取拖动的值
        //print(endPosition);
        var index = 0;
        float offset = Mathf.Abs(listPageValue[index] - endPosition);    //拖动的绝对值
        for (int i = 1; i < listPageValue.Count; i++)
        {
           
            float temp = Mathf.Abs(endPosition - listPageValue[i]);
            //print(temp+"\n"+ offset);
            if (beginPosition - endPosition < 0)
            {
              //  Debug.Log(1f / listPageValue.Count);
                if (temp < offset + 1f/listPageValue.Count)
                {
                    index = i;
                    offset = temp;
                }
            }
            else
            {
                if (temp < offset - 1f / listPageValue.Count)
                {
                    index = i;
                    offset = temp;
                }
            }
        }
        targetPos = listPageValue[index];
        nowindex = index;
        listItem[index].GetComponent<ItemChild>();
       // print(nowindex);
    }

    public void BtnLeftGo()
    {
        nowindex = Mathf.Clamp(nowindex - 1, 0, pageIndex);
        targetPos = listPageValue[Convert.ToInt32(nowindex)];
    }

    public void BtnRightGo()
    {
        nowindex = Mathf.Clamp(nowindex + 1, 0, pageIndex);
        targetPos = listPageValue[Convert.ToInt32(nowindex)];

    }
}
