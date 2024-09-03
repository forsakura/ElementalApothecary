using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BaseAttribute;

public abstract class BuffBase : ScriptableObject
{
    public int id;
    public List<int> buffId;//涉及的buffid
    public string description;
    public EElement Element;
    public EElement Element2;
    public float Duration;
    public float _remainingTime;
    public float leak;
    public bool IsPermanent = true;


    //这些GameObject估计要改
    public virtual void OnApply(GameObject target) { }
    public virtual void OnExpired(GameObject target) { }
    public virtual void Update(GameObject target, float deltaTime) { }
    public virtual bool IsExpired()
    {
        return !IsPermanent && _remainingTime <= 0;
    }
}
