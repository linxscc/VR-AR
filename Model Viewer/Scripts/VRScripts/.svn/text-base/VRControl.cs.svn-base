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
        }

        /// <summary>
        /// 扣下扳机
        /// </summary>
        /// <param name="sender"></param>
        public virtual void OnTriggerClicked(GameObject sender)
        {

        }
        /// <summary>
        /// 松开扳机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnTriggerUnclicked(GameObject sender)
        {

        }

    }
}
