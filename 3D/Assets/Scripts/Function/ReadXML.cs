using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Collections.Generic;
using DG.Tweening;
using ModelViewerProject.Label3D;
using DG.Tweening.Core;
using UnityEngine.SceneManagement;
using System.Xml;
using PureMVCDemo;
/// 版本号
/// </summary>
public class Versions
{
    public string versions;
}
/// <summary>
/// 3d相机设置
/// </summary>
public class StereoCamera
{
    /// <summary>
    /// 汇聚点
    /// </summary>
    public string point;
    /// <summary>
    /// 双眼距离
    /// </summary>
    public string eyeDistance;
    /// <summary>
    /// 模型位置距离
    /// </summary>
    public string modelDistance;
}
/// <summary>
/// 模型名称
/// </summary>
public class Prefab
{
    public string prefabName;
}
/// <summary>
/// 配置文件实体
/// </summary>
public class UserDataNew
{
    public StereoCamera stereoCamera = new StereoCamera();
    public Versions versions = new Versions();
    public Prefab prefab = new Prefab();
}
/// <summary>
/// 读取基本信息，根据结构读取
/// </summary>
public class ReadXML : MonoBehaviour
{
    /// <summary>
    /// xml文件名称
    /// </summary>
    private const string fileName = "modification.xml";
    //private UserDataNew myData = new UserDataNew();
    /// <summary>
    /// 所有的模型
    /// </summary>
    private List<GameObject> prefabList;
    /// <summary>
    /// 当前模型位置
    /// </summary>
    private int index = 0;
    [SerializeField]
    private StereoCam strCam;
    /// <summary>
    /// 所有的模型分页显示
    /// </summary>
    private Dictionary<int, List<string>> allButtonName;
    private List<string> groundName;
    private GameObject buttonPrefab;
    void Awake()
    {

        prefabList = new List<GameObject>();
        Global.UserDataNew = ToolGlobal.Read(Global.Url + "/Resources/Config/modification.xml");
        buttonPrefab = Resources.Load<GameObject>(Global.buttonPrefab);
        List<string> prefabName = new List<string>(Global.UserDataNew.prefab.prefabName.Split(','));
        allButtonName = new Dictionary<int, List<string>>();
        for (int i = 0; i < prefabName.Count; i++)
        {
            if (i % 8 == 0)
            {
                groundName = new List<string>();
                allButtonName.Add(i / 8, groundName);
            }
            groundName.Add(prefabName[i]);
        }
        LoadButton();
        // StartCoroutine(LoadPrefab(new List<string>(Global.UserDataNew.prefab.prefabName.Split(','))));
        //SetMenuControl.Singleton.Close();

    }
    private void LoadXML()
    {
        // TextAsset xml = Resources.Load<TextAsset>("Config/" + fileName);
        //  myData = (UserDataNew)DeserializeObject(xml.text);
        //  string[] array = myData.prefab.prefabName.Split(',');
        // Global.stereoCamera = sc;
        // StartCoroutine(LoadPrefab(new List<string>(array)));
        // Load(Global.Url+ "/Resources/Config/" + fileName+".xml");

    }
    private void LoadButton()
    {
        foreach (KeyValuePair<int, List<string>> kvp in allButtonName)
        {
           // ChoiceMenuControl cmc = new ChoiceMenuControl(kvp.Value, buttonPrefab);
        }
    }
    /// <summary>
    /// 加载所有的模型
    /// </summary>
    /// <param name="prefabName"></param>
    /// <returns></returns>
    private IEnumerator LoadPrefab(List<string> prefabName)
    {
        WWW www = new WWW(string.Format("{0}/Resources/{1}/{2}.unity3d", Global.Url, prefabName[0], prefabName[0]));
        yield return www;
        if (www.error == null)
        {

            GameObject obj = Instantiate(www.assetBundle.LoadAsset<GameObject>("model"));
            obj.name = prefabName[0];
            //obj.transform.parent = transform;
            obj.transform.parent = transform;
            obj.transform.localPosition = Vector3.zero;

            TextAsset txt = Resources.Load<TextAsset>(prefabName[0] + "/" + prefabName[0]);
            LabelDataList label = JsonFx.Json.JsonReader.Deserialize<LabelDataList>(txt.text);
            obj.transform.eulerAngles = label.transform.Rotation;
            obj.transform.localScale = label.transform.Scale;
            obj.layer = LayerMask.NameToLayer("model");
            obj.tag = Tag.prefab;
            //obj.AddComponent<OptionPrefab>();
            BoxCollider col = obj.AddComponent<BoxCollider>();
            col.size = new Vector3(1.5f, 2, 1);
            foreach (Transform item in obj.transform)
            {
                item.gameObject.layer = 8;
            }
            AddAnimation(obj, label.animationType);
            //播放动画
            //Tweener tweener = obj.transform.DOLocalMove(new Vector3(0,0.2f,0), 2f,false);
            //tweener.SetLoops(-1, LoopType.Yoyo);
            //tweener.SetEase(Ease.InOutQuad);
            if (prefabList.Count != 0)
                obj.SetActive(false);
            prefabList.Add(obj);
            if (prefabList.Count == 1)
                Global.modelName = prefabList[0].name;
            prefabName.Remove(prefabName[0]);

        }
        else
        {
            prefabName.Remove(prefabName[0]);
            Debug.LogError(www.error);
        }
        www.assetBundle.Unload(false);
        if (prefabName.Count > 0)
        {
            StartCoroutine(LoadPrefab(prefabName));
        }


    }
    private void AddAnimation(GameObject obj, int type)
    {
        Tweener tweener;
        switch (type)
        {
            case 0:
                tweener = obj.transform.DOLocalMove(new Vector3(0, 0.2f, 0), 2f, false);
                tweener.SetLoops(-1, LoopType.Yoyo);
                tweener.SetEase(Ease.InOutQuad);
                break;
            case 1:
                tweener = obj.transform.DOLocalMove(new Vector3(0.2f, 0, 0), 2f, false);
                tweener.SetLoops(-1, LoopType.Yoyo);
                tweener.SetEase(Ease.InOutQuad);
                break;
            case 2:
                tweener = obj.transform.DOLocalMove(new Vector3(0, 0, 0.2f), 2f, false);
                tweener.SetLoops(-1, LoopType.Yoyo);
                tweener.SetEase(Ease.InOutQuad);
                break;
            case 3:
                tweener = obj.transform.DORotate(new Vector3(0, 1, 0), 1f);
                tweener.SetLoops(-1, LoopType.Incremental);
                tweener.SetEase(Ease.Linear);
                break;
            case 4:
                tweener = obj.transform.DORotate(new Vector3(8, 0, 0), 1f);
                tweener.SetLoops(-1, LoopType.Incremental);
                tweener.SetEase(Ease.Linear);
                break;
            case 5:
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 选择模型左移动
    /// </summary>
    public void OptionLift()
    {
        if (index - 1 < 0) return;
        for (int i = 0; i < prefabList.Count; i++)
        {

            if (i == index - 1)
            {

                prefabList[i].SetActive(true);
                Global.modelName = prefabList[i].name;
                //print(optionPrefabName+"lift");
            }

            else
                prefabList[i].SetActive(false);

        }
        //  print(index);
        index--;
        //print(index);
    }
    /// <summary>
    /// 选择模型右移动
    /// </summary>
    public void OptionRight()
    {
        if (index + 1 >= prefabList.Count) return;
        for (int i = 0; i < prefabList.Count; i++)
        {

            if (i == index + 1)
            {

                prefabList[i].SetActive(true);
                Global.modelName = prefabList[i].name;
                // print(optionPrefabName);
                // prefabList[i].GetComponent<DOTweenAnimation>().DOPlay();
            }

            else
                prefabList[i].SetActive(false);

        }
        //print(index);
        index++;
        // print(index);
    }
    public void OptionPrefab()
    {

        SceneManager.LoadScene("MainScene");
        //Global.modelName = optionPrefabName;
        //print(Global.modelName);
        TextAsset[] text = Resources.LoadAll<TextAsset>(Global.modelName);
        Global.labelDataList = JsonFx.Json.JsonReader.Deserialize<LabelDataList>(text[0].text);
        //SetMenuControl.Singleton = null;
    }
    // SceneManager.LoadScene("StartScene");
    private object DeserializeObject(string pXmlizedString)
    {
        XmlSerializer xs = new XmlSerializer(typeof(UserDataNew));
        MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
        // XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        return xs.Deserialize(memoryStream);
    }
    private byte[] StringToUTF8ByteArray(string pXmlString)
    {
        UTF8Encoding encoding = new UTF8Encoding();
        byte[] byteArray = encoding.GetBytes(pXmlString);
        return byteArray;
    }
}
