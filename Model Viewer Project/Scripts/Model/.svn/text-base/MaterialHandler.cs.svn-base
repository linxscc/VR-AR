using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MaterialHandler : MonoBehaviour
{

    private Material[] rawMaterial;
    private List<Shader> shader;
    private List<Color> color;
    void Awake()
    {
        color = new List<Color>();
        shader = new List<Shader>();
        rawMaterial = GetComponent<MeshRenderer>().sharedMaterials;
        for (int i = 0; i < rawMaterial.Length; i++)
        {
         
            shader.Add(rawMaterial[i].shader);
            if (rawMaterial[i].color !=null)
                color.Add(rawMaterial[i].color);
        }
    }

    public void OnUpdate(Material mat = null)
    {
        if (mat != null)
        {
            for (int i = 0; i < rawMaterial.Length; i++)
            {
                GetComponent<MeshRenderer>().materials[i].color = new Color(0, 64 / 225f, 128 / 225f, 15 / 225f);
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
