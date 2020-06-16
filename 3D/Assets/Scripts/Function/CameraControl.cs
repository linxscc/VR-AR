using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{

    int index = 0;
    void Start()
    {
        StereoControl.Singleton.Open();
       
    }

    // Update is called once per frame
    void Update()
    {

    }
}
