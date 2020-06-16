/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace MoDouAR
{
    public class MainMenuButton : MonoBehaviour
    {
        public Text modelName;
        public Image modelImage;
        /// <summary>
        /// 官方精选
        /// </summary>
        public GameObject offcial;
        /// <summary>
        /// 选择框
        /// </summary>
        public GameObject backImage;
        private ChildItemData data;
        public void OnInit(ChildItemData data)
        {
            this.data = data;
            backImage.SetActive(data.option);
            offcial.SetActive(data.offcial);
            modelName.text = data.item.name;
            modelImage.sprite = data.sprite;
        }
        void Awake()
        {
            backImage.SetActive(false);
            offcial.SetActive(false);
            GetComponent<Button>().onClick.AddListener(Click);
        }
        private void Click()
        {
            BottomMenu.Instance.CloseAll();
            backImage.SetActive(true);
            data.option = true;
            BottomMenu.Instance.CreatModel(data);
        }
        public void Close()
        {
            backImage.SetActive(false);

        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}