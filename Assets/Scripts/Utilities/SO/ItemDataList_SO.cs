using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 将要弃用的SO
/// </summary>
[CreateAssetMenu(fileName = "ItemDataList_SO", menuName = "Inventory/ItmDataList")]
public class ItemDataList_SO : ScriptableObject
{
    public List<LegacyItemDetails> itemDetailsList;
}
