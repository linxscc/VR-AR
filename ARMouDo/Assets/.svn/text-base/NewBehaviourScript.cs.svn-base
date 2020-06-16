/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace MoDouAR
{
    public class NewBehaviourScript : MonoBehaviour
    {
        public GameObject obj;
        // Use this for initialization
        void Start()
        {
            obj.transform.position= transform.TransformPoint(0,0, 1);
            //obj.transform.position = aRCamera.transform.TransformPoint(0, 0, 1);
            Tween t = obj.transform.DOScale(0.6f, 0.5f).SetLoops(-1,LoopType.Yoyo);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}