using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ATTRSO", menuName = "ScriptableObjects/ATTRSO")]
public class ATTRSO : ScriptableObject
{
    List<Attribute> attributes;
}

public class Attribute
{
    int id;
}
