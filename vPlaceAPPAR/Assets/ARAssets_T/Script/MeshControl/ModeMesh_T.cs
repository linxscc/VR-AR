using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ModeMesh_T : MonoBehaviour
{
    public Mesh singleMesh;
    public Mesh minMesh;
    public Mesh maxMesh;

    private void Awake()
    {
        instance = this;
    }
    private void OnDestroy()
    {
        instance = null;
    }
    private void OnDisable()
    {
        instance = null;
    }
    static private ModeMesh_T instance = null;
    static public ModeMesh_T Instance
    {
        get
        {
            return instance;
        }
    }
}
