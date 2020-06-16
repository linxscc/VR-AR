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
        public ChoiceMenuControl(List<string> prefabName, GameObject buttonPrefab)
        {
            canvas = GameObject.FindGameObjectWithTag(Tag.mainUI);
            menu = Resources.Load<GameObject>(Global.choiceMenuControlUrl);
            menu = GameObject.Instantiate(menu);
            menu.transform.parent = canvas.transform;
            menu.transform.localPosition = new Vector3(0,0,-0.4f);
            menu.transform.localScale = new Vector3(.1f, .1f, .1f);
            grid = menu.GetComponent<ChoiceMenu>().grid;
            foreach (string item in prefabName)
            {
                GameObject button = GameObject.Instantiate(buttonPrefab);
                button.transform.parent = grid;
                button.transform.localScale = Vector3.one;
                menu.GetComponent<ChoiceMenu>().buttonList.Add(button);
                TextAsset text = Resources.Load<TextAsset>(item+"/"+item);
                button.GetComponent<ButtonChild>().OnInit(JsonFx.Json.JsonReader.Deserialize<LabelDataList>(text.text));
                //Resources.Load<Texture>("");
            }
        }

    }
}
