using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct InventoryItem
{
    public ItemID itemID;
    public int itemAmount;
}

[System.Serializable]
public class LegacyItemDetails
{
    public ItemID itemID;
    public string itemName;
    public ItemType itemType;
    
    public Sprite itemIcon;
    public Sprite itemOnWorldSprite;
    public string itemDescription;
    public int itemUseRadius;
    public int foeverEffect;
    public float purity;

}



[System.Serializable]
public class LegacyPickableItem
{
    public ItemID itemID;
    public Vector3 position;
}