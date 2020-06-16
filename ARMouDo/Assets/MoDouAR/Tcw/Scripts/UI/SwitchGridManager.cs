using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(GridLayoutGroup))]
[RequireComponent(typeof(ContentSizeFitter))]
public class SwitchGridManager : MonoBehaviour
{

    public delegate void UpdateGridHandle(int index, int currentMaxID, Transform trans);
    public UpdateGridHandle updateGridHandle = null;
    public delegate void TouchDirection(DragDirection drag, int currentMaxID);
    public TouchDirection touchDirection = null;
    [SerializeField]
    private ScrollRect scrollRect;
    [SerializeField]
    private GridDirection gridDirection = GridDirection.None;
    [SerializeField]
    private GridPivot gridPivot = GridPivot.None;
    [SerializeField]
    private int minAmount = 0;
    public int amount = 0;
    private bool hasInit = false;
    private RectTransform rectTransform;
    private Vector2 gridLayoutSize;
    private Vector2 gridLayoutPos;
    private GridLayoutGroup gridLayoutGroup;
    private ContentSizeFitter contentSizeFitter;
    private Vector2 startPosition;
    private int realIndex = -1;
    private int realIndexUp = -1;
    private List<RectTransform> children = new List<RectTransform>();
    private Dictionary<Transform, Vector2> childsAnchoredPosition = new Dictionary<Transform, Vector2>();
    private Dictionary<Transform, int> childsSiblingIndex = new Dictionary<Transform, int>();


    private IEnumerator InitChildren()
    {
        yield return 0;
        if (!hasInit)
        {
            rectTransform = GetComponent<RectTransform>();
            gridLayoutGroup = GetComponent<GridLayoutGroup>();
            contentSizeFitter = GetComponent<ContentSizeFitter>();
            switch (gridDirection)
            {
                case GridDirection.None:
                    Debug.Log("错误:  " + "请选择滑动方式!");
                    break;
                case GridDirection.左右:
                    scrollRect.horizontal = true;
                    scrollRect.vertical = false;
                    gridPivot = GridPivot.左;
                    break;
                case GridDirection.上下:
                    scrollRect.horizontal = false;
                    scrollRect.vertical = true;
                    gridPivot = GridPivot.上;
                    break;
            }
            switch (gridPivot)
            {
                case GridPivot.None:
                    break;
                case GridPivot.上:
                    rectTransform.pivot = new Vector2(0.5f, 1);
                    break;
                case GridPivot.下:
                    rectTransform.pivot = new Vector2(0.5f, 0);
                    break;
                case GridPivot.左:
                    rectTransform.pivot = new Vector2(0, 0.5f);
                    rectTransform.localPosition = new Vector3(0, rectTransform.localPosition.y, 0);
                    break;
                case GridPivot.右:
                    rectTransform.pivot = new Vector2(1, 0.5f);
                    break;
            }
            gridLayoutGroup.enabled = false;
            contentSizeFitter.enabled = false;
            gridLayoutPos = rectTransform.anchoredPosition;
            gridLayoutSize = rectTransform.sizeDelta;

            scrollRect.onValueChanged.AddListener((data) => { ScrollCallback(data); });
            for (int index = 0; index < transform.childCount; index++)
            {
                Transform child = transform.GetChild(index);
                RectTransform childRectTrans = child.GetComponent<RectTransform>();
                childsAnchoredPosition.Add(child, childRectTrans.anchoredPosition);

                childsSiblingIndex.Add(child, child.GetSiblingIndex());
            }
        }
        else
        {
            rectTransform.anchoredPosition = gridLayoutPos;
            rectTransform.sizeDelta = gridLayoutSize;

            children.Clear();

            realIndex = -1;
            realIndexUp = -1;

            foreach (var info in childsSiblingIndex)
            {
                info.Key.SetSiblingIndex(info.Value);
            }

            for (int index = 0; index < transform.childCount; index++)
            {
                Transform child = transform.GetChild(index);

                RectTransform childRectTrans = child.GetComponent<RectTransform>();
                if (childsAnchoredPosition.ContainsKey(child))
                {
                    childRectTrans.anchoredPosition = childsAnchoredPosition[child];
                }
                else
                {
                    Debug.LogError("childsAnchoredPosition no contain " + child.name);
                }
            }
        }


        for (int index = 0; index < transform.childCount; index++)
        {
            Transform trans = transform.GetChild(index);
            trans.gameObject.SetActive(true);

            children.Add(transform.GetChild(index).GetComponent<RectTransform>());

            UpdateGridHandles(children.Count - 1, transform.GetChild(index));
            TouchDirections(DragDirection.None);
        }

        startPosition = rectTransform.anchoredPosition;
        realIndex = children.Count - 1;
        hasInit = true;

        for (int index = 0; index < minAmount; index++)
        {
            children[index].gameObject.SetActive(index < amount);
        }

        if (gridLayoutGroup.constraint == GridLayoutGroup.Constraint.FixedColumnCount)
        {
            int row = (minAmount - amount) / gridLayoutGroup.constraintCount;
            if (row > 0)
            {
                rectTransform.sizeDelta -= new Vector2(0, (gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y) * row);
            }
        }
        else
        {
            int column = (minAmount - amount) / gridLayoutGroup.constraintCount;
            if (column > 0)
            {
                rectTransform.sizeDelta -= new Vector2((gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x) * column, 0);
            }
        }
        StopCoroutine(InitChildren());
    }


