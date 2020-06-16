/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PlaceAR
{
    //镭射光标事件结构体
    public struct PointerEventArgs
    {
        //控制器索引
        public uint controllerIndex;
        //标记
        public uint flags;
        //距离
        public float distance;
        //目标
        public Transform target;
    }
    public class LaserPointer : MonoBehaviour
    {
        public RaycastHit hit;
        //之前接触的对象
        private Transform previousContact = null;
        public Button button;
        // Use this for initialization
        void Start()
        {
            EventTriggerListener.Get(button.gameObject).onClick += OnClick;
        }
        private void OnClick(GameObject obj)
        {
            print(obj.name+"--");
        }
        // Update is called once per frame
        void Update()
        {
            Ray raycast = new Ray(transform.position, transform.forward);
            // RaycastHit hit;
            bool bHit = Physics.Raycast(raycast, out hit);
            if (bHit)
                Debug.DrawLine(transform.position, hit.point, Color.red);
            if (bHit && previousContact != hit.transform && (hit.transform.tag == Tag.prefab || hit.transform.gameObject.layer == 5))
            {
                PointerEventArgs argsIn = new PointerEventArgs();
                argsIn.distance = hit.distance;
                argsIn.flags = 0;
                argsIn.target = hit.transform;
                OnPointerOut(argsIn);
                previousContact = hit.transform;
            }
            //没有击中
            if (!bHit)
            {

                previousContact = null;
            }
        }
        private void OnPointerOut(PointerEventArgs args)
        {
            print(args);
            ExecuteEvents.Execute<IPointerClickHandler>(args.target.transform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
        }
    }
}