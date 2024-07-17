using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

public class AlchemyCauldron : Singleton<AlchemyCauldron>
{
    
    
    /// <summary>
    /// ҩˮ����
    /// </summary>
    /// <param name="m1">����1</param>
    /// <param name="m2">����2</param>
    public void Refining(MaterialEntity m1, MaterialEntity m2)
    {
        foreach (var VARIABLE1 in SyntheticList.Instance.cauldronRecipes.CauldronRecipeEntities)
        {
            if (m1.id == VARIABLE1.materialId_1 && m2.id == VARIABLE1.materialId_2)
            {
                var id = VARIABLE1.productionId;
                foreach (var potionEntity in from VARIABLE2 in SyntheticList.Instance.potions.PotionEntities 
                         where id == VARIABLE2.id 
                         select new PotionEntity()
                         {
                             id = VARIABLE2.id, potionName = VARIABLE2.potionName, aerNum = VARIABLE2.aerNum,
                             ignisNum = VARIABLE2.ignisNum,
                             aquaNum = VARIABLE2.aquaNum, terraNum = VARIABLE2.terraNum,
                             toEnemyEffectIds = VARIABLE2.toEnemyEffectIds, foreverEffectId = VARIABLE2.foreverEffectId,
                             description = VARIABLE2.description, purity = 0.8f
                         })
                {
                    InventoryManager.Instance.AddItem(potionEntity.id,10);
                }
            }
            else
            {
                foreach (var material in from VARIABLE3 in SyntheticList.Instance.materials.MaterialEntities
                         where VARIABLE3.id == 301
                         select new DrugMaterial()
                         {
                             id = VARIABLE3.id, name = VARIABLE3.materialName, description = VARIABLE3.description,
                             type = (MaterialType)VARIABLE3.type
                         })
                {
                    InventoryManager.Instance.AddItem(material.id,10);
                }
            }
            
        }
    }
    
}
