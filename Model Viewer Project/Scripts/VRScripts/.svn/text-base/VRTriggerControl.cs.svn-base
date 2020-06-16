using UnityEngine;
using UnityEngine.SceneManagement;
using ModelViewerProject.Label3D;
using ModelViewerProject.Model;
using ModelViewerProject.UI;
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
        /// <summary>
        /// 记录当前点击的模型
        /// </summary>
        public GameObject pointPrefab;
        /// <summary>
        /// 模型的父物体
        /// </summary>
        private Transform parentPrefab;
        private bool isPlay = true;
        RaycastHit hit;
        public override void Awake()
        {
            base.Awake();
            modelRotateUIGroup = uIController.transform.GetComponentInChildren<ModelRotateUIGroup>();
            lift.menuButtonClicked += OnMenuClicked;
            right.menuButtonClicked += OnMenuClicked;
            lift.gripped += OnGripped;
            right.gripped += OnGripped;
            lift.padClicked += PadChilcked;
            right.padClicked += PadChilcked;
            lift.PadUnclicked += OnPadUnclicked;
            right.PadUnclicked += OnPadUnclicked;
            //lift.Gripped += OnGripped;
            //right.Gripped += OnGripped;
            //lift.Ungripped += OnUngripped;
            //right.Ungripped += OnUngripped;
            //lift.MenuButtonClicked += OnMenuClicked;
            //right.MenuButtonClicked += OnMenuClicked;
            //lift.PadClicked += OnPadClicked;
            //right.PadClicked += OnPadClicked;
            //lift.PadUnclicked += OnPadUnclicked;
            //right.PadUnclicked += OnPadUnclicked;

        }
        /// <summary>
        /// 弹起圆盘按钮
        /// </summary>
        /// <param name="obj"></param>
        private void OnPadUnclicked(GameObject obj)
        {
            modelRotateUIGroup.StopRotate();
        }
        /// <summary>
        /// 圆盘触摸
        /// </summary>
        /// <param name="arg"></param>
        private void PadChilcked(ChickArg arg)
        {
            switch (arg.padDirection)
            {
                case Direction.up:
                    modelRotateUIGroup.RotateToUp();
                    break;
                case Direction.down:
                    modelRotateUIGroup.RotateToDown();
                    break;
                case Direction.lift:
                    modelRotateUIGroup.RotateToLeft();
                    break;
                case Direction.right:
                    modelRotateUIGroup.RotateToRight();
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
        private void OnMenuClicked(GameObject sender)
        {
            modelRotateUIGroup.BackHome();
            pointPrefab = null;
        }
        /// <summary>
        /// 引发握紧事件
        /// </summary>
        /// <param name="e">E.</param>
        private void OnGripped(GameObject sender)
        {
            if (uIController.AssembleToggle.isOneEnable)
                uIController.AssembleToggle.OnButtonClicked("Assemble");
            else
                uIController.AssembleToggle.OnButtonClicked("Disassemble");
            pointPrefab = null;
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
        /// <summary>
        /// 扣下扳机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnTriggerClicked(GameObject sender)
        {
            hit = sender.GetComponent<SteamVR_LaserPointer>().hit;

            if (hit.transform != null && hit.transform.tag == Tag.prefab)
            {
               
                if (hit.transform.gameObject != pointPrefab)
                {
                    isPlay = false;
                    modelController.HideOthersButOne(hit.transform.GetComponent<Label3DHandler>(), OnComplete);
                    pointPrefab = hit.transform.gameObject;
                }
                else
                {
                  
                    // print("enter");
                    if (parentPrefab == null)
                        parentPrefab = hit.transform.parent;
                    hit.transform.parent = sender.transform;
                }

            }
            //  modelController.HideOthersBut();
            // print(sender.GetComponent<SteamVR_LaserPointer>().hit.transform.gameObject.tag);
            // print((sender));
        }
        public override void OnTriggerUnclicked(GameObject sender)
        {
            if(isPlay&&hit.transform!=null )
            hit.transform.parent = null;
        }

    }
}
