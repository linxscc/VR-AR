/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PureMVCDemo
{

    public class StartScene : MonoBehaviour
    {
        [SerializeField]
        private Button liftButton;
        [SerializeField]
        private Button rightButton;
        /// <summary>
        /// 所有的模型分页显示
        /// </summary>
        private Dictionary<int, List<string>> allButtonName;
        private List<string> groundName;
        private GameObject buttonPrefab;
        void Awake()
        {

            Global.UserDataNew = ToolGlobal.Read(Global.Url + "/Resources/Config/modification.xml");
            buttonPrefab = Resources.Load<GameObject>(Global.buttonPrefab);
            List<string> prefabName = new List<string>(Global.UserDataNew.prefab.prefabName.Split(','));
            allButtonName = new Dictionary<int, List<string>>();
            for (int i = 0; i < prefabName.Count; i++)
            {
                if (i % 8 == 0)
                {
                    groundName = new List<string>();
                    allButtonName.Add(i / 8, groundName);
                }
                groundName.Add(prefabName[i]);
            }
            LoadButton();
            EventTriggerListener.Get(liftButton.gameObject).onClick = Liftbutton;
            EventTriggerListener.Get(liftButton.gameObject).onEnter = OnEnter;
            EventTriggerListener.Get(liftButton.gameObject).onExit = OnExit;
            EventTriggerListener.Get(rightButton.gameObject).onClick = Rightbutton;
            EventTriggerListener.Get(rightButton.gameObject).onEnter = OnEnter;
            EventTriggerListener.Get(rightButton.gameObject).onExit = OnExit;
            //liftButton.OnPointerEnter()
            // StartCoroutine(LoadPrefab(new List<string>(Global.UserDataNew.prefab.prefabName.Split(','))));
            SetMenuControl.Singleton.Close();
        }
        private void OnEnter(GameObject b)
        {
            switch (b.name)
            {
                case "LiftButton":
                    HintInfoControl.Singleton.Open("上一页", b.transform.position.x, b.transform.position.y, 0);
                    break;
                case "RightButton":
                    HintInfoControl.Singleton.Open("下一页", b.transform.position.x, b.transform.position.y, 0);
                    break;
                default:
                    break;
            }
           
        }
        private void OnExit(GameObject b)
        {
            HintInfoControl.Singleton.Close();
        }
        /// <summary>
        /// 左选择
        /// </summary>
        /// <param name="b"></param>
        private void Liftbutton(GameObject b)
        {
            print("123");
        }
        /// <summary>
        /// 右选择
        /// </summary>
        /// <param name="b"></param>
        private void Rightbutton(GameObject b)
        {
            print("456");
        }
        private void LoadButton()
        {
            foreach (KeyValuePair<int, List<string>> kvp in allButtonName)
            {
                ChoiceMenuControl cmc = new ChoiceMenuControl(kvp.Value, buttonPrefab);
            }
        }

        void Update()
        {

        }
    }
}
