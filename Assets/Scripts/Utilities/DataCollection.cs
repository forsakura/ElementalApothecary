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
}

[System.Serializable]
public struct ElementVector
{
    public Vector2 elementVector;
    
    public EElement[] GetEElements()
    {
        EElement[] eElements = new EElement[2];
        if (elementVector.x > 0) eElements[1] = EElement.Terra;
        else eElements[1] = EElement.Aer;
        if (elementVector.y > 0) eElements[2] = EElement.Ignis;
        else eElements[2] = EElement.Aqua;
        return eElements;
    }

    

}