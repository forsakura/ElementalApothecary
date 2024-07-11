using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct InventoryItem
{
    public int itemID;
    public int itemAmount;
}

[System.Serializable]
public class ItemDetails
{
    public int itemID;
    public string itemName;
    public ItemType itemType;

    public Sprite itemIcon;
    public Sprite itemOnWorldSprite;
    public string itemDescription;
    public int itemUseRadius;

    public List<int> effectsIDs;
    public int foeverEffect;
    public float purity;
}



[System.Serializable]
public class SceneItem
{
    public int itemID;
    public Vector3 position;
}