using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using ModelViewerProject.Label3D;
using UnityEngine.EventSystems;

namespace HTCVIVE
{
    public class VRControlSelect : VRControl
    {
        public StartScene startScene;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                SetMenuControl.Singleton.Set2D3D(null);
            }
        }
        /// <summary>
        /// 圆盘触摸
        /// </summary>
        /// <param name="arg"></param>
        protected override void PadChilcked(ChickArg arg)
        {
            switch (arg.padDirection)
            {
                case Direction.lift:
                    startScene.Liftbutton(null);
                    break;
                case Direction.right:
                    startScene.Rightbutton(null);
                    break;
            }
        }
        /// <summary>
        /// 菜单事件
        /// </summary>
        /// <param name="sender"></param>
        protected override void OnMenuClicked(GameObject sender)
        {
            SetMenuControl.Singleton.TriggerControl();
        }
        protected override void OnGripped(Vector2 sender)
        {
            SetMenuControl.Singleton.Set2D3D(null);
        }
        protected override void PadPress(Vector2 v)
        {
            if (Mathf.Abs(v.x) > Mathf.Abs(v.y))
                SetMenuControl.Singleton.PointControl(v.x);
            else
                SetMenuControl.Singleton.EyeDistanceControl(v.y);
        }
        protected override void TriggerPress(GameObject sender)
        {
            //print("ddd");
            // RaycastHit hit = sender.GetComponent<SteamVR_LaserPointer>().hit;
            // ExecuteEvents.Execute<IDragHandler>(hit.transform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.dragHandler);
        }
        /// <summary>
        /// 扣下扳机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnTriggerClicked(GameObject sender)
        {
            // GL.
            RaycastHit hit = sender.GetComponent<SteamVR_LaserPointer>().hit;
            if (hit.transform != null && hit.transform.tag == Tag.prefab)
            {
                SceneManager.LoadScene("MainScene");
                Global.modelName = hit.transform.name;
                TextAsset[] text = Resources.LoadAll<TextAsset>(hit.transform.name);
                Global.labelDataList = JsonFx.Json.JsonReader.Deserialize<LabelDataList>(text[0].text);
            }
            if (hit.transform != null && hit.transform.gameObject.layer == 5)
            {
                ExecuteEvents.Execute<IPointerClickHandler>(hit.transform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                //ExecuteEvents.Execute<IPointerDownHandler>(hit.transform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
                // ExecuteEvents.Execute<IDragHandler>(hit.transform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.dragHandler);
                //  ExecuteEvents.Execute<IDropHandler>(hit.transform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.dropHandler);
                // ExecuteEvents.Execute<ISubmitHandler>(hit.transform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler);
            }
            //print(sender.GetComponent<SteamVR_LaserPointer>().hit.transform.gameObject.tag);

        }
    }
}
