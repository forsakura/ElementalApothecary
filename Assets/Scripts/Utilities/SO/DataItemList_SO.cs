using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����õ���ƷSO
/// </summary>
[CreateAssetMenu(fileName = "DataItemList_SO", menuName = "Inventory/DataItemList_SO")]
public class DataItemList_SO : ScriptableObject
{
    public List<DataItem> itemList;
}
