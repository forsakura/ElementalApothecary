using System;
using UnityEngine;

/// <summary>
/// 继承Mono的脚本单例类
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    new GameObject("Singleton of" + typeof(T)).AddComponent<T>();
                }
                else
                {
                    instance.Init();
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            Init();
        }
    }


    protected virtual void Init()
    {
    
    }
}
