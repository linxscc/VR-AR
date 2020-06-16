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
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PureMVCDemo
{

    public class ButtonChild : MonoBehaviour
    {
        public LabelDataList labelDataList;

        public void OnInit(LabelDataList labelDataList)
        {
            this.labelDataList = labelDataList;
            EventTriggerListener.Get(gameObject).onClick = OnClick;
            EventTriggerListener.Get(gameObject).onEnter = OnEnter;
            EventTriggerListener.Get(gameObject).onExit = OnExit;
            GetComponentInChildren<Text>().text = labelDataList.transform.Name;
        }
        private void OnClick(GameObject o)
        {
            ConfirmMenuControl.Singleton.Open("确认选择:"+ labelDataList.transform.Name, Do);
        }
        private void Do()
        {
            SceneManager.LoadScene("MainScene");
            Global.modelName = labelDataList.transform.Name;
            Global.labelDataList = labelDataList;
        }
        private void OnEnter(GameObject o)
        {
            HintInfoControl.Singleton.Open(labelDataList.transform.Name,transform.position.x,transform.position.y,0);
        }
        private void OnExit(GameObject o)
        {
            HintInfoControl.Singleton.Close();
        }
        void Update()
        {

        }
    }
}
