/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PlaceAR.LabelDatas;
using System.Collections.Generic;

namespace MoDouAR
{
    /// <summary>
    /// 模型类型按钮
    /// </summary>
    public class TypeButton : MonoBehaviour
    {
        /// <summary>
        /// 选中颜色
        /// </summary>
        private Color optionColor = new Color(61f / 255, 220f / 255, 1, 1);
        /// <summary>
        /// 默认颜色
        /// </summary>
        private Color normaColor = new Color(154f / 255, 154f / 255, 154f / 255, 154f / 255);
        [SerializeField]
        private Text text;
        [SerializeField]
        private GameObject backGround;
        /// <summary>
        /// 类型数据
        /// </summary>
        private ConfigData data;
        /// <summary>
        /// 网络读取到的数据
        /// </summary>
        private ItemDataList itemDataList;
        /// <summary>
        /// 本地数据
        /// </summary>
        private ItemDataList localData;
        /// <summary>
        /// 上次读取的数据
        /// </summary>
        private string webData = "webData.txt";
        /// <summary>
        /// 包含的模型
        /// </summary>
        public List<ChildItemData> childItem = new List<ChildItemData>();
        /// <summary>
        /// 创建出来的模型
        /// </summary>
        public Dictionary<int, ModelButton> modelList = new Dictionary<int, ModelButton>();
        private bool isOpen = false;
        private void Awake()
        {
            Close();
            GetComponent<Button>().onClick.AddListener(Click);
        }
        public void OnInit(ConfigData data)
        {
            this.data = data;
            text.text = data.name;

        }
        /// <summary>
        /// 显示类型按钮
        /// </summary>
        public void Click()
        {
            if (isOpen) return;
            ModelLibrary.Instance.CloseAllChild();
            Open();
            if (itemDataList == null)
                StartCoroutine(LoadWebData());
            else
            {
                ModelLibrary.Instance.childGrid.parent.GetComponent<ScrollChild>().SetItem(this);
            }
            isOpen = true;
        }
        public void Open()
        {
            text.color = optionColor;
            backGround.SetActive(true);
        }
        public void Close()
        {
            isOpen = false;
            text.color = normaColor;
            backGround.SetActive(false);
            // StopCoroutine(CreatButton(null));
            foreach (KeyValuePair<int, ModelButton> item in modelList)
            {
                ObjectBool.Return(item.Value.gameObject);
            }
            modelList.Clear();
        }
        /// <summary>
        /// 下载模型
        /// </summary>
        public IEnumerator DownModel(ChildItemData data)
        {
            WWW www = new WWW(data.item.fileUrl);
            data.www = www;
            yield return www;
            if (www.error == "")
            {
                byte[] model = www.bytes;
                int length = model.Length;
                FileTools.CreateModelFile(Global.LocalUrl + data.item.catId + "/" + data.item.id, FileTools.ReturnNmae(data.item.fileUrl), model, length);
                data.item.fileDown = 1;
               // print(data.item.name);
                LoadData.Instance.downloadData.data.Insert(0,data.item);
                LoadData.Instance.childItem.Insert(0,data);
                BottomMenu.Instance.Open();
                string config = JsonFx.Json.JsonWriter.Serialize(LoadData.Instance.downloadData);
                FileTools.CreateFile(Global.LocalUrl, "download.txt", config);                
            }
            else
            {
#if UNITY_IPHONE
                IOSNativePopUpManager.showMessage("提示", "模型下载失败", "确认");
#elif UNITY_ANDROID
                   PrintMenuControl.Singleton.Open("模型下载失败");
#endif
                print(www.error);
            }

        }
        /// <summary>
        /// 读取网络数据
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoadWebData()
        {
            WWW www = new WWW(Global.getModelList + data.id);
            yield return www;
            if (www.error == "")
            {
                itemDataList = JsonFx.Json.JsonReader.Deserialize<ItemDataList>(www.text);
                itemDataList.data.Sort(delegate (ItemData p1, ItemData p2)
                {
                    return p1.orderNum.CompareTo(p2.orderNum);//升序
                });
                string config = JsonFx.Json.JsonWriter.Serialize(itemDataList);
                localData = FileTools.ReadText<ItemDataList>(Global.LocalUrl + data.id + "/" + webData);
                if (localData != null)
                {
                    for (int i = 0; i < localData.data.Count; i++)
                    {

                    }
                }

                FileTools.CreateFile(Global.LocalUrl + data.id + "/", webData, config);

                for (int i = 0; i < itemDataList.data.Count; i++)
                {
                    ChildItemData ci = new ChildItemData(itemDataList.data[i], null);
                    ci.state = LoadState.down;
                    childItem.Add(ci);
                }

            }
            else
            {
                localData = FileTools.ReadText<ItemDataList>(Global.LocalUrl + data.id + "/" + webData);
                for (int i = 0; i < localData.data.Count; i++)
                {
                    ChildItemData ci = new ChildItemData(localData.data[i], null);
                    ci.state = LoadState.down;
                    childItem.Add(ci);
                }
            }
            List<ChildItemData> itemData = new List<ChildItemData>();
            ModelLibrary.Instance.childGrid.parent.GetComponent<ScrollChild>().SetItem(this);
        }
    }
}