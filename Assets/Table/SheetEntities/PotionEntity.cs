using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PotionEntity 
{
    public int id;
    public string potionName;
    public float aerNum;
    public float ignisNum;
    public float aquaNum;
    public float terraNum;
    public int toEnemyEffectIds;
    public int foreverEffectId;
    public string description;
    public float purity;
}
