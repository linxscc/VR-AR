using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StereoModes { Disabled, Anaglyph, Interlaced, Active, SideBySide };
public class StereoCam : MonoBehaviour
{


    public Camera CamL;
    public Camera CamR;
    public StereoModes stereo = StereoModes.Disabled;

    public float eyeDistance = 0.06f;
    public float parallaxDistance = 3;

    public bool autoEyeDistance = false;
    public bool autoEyeParallax = false;
    public LayerMask autoEyeLayerMask = (1 << 32) - 1;
    public float autoEyeMinRange = 0.25f;
    public float autoEyeMaxRange = 0.75f;
    public float autoEyeDistanceMin = 0.01f;
    public float autoEyeDistanceMax = 0.06f;
    public float autoEyeDistanceSpeed = 2.0f;
    public float autoEyeParallaxMin = 1.0f;
    public float autoEyeParallaxMax = 3.0f;
    public float autoEyeParallaxSpeed = 2.0f;


    private Stereoskopix m_StereoScript;
    public Transform prefabPoint;
    private Text text;
    void Awake()
    {
        //CamL.targetDisplay
        m_StereoScript = GetComponent<Stereoskopix>();
        if (!Global.is2D)
        {
            stereo= StereoModes.SideBySide;
        }
        else
        {
            stereo = StereoModes.Disabled;
        }
        DontDestroyOnLoad(this);
        GameObject.FindGameObjectWithTag(Tag.mainUI).GetComponent<Canvas>().worldCamera = CamL;
        text = GameObject.Find("eyedistance").GetComponentInChildren<Text>();
    }


    private void DoAutoEyeParameters()
    {
        RaycastHit Hit;
        var touch = false;
        float distanceRatio = 0;

        if (autoEyeDistance || autoEyeParallax)
        {
            touch = Physics.Raycast(transform.position, transform.forward, out Hit, Mathf.Infinity, autoEyeLayerMask);
            distanceRatio = Mathf.InverseLerp(autoEyeMinRange, autoEyeMaxRange, Hit.distance);
            if (!touch) distanceRatio = 1;
        }

        if (autoEyeDistance)
        {
            var newDistance = Mathf.Lerp(autoEyeDistanceMin, autoEyeDistanceMax, distanceRatio);
            eyeDistance = Mathf.Lerp(eyeDistance, newDistance, autoEyeDistanceSpeed * Time.deltaTime);
        }

        if (autoEyeParallax)
        {
            var newParallax = Mathf.Lerp(autoEyeParallaxMin, autoEyeParallaxMax, distanceRatio);
            parallaxDistance = Mathf.Lerp(parallaxDistance, newParallax, autoEyeParallaxSpeed * Time.deltaTime);
        }
    }


    void Update()
    {
        DoAutoEyeParameters();

        if (parallaxDistance < 0.2) parallaxDistance = 0.2f;
        if (eyeDistance < 0.0) eyeDistance = 0.0f;
        if (eyeDistance > 0.2) eyeDistance = 0.2f;

        if (stereo != StereoModes.Disabled && CamR)
        {
            // Enable stereoscopic 3D mode always as Parallel 
            // (uses camera.fieldOfView to calculate camera's parallel frustrums)

            if (!m_StereoScript.isStereoEnabled() || m_StereoScript.cameraMethod != method3D.Parallel)
            {
                // EnableStereo will disable the children cameras and render them on demand

                m_StereoScript.DisableStereo();
                m_StereoScript.cameraMethod = method3D.Parallel;
                m_StereoScript.EnableStereo(CamL, CamR, null, null);
            }

            // Apply stereoscopic 3D parameters to the Scereoskopix script

            switch (stereo)
            {
                case StereoModes.Anaglyph: m_StereoScript.format3D = mode3D.Anaglyph; break;
                case StereoModes.Interlaced: m_StereoScript.format3D = mode3D.Interlace; break;
                case StereoModes.Active: m_StereoScript.format3D = mode3D.Alternate; break;
                case StereoModes.SideBySide: m_StereoScript.format3D = mode3D.SideBySide; break;
            }

            m_StereoScript.zeroParallax = parallaxDistance;
            m_StereoScript.interaxial = eyeDistance;
        }
        else
        {
            // Stereoscopic 3D disabled.
            // DisableStereo relocates the left camera at the local center of the GameObject.
            // We use the left camera for non-stereoscopic 3D render.

            m_StereoScript.DisableStereo();

            CamL.enabled = true;
            CamL.fieldOfView = GetComponent<Camera>().fieldOfView;

            if (CamR) CamR.enabled = false;
        }
        text.text = eyeDistance + "==" + parallaxDistance;
    }


    void SwitchEyes()
    {
        m_StereoScript.SwitchEyes();
    }

    private void OnGUI()
    {
        //GUI.Label(new Rect(0, 0, 100, 50),eyeDistance+"=="+parallaxDistance);
    }
    void SetCameraClearFlags(CameraClearFlags flags, Color clColor)
    {
        CamL.clearFlags = flags;
        CamL.backgroundColor = clColor;

        if (CamR)
        {
            CamR.clearFlags = flags;
            CamR.backgroundColor = clColor;
        }
    }
}
