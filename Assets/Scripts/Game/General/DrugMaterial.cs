using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DrugMaterial
{
    public int id;
    public string name;
    public MaterialType type;
    public string description;
}

public enum MaterialType
{
    Special,
    Material
}