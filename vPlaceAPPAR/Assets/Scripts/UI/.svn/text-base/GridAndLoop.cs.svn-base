/* Creator:
 * Usage:
 * Designing:
 * Remaining problems:
 * Caution:
 * Version:
*/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using PlaceAR;

[ExecuteInEditMode]
public class GridAndLoop : MonoBehaviour
{

    /// <summary>
    /// 设置Item内容的委托
    /// </summary>
    /// <param name="item">Item对象</param>
    /// <param name="wrapIndex">Item在Grid中的序号</param>
    /// <param name="realIndex">当前Item在List中的序号</param>
    // public delegate void OnInitializeItem(GameObject item, int wrapIndex, int realIndex);

    /// <summary>
    /// 排列方式枚举
    /// </summary>
    public enum ArrangeType
    {
        Horizontal = 0,//水平排列
        Vertical = 1,//垂直排列
    }


    /// <summary>
    /// Item的尺寸
    /// </summary>
    public int cellx = 120, celly = 120;

    /// <summary>
    /// 是否隐藏裁剪部分
    /// </summary>
    public bool cullContent = true;

    /// <summary>
    /// Item最小序号
    /// </summary>
    public int minIndex = 0;

    /// <summary>
    /// Item最大序号
    /// </summary>
    public int maxIndex =0;

    /// <summary>
    /// 排列方式
    /// </summary>
    public ArrangeType arrangeType = ArrangeType.Horizontal;

    /// <summary>
    /// 行列个数 0表示1列
    /// </summary>
    public int ConstraintCount = 0;

    /// <summary>
    /// 设置Item的委托
    /// </summary>
    public CallBack<Transform, int> onInitializeItem;

    /// <summary>
    ///当前对象
    /// </summary>
    Transform mTrans;

    /// <summary>
    /// 当前RectTransform对象
    /// </summary>
    RectTransform mRTrans;

    /// <summary>
    /// ScrollRect
    /// </summary>
    ScrollRect mScroll;


    /// <summary>
    /// 滚动方向
    /// </summary>
    bool mHorizontal;
    [HideInInspector]
    /// <summary>
    /// 元素链表
    /// </summary>
    public List<Transform> mChild = new List<Transform>();

    /// <summary>
    /// 显示区域长度或高度的一半
    /// </summary>
    float extents = 0;
    Vector2 SRsize = Vector2.zero;//SrollRect的尺寸
    Vector3[] conners = new Vector3[4];//ScrollRect四角的世界坐标 
    //ScrollRect的初始位置
    Vector2 startPos;
    void Start()
    {
        //InitList();
    }
    private void Awake()
    {
        mScroll = transform.parent.GetComponent<ScrollRect>();
        mRTrans = transform.GetComponent<RectTransform>();
    }
    int sortByName(Transform a, Transform b) { return string.Compare(a.name, b.name); }
    /// <summary>
    /// 初始化
    /// </summary>
    public void OnInit(int maxIndex)
    {
        this.maxIndex = maxIndex;

        InitList();

    }
    private void Creat()
    {

    }
    /// <summary>
    /// 初始化mChild链表
    /// </summary>
    void InitList()
    {

        int i, ChildCount;

        mChild.Clear();
        StartCanvas canvas = GameObject.FindGameObjectWithTag(Tag.mainUICanvas).GetComponent<StartCanvas>();
        for (i = 0, ChildCount = transform.childCount; i < ChildCount; i++)
        {
            canvas.rotaterUI.Add(transform.GetChild(i).gameObject);
            mChild.Add(transform.GetChild(i));
            //transform.GetChild(i).gameObject.SetActive(false);
        }

        if (maxIndex >= 7)
        {
            ResetChildPosition();
            InitValue();
        }

        else
        {
            mScroll.onValueChanged.RemoveAllListeners();
            for (int j = 0; j < mChild.Count; j++)
            {
                mChild[j].gameObject.SetActive(false);
            }
            Vector2 startAxis = new Vector2(cellx / 2f, -celly / 2f);
            float he = (750 - (celly * maxIndex)) / 2;
            mRTrans.sizeDelta = new Vector2(ConstraintCount * cellx, 750);
            //print(he + "==" + maxIndex);
            for (i = 0; i < maxIndex; i++)
            {
                Transform temp = mChild[i];
                temp.gameObject.SetActive(true);
                temp.localPosition = new Vector3(startAxis.x, -he - celly * i - celly * 0.5f);
                UpdateItem(temp, i);

            }

        }
        //WrapContent();
        //     mChild.Sort(sortByName);//按照Item名字排序 

    }

