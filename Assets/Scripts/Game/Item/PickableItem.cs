using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 目前只是作为掉落物
/// </summary>
public class PickableItem : TrItem
{
    public bool isPickable;
    public Sprite itemOnWorldSprite;

    /// <summary>
    /// 初始化基础模板
    /// </summary>
    public void initByTemplet()
    {
        itemOnWorldSprite=InventoryManager.Instance.GetItemDetails(ID).itemIcon;
    }

    public void applyATTR(int id)
    {

    }
}
