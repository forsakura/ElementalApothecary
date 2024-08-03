using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ATTRSO", menuName = "ScriptableObjects/ATTRSO")]
public class ATTRSO : ScriptableObject
{
    [SerializeField]
    private List<Attribute> attributes;

    public List<Attribute> Attributes => attributes;

    public Attribute GetAttributeById(int id)
    {
        return attributes.FirstOrDefault(attribute => attribute.id == id);
    }

    public List<Attribute> GetAttributesByElement(EElement element)
    {
        return attributes.Where(attribute => attribute.baseElement == element).ToList();
    }
}

[Serializable]
public  class Attribute:ScriptableObject
{
    public int id;
    public EElement baseElement;
    public float Duration;
    public float _remainingTime;
    public bool IsPermanent=true;

    public virtual void Apply(GameObject target) { }
    public virtual void Update(GameObject target, float deltaTime) { }
    public virtual void Expire(GameObject target) { }
    public virtual bool IsExpired()
    {
        return !IsPermanent && _remainingTime <= 0;
    }
}
