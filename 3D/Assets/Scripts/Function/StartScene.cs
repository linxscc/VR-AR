/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PureMVCDemo;
using Kinect_XYRF;

public class StartScene : MonoBehaviour
{
    [SerializeField]
    private Button liftButton;
    [SerializeField]
    private Button rightButton;
    private int pageIndex = 0;
    /// <summary>
    /// 当前页
    /// </summary>
    private int PageIndex
    {
        get { return pageIndex; }
        set
        {
            if (value <= 0)
                pageIndex = 0;
            if (value >= allMenu.Count - 1)
                pageIndex = allMenu.Count - 1;
        }
    }
    /// <summary>
    /// 创建出来的分页
    /// </summary>
    private List<ChoiceMenuControl> allMenu;
    /// <summary>
    /// 所有的模型分页显示
    /// </summary>
    private Dictionary<int, List<string>> allButtonName;
    /// <summary>
    /// 分页数据
    /// </summary>
    private List<string> groundName;
    /// <summary>
    /// 按钮预制物
    /// </summary>
    private GameObject buttonPrefab;
    private bool isClickEscape = false;
    public GameObject cube;
    void Awake()
    {

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

        EventTriggerListener.Get(liftButton.gameObject).onClick = Liftbutton;
        EventTriggerListener.Get(liftButton.gameObject).onEnter = OnEnter;
        EventTriggerListener.Get(liftButton.gameObject).onExit = OnExit;
        EventTriggerListener.Get(rightButton.gameObject).onClick = Rightbutton;
        EventTriggerListener.Get(rightButton.gameObject).onEnter = OnEnter;
        EventTriggerListener.Get(rightButton.gameObject).onExit = OnExit;
        //liftButton.OnPointerEnter()
        // StartCoroutine(LoadPrefab(new List<string>(Global.UserDataNew.prefab.prefabName.Split(','))));

        LoadButton();
        SetMenuControl.Singleton.Close();
       // ARStartupControl.ARInstance.OpenAR();
      //  ARCaptureControl.Instance.Open();
        //KinectV2Pos.Instance.OpenKinectV2Pos(transform.FindChild("Canvas").transform);
       // KinectV2Pos.Instance.inintKinectV2Pos.backEulerAngles += HumanEulerAngles;
        //KinectV2Pos.Instance.inintKinectV2Pos.backVector += HumanVector;
        //print(KinectV2Pos.Instance.GetUserRot());
    }
    private void HumanVector(Vector3 vector)
    {
       // DontDestroyOnLoad
        // print(vector);
    }
    private void HumanEulerAngles(float eulerAngles)
    {
        cube.transform.eulerAngles = new Vector3(cube.transform.eulerAngles.x, eulerAngles, cube.transform.eulerAngles.z);
        // print(eulerAngles);
    }
    private void OnEnter(GameObject b)
    {

        switch (b.name)
        {
            case "LiftButton":
                HintInfoControl.Singleton.Open("上一页", b.transform.position.x, b.transform.position.y, 0);
                break;
            case "RightButton":
                HintInfoControl.Singleton.Open("下一页", b.transform.position.x, b.transform.position.y, 0);
                break;
            default:
                break;
        }

    }
    private void OnExit(GameObject b)
    {
        HintInfoControl.Singleton.Close();
    }
    /// <summary>
    /// 左选择
    /// </summary>
    /// <param name="b"></param>
    public void Liftbutton(GameObject b)
    {
        PageIndex--;
        for (int i = 0; i < allMenu.Count; i++)
        {
            if (PageIndex == i)
                allMenu[i].SetMenu(true);
            else
                allMenu[i].SetMenu(false);
        }
    }
    /// <summary>
    /// 右选择
    /// </summary>
    /// <param name="b"></param>
    public void Rightbutton(GameObject b)
    {
        PageIndex++;
        for (int i = 0; i < allMenu.Count; i++)
        {
            if (PageIndex == i)
                allMenu[i].SetMenu(true);
            else
                allMenu[i].SetMenu(false);
        }
    }
    private void LoadButton()
    {
        allMenu = new List<ChoiceMenuControl>();
        foreach (KeyValuePair<int, List<string>> kvp in allButtonName)
        {
            ChoiceMenuControl cmc = new ChoiceMenuControl(kvp.Value, buttonPrefab);
            allMenu.Add(cmc);
            if (allMenu.Count > 1)
                cmc.SetMenu(false);
        }

    }
    public void CloseCancel()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitProcedure.Quit();

        }
    }
}

