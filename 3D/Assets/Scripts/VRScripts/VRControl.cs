using UnityEngine;
using ModelViewerProject.Model;
namespace HTCVIVE
{

    public class VRControl : MonoBehaviour
    {
        // public SteamVR_TrackedObject lift;
        // public SteamVR_TrackedObject right;

        public VRButtonTouchAction lift;
        public VRButtonTouchAction right;
        //  public bool isOnTrigger = false;
        // Use this for initialization
        public virtual void Awake()
        {
            lift.triggerClicked += OnTriggerClicked;
            right.triggerClicked += OnTriggerClicked;
            // lift.TriggerClicked += OnTriggerClicked;
            // right.TriggerClicked += OnTriggerClicked;
            lift.triggerUnclicked += OnTriggerUnclicked;
            right.triggerUnclicked += OnTriggerUnclicked;
            lift.padClicked += PadChilcked;
            right.padClicked += PadChilcked;
            lift.PadUnclicked += OnPadUnclicked;
            right.PadUnclicked += OnPadUnclicked;
            lift.menuButtonClicked += OnMenuClicked;
            right.menuButtonClicked += OnMenuClicked;
            lift.gripped += OnGripped;
            right.gripped += OnGripped;
            lift.triggerPress += TriggerPress;
            right.triggerPress += TriggerPress;
            lift.padPress += PadPress;
            right.padPress += PadPress;
        }
        protected virtual void TriggerPress(GameObject obj)
        {

        }
        protected virtual void PadPress(Vector2 v)
        {

        }
        /// <summary>
        /// 圆盘触摸
        /// </summary>
        /// <param name="arg"></param>
        protected virtual void PadChilcked(ChickArg arg)
        {

        }
        /// <summary>
        /// 引发握紧事件
        /// </summary>
        /// <param name="e">E.</param>
        protected virtual void OnGripped(Vector2 sender)
        {

        }
        /// <summary>
        /// 菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnMenuClicked(GameObject sender)
        {

        }
        /// <summary>
        /// 弹起圆盘按钮
        /// </summary>
        /// <param name="obj"></param>
        protected virtual void OnPadUnclicked(GameObject obj)
        {
            
        }
        /// <summary>
        /// 扣下扳机
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnTriggerClicked(GameObject sender)
        {

        }
        /// <summary>
        /// 松开扳机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnTriggerUnclicked(GameObject sender)
        {

        }

    }
}
