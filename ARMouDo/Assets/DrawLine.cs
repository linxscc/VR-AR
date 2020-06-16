/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MoDouAR
{
    public class DrawLine : MonoBehaviour
    {

        public GameObject line;
        public List<Transform> wayPoint=new List<Transform>();
        public Canvas canvas;
        public Transform button;
        int i;
        void Awake()
        {
            i = 0;
            foreach (Transform item in transform)
            {
                wayPoint.Add(item);
            }
            //wayPoint = transform.GetComponentInChildren<Transform>();
        }
        void Update()
        {
            if (i < wayPoint.Count - 1)
            {
                Vector3 tempPos = (Vector3.zero + wayPoint[i ].position) / 2;//计算两个点的中点坐标，
                GameObject go = (GameObject)Instantiate(line, tempPos, Quaternion.identity);//在两个点的中点处实例化线条，因为对物体的缩放，是从中心向两边延伸
                go.name = "" + i;
                go.transform.right = (go.transform.position - wayPoint[i].position).normalized;//改变线条的朝向
                float distance = Vector3.Distance(Vector3.zero, wayPoint[i ].position);//计算两点的距离
                go.transform.localScale = new Vector3(distance, 0.01f, 0.01f);//延长线条，连接两点。
                i++;
            }
            for (int i = 0; i < wayPoint.Count; i++)
            {
                

            }
            button.GetComponent<RectTransform>().anchoredPosition= WorldToUIPoint(wayPoint[0]);
        }
        public  Vector2 WorldToUIPoint(Transform worldGo)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Camera.main.WorldToScreenPoint(worldGo.transform.position), canvas.worldCamera, out pos);
            RectTransform rect = transform.transform as RectTransform;
            return pos;
        }
    }
}