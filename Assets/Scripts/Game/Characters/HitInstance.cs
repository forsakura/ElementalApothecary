using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HitInstance
{
    public GameObject Source;
    public int Damage = 0;
    public int[] ElementContain = { 0, 0 };
    public EElement[] ElementName = { EElement.None, EElement.None };
    public bool IgnoreInvincible = false;
}
