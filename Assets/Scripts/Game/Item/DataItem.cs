using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ʒ���ݣ���������ͨ��id��SO���ù�������
/// </summary>
public class DataItem : TrItem, IDataItem
{
    public int useTimes { get; set; }
    public int itemUseRadius { get; set; }
    public string itemDescription { get; set; }
    // public Sprite itemIcon { get; set; } // �����Ҫ������ȡ��ע��  
    public string itemName { get; set; }
    public List<TrItem.ItemTag> tags { get; set; } = new List<TrItem.ItemTag>(); // ��ʼ���б�  
    public EElement[] BaseElement { get; set; } = new EElement[2];
    public Vector2 currentElementCount { get; set; }

    /// <summary>
    /// ��ʼ������ģ��
    /// </summary>
    public void initByTemplet()
    {

    }

    public void applyATTR()
    {
        
    }
}
