/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace PlaceAR
{
    public class DownChild : MonoBehaviour
    {


        [SerializeField]
        private GameObject progress;
        [SerializeField]
        private GameObject AllName;
        [SerializeField]
        private GameObject selectBtn;
        [SerializeField]
        private GameObject downBtn;
        [SerializeField]
        private Text itemName;
        [SerializeField]
        private Text progressLabel;
        [SerializeField]
        private Image image;
        [HideInInspector]
        /// <summary>
        /// 数据
        /// </summary>
        public ItemChild data;
        /// <summary>
        /// 是否选择了按钮
        /// </summary>
        [HideInInspector]
        public bool isOn = false;
        /// <summary>
        /// 是否在下载
        /// </summary>
        private bool isDownloding;
        /// <summary>
        /// 是否在下载
        /// </summary>
        public bool IsDownloding
        {
            get { return IsDownloding; }
            set
            {
                isDownloding = value;
                progress.SetActive(!isDownloding);
                AllName.SetActive(isDownloding);


            }
        }
        public float Progress
        {
            set
            {
                //print(value+ data.name);
                RectTransform rect = progress.transform.Find("Image").GetComponent<RectTransform>();
                float x = rect.rect.width;
                rect.offsetMax = new Vector2(-966 * (1-value), rect.offsetMax.y);
                // progress.transform.Find("Image").GetComponent<Image>().fillAmount = value;
            }
        }
        private bool isSetSelect;
        /// <summary>
        /// 是否在选择模式
        /// </summary>
        public bool IsSetSelect
        {
            get { return isSetSelect; }
            set
            {
                isSetSelect = value;
                Return();
                // selectBtn.SetActive(isSetSelect);
                if (!isSetSelect)
                    downBtn.transform.localPosition = new Vector3(-3.5f, 0, 0);
                else
                    downBtn.transform.localPosition = new Vector3(100, 0, 0);
            }
        }
        public void OnInit(bool isDown, ItemChild data)
        {
            image.sprite = data.typeImage.sprite;
            IsDownloding = isDown;
            this.data = data;
            itemName.text = data.title.text;
            progressLabel.text = string.Format("{0}/{1}", data.configLocal.Count, data.configServer.Count);
            string alln = "";
            for (int i = 0; i < data.configServer.Count; i++)
            {
                alln += data.configServer[i].name + "，";
            }
            AllName.GetComponent<Text>().text = alln;
            Progress = (float)data.configLocal.Count / data.configServer.Count;
        }
        public void SelectBtn(bool b)
        {
            isOn = b; ;
        }
        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="isAdd"></param>
        public void AddBtn(bool isAdd)
        {

            if (isOn == false)
            {
                ExecuteEvents.Execute<IPointerClickHandler>(selectBtn.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                isOn = isAdd;
            }
            else
            {
                ExecuteEvents.Execute<IPointerClickHandler>(selectBtn.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                isOn = false;
            }
            //selectBtn.GetComponent<Toggle>().isOn = isAdd;
            // selectBtn.transform.Find("Button").gameObject.SetActive(isAdd);
        }
        public void Return()
        {
            if (isOn == true)
            {
                ExecuteEvents.Execute<IPointerClickHandler>(selectBtn.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                isOn = false;
            }
        }
        public void DelPrefab()
        {
            IsDownloding = false;
            progressLabel.text = string.Format("{0}/{1}", 0, data.configServer.Count);
            Progress = 0;
            Return();
            data.UpdataData();
        }
    }
}