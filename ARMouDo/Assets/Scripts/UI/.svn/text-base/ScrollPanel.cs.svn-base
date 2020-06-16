using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class ScrollPanel : MonoBehaviour
{
    Vector3 beginX = Vector3.zero;
    Vector3 endX = Vector3.zero;

    public int picWidth;//图片的宽度
    public RectTransform gridRect;
    public Scrollbar scrollBar;

    float beginValue;
    float endValue;
    public int count = 0;
    float distance;
    int[] bars;   //存储图片对于的position.x

    int childID;   //图片的id
    float size;

    Dictionary<int, int> dicMove = new Dictionary<int, int>();   //记录哪个图片对应哪个位置

    public AnimationCurve m_FadeCurve;
    public float m_AnimTime = 0.4f;

    void Start()
    {
        size = scrollBar.size;

        bars = new int[count];

        for (int i = 0; i < count; i++)
        {
            bars[i] = picWidth * 2 - picWidth * i;
            dicMove.Add(i, bars[i]);
        }
    }

    float beginTime = 0;
    float endTime = 0;

    void Update()
    {
        if (scrollBar.size < size - 0.0001f)   //如果scrollBar的size变形了就return
        {
            return;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                beginX = gridRect.transform.localPosition;
                beginValue = scrollBar.value;
                beginTime = Time.time;
            }

            if (Input.GetMouseButtonUp(0))
            {
                endTime = Time.time;

                endX = gridRect.transform.localPosition;
                endValue = scrollBar.value;
                distance = Vector3.Distance(beginX, endX);

                if (distance >= picWidth / 3 || endTime - beginTime < 0.3f) //滑动距离超过图片宽度的1/3或者滑动时间少于0.3f就滑动至下张图
                {
                    if (endValue - beginValue > 0)  //向左滑
                    {
                        int tmp = childID;

                        int tmp1 = tmp + 1;

                        if (dicMove.ContainsKey(tmp1))   
                        {
                            int pos = dicMove[tmp1];

                            StartCoroutine(ProcessScroll(endX.x, pos));
                        }
                    }
                    else  //向右滑
                    {
                        int tmp = childID;

                        int tmp1 = tmp - 1;

                        if (dicMove.ContainsKey(tmp1))   
                        {
                            int pos = dicMove[tmp1];

                            StartCoroutine(ProcessScroll(endX.x, pos));
                        }
                    }
                }
                else  //移动距离不够 返回原来位置
                {
                    if (endValue - beginValue > 0)  //得要向右移动
                    {
                        int tmp = childID;

                        if (dicMove.ContainsKey(tmp))
                        {
                            int pos = dicMove[tmp];

                            StartCoroutine(ProcessScroll(endX.x, pos));
                        }
                    }
                    else if (endValue - beginValue < 0)   //得要向左移动
                    {
                        int tmp = childID;

                        if (dicMove.ContainsKey(tmp))   
                        {
                            int pos = dicMove[tmp];

                            StartCoroutine(ProcessScroll(endX.x, pos));
                        }
                    }
                }
            }
        }
    }

    public void ReciveID(int id)
    {
        childID = id;
    }

    IEnumerator ProcessScroll(float beginPosX, float endPoxX)
    {
        float starttime = Time.time;
        float ratio = 0.0f;
        float curvevalue = 0.0f;
        Vector3 tempVector;
        while (Time.time - starttime < m_AnimTime)
        {
            ratio = (Time.time - starttime) / m_AnimTime;
            curvevalue = m_FadeCurve.Evaluate(ratio);

            tempVector = gridRect.localPosition;
            tempVector.x = endPoxX + (1.0f - curvevalue) * (beginPosX - endPoxX);
            gridRect.localPosition = tempVector;
            yield return null;
        }
        gridRect.localPosition = new Vector3(endPoxX, 0, 0);
    }

}
