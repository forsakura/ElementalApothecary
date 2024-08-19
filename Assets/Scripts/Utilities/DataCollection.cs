using UnityEngine;

[System.Serializable]
public struct LegacyInventoryItem
{
    public ItemID itemID;
    public int itemAmount;
}

[System.Serializable]
public class LegacyItemDetails :TrItem
{
    public ItemID itemId;
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