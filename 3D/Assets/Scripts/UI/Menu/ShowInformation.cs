using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 显示信息
/// </summary>
public class ShowInformation : MonoBehaviour
{

    public static ShowInformation showInformation;
    private Text text;
    void Start()
    {
        showInformation = this;
        text = GetComponentInChildren<Text>();
        Close();
    }
    public void SetValue(string txt)
    {
        text.gameObject.SetActive(true);
        GetComponent<Image>().enabled = true;
        text.text = txt;
    }
    public void Close()
    {
        text.gameObject.SetActive(false);
        GetComponent<Image>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
