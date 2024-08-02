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
        return attributes.FirstOrDefault(attribute => attribute.Id == id);
    }

    public List<Attribute> GetAttributesByElement(EElement element)
    {
        return attributes.Where(attribute => attribute.BaseElement == element).ToList();
    }
}

[Serializable]
public class Attribute
{
    [SerializeField]
    private int id;
    [SerializeField]
    private EElement baseElement;

    public int Id => id;
    public EElement BaseElement => baseElement;
}
