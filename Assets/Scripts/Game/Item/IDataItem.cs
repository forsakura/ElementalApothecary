using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataItem:ITrItem
{
    int itemUseRadius { get; set; }
    string itemDescription { get; set; }
    // Sprite itemIcon { get; set; } // 如果需要，可以取消注释  
    string itemName { get; set; }
    List<TrItem.ItemTag> tags { get; set; }
    float currentElementCount { get; set; }
    EElement BaseElement { get; set; }

    void initByTemplet();
    void applyATTR();
    void DebugDisplayData();
}