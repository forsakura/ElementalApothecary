using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Flask : MonoBehaviour
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
    /// 混合药水制作
    /// </summary>
    /// <param name="potionList"></param>
    /// <returns></returns>
    public PotionEntity SuperConvergence(List<PotionEntity> potionList)
    {
        float aerTemp = 0, ignisTemp = 0, aquaTemp = 0, terraTemp = 0, offSet=0;
        foreach (var VARIABLE in potionList)
        {
            aerTemp += VARIABLE.aerNum;
            ignisTemp += VARIABLE.ignisNum;
            aquaTemp += VARIABLE.aquaNum;
            terraTemp += VARIABLE.terraNum;
        }

        switch (aerTemp - terraTemp)
        {
            case > 0:
                aerTemp -= terraTemp;
                terraTemp = 0;
                offSet += terraTemp;
                break;
            case < 0:
                aerTemp = 0;
                terraTemp -= aerTemp;
                offSet += aerTemp;
                break;
            default:
                aerTemp = terraTemp = 0;
                offSet += aerTemp;
                break;
        }
        switch (ignisTemp - aquaTemp)
        {
            case > 0:
                ignisTemp -= aquaTemp;
                aquaTemp = 0;
                offSet += aquaTemp;
                break;
            case < 0:
                ignisTemp = 0;
                aquaTemp -= ignisTemp;
                offSet += ignisTemp;
                break;
            default:
                ignisTemp = aquaTemp = 0;
                offSet += ignisTemp;
                break;
        }

        var newPotionPurity = potionList.Sum(VARIABLE => VARIABLE.purity);

        newPotionPurity = newPotionPurity - 2 - offSet;
        
        if (aerTemp == 0 && ignisTemp == 0 && aquaTemp == 0 && terraTemp == 0 || newPotionPurity==0)
        {
            foreach (var newPotion in from VARIABLE in SyntheticList.Instance.potions.PotionEntities where VARIABLE.id==112 select new PotionEntity()
                     {
                         id = VARIABLE.id, potionName = VARIABLE.potionName, aerNum = VARIABLE.aerNum, ignisNum = VARIABLE.ignisNum, aquaNum = VARIABLE.aquaNum, terraNum = VARIABLE.terraNum, toEnemyEffectIds = VARIABLE.toEnemyEffectIds, foreverEffectId = VARIABLE.foreverEffectId, description = VARIABLE.description, purity = VARIABLE.purity
                     })
            {
                return newPotion;
            }
        }

        return null;
    }
}
