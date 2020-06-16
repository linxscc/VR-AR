using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using ModelViewerProject.Label3D;
namespace HTCVIVE
{
    public class VRControlSelect : VRControl
    {
        public ReadXML readXML;
        public override void Awake()
        {
            base.Awake();
            lift.padClicked += PadChilcked;
            right.padClicked += PadChilcked;
        }
        /// <summary>
        /// 圆盘触摸
        /// </summary>
        /// <param name="arg"></param>
        private void PadChilcked(ChickArg arg)
        {
            switch (arg.padDirection)
            {
                case Direction.lift:
                    readXML.OptionLift();
                    break;
                case Direction.right:
                    readXML.OptionRight();
                    break;
            }
        }
        /// <summary>
        /// 扣下扳机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnTriggerClicked(GameObject sender)
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
            //print(sender.GetComponent<SteamVR_LaserPointer>().hit.transform.gameObject.tag);

        }
    }
}