    void ScrollCallback(Vector2 data)
    {
        UpdateChildren();
    }

    void UpdateChildren()
    {
        if (transform.childCount < minAmount)
        {
            Debug.Log("错误:  " + "Grid子对象数量小于循环利用个数,请检查minAmount与Grid子对象数!");
            return;
        }

        Vector2 currentPos = rectTransform.anchoredPosition;

        if (gridLayoutGroup.constraint == GridLayoutGroup.Constraint.FixedColumnCount)
        {
            float offsetY = currentPos.y - startPosition.y;

            if (offsetY > 0)
            {
                if (realIndex >= amount - 1)
                {
                    startPosition = currentPos;
                    return;
                }
                TouchDirections(DragDirection.上);
                float scrollRectUp = Screen.height + (gridLayoutGroup.spacing.y / 2) - 120 - 45;
                Vector3 childBottomLeft = new Vector3(children[0].anchoredPosition.x, children[0].anchoredPosition.y - gridLayoutGroup.cellSize.y, 0f);
                float childBottom = transform.TransformPoint(childBottomLeft).y;

                if (childBottom >= scrollRectUp)
                {
                    for (int index = 0; index < gridLayoutGroup.constraintCount; index++)
                    {
                        children[index].SetAsLastSibling();

                        children[index].anchoredPosition = new Vector2(children[index].anchoredPosition.x, children[children.Count - 1].anchoredPosition.y - gridLayoutGroup.cellSize.y - gridLayoutGroup.spacing.y);

                        realIndex++;

                        if (realIndex > amount - 1)
                        {
                            children[index].gameObject.SetActive(false);
                        }
                        else
                        {
                            UpdateGridHandles(realIndex, children[index]);
                        }
                    }


                    rectTransform.sizeDelta += new Vector2(0, gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y);

                    for (int index = 0; index < children.Count; index++)
                    {
                        children[index] = transform.GetChild(index).GetComponent<RectTransform>();
                    }
                }
            }
            else
            {
                if (realIndex + 1 <= children.Count)
                {
                    startPosition = currentPos;
                    return;
                }
                TouchDirections(DragDirection.下);
                float scrollRectBottom = -gridLayoutGroup.spacing.y / 2 - 120;
                Vector3 childUpLeft = new Vector3(children[children.Count - 1].anchoredPosition.x, children[children.Count - 1].anchoredPosition.y, 0f);
                float childUp = transform.TransformPoint(childUpLeft).y;

                if (childUp < scrollRectBottom)
                {
                    for (int index = 0; index < gridLayoutGroup.constraintCount; index++)
                    {
                        children[children.Count - 1 - index].SetAsFirstSibling();

                        children[children.Count - 1 - index].anchoredPosition = new Vector2(children[children.Count - 1 - index].anchoredPosition.x, children[0].anchoredPosition.y + gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y);

                        children[children.Count - 1 - index].gameObject.SetActive(true);

                        UpdateGridHandles(realIndex - children.Count - index, children[children.Count - 1 - index]);
                    }
                    realIndex -= gridLayoutGroup.constraintCount;

                    rectTransform.sizeDelta -= new Vector2(0, gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y);

                    for (int index = 0; index < children.Count; index++)
                    {
                        children[index] = transform.GetChild(index).GetComponent<RectTransform>();
                    }
                }
            }
        }
        else { }
        startPosition = currentPos;
    }

    void UpdateGridHandles(int index, Transform trans)
    {
        if (updateGridHandle != null)
        {
            updateGridHandle(index, realIndex, trans);
        }
    }
    void TouchDirections(DragDirection drag)
    {
        if (touchDirection != null)
        {
            touchDirection(drag, realIndex);
        }
    }

    /// <summary>
    /// 设置总的个数;
    /// </summary>
    /// <param name="count"></param>
    public void SetAmount(int count)
    {
        amount = count;

        StartCoroutine(InitChildren());
    }
}

/// <summary>
/// 滑动方式
/// </summary>
public enum GridDirection
{
    /// <summary>
    /// 无
    /// </summary>
    None,
    /// <summary>
    /// 水平
    /// </summary>
    左右,
    /// <summary>
    /// 垂直
    /// </summary>
    上下
}
/// <summary>
/// Grid对象 Pivot
/// </summary>
public enum GridPivot
{
    None,
    上,
    下,
    左,
    右
}
public enum DragDirection
{
    None,
    上,
    下,
    左,
    右
}