    void InitValue()
    {
        if (ConstraintCount <= 0)
            ConstraintCount = 1;
        if (minIndex > maxIndex) minIndex = maxIndex;
        mTrans = transform;


        mHorizontal = mScroll.horizontal;

        SRsize = transform.parent.GetComponent<RectTransform>().rect.size;

        //四角坐标  横着数
        conners[0] = new Vector3(-SRsize.x / 2f, SRsize.y / 2f, 0);
        conners[1] = new Vector3(SRsize.x / 2f, SRsize.y / 2f, 0);
        conners[2] = new Vector3(-SRsize.x / 2f, -SRsize.y / 2f, 0);
        conners[3] = new Vector3(SRsize.x / 2f, -SRsize.y / 2f, 0);
        for (int i = 0; i < 4; i++)
        {
            Vector3 temp = transform.parent.TransformPoint(conners[i]);
            conners[i].x = temp.x;
            conners[i].y = temp.y;
        }

        mRTrans.pivot = new Vector2(0, 1);//设置panel的中心在左上角

        mScroll.onValueChanged.AddListener(delegate { WrapContent(); });//添加滚动事件回调
        startPos = mTrans.localPosition;
    }

    //初始化各Item的坐标
    [ContextMenu("RePosition")]
    public virtual void RePosition()
    {
        // print("12");
        InitList();
    }


    void Update()
    {
        //if (Application.isPlaying) enabled = false;
        // RePosition();

    }

    void ResetChildPosition()
    {
        int rows = 1, cols = 1;

        Vector2 startAxis = new Vector2(cellx / 2f, -celly / 2f);//起始位置
        int i;

        int imax = mChild.Count;//Item元素数量

        //初始化行列数
        if (arrangeType == ArrangeType.Vertical) //垂直排列 则适应行数
        {
            rows = ConstraintCount;
            cols = (int)Mathf.Ceil((float)imax / (float)rows);

            extents = (float)(cols * cellx) * 0.5f;
        }
        else if (arrangeType == ArrangeType.Horizontal) //水平排列则适应列数
        {
            cols = ConstraintCount;
            rows = (int)Mathf.Ceil((float)imax / (float)cols);
            extents = (float)(rows * celly) * 0.5f;
        }

        for (i = 0; i < imax; i++)
        {
            Transform temp = mChild[i];

            int x = 0, y = 0;//行列号
            if (arrangeType == ArrangeType.Horizontal) { x = i / cols; y = i % cols; }
            else if (arrangeType == ArrangeType.Vertical) { x = i % rows; y = i / rows; }


            temp.localPosition = new Vector2(startAxis.x + y * cellx, startAxis.y - x * celly - 24);

            if (minIndex == maxIndex || (i >= minIndex && i <= maxIndex))
            {
                //print("123q"+i);
                cullContent = true;
                temp.gameObject.SetActive(true);
                UpdateRectsize(temp.localPosition);//更新panel的尺寸

                UpdateItem(temp, i);
            }
            else
            {
                print(i);
                cullContent = false;
                temp.gameObject.SetActive(false); ;//如果预制Item数超过maxIndex则将超过部分隐藏 并 设置cullCintent为ufalse 并且不再更新 panel尺寸
            }



        }
    }

    /// <summary>
    /// ScrollRect复位
    /// </summary>
    public void ResetPosition()
    {
        mTrans.localPosition = startPos;
    }

    /// <summary>
    /// 更新panel的尺寸
    /// </summary>
    /// <param name="pos"></param>
    void UpdateRectsize(Vector2 pos)
    {


        if (arrangeType == ArrangeType.Vertical)
        {

            //   if(mRTrans.rect.width<pos.x+cell_x)
            mRTrans.sizeDelta = new Vector2(pos.x + cellx, ConstraintCount * celly);
        }
        else
        {

            //   if(mRTrans.rect.height<-pos.y+cell_y)
            mRTrans.sizeDelta = new Vector2(ConstraintCount * cellx, -pos.y + celly - 48);
        }
    }
    //Vector2 calculatePos(Vector2 world,Vector2 target,Vector2 lcal)
    //{
    //    Vector2 temp = world - target;
    //    temp.x /= (target.x/lcal.x);
    //    temp.y /= (target.y/lcal.y);

