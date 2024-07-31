using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背包物品数据，尽量还是通过id从SO调用固有数据
/// </summary>
public class DataItem : TrItem
{
    public int itemUseRadius;
    public string itemDescription; 
    //public Sprite itemIcon; 
    public string itemName;
    public List<ItemTag> tags;//这个没有很细的去想，先这么做
    public EElement BaseElement;
    public Vector2 currentElementCount;

    /// <summary>
    /// 初始化基础模板
    /// </summary>
    public void initByTemplet()
    {

    }

    public void applyATTR(int id)
    {

    }
}
