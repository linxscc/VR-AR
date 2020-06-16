using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum mode3D { Anaglyph, SideBySide, OverUnder, Interlace, Checkerboard, Alternate };
public enum method3D { Parallel, ToedIn };
public class Stereoskopix : MonoBehaviour
{
    public mode3D format3D = mode3D.Anaglyph;
    public Material stereoMaterial;
    public enum modeSBS { Squeezed, Unsqueezed };
    public modeSBS sideBySideOptions = modeSBS.Squeezed;
    public float interaxial = 0.25f;
    public float zeroParallax = 6;

   
    public method3D cameraMethod = method3D.Parallel;

    public enum cams3D { LeftRight, LeftOnly, RightOnly, RightLeft };
    public cams3D cameraSelect = cams3D.LeftRight;


    private Camera leftCam;             // These cameras must NOT use RenderTextures
    private Camera rightCam;
    private RenderTexture leftCamRT;
    private RenderTexture rightCamRT;

    private bool stereoEnabled = false;
    private float m_screenWidth = 0;
    private float m_screenHeight = 0;
    // Use this for initialization
    void Start()
    {

        if (!stereoMaterial)
        {
            Debug.LogError("No Stereo Material Found. Please drag 'stereoMat' into the Stereo Material Field");
            this.enabled = false;
            return;
        }

        UpdateView();

        GetComponent<Camera>().cullingMask = 0;
        GetComponent<Camera>().backgroundColor = new Color(0, 0, 0, 0);
        GetComponent<Camera>().clearFlags = CameraClearFlags.Nothing;
        GetComponent<Camera>().enabled = false;
    }
    private void EnsureRenderTextures()
    {
        if (m_screenWidth != Screen.width || m_screenHeight != Screen.height || leftCamRT == null || rightCamRT == null)
        {
            if (leftCamRT) leftCamRT.Release();
            if (rightCamRT) rightCamRT.Release();
            leftCamRT = new RenderTexture(Screen.width, Screen.height, 24);
            rightCamRT = new RenderTexture(Screen.width, Screen.height, 24);

            stereoMaterial.SetTexture("_LeftTex", leftCamRT);
            stereoMaterial.SetTexture("_RightTex", rightCamRT);

            if (stereoEnabled)
            {
                leftCam.targetTexture = leftCamRT;
                rightCam.targetTexture = rightCamRT;
            }

            m_screenWidth = Screen.width;
            m_screenHeight = Screen.height;
        }
    }
    void LateUpdate()
    {
        UpdateView();
    }


