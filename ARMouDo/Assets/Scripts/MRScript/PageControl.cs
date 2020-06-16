/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace PlaceAR
{
    public class PageControl : MonoBehaviour
    {
        public class Page
        {
            public CallBack<int> next;
            public CallBack<int> last;
            public int index;
            public int count;
        }
        private Button next;
        private Button last;
        private Transform pageTran;
        private GameObject pageItem;
        private List<GameObject> itemList=new List<GameObject>();
        /// <summary>
        /// 当前操作数据
        /// </summary>
        private Page page;
        /// <summary>
        /// 当前类型页索引
        /// </summary>
        private int index = 0;
        private int Index
        {
            get { return index; }
            set
            {
                index = value;
                if (index <= 0)
                {
                    // last.interactable = false;
                    // next.interactable = (true);
                    index = 0;
                }
                if (index >= page.count - 1)
                {
                    //next.interactable=(false);
                    // last.interactable = (true);
                    index = page.count - 1;
                }


            }
        }

        public void OnInit(Page p)
        {
            page = p;
            for (int i = 0; i < itemList.Count; i++)
            {
                ObjectBool.Return(itemList[i]);
            }
            itemList.Clear();
            for (int i = 0; i < page.count; i++)
            {
                GameObject obj = ObjectBool.Get(pageItem);
                obj.transform.Find("BackGround").gameObject.SetActive(false);
                itemList.Add(obj);
            }
        }
        // Use this for initialization
        private void Awake()
        {
            //Index = 0;
           
            next = transform.Find("Next").GetComponent<Button>();
            last = transform.Find("Last").GetComponent<Button>();
            pageTran = transform.Find("Page");
            next.onClick.AddListener(Next);
            last.onClick.AddListener(Last);
            pageItem = transform.Find("Page/PageItem").gameObject;
            pageItem.SetActive(false);
        }
        private void Next()
        {
            Index++;
            page.next(Index);
        }
        private void Last()
        {
            Index--;
            page.last(Index);
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}