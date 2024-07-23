using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// "That's true music,bro."最表层去使用的一个item数据类基础
/// </summary>
public class TrItem 
{
    ItemID ID;//物品id(或者tag)

    public void AddATTRID(int singleATTR)
    {
        ID.ATTR.Add(singleATTR);
    }

    /// <summary>
    /// 基础模板的id最好从一开始设定
    /// </summary>
    /// <param name="idGroup"></param>
    public void SetBaseID(List<int> idGroup)
    {
        ID.BaseId = idGroup;
    }

    enum Tag
    {
        material,
        consumable,
        QuestItem
    }
}

/// <summary>
/// id数组使得随机组合获得可能
/// </summary>
public class ItemID
{
    public List<int> BaseId;//基础模板id
    public List<int> ATTR;//属性id
}