    public bool isStereoEnabled()
    {
        return stereoEnabled;
    }
   public void EnableStereo(Camera CameraLeft, Camera CameraRight, Transform CameraRigLeft, Transform CameraRigRight)
    {
        if (!CameraLeft || !CameraRight) return;

        leftCam = CameraLeft;
        rightCam = CameraRight;

        // Store left

        GetComponent<Camera>().projectionMatrix = leftCam.projectionMatrix;
        GetComponent<Camera>().nearClipPlane = leftCam.nearClipPlane;
        GetComponent<Camera>().farClipPlane = leftCam.farClipPlane;
        GetComponent<Camera>().aspect = leftCam.aspect;

        // Set up stereoscopic cameras

        GetComponent<Camera>().depth = Mathf.Max(leftCam.depth, rightCam.depth) + 1;

        leftCam.enabled = false;        // Cameras will render on demand at OnRenderImage
        rightCam.enabled = false;

        GetComponent<Camera>().enabled = true;
        stereoEnabled = true;
    }
    public void DisableStereo()
    {
        if (!stereoEnabled) return;

        leftCam.targetTexture = null;
        rightCam.targetTexture = null;
        if (leftCamRT) leftCamRT.Release();
        if (rightCamRT) rightCamRT.Release();
        leftCamRT = null;
        rightCamRT = null;

        leftCam.ResetProjectionMatrix();
        rightCam.ResetProjectionMatrix();

        //leftCam.transform.localPosition.x = 0.0;
        leftCam.transform.localPosition = new Vector3(0, leftCam.transform.localPosition.y, leftCam.transform.localPosition.z);
        GetComponent<Camera>().enabled = false;
        stereoEnabled = false;
    }
    void UpdateView()
    {
        if (!stereoEnabled)
            return;

        switch (cameraSelect)
        {
            case cams3D.LeftRight:
                leftCam.transform.position = transform.position + transform.TransformDirection(-interaxial / 2, 0, 0);
                rightCam.transform.position = transform.position + transform.TransformDirection(interaxial / 2, 0, 0);
                break;
            case cams3D.LeftOnly:
                leftCam.transform.position = transform.position + transform.TransformDirection(-interaxial / 2, 0, 0);
                rightCam.transform.position = transform.position + transform.TransformDirection(-interaxial / 2, 0, 0);
                break;
            case cams3D.RightOnly:
                leftCam.transform.position = transform.position + transform.TransformDirection(interaxial / 2, 0, 0);
                rightCam.transform.position = transform.position + transform.TransformDirection(interaxial / 2, 0, 0);
                break;
            case cams3D.RightLeft:
                leftCam.transform.position = transform.position + transform.TransformDirection(interaxial / 2, 0, 0);
                rightCam.transform.position = transform.position + transform.TransformDirection(-interaxial / 2, 0, 0);
                break;
        }

        if (cameraMethod == method3D.ToedIn)
        {
            leftCam.projectionMatrix = GetComponent<Camera>().projectionMatrix;
            rightCam.projectionMatrix = GetComponent<Camera>().projectionMatrix;
            leftCam.transform.LookAt(transform.position + (transform.TransformDirection(Vector3.forward) * zeroParallax));
            rightCam.transform.LookAt(transform.position + (transform.TransformDirection(Vector3.forward) * zeroParallax));
        }
        else
        {
            leftCam.transform.rotation = transform.rotation;
            rightCam.transform.rotation = transform.rotation;
            switch (cameraSelect)
            {
                case cams3D.LeftRight:
                    leftCam.projectionMatrix = projectionMatrix(true);
                    rightCam.projectionMatrix = projectionMatrix(false);
                    break;
                case cams3D.LeftOnly:
                    leftCam.projectionMatrix = projectionMatrix(true);
                    rightCam.projectionMatrix = projectionMatrix(true);
                    break;
                case cams3D.RightOnly:
                    leftCam.projectionMatrix = projectionMatrix(false);
                    rightCam.projectionMatrix = projectionMatrix(false);
                    break;
                case cams3D.RightLeft:
                    leftCam.projectionMatrix = projectionMatrix(false);
                    rightCam.projectionMatrix = projectionMatrix(true);
                    break;
            }
        }
    }
    private int m_frameNum = 0;

