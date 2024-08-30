using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Distiller : MonoBehaviour
{
    /// <summary>
    /// 蒸馏功能（提纯药水浓度）
    /// </summary>
    /// <param name="potion1">药水1</param>
    /// <param name="potion2">药水2</param>
    //public DataItem Distillation(PotionEntity potion1,PotionEntity potion2)
    //{
    //    var newPotion = new DataItem();
    //    var newPurity = Mathf.Ceil((potion1.purity + potion2.purity + 2) / 4) > 1
    //        ? 1
    //        : Mathf.Ceil((potion1.purity + potion2.purity + 2) / 4);
    //    foreach (var VARIABLE in InventoryManager.Instance.itemDataList_SO.itemDetailsList)
    //    {
    //        if (VARIABLE.itemID == potion1.id)
    //        {
    //            var potion = new DataItem()
    //            {
    //                itemID = int.Parse(VARIABLE.itemID.ToString()+(newPurity*100).ToString()), itemDescription = VARIABLE.itemDescription, itemIcon = VARIABLE.itemIcon,
    //                itemName = VARIABLE.itemName, itemType = VARIABLE.itemType, itemUseRadius = VARIABLE.itemUseRadius,
    //                itemOnWorldSprite = VARIABLE.itemOnWorldSprite, foeverEffect = VARIABLE.foeverEffect,
    //                effectsIDs = VARIABLE.effectsIDs,
    //                purity = newPurity
    //            };
    //            newPotion = potion;
    //        }
    //    }
        
    //    return newPotion;
    //}
}
