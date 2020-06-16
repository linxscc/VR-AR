/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using ModelViewerProject.Label3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PureMVCDemo
{

    /// <summary>
    /// 选择菜单
    /// </summary>
    public class ChoiceMenuControl
    {
        /// <summary>
        /// UI根目录文件
        /// </summary>
        private GameObject canvas;
        /// <summary>
        /// 菜单
        /// </summary>
        private GameObject menu;
        /// <summary>
        /// 网格组件
        /// </summary>
        private Transform grid;
        /// <summary>
        /// 菜单编号
        /// </summary>
        public int index;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="prefabName"></param>
        /// <param name="buttonPrefab"></param>
        /// pr
        private List<GameObject> buttonList;
        public ChoiceMenuControl(List<string> prefabName, GameObject buttonPrefab)
        {
            buttonList = new List<GameObject>();
            canvas = GameObject.FindGameObjectWithTag(Tag.mainUI);
            menu = Resources.Load<GameObject>(Global.choiceMenuControlUrl);
            menu = GameObject.Instantiate(menu);
            menu.transform.parent = canvas.transform;
            menu.transform.localPosition = new Vector3(0,0,-0.4f);
            menu.transform.localScale = new Vector3(.1f, .1f, .1f);
            grid = menu.GetComponent<ChoiceMenu>().grid;
            for (int i = 0; i < prefabName.Count; i++)
            {
                GameObject button = GameObject.Instantiate(buttonPrefab);
                button.transform.parent = grid;
                button.transform.localPosition = Vector3.zero;
                float z = 0;
                if (i == 0 || i == 4)
                {
                    z = -20f;

                    button.transform.eulerAngles = new Vector3(0, -50, 0);
                }
                else if (i == 3 || i == 7)
                {
                    z = -20f;
                    button.transform.eulerAngles = new Vector3(0,50, 0);
                }
                //else if(i)
                button.transform.localPosition = new Vector3(-(40 * 4 + 3 * 5 - 40) / 2 + i % 4 * 45, ((50 + 10) / 2 - (i / 4) * 60), z);
                button.transform.localScale = Vector3.one;
               
                menu.GetComponent<ChoiceMenu>().buttonList.Add(button);
                TextAsset text = Resources.Load<TextAsset>(prefabName[i] + "/" + prefabName[i]);
                // Texture texture = Resources.Load<Texture>(prefabName[i] + "/" + prefabName[i]);
                Sprite buttonSprite = Resources.Load(prefabName[i] + "/" + prefabName[i], typeof(Sprite)) as Sprite;
                button.GetComponent<ButtonChild>().OnInit(JsonFx.Json.JsonReader.Deserialize<LabelDataList>(text.text), buttonSprite);
                button.name = prefabName[i];
                
                buttonList.Add(button);
               
               // if (buttonSprite != null)
                  //  button.GetComponentInChildren<Image>().sprite = buttonSprite;


            }
        }
        /// <summary>
        /// 设置菜单显示隐藏
        /// </summary>
        /// <param name="isShow"></param>
        public void SetMenu(bool isShow)
        {
            if (isShow)
                menu.SetActive(true);
            else
                menu.SetActive(false);
        }
    }
}
