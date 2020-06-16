using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ModelViewerProject.Label3D;

public class MaterialHandler : MonoBehaviour
{

    private Material[] rawMaterial;
    private List<Shader> shader;
    private List<Color> color;
    public bool isShow = true;
    void Awake()
    {
        color = new List<Color>();
        shader = new List<Shader>();
        rawMaterial = GetComponent<MeshRenderer>().sharedMaterials;
        for (int i = 0; i < rawMaterial.Length; i++)
        {
         
            shader.Add(rawMaterial[i].shader);
            if (rawMaterial[i].color !=null&&(transform.parent.name== "Skull_Mod" || transform.parent.name == "001_IRONMAN"))
                color.Add(rawMaterial[i].color);
        }
    }
    /// <summary>
    /// 逐层模式
    /// </summary>
    /// <param name="mat"></param>
    public void OnUpdateByLayer(Material mat)
    {

        OnUpdateByLayer(mat, isShow);
                //isShow = !isShow;
    }
    /// <summary>
    /// 逐层模式
    /// </summary>
    /// <param name="mat"></param>
    /// <param name="isSh"></param>
    public void OnUpdateByLayer(Material mat, bool isSh)
    {
        
        if (!isSh)
            OnUpdate();
        else
            OnUpdate(mat, 30 - GetComponent<Label3DHandler>().Layer * 5);
        isShow = !isSh;
    }
    /// <summary>
    /// 剖面模式
    /// </summary>
    public void Profile(Material mat, bool isSh)
    {
        if (!isSh)
            OnUpdate();
        else
            OnUpdate(mat, 0);
        isShow = !isSh;
    }
    public void OnUpdate(Material mat = null,float transparentValue=30)
    {
        if (mat != null)
        {
            for (int i = 0; i < rawMaterial.Length; i++)
            {
                GetComponent<MeshRenderer>().materials[i].color = new Color(150 / 225f, 150 / 225f, 150 / 225f, transparentValue / 225f);
                GetComponent<MeshRenderer>().materials[i].shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
            }
        }
        else
        {
            for (int i = 0; i < rawMaterial.Length; i++)
            {
                if(color.Count>0)
                GetComponent<MeshRenderer>().materials[i].color = color[i];
                GetComponent<MeshRenderer>().materials[i].shader = shader[i];
            }
        }
        // GetComponent<MeshRenderer> ( ).material = mat ? mat : rawMaterial;

    }
}
