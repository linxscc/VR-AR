using UnityEngine;
using System.Collections;

public class VRMoveVrCamera : MonoBehaviour
{
    private float x, y, z;
    // Use this for initialization
    void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            y += 0.01f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            y -= 0.01f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            x -= 0.01f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            x += 0.01f;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            z += 0.01f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            z -= 0.01f;
        }
        transform.position = new Vector3(x, y, z);
    }
}
