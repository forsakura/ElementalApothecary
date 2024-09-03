using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public string Name;
    public int ID;
    public int MaxHealth;
    // ·çÍÁ»ðË®
    public int MaxAer;
    public int MaxTerra;
    public int MaxIgnis;
    public int MaxAqua;

    public int Damage;
    public int MaxBulletCount;
    public float AttackInterval;
    public float AttackDistance;

    public float MoveSpeed;
    public float InvincibleTime;
}
