using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using ModelViewerProject.Label3D;
/// <summary>
/// 选择模型
/// </summary>
public class OptionPrefab : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        
    }
    void OnMouseDown()
    {

        SceneManager.LoadScene("MainScene");
        Global.modelName = transform.name;
        TextAsset[] text = Resources.LoadAll<TextAsset>(transform.name);
        Global.labelDataList = JsonFx.Json.JsonReader.Deserialize<LabelDataList>(text[0].text);

    }

}
