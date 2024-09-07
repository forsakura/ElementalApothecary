using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BaseAttribute;

[System.Serializable]
public class BuffBase : ScriptableObject
{
    public int id;
    public List<int> buffId;//�漰��buffid
    public string description;
    public EElement Element;
    public EElement Element2;
    public float Duration;
    public float _remainingTime;
    public float leak;
    public bool IsPermanent = true;

    public BuffBase() 
    {
        buffId = new List<int>();
        description = null;
        Element = EElement.None;
        Element2 = EElement.None;
    }

    //��ЩGameObject����Ҫ��
    public virtual void OnApply(GameObject target) { }
    public virtual void OnExpired(GameObject target) { }
    public virtual void OnUpdate(GameObject target, float deltaTime) { }
    public virtual bool IsExpired()
    {
        return !IsPermanent && _remainingTime <= 0;
    }
}
