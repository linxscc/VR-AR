using UnityEngine;
using UnityEngine.SceneManagement;
using ModelViewerProject.Label3D;
using ModelViewerProject.Model;
using ModelViewerProject.UI;
using UnityEngine.EventSystems;
using HighlightingSystem;

namespace HTCVIVE
{
    /// <summary>
    /// 操作场景vive手柄控制
    /// </summary>
    public class VRTriggerControl : VRControl
    {
        public ModelController modelController;
        public UIController uIController;
        private ModelRotateUIGroup modelRotateUIGroup;
        public UIModelMenu uIModelMenu;
        /// <summary>
        /// 记录当前点击的模型
        /// </summary>
        [HideInInspector]
        public GameObject pointPrefab;
        /// <summary>
        /// 模型
        /// </summary>
        [HideInInspector]
        public Transform parentPrefab;
        private bool isPlay = true;
        RaycastHit hit;

        public override void Awake()
        {
            base.Awake();
           // pointPrefab = parentPrefab.gameObject;
           // parentPrefab = GetComponent<ModelImporter>().obj.transform;
            lift.padPress += PadPress;
            right.padPress += PadPress;
            modelRotateUIGroup = uIController.transform.GetComponentInChildren<ModelRotateUIGroup>();
        }
        /// <summary>
        /// 弹起圆盘按钮
        /// </summary>
        /// <param name="obj"></param>
        protected override void OnPadUnclicked(GameObject obj)
        {
           // modelRotateUIGroup.StopRotate();
        }
        /// <summary>
        /// 触摸圆盘按住
        /// </summary>
        /// <param name="chickArg"></param>
        protected override void PadPress(Vector2 speed)
        {
            //uIController.RotateGroup_OnRotate(speed, trans);
            // if (pointPrefab == null)
            // RotateGroup(arg, null);
            if (Global.labelDataList.controlType == 0)
            {
                if (pointPrefab == null)
                    pointPrefab = parentPrefab.gameObject;
                uIController.RotateGroup_OnRotate(speed * 50, pointPrefab.transform);
               print( pointPrefab.name);
            }
                //RotateGroup(arg, pointPrefab.transform);
            
            else if (Global.labelDataList.controlType == 1 || Global.labelDataList.controlType == 2)
            {
                //RotateGroup(arg, parentPrefab.transform);
               
                uIController.RotateGroup_OnRotate(speed*50, parentPrefab);
            }
            //print(speed.x);
        }
        /// <summary>
        /// 圆盘点击触摸
        /// </summary>
        /// <param name="arg"></param>
        protected override void PadChilcked(ChickArg arg)
        {

            //if (pointPrefab == null)
            //    RotateGroup(arg, null);
            //else if (Global.labelDataList.controlType == 0)
            //    RotateGroup(arg, pointPrefab.transform);
            //else if (Global.labelDataList.controlType == 1 || Global.labelDataList.controlType == 2)
            //{
            //    RotateGroup(arg, parentPrefab.transform);
            //}
        }
        /// <summary>
        /// 旋转
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="trans"></param>
        private void RotateGroup(ChickArg arg,Transform trans)
        {
            switch (arg.padDirection)
            {
                case Direction.up:

                    uIController.RotateGroup_OnRotate(Vector2.up, trans);
                    //modelRotateUIGroup.RotateToUp();
                    break;
                case Direction.down:
                    uIController.RotateGroup_OnRotate(Vector2.down, trans);
                    // modelRotateUIGroup.RotateToDown();
                    break;
                case Direction.lift:
                    uIController.RotateGroup_OnRotate(Vector2.left, trans);
                    // modelRotateUIGroup.RotateToLeft();
                    break;
                case Direction.right:
                    uIController.RotateGroup_OnRotate(Vector2.right, trans);
                    //modelRotateUIGroup.RotateToRight();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnMenuClicked(GameObject sender)
        {
            pointPrefab = parentPrefab.gameObject;
            uIModelMenu.BackHome(null);
            //modelRotateUIGroup.BackHome();
            // modelRotateUIGroup.
            
        }
        /// <summary>
        /// 引发握紧事件
        /// </summary>
        /// <param name="e">E.</param>
        protected override void OnGripped(Vector2 sender)
        {
            uIModelMenu.OnAssembleDis(null);
           // if (uIController.AssembleToggle.isOneEnable)
              //  uIController.AssembleToggle.OnButtonClicked("Assemble");
          //  else
               // uIController.AssembleToggle.OnButtonClicked("Disassemble");
            pointPrefab = parentPrefab.gameObject;
        }
        /// <summary>
        /// 动画结束回调
        /// </summary>
        private void OnComplete()
        {
            isPlay = true;
            // parentPrefab = hit.transform.parent;
            //  hit.transform.parent = null;
        }
        protected override void TriggerPress(GameObject obj)
        {
            print("enter");
            //base.TriggerPress();
        }
        /// <summary>
        /// 扣下扳机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnTriggerClicked(GameObject sender)
        {
            hit = sender.GetComponent<SteamVR_LaserPointer>().hit;

            if (hit.transform != null && hit.transform.tag == Tag.prefab)
            {

                // if (hit.transform.gameObject != pointPrefab)
                //  {
                // isPlay = false;
                //modelController.HideOthersButOne(hit.transform.GetComponent<Label3DHandler>(), OnComplete);
                //pointPrefab = hit.transform.gameObject;
                //  }
                // else
                // {


                pointPrefab = hit.transform.gameObject;
                if (Global.labelDataList.controlType == 0)
                {
                    hit.transform.parent = sender.transform;
                    //if (hit.transform.GetComponent<FlashingController>() == null)
                    //{
                    //    hit.transform.gameObject.AddComponent<FlashingController>();
                    //}
                }
                else if (Global.labelDataList.controlType == 1|| Global.labelDataList.controlType == 2)
                {
                    parentPrefab.transform.parent = sender.transform;
                    //if (parentPrefab.GetComponent<FlashingController>() == null)
                    //{
                    //    parentPrefab.gameObject.AddComponent<FlashingController>();
                    //}
                   
                }   


            }
            if (hit.transform != null && hit.transform.gameObject.layer == 5)
            {
                ExecuteEvents.Execute<IPointerClickHandler>(hit.transform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                ExecuteEvents.Execute<ISubmitHandler>(hit.transform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler);
            }
            //  modelController.HideOthersBut();
            // print(sender.GetComponent<SteamVR_LaserPointer>().hit.transform.gameObject.tag);
            // print((sender));
        }
        /// <summary>
        /// 弹起扳机
        /// </summary>
        /// <param name="sender"></param>
        protected override void OnTriggerUnclicked(GameObject sender)
        {
            if (hit.transform != null && hit.transform.tag == Tag.prefab)
            {
                if (Global.labelDataList.controlType == 0)
                {
                    hit.transform.parent = null;
                   
                    //if (hit.transform.GetComponent<FlashingController>())
                    //{
                    //    Destroy(hit.transform.gameObject.GetComponent<FlashingController>());
                    //    Destroy(hit.transform.gameObject.GetComponent<Highlighter>());
                    //    // Destroy(gameObject.GetComponent<hi>());
                    //}

                }
                else if (Global.labelDataList.controlType == 1 || Global.labelDataList.controlType == 2)
                {
                    //if (parentPrefab.transform.GetComponent<FlashingController>())
                    //{
                    //    Destroy(parentPrefab.gameObject.GetComponent<FlashingController>());
                    //    Destroy(parentPrefab.gameObject.GetComponent<Highlighter>());
                    //    // Destroy(gameObject.GetComponent<hi>());
                    //}
                    parentPrefab.transform.parent = modelController.transform;
                }
              
            }
           
        }

    }
}
