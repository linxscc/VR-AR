using UnityEngine;
using System.Collections;

public class ControlARCamera : MonoBehaviour
{
    float x, y, z,q,a;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                x = transform.position.x - 0.01f;
            }
            else
            {
                x = transform.position.x + 0.01f;
                
            }
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                y = transform.position.y - 0.01f;
            }
            else
            {
                y = transform.position.y + 0.01f;

            }
            transform.position = new Vector3(transform.position.x,y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                z = transform.position.z - 0.01f;
            }
            else
            {
                z = transform.position.z + 0.01f;

            }
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                q = transform.localEulerAngles.x - 0.02f;
            }
            else
            {
                q = transform.localEulerAngles.x + 0.02f;

            }
            transform.localEulerAngles = new Vector3(q, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
        if (Input.GetKey(KeyCode.End))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                a = transform.localEulerAngles.y - 0.02f;
            }
            else
            {
                a = transform.localEulerAngles.y + 0.02f;

            }
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, a, transform.localEulerAngles.z);
        }
    }
}
