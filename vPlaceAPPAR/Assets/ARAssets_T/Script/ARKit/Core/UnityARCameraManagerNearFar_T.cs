
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;


namespace ARKit_T
{
    [RequireComponent(typeof(Camera))]
    public class UnityARCameraManagerNearFar_T : MonoBehaviour
    {
        public Camera m_camera;
        private UnityARSessionNativeInterface m_session;
        private Material savedClearMaterial;

        private Camera attachedCamera;
        private float currentNearZ;
        private float currentFarZ;
        public CallBack<Vector3, Vector3> cameraLocat;
        // Use this for initialization
        void Start()
        {

            m_session = UnityARSessionNativeInterface.GetARSessionNativeInterface();

#if !UNITY_EDITOR
            //	Application.targetFrameRate = 60;
            ARKitWorldTrackingSessionConfiguration config = new ARKitWorldTrackingSessionConfiguration();
        config.planeDetection = UnityARPlaneDetection.Horizontal;
        config.alignment = UnityARAlignment.UnityARAlignmentGravity;
        config.getPointCloudData = true;
        config.enableLightEstimation = true;
        m_session.RunWithConfig(config);

		if (m_camera == null) {
			m_camera = Camera.main;
		}
#else
            //put some defaults so that it doesnt complain
            UnityARCamera scamera = new UnityARCamera();
            scamera.worldTransform = new UnityARMatrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));
            Matrix4x4 projMat = Matrix4x4.Perspective(60.0f, 1.33f, 0.1f, 30.0f);
            scamera.projectionMatrix = new UnityARMatrix4x4(projMat.GetColumn(0), projMat.GetColumn(1), projMat.GetColumn(2), projMat.GetColumn(3));

            UnityARSessionNativeInterface.SetStaticCamera(scamera);

#endif
            attachedCamera = GetComponent<Camera>();
            UpdateCameraClipPlanes();
        }
        void UpdateCameraClipPlanes()
        {
            currentNearZ = attachedCamera.nearClipPlane;
            currentFarZ = attachedCamera.farClipPlane;
            UnityARSessionNativeInterface.GetARSessionNativeInterface().SetCameraClipPlanes(currentNearZ, currentFarZ);
        }

        public void SetCamera(Camera newCamera)
        {
            if (m_camera != null)
            {
                UnityARVideo oldARVideo = m_camera.gameObject.GetComponent<UnityARVideo>();
                if (oldARVideo != null)
                {
                    savedClearMaterial = oldARVideo.m_ClearMaterial;
                    Destroy(oldARVideo);
                }
            }
            SetupNewCamera(newCamera);
        }

        private void SetupNewCamera(Camera newCamera)
        {
            m_camera = newCamera;

            if (m_camera != null)
            {
                UnityARVideo unityARVideo = m_camera.gameObject.GetComponent<UnityARVideo>();
                if (unityARVideo != null)
                {
                    savedClearMaterial = unityARVideo.m_ClearMaterial;
                    Destroy(unityARVideo);
                }
                unityARVideo = m_camera.gameObject.AddComponent<UnityARVideo>();
                unityARVideo.m_ClearMaterial = savedClearMaterial;
            }
        }

        // Update is called once per frame

        void Update()
        {

            if (m_camera != null)
            {
                // JUST WORKS!
                Matrix4x4 matrix = m_session.GetCameraPose();
                m_camera.transform.localPosition = UnityARMatrixOps.GetPosition(matrix);
               // m_camera.transform.localRotation = UnityARMatrixOps.GetRotation(matrix);
                m_camera.projectionMatrix = m_session.GetCameraProjection();
                if (cameraLocat != null)
                    cameraLocat(m_camera.transform.localPosition, m_camera.transform.eulerAngles);
            }
            if (currentNearZ != attachedCamera.nearClipPlane || currentFarZ != attachedCamera.farClipPlane)
            {
                UpdateCameraClipPlanes();
            }

        }
    }
}
