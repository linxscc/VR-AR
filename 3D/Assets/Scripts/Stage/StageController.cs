using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ModelViewerProject.Stage
{

    public class StageController : MonoBehaviour
    {

        #region Stereo
       // public StereoCam stereoCamera;

        public RectTransform cursor;
        public Transform root;
        float z = 0;
       // public Text text;
        public virtual void Start()
        {
            z= root.position.z;
            Render2D(Global.is2D);
        }
        void Update()
        {

            var cam = StereoControl.Singleton.stereoCam.CamL;//GetComponent<Camera>();
            cursor.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.9F));
            cursor.transform.SetSiblingIndex(cursor.transform.parent.childCount - 1);
            if (Input.GetKeyDown(KeyCode.F1))
            {

                StereoControl.Singleton.stereoCam.parallaxDistance += 0.1f;
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                StereoControl.Singleton.stereoCam.parallaxDistance -= 0.1f;
            }
            //text.text = stereoCamera.parallaxDistance.ToString()+"=="+ z;
            if (Input.GetKeyDown(KeyCode.F3))
            {
                z += 0.1f;
                root.position = new Vector3(root.position.x, root.position.y, z);
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                z -= 0.1f;
                root.position = new Vector3(root.position.x, root.position.y, z);
            }
        }
        private void OnGUI()
        {

        }
        //  public void Render3D()
        //  {
        // stereoCamera.stereo = StereoModes.SideBySide;
        //  }

        public void Render2D(bool is2D)
        {
           // print(is2D);
            if (is2D)
                StereoControl.Singleton.stereoCam.stereo = StereoModes.Disabled;
            else
                StereoControl.Singleton.stereoCam.stereo = StereoModes.SideBySide;
            Global.is2D = is2D;
        }
        #endregion Stereo
    }

}