using PlaceAR.LabelDatas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGridLayout : MonoBehaviour
{
    /// <summary>
    /// 每个按钮大小
    /// </summary>
    public Vector2 cellSize;
    /// <summary>
    /// 间隔
    /// </summary>
    public Vector2 spacing;
    RectTransform rect;
    public void UpdataCount(int count)
    {
        rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, count * (cellSize.y + spacing.y) - spacing.y);

    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
