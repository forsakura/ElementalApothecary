using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 最表层去使用的一个item数据类基础
/// </summary>
public class TrItem : ITrItem
{
    public ItemID ID;//物品id(或者tag)

    public void AddATTRID(int singleATTR)
    {
        ID.ATTR.Add(singleATTR);
    }

    public List<int> GetATTRID()
    {
        return ID.ATTR;
    }
    /// <summary>
    /// 基础模板的id最好一开始就设定好
    /// </summary>
    /// <param name="id"></param>
    public void SetBaseID(int id)
    {
        ID.BaseId = id;
    }

    public int GetBaseID()
    {
        return ID.BaseId;
    }


    public enum ItemTag
    {
        material,
        consumable,
        QuestItem
    }
}

/// <summary>
/// id数组使得随机组合获得可能
/// </summary>
public struct ItemID
{
    public int id;//物品自己的id（只是同类区分用的）
    public int BaseId;//基础模板id
    public List<int> ATTR;//属性id
}
