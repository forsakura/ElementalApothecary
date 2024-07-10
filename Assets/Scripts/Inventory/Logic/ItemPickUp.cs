using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();

        if (item != null)
        {
            //拾取物品添加到背包
            InventoryManager.Instance.AddItem(item, true);
        }
    }
}