using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ForgingFurnaces : SingletonMono<ForgingFurnaces>
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// Ï—…’“©ÀÆ
    /// </summary>
    /// <param name="potion"></param>
    public ItemDetails Calcination(PotionEntity potion)
    {
        var newPotion = new ItemDetails();
        foreach (var VARIABLE1 in SyntheticList.Instance.cauldronRecipes.CauldronRecipeEntities)
        {
            if (potion.id == VARIABLE1.productionId)
            {
                var newID = VARIABLE1.productionId;
                foreach (var potion1 in from VARIABLE2 in InventoryManager.Instance.itemDataList_SO.itemDetailsList
                         where VARIABLE2.itemID == newID
                         select new ItemDetails()
                         {
                             itemID = VARIABLE2.itemID, itemName = VARIABLE2.itemName, itemIcon = VARIABLE2.itemIcon,
                             itemDescription = VARIABLE2.itemDescription, itemType = VARIABLE2.itemType,
                             itemUseRadius = VARIABLE2.itemUseRadius, itemOnWorldSprite = VARIABLE2.itemOnWorldSprite,
                             foeverEffect = VARIABLE2.foeverEffect, effectsIDs = VARIABLE2.effectsIDs,
                             purity = VARIABLE2.purity
                         })
                {
                    newPotion = potion1;
                }
                
            }
        }
        return newPotion;
    }
}
