using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlaceAR;
public class MaterialHandler : MonoBehaviour
{
    /// <summary>
    /// 存储子级材质
    /// </summary>
    private Material[] rawMaterial;
    /// <summary>
    /// 存储子级Shader
    /// </summary>
    private List<Shader> shader;
    /// <summary>
    /// 存储子级颜色
    /// </summary>
    private List<Color> color;
    /// <summary>
    /// 是否显示
    /// </summary>
    public bool isShow = true;
    void Awake()
    {
        color = new List<Color>();
        shader = new List<Shader>();
        rawMaterial = GetComponent<MeshRenderer>().sharedMaterials;
        for (int i = 0; i < rawMaterial.Length; i++)
        {
            shader.Add(rawMaterial[i].shader);
            //if (rawMaterial[i].color !=null&&(transform.parent.name== "Skull_Mod" || transform.parent.name == "001_IRONMAN"))
            //print(string.Format("序号：{0}，材质名称：{1}，当前物体名称：{2}", i, rawMaterial[i].name, gameObject.name));
            color.Add(rawMaterial[i].color);
        }
    }

    /// <summary>
    /// 逐层模式
    /// </summary>
    /// <param name="mat">透明材质球-默认为空-若不为空则将其他变透明-若为空则复原不变化</param>
    public void OnUpdateByLayer(Material mat)
    {
        OnUpdateByLayer(mat, isShow);
    }

    /// <summary>
    /// 显示当前层
    /// </summary>
    /// <param name="mat">透明材质球-默认为空-若不为空则将其他变透明-若为空则复原不变化</param>
    /// <param name="isSh">是否高亮-false:复原无变化  true:高亮显示</param>
    public void OnUpdateByLayer(Material mat, bool isSh)
    {
        if (!isSh)
            OnUpdate();
        else
            OnUpdate(mat, 30 - GetComponent<PrefabChildControl>().Layer * 5);
        isShow = !isSh;
    }

    /// <summary>
    /// 剖面模式
    /// </summary>
    /// <param name="mat">透明材质球-默认为空-若不为空则将其他变透明-若为空则复原不变化</param>
    /// <param name="isSh">是否高亮-false:复原无变化  true:高亮显示</param>
    public void Profile(Material mat, bool isSh)
    {
        if (!isSh)
            OnUpdate();
        else
            OnUpdate(mat, 0);
        isShow = !isSh;
    }

    /// <summary>
    /// 截面模式
    /// </summary>
    /// <param name="mat">透明材质球-默认为空-若不为空则将其他变透明-若为空则复原不变化</param>
    /// <param name="isSh">是否高亮-false:复原无变化  true:高亮显示</param>
    public void Section(Material mat, bool isSh)
    {
        if (!isSh)
            OnUpdate();
        else
            OnUpdate(mat, 0);
        isShow = !isSh;
    }

    /// <summary>
    /// 子模型高亮与透明显示
    /// </summary>
    /// <param name="mat">透明材质球-默认为空-若不为空则将其他变透明-若为空则复原不变化</param>
    /// <param name="transparentValue">默认透明度=30</param>
    public void OnUpdate(Material mat = null, float transparentValue = 30)
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
                if (color.Count > 0)
                    GetComponent<MeshRenderer>().materials[i].color = color[i];
                GetComponent<MeshRenderer>().materials[i].shader = shader[i];
            }
        }
    }
}
