using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 协程调用
/// </summary>
public class TCoroutine : MonoBehaviour
{
    static public GameObject tCoroutine;
    static private TCoroutine instance = null;
    static public TCoroutine Instance
    {
        get
        {
            if (instance == null || tCoroutine == null)
            {
                tCoroutine = new GameObject();
                tCoroutine.name = "TCoroutine";
                tCoroutine.AddComponent<TCoroutine>();
                instance = tCoroutine.GetComponent<TCoroutine>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    private void OnDisable()
    {
        instance = null;
    }
    private void OnDestroy()
    {
        instance = null;
    }


    /// <summary>
    /// 开启协程
    /// </summary>
    /// <param name="fun"></param>
    public void TStartCoroutine(IEnumerator fun)
    {
        if (tCoroutine == null)
        {
            tCoroutine = new GameObject();
            tCoroutine.name = "TCoroutine";
            tCoroutine.AddComponent<TCoroutine>();
        }
        StartCoroutine(fun);
    }
    /// <summary>
    /// 关闭协程
    /// </summary>
    /// <param name="fun"></param>
    public void TStopCoroutine(IEnumerator fun)
    {
        if (tCoroutine != null)
        {
            StopCoroutine(fun);
        }
    }
    public void TStopAllCoroutine()
    {
        if (tCoroutine != null)
        {
            StopAllCoroutines();
            Destroy(tCoroutine);
        }
    }
}
