/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using DG.Tweening;
using ModelViewerProject.Label3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace PureMVCDemo
{
    /// <summary>
    /// 选择模型
    /// </summary>
    public class ButtonChild : MonoBehaviour
    {
        /// <summary>
        /// 数据
        /// </summary>
        public LabelDataList labelDataList;
        /// <summary>
        /// z的初始距离
        /// </summary>
        private float localZ;
        public void OnInit(LabelDataList labelDataList,Sprite buttonSprite)
        {
            this.labelDataList = labelDataList;
            EventTriggerListener.Get(gameObject).onClick = OnClick;
            EventTriggerListener.Get(gameObject).onEnter = OnEnter;
            EventTriggerListener.Get(gameObject).onExit = OnExit;
            GetComponentInChildren<Text>().text = labelDataList.transform.Name;
            localZ = transform.localPosition.z;
            if (buttonSprite != null)
                transform.FindChild("Image").GetComponent<Image>().sprite = buttonSprite;
        }
        private void OnClick(GameObject o)
        {
            SceneManager.LoadScene("MainScene");
            Global.modelName = labelDataList.transform.LocalName;
            Global.labelDataList = labelDataList;
            //ARStartupControl.ARInstance.ARStartup.LoadMainSceneSetCameraPos();
            // ConfirmMenuControl.Singleton.Open("确认选择:" + labelDataList.transform.Name, Do);
        }
        private void Do(bool d)
        {
            if (!d) return;
            SceneManager.LoadScene("MainScene");
            Global.modelName = labelDataList.transform.LocalName;
            Global.labelDataList = labelDataList;
           // ARStartupControl.ARInstance.ARStartup.LoadMainSceneSetCameraPos();
        }
        private void OnEnter(GameObject o)
        {
            HintInfoControl.Singleton.Open(labelDataList.transform.Name, transform.position.x, transform.position.y, 0);
            transform.DOLocalMoveZ(localZ - 6,0.1f);
            //transform.DOScale(1.2f,0.1f);
        }
        private void OnExit(GameObject o)
        {
            transform.DOLocalMoveZ(localZ+6, 0.1f);
            //transform.DOScale(1f, 0.1f);
            HintInfoControl.Singleton.Close();
        }
        void Update()
        {

        }
    }
}
