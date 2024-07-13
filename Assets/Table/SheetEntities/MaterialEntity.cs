using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfMaterial
{
    Special,
    Material,
}

[System.Serializable]
public class MaterialEntity 
{
    public int id;
    public string materialName;
    public TypeOfMaterial type;
    public string description;
}
