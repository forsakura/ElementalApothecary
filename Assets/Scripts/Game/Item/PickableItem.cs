using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ŀǰֻ����Ϊ������
/// </summary>
public class PickableItem : TrItem
{
    public bool isPickable;
    public Sprite itemOnWorldSprite;

    /// <summary>
    /// ��ʼ������ģ��
    /// </summary>
    public void initByTemplet()
    {
        itemOnWorldSprite=InventoryManager.Instance.GetItemDetails(ID).itemIcon;
    }

    public void applyATTR(int id)
    {

    }
}