    public void SwitchEyes()
    {
        m_frameNum++;
        Debug.Log("Eyes switched!  " + m_frameNum);
    }
    void OnRenderImage(RenderTexture sourc, RenderTexture destination)
    {
        EnsureRenderTextures();

        // Alternate mode adapted to active stereoscopic 3D via plugin

        if (format3D == mode3D.Alternate)
        {
            leftCam.Render();
            Graphics.Blit(leftCamRT, destination);
            GL.IssuePluginEvent(1);

            rightCam.Render();
            Graphics.Blit(rightCamRT, destination);
            GL.IssuePluginEvent(2);

            return;
        }

        leftCam.targetTexture = leftCamRT;
        rightCam.targetTexture = rightCamRT;

        leftCam.Render();
        rightCam.Render();

        RenderTexture.active = destination;
        GL.PushMatrix();
        GL.LoadOrtho();

        switch (format3D)
        {
            case mode3D.Anaglyph:
                stereoMaterial.SetPass(0);
                DrawQuad(0);
                break;

            case mode3D.SideBySide:
            case mode3D.OverUnder:
                stereoMaterial.SetPass(1);
                DrawQuad(1);
                stereoMaterial.SetPass(2);
                DrawQuad(2);
                break;

            case mode3D.Interlace:
            case mode3D.Checkerboard:
                stereoMaterial.SetPass(3);
                DrawQuad(3);
                break;

            default:
                break;
        }
        GL.PopMatrix();

        if (format3D == mode3D.SideBySide)
            GL.IssuePluginEvent(0);
    }
    private void DrawQuad(int cam)
    {
        if (format3D == mode3D.Anaglyph)
        {
            GL.Begin(GL.QUADS);
            GL.TexCoord2(0, 0); GL.Vertex3(0, 0, 0.1f);
            GL.TexCoord2(1, 0); GL.Vertex3(1, 0, 0.1f);
            GL.TexCoord2(1, 1); GL.Vertex3(1, 1, 0.1f);
            GL.TexCoord2(0, 1); GL.Vertex3(0, 1, 0.1f);
            GL.End();
        }
        else
        {
            if (format3D == mode3D.SideBySide)
            {
                if (cam == 1)
                {
                    GL.Begin(GL.QUADS);
                    GL.TexCoord2(0, 0); GL.Vertex3(0, 0, 0.1f);
                    GL.TexCoord2(1, 0); GL.Vertex3(0.5f, 0, 0.1f);
                    GL.TexCoord2(1, 1); GL.Vertex3(0.5f, 1, 0.1f);
                    GL.TexCoord2(0, 1); GL.Vertex3(0, 1, 0.1f);
                    GL.End();
                }
                else
                {
                    GL.Begin(GL.QUADS);
                    GL.TexCoord2(0, 0); GL.Vertex3(0.5f, 0, 0.1f);
                    GL.TexCoord2(1, 0); GL.Vertex3(1, 0, 0.1f);
                    GL.TexCoord2(1, 1); GL.Vertex3(1, 1, 0.1f);
                    GL.TexCoord2(0, 1); GL.Vertex3(0.5f, 1, 0.1f);
                    GL.End();
                }
            }
            else if (format3D == mode3D.OverUnder)
            {
                if (cam == 1)
                {
                    GL.Begin(GL.QUADS);
                    GL.TexCoord2(0, 0); GL.Vertex3(0, 0.5f, 0.1f);
                    GL.TexCoord2(1, 0); GL.Vertex3(1, 0.5f, 0.1f);
                    GL.TexCoord2(1, 1); GL.Vertex3(1, 1, 0.1f);
                    GL.TexCoord2(0, 1); GL.Vertex3(0, 1, 0.1f);
                    GL.End();
                }
                else
                {
                    GL.Begin(GL.QUADS);
                    GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(0.0f, 0.0f, 0.1f);
                    GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1.0f, 0.0f, 0.1f);
                    GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(1.0f, 0.5f, 0.1f);
                    GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(0.0f, 0.5f, 0.1f);
                    GL.End();
                }
            }
            else if (format3D == mode3D.Interlace || format3D == mode3D.Checkerboard)
            {
                GL.Begin(GL.QUADS);
                GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(0.0f, 0.0f, 0.1f);
                GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(1, 0.0f, 0.1f);
                GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(1, 1.0f, 0.1f);
                GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(0.0f, 1.0f, 0.1f);
                GL.End();
            }
        }
    }
    Matrix4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far)
    {
        var x = (2 * near) / (right - left);
        var y = (2 * near) / (top - bottom);
        var a = (right + left) / (right - left);
        var b = (top + bottom) / (top - bottom);
        var c = -(far + near) / (far - near);
        var d = -(2 * far * near) / (far - near);
        var e = -1;

        Matrix4x4 m = new Matrix4x4();
        m[0, 0] = x; m[0, 1] = 0; m[0, 2] = a; m[0, 3] = 0;
        m[1, 0] = 0; m[1, 1] = y; m[1, 2] = b; m[1, 3] = 0;
        m[2, 0] = 0; m[2, 1] = 0; m[2, 2] = c; m[2, 3] = d;
        m[3, 0] = 0; m[3, 1] = 0; m[3, 2] = e; m[3, 3] = 0;
        return m;
    }
    Matrix4x4 projectionMatrix(bool isLeftCam)
    {
        float left;
        float right;
        float a;
        float b;
        float FOVrad;
        float aspect = GetComponent<Camera>().aspect;
        float tempAspect;
        if (sideBySideOptions == modeSBS.Unsqueezed && format3D == mode3D.SideBySide)
        {
            FOVrad = GetComponent<Camera>().fieldOfView / 90 * Mathf.PI;
            tempAspect = aspect / 2;
        }
        else
        {
            FOVrad = GetComponent<Camera>().fieldOfView / 180 * Mathf.PI;
            tempAspect = aspect;
        }

        a = GetComponent<Camera>().nearClipPlane * Mathf.Tan(FOVrad * 0.5f);
        b = GetComponent<Camera>().nearClipPlane / (zeroParallax + GetComponent<Camera>().nearClipPlane);

        if (isLeftCam)
        {
            left = -tempAspect * a + (interaxial / 2) * b;
            right = tempAspect * a + (interaxial / 2) * b;
        }
        else
        {
            left = -tempAspect * a - (interaxial / 2) * b;
            right = tempAspect * a - (interaxial / 2) * b;
        }

        return PerspectiveOffCenter(left, right, -a, a, GetComponent<Camera>().nearClipPlane, GetComponent<Camera>().farClipPlane);

    }
    // Update is called once per frame
    void Update()
    {

    }
}
