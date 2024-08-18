using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背包物品数据，尽量还是通过id从SO调用固有数据
/// </summary>
public class DataItem : TrItem, IDataItem
{
    public int useTimes { get; set; }
    public int itemUseRadius { get; set; }
    public string itemDescription { get; set; }
    // public Sprite itemIcon { get; set; } // 如果需要，可以取消注释  
    public string itemName { get; set; }
    public List<TrItem.ItemTag> tags { get; set; } = new List<TrItem.ItemTag>(); // 初始化列表  
    public EElement[] BaseElement { get; set; } = new EElement[2];
    public Vector2 currentElementCount { get; set; }

    /// <summary>
    /// 初始化基础模板
    /// </summary>
    public void initByTemplet()
    {

    }

    public void applyATTR()
    {
        
    }
}
