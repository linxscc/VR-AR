/*
 *    日期:2017/7/7
 *    作者:
 *    标题:对象池
 *    功能:
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 对象池
/// </summary>
public class ObjectBool : MonoBehaviour
{
    /// <summary>
    /// 存储对象字典
    /// </summary>
    private static Dictionary<string, List<GameObject>> dictionary = new Dictionary<string, List<GameObject>>();
    /// <summary>
    /// 取对象
    /// </summary>
    /// <param name="o"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static GameObject Get(GameObject o, Vector3 position, Quaternion rotation)
    {
        GameObject obj;
        if (dictionary.ContainsKey(o.name) && dictionary[o.name].Count > 0)
        {
            List<GameObject> list = dictionary[o.name];
            obj = list[0];
            list.RemoveAt(0);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);

        }
        else
        {
            obj = Instantiate(o, position, rotation) as GameObject;
            obj.name = o.name;
            // obj.name = o.tag;
        }
        return obj;
    }
    public static GameObject Get(GameObject o)
    {
        return Get(o, Vector3.zero, Quaternion.identity);
    }
    /// <summary>
    /// 返回对象
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static GameObject Return(GameObject obj)
    {
        //Tag
        // print(obj.name);
        string key = obj.name;
        if (dictionary.ContainsKey(key))
        {
            dictionary[key].Add(obj);
        }
        else
        {
            dictionary[key] = new List<GameObject>() { obj };
        }
        obj.SetActive(false);
        return obj;
    }
    private void OnDisable()
    {
        //dictionary.Clear();
    }
}

