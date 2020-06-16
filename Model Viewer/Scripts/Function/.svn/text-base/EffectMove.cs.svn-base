using UnityEngine;
using System.Collections;

public class EffectMove : MonoBehaviour
{
    public GameObject effect;
    public GameObject plane;
    // Use this for initialization
    void Start()
    {
        OnInIt();
    }
    private void OnInIt()
    {
        float grid = plane.GetComponent<MeshRenderer>().material.mainTextureScale.x;
        float length = transform.localScale.x;
        //length/grid*10
        for (int i = 0; i < grid; i++)
        {
            Vector3 effectVector3 = new Vector3(-length *20+ length/grid*40*i, plane.transform.position.y,-3f);         
            Instantiate(effect, effectVector3, Quaternion.identity);
        } 
    }
    // Update is called once per frame
    void Update()
    {

    }
}
