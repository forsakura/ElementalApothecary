using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// "That's true music,bro."����ȥʹ�õ�һ��item���������
/// </summary>
public class TrItem 
{
    public ItemID ID;//��Ʒid(����tag)

    public void AddATTRID(int singleATTR)
    {
        ID.ATTR.Add(singleATTR);
    }

    /// <summary>
    /// ����ģ���id���һ��ʼ���趨��
    /// </summary>
    /// <param name="id"></param>
    public void SetBaseID(int id)
    {
        ID.BaseId = id;
    }

    public enum ItemTag
    {
        material,
        consumable,
        QuestItem
    }
}

/// <summary>
/// id����ʹ�������ϻ�ÿ���
/// </summary>
public struct ItemID
{
    public int id;//��Ʒ�Լ���id��ֻ��ͬ�������õģ�
    public int BaseId;//����ģ��id
    public List<int> ATTR;//����id
}
