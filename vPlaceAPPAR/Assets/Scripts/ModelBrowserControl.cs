using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using vPlace_zpc;

public class ModelBrowserControl
{
    private GameObject modelBrowser;
    public ModelBrowserMainView modelBrowserView ;
    private ModelBrowserControl()
    {
        modelBrowser = Resources.Load<GameObject>(Global.modelBrowser);
        modelBrowser = GameObject.Instantiate(modelBrowser);
        modelBrowserView = modelBrowser.GetComponent<ModelBrowserMainView>();
       
    }
    private static ModelBrowserControl singleton;
    public static ModelBrowserControl Singleton
    {
        get
        {
            if (singleton == null)
                singleton = new ModelBrowserControl();
            return singleton;
        }
        set
        {
            singleton = value;
        }
    }
    public void Open()
    {
        modelBrowserView.Open();
    }
    public void Close()
    {
        modelBrowserView.Close();
    }
}