    //    return temp;
    //}
    int getRealIndex(Vector2 pos)//计算realindex
    {
        int x = (int)Mathf.Ceil(-pos.y / celly) - 1;//行号
        int y = (int)Mathf.Ceil(pos.x / cellx) - 1;//列号

        int realIndex;
        if (arrangeType == ArrangeType.Horizontal) realIndex = x * ConstraintCount + y;
        else realIndex = x + ConstraintCount * y;

        return realIndex;

    }


    void WrapContent()
    {

        Vector3[] conner_local = new Vector3[4];
        for (int i = 0; i < 4; i++)
        {

            conner_local[i] = mTrans.InverseTransformPoint(conners[i]);

        }
        //计算ScrollRect的中心坐标 相对于this的坐标
        Vector2 center = (conner_local[3] + conner_local[0]) / 2f;


        if (mHorizontal)
        {

            float min = conner_local[0].x - cellx;//显示区域
            float max = conner_local[3].x + cellx;
            for (int i = 0, imax = mChild.Count; i < imax; i++)
            {
                Transform temp = mChild[i];
                float distance = temp.localPosition.x - center.x;

                if (distance < -extents)
                {

                    Vector2 pos = temp.localPosition;
                    pos.x += extents * 2f;

                    int realIndex = getRealIndex(pos);

                    if (minIndex == maxIndex || (realIndex >= minIndex && realIndex < maxIndex))
                    {
                        UpdateRectsize(pos);
                        temp.localPosition = pos;
                        //设置Item内容
                        UpdateItem(temp, realIndex);
                    }

                }

                if (distance > extents)
                {
                    Vector2 pos = temp.localPosition;
                    pos.x -= extents * 2f;

                    int realIndex = getRealIndex(pos);

                    if (minIndex == maxIndex || (realIndex >= minIndex && realIndex < maxIndex))
                    {
                        temp.localPosition = pos;
                        //设置Item内容
                        UpdateItem(temp, realIndex);
                    }
                }

                if (cullContent)//设置裁剪部分是否隐藏
                {
                    Vector2 pos = temp.localPosition;
                    bool s = (pos.x > min && pos.x < max) ? true : false;
                   // temp.gameObject.SetActive(s);
                }

            }
        }
        else
        {
            float min = conner_local[3].y - celly;//显示区域
            float max = conner_local[0].y + celly;
            for (int i = 0, imax = mChild.Count; i < imax; i++)
            {
                Transform temp = mChild[i];
                float distance = temp.localPosition.y - center.y;

                if (distance < -extents)
                {

                    Vector2 pos = temp.localPosition;
                    pos.y += extents * 2f;

                    int realIndex = getRealIndex(pos);

                    if (minIndex == maxIndex || (realIndex >= minIndex && realIndex < maxIndex))
                    {

                        temp.localPosition = pos;

                        //设置Item内容
                        UpdateItem(temp, realIndex);
                    }
                }

                if (distance > extents)
                {

                    Vector2 pos = temp.localPosition;
                    pos.y -= extents * 2f;

                    int x = (int)Mathf.Ceil(-pos.y / celly) - 1;//行号
                    int y = (int)Mathf.Ceil(pos.x / cellx) - 1;//列号

                    int realIndex;
                    if (arrangeType == ArrangeType.Horizontal) realIndex = x * ConstraintCount + y;
                    else realIndex = x + ConstraintCount * y;

                    if (minIndex == maxIndex || (realIndex >= minIndex && realIndex < maxIndex))
                    {
                        UpdateRectsize(pos);

                        temp.localPosition = pos;

                        //设置Item内容
                        UpdateItem(temp, realIndex);
                    }

                }
                if (cullContent)//设置裁剪部分是否隐藏
                {
                    Vector2 pos = temp.localPosition;
                    bool s = (pos.y > min && pos.y < max) ? true : false;
                    //temp.gameObject.SetActive(s);
                }

            }
        }

    }

    void UpdateItem(Transform item, int realIndex)
    {
        if (onInitializeItem != null)
        {
            onInitializeItem(item, realIndex);
        }
    }

}
