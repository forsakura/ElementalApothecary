using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffSO", menuName = "Buffs/Buff SO")]
public class BuffSO : ScriptableObject
{
    public List<BuffBase> Buffs = new List<BuffBase>();

    public BuffBase GetBuffById(int id)
    {
        foreach (var buff in Buffs)
        {
            if (buff.id == id)
            {
                return buff;
            }
        }
        return null; // 如果找不到匹配的Buff，返回null  
    }
}
