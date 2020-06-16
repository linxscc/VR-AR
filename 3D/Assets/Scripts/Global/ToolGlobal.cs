using UnityEngine;
using System.Collections;
using System.Xml;
/// <summary>
/// 工具
/// </summary>
public static class ToolGlobal
{
    /// <summary>
    /// 读写文件只修改3D相机的部分
    /// </summary>
    /// <param name="strc"></param>
    /// <returns></returns>
    // public static UserDataNew ReadWrite(StereoCamera strc=null)
    // {
    // UserDataNew myData = new UserDataNew();
    // myData.versions.versions = "";
    //  myData.stereoCamera = strc;
    //   //  return Read();
    // }
    public static UserDataNew Read(string url)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(url);
        XmlNodeList nodeList = doc.SelectSingleNode("UserDataNew").ChildNodes;
        UserDataNew myData = new UserDataNew();
        foreach (XmlNode xn in nodeList)
        {
            switch (xn.Name)
            {
                case "versions":
                    myData.versions.versions = xn.ChildNodes.Item(0).InnerXml;
                    break;
                case "stereoCamera":
                    XmlNodeList nls = xn.ChildNodes;
                    foreach (XmlNode item in nls)
                    {
                        switch (item.Name)
                        {
                            case "point":
                                myData.stereoCamera.point = item.InnerXml;
                                break;
                            case "eyeDistance":
                                myData.stereoCamera.eyeDistance = item.InnerXml;
                                break;
                            case "modelDistance":
                                myData.stereoCamera.modelDistance = item.InnerXml;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case "prefab":
                    myData.prefab.prefabName = xn.ChildNodes.Item(0).InnerXml;
                    break;
                default:
                    break;
            }
        }
        return myData;
    }
    /// <summary>
    /// 读写文件
    /// </summary>
    /// <param name="url"></param>
    /// <param name="strC"></param>
    /// <returns></returns>
    public static UserDataNew Write(UserDataNew udn)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Global.Url + "/Resources/Config/modification.xml");
        XmlNodeList nodeList = doc.SelectSingleNode("UserDataNew").ChildNodes;
        UserDataNew myData = new UserDataNew();
        foreach (XmlNode xn in nodeList)
        {
            switch (xn.Name)
            {
                case "versions":
                    xn.ChildNodes.Item(0).InnerText = udn.versions.versions == "" ? xn.ChildNodes.Item(0).InnerXml : udn.versions.versions;
                    myData.versions.versions = xn.ChildNodes.Item(0).InnerXml;
                    break;
                case "stereoCamera":
                    XmlNodeList nls = xn.ChildNodes;
                    foreach (XmlNode item in nls)
                    {
                        switch (item.Name)
                        {
                            case "point":
                                item.InnerText = udn == null ? item.InnerXml : udn.stereoCamera.point;
                                myData.stereoCamera.point = item.InnerXml;
                                break;
                            case "eyeDistance":
                                item.InnerText = udn == null ? item.InnerXml : udn.stereoCamera.eyeDistance;
                                myData.stereoCamera.eyeDistance = item.InnerXml;
                                break;
                            case "modelDistance":
                                item.InnerText = udn == null ? item.InnerXml : udn.stereoCamera.modelDistance;
                                myData.stereoCamera.modelDistance = item.InnerXml;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case "prefab":
                    myData.prefab.prefabName = xn.ChildNodes.Item(0).InnerXml;
                    break;
                default:
                    break;
            }
        }
#if UNITY_EDITOR
        doc.Save("Assets/Resources/Config/modification.xml");
#elif UNITY_STANDALONE
      //doc.Save("vPlace_View_V0.2.2_Data/Resources/Config/modification.xml");
#endif
        //doc.Save("Assets/Resources/Config/modification.xml");
        return myData;
    }
}
