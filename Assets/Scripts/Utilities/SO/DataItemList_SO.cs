using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 现在用的物品SO
/// </summary>
[CreateAssetMenu(fileName = "DataItemList_SO", menuName = "Inventory/DataItemList_SO")]
public class DataItemList_SO : ScriptableObject
{
    public List<DataItem> itemList;
}
