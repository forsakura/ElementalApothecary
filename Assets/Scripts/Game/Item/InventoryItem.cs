using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背包物品数据，需要补充（不是物品堆）
/// </summary>
public class InventoryItem : TrItem
{
    public int itemUseRadius;
    public string itemDescription; 
    public Sprite itemIcon; 
    public string itemName;

    /// <summary>
    /// 初始化基础模板
    /// </summary>
    public void initModel()
    {

    }
}
