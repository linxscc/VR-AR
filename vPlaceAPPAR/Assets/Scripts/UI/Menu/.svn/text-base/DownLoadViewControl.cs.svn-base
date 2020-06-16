/*
 *    日期:2017,8,18
 *    作者:
 *    标题:
 *    功能:下载管理
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace PlaceAR
{
    /// <summary>
    /// 下载管理
    /// </summary>
	public class DownLoadViewControl
    {
        private DownLoadView downLoadView;
        private Dictionary<int, ItemChild> buttonIte;
        private List<DownChild> downChildList;
        /// <summary>
        /// 下载按钮预制物
        /// </summary>
        private GameObject downChild;
        /// <summary>
        /// 是否点击了全选按钮
        /// </summary>
        private bool isSelectAll = false;
        private bool isSelect = false;
        /// <summary>
        /// 是否是选择模式
        /// </summary>
        private bool IsSelect
        {
            get { return isSelect; }
            set
            {

                isSelect = value;
                isSelectAll = false;
                downLoadView.selectAll.transform.Find("SelectAll/Text").GetComponent<Text>().text = "全选";
                //downLoadView.select.GetComponentInChildren<Text>().text = "全选";
                downLoadView.select.SetActive(!isSelect);
                downLoadView.selectAll.SetActive(isSelect);

                if (downChildList != null)
                    foreach (DownChild item in downChildList)
                    {
                        //if(IsSelect)
                        item.IsSetSelect = isSelect;
                    }
            }
        }
        private DownLoadViewControl()
        {
            GameObject obj = Resources.Load<GameObject>(Global.downLoadView);
            downLoadView = GameObject.Instantiate<GameObject>(obj).GetComponent<DownLoadView>();
            //Resources.UnloadAsset(obj);
            downChild = Resources.Load<GameObject>(Global.downChild);

            downLoadView.transform.parent = GameObject.FindGameObjectWithTag(Tag.mainUICanvas).transform;
            downLoadView.transform.localScale = Vector3.one;
            downLoadView.transform.localPosition = Vector3.zero;
            buttonIte = new Dictionary<int, ItemChild>();
            downLoadView.select.GetComponent<Button>().onClick.AddListener(Select);
            downLoadView.selectAll.transform.Find("SelectAll").GetComponent<Button>().onClick.AddListener(SelectAll);
            downLoadView.selectAll.transform.Find("CancelSelect").GetComponent<Button>().onClick.AddListener(ChangeSelect);
            downLoadView.selectAll.transform.Find("Deletebar").GetComponent<Button>().onClick.AddListener(Deletebar);
        }
        private static DownLoadViewControl singleton;
        public static DownLoadViewControl Singleton
        {
            get
            {
                if (singleton == null)
                    singleton = new DownLoadViewControl();
                return singleton;
            }
        }
        public void Open(Dictionary<int, ItemChild> buttonIte)
        {
            IsSelect = false;
            downLoadView.transform.localScale = Vector3.one;
            if (downChildList != null)
            {
                foreach (DownChild item in downChildList)
                {
                    ObjectBool.Return(item.gameObject);
                    item.Return();
                }
                downChildList.Clear();
            }

            if (downChildList == null)
                downChildList = new List<DownChild>();

            else if ((downChildList.Count > 0)) return;


            foreach (KeyValuePair<int, ItemChild> kvp in buttonIte)
            {
                // DownChild obj = GameObject.Instantiate<GameObject>(downChild).GetComponent<DownChild>();
                DownChild obj = ObjectBool.Get(downChild).GetComponent<DownChild>();
                obj.OnInit(kvp.Value.DownLoad, kvp.Value);
                obj.IsSetSelect = false;
                downChildList.Add(obj);
                if (!kvp.Value.DownLoad)
                {

                    obj.transform.parent = downLoadView.downlodingGrid.transform;
                }
                else
                {
                    obj.transform.parent = downLoadView.downloadedGrid.transform;
                }
                obj.transform.localScale = Vector3.one;
            }
            this.buttonIte = buttonIte;

        }
        public void Close()
        {
            downLoadView.transform.localScale = Vector3.zero;
        }
        /// <summary>
        /// 选择按钮
        /// </summary>
        private void Select()
        {

            IsSelect = true;
        }
        /// <summary>
        /// 选择所有
        /// </summary>
        private void SelectAll()
        {
            if (isSelectAll)
            {
                downLoadView.selectAll.transform.Find("SelectAll/Text").GetComponent<Text>().text = "全选";
            }
            else
            {
                downLoadView.selectAll.transform.Find("SelectAll/Text").GetComponent<Text>().text = "全不选";
            }
           
            foreach (DownChild item in downChildList)
            {
                item.AddBtn(!isSelectAll);
            }
            isSelectAll = !isSelectAll;
        }
        /// <summary>
        /// 取消选择
        /// </summary>
        private void ChangeSelect()
        {
            IsSelect = false;
        }
        /// <summary>
        /// 删除
        /// </summary>
        private void Deletebar()
        {
            Debug.Log("del");
            downLoadView.delAlert.GetComponent<DelAlert>().Open(Del);
           // FileTools.DeleteFolder();
        }
        private void Del()
        {
            foreach (var item in downChildList)
            {
                if (item.isOn)
                {
                    Debug.Log(Global.LocalUrl + item.data.data.name);
                    FileTools.DeleteFolder(Global.LocalUrl + item.data.data.id);
                    item.DelPrefab();
                    item.transform.parent = downLoadView.downlodingGrid.transform;
#if UNITY_IPHONE
                    IOSNativePopUpManager.showMessage("提示", "删除成功", "确认");
#elif UNITY_ANDROID
                    PrintMenuControl.Singleton.Open("删除"+ item.data.data.name + "成功");
#endif
                }

            }
        }
    }
}