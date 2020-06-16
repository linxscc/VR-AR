using UnityEngine;
using System.Collections;

public class ARCamMove : MonoBehaviour
{
    public GameObject ARCamera = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            ARCamera.transform.Translate(new Vector3(0,0,0.1f));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            ARCamera.transform.Translate(new Vector3(0, 0, -0.1f));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ARCamera.transform.Translate(new Vector3(-0.1f, 0, 0));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            ARCamera.transform.Translate(new Vector3(0.1f, 0, 0));
        }
    }
}
