using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class BasePanel<T> : MonoBehaviour where T : class
{
    private static T instance;
    public static T Instance => instance;
    protected virtual void Awake()
    {
        instance = this as T;
    }
    void Start()
    {
        Init();
    }
    //���ڳ�ʼ�����¼��������߼����߼�
    public abstract void Init();

    public virtual void ShowMe()
    {
        this.gameObject.SetActive(true);
    }
    public virtual void HideMe()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
