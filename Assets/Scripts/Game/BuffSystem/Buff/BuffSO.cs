using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffSO", menuName = "Buffs/Buff SO")]
public class BuffSO : ScriptableObject
{
    List<BuffBase> Buffs = new List<BuffBase>();
}
