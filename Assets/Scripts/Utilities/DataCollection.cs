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
        if (elementVector.x > 0)
        {
            eElements[1] = EElement.Aer;
        }
        else if (elementVector.x == 0) eElements[0] = EElement.None;
        else eElements[0] = EElement.Terra;
      
        if (elementVector.y > 0) eElements[1] = EElement.Ignis;
        else if (elementVector.y == 0) eElements[1] = EElement.None;
        else eElements[1] = EElement.Aqua;
        return eElements;
    }
}
/// <summary>
/// 全局变量（
/// </summary>
public static class GlobalValue
{
    public static int EnviormentLeak=1;
    public static int ElementInterval = 1;
}