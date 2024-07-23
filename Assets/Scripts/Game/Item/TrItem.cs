using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// "That's true music,bro."����ȥʹ�õ�һ��item���������
/// </summary>
public class TrItem 
{
    ItemID ID;//��Ʒid(����tag)

    public void AddATTRID(int singleATTR)
    {
        ID.ATTR.Add(singleATTR);
    }

    /// <summary>
    /// ����ģ���id��ô�һ��ʼ�趨
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
/// id����ʹ�������ϻ�ÿ���
/// </summary>
public class ItemID
{
    public List<int> BaseId;//����ģ��id
    public List<int> ATTR;//����id
}
