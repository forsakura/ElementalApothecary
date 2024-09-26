using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ȥʹ�õ�һ��item���������(�����࣡����)
/// </summary>
[System.Serializable]
public class TrItem : ITrItem
{
    [SerializeField]
    private ItemID id = new();
    public ItemID ID 
    {
        get => id;
        set => id = value; 
    } //��Ʒid(����tag)

    public void AddATTRID(int singleATTR)
    {
        ID.ATTR.Add(singleATTR);
    }

    public List<int> GetATTRID()
    {
        return ID.ATTR;
    }
    /// <summary>
    /// ����ģ���id���һ��ʼ���趨��
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
        QuestItem,
        None
    }
}

/// <summary>
/// id����ʹ�������ϻ�ÿ���
/// </summary>
[System.Serializable]
public class ItemID
{
    public int id;//��Ʒ�Լ���id��ֻ��ͬ�������õģ���Ҫ��guid����
    public int BaseId;//����ģ��id
    public List<int> ATTR = new List<int>(); //����id
}
