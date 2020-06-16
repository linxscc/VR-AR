/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.UI;
using UI_XYRF;
using UnityEngine.SceneManagement;

namespace MoDouAR
{
    public class SplshScene : MonoBehaviour
    {
        private List<Image> allImage = new List<Image>();
        private float intervalTime = 2f;
        private PanelEnabl panelEnabl;
        private void Start()
        {
           // StartSceneControl.Singleton.Close();
            ARKitControl.Instance.IntoOther();
            panelEnabl = GameObject.Find("PanelEnabl").GetComponent<PanelEnabl>();
            foreach (Transform item in transform)
            {
                if (item.GetComponent<Image>())
                {
                    Color cl = item.GetComponent<Image>().color;
                    item.GetComponent<Image>().color = new Color(cl.r, cl.g, cl.b, 0);
                    allImage.Add(item.GetComponent<Image>());
                }
            }
            Color c = allImage[0].GetComponent<Image>().color;
            Tweener tween = allImage[0].DOColor(new Color(c.r, c.g, c.b, 1), 1.5f);
            tween.OnComplete(DoEnd);
            //StartCoroutine(ShowImage());
        }
        private void DoEnd()
        {
            Color c = allImage[0].GetComponent<Image>().color;
            Tweener tween = allImage[0].DOColor(new Color(c.r, c.g, c.b, 0), 1.5f);
            tween.OnComplete(OverEnd);
        }
        private void OverEnd()
        {
            //print("enter");
            if (panelEnabl.isFistOpen)
            {
                Destroy(gameObject);
                //SceneManager.LoadScene(Global.modouAR);
            }
               
            else
            {


                //Scene allScene = SceneManager.GetSceneByName(Global.startScene);
                //SceneManager.MoveGameObjectToScene(StartSceneControl.Singleton.startObj, allScene);
                //StartSceneControl.Singleton.Open();
               // Global.OperatorModel = OperatorMode.BrowserMode;
                SceneManager.LoadScene(Global.modouAR);
               // print("end");
              //  panelEnabl.async.allowSceneActivation = true;
                
               // SceneManager.UnloadScene(SceneManager.GetSceneByName(Global.enablScene));
                
            }

        }
        private IEnumerator ShowImage()
        {
            float a = 0;
            while (intervalTime <= 0)
            {
                if (intervalTime >= intervalTime / 2)
                    allImage[0].color = new Color(allImage[0].color.r, allImage[0].color.g, allImage[0].color.b, a);
                intervalTime -= 0.2f;
                yield return new WaitForFixedUpdate();
            }
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}