using UnityEngine;


public class Singleton<T> where T : new()
{
    private static T _instance;
    static private readonly object syncRoot = new object();

    public static T Instance
    {
        get
        {
            lock (syncRoot)
            {
                if (_instance == null)
                {
                    _instance = new T();
                }
            }
            return _instance;
        }
    }

    protected virtual void Init()
    {
    }

    protected Singleton()
    {
        Init();
    }
}