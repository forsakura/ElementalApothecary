using FischlWorks_FogWar;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FischlWorks_FogWar.csFogWar;

public class LightManager : MonoBehaviour//这一层主要是写方法
{
    csFogWar DarknessSystem;
    Dictionary<GameObject, int> RevealerIndexs = new Dictionary<GameObject, int>();

    private void Start()
    {
        try
        {
            DarknessSystem = GameObject.Find("Darkness").GetComponent<csFogWar>();
        }
        catch
        {
            Debug.LogErrorFormat("Failed to fetch csFogWar component. " +
                "Please rename the gameobject that the module is attachted to as \"Darkness\", " +
                "or change the implementation located in theDarknessManager.cs script.");
        }
    }

    public void CreateSingleLight(Vector2 lightPostion, int lightRange, bool updateOnlyOnMove, out GameObject light)
    {
        light = new GameObject();
        light.name = "LightSource[Run_Time]";

        FogRevealer LightRevealer = new FogRevealer(light.transform, lightRange, updateOnlyOnMove);
        RevealerIndexs.Add(light, DarknessSystem.AddFogRevealer(LightRevealer));

        light.transform.position = lightPostion;
    }

    public void DeleteSingleLight(GameObject LightToDestory)
    {
        int RevealerIndex;
        if (RevealerIndexs.TryGetValue(LightToDestory, out RevealerIndex))
        {
            if (DarknessSystem.RemoveFogRevealer(RevealerIndex))
            {
                RevealerIndexs.Remove(LightToDestory);
                Destroy(LightToDestory);
            }
                
        }
        else
        {
            Debug.LogFormat("Given Light's Index of {0} exceeds the RevealerIndexs' container range", RevealerIndex);
        }

    }

    public void SetLightRange(GameObject LightToSet,int LightRange)//这里由于本身没有方法故如此（其实也可以改成方法在里面的，但我嫌它塞的太紧凑）
    {
        int RevealerIndex;
        if (RevealerIndexs.TryGetValue(LightToSet, out RevealerIndex))
        {
            if (DarknessSystem._FogRevealers.Count > RevealerIndex && RevealerIndex > -1)
            { 
                DarknessSystem._FogRevealers[RevealerIndex]._SightRange = LightRange;
            }
            else
            {
                Debug.LogFormat("Given index of {0} exceeds the revealers' container range", RevealerIndex);
            }
        }
        else
        {
            Debug.LogFormat("Given Light's Index of {0} exceeds the RevealerIndexs' container range", RevealerIndex);
        }
    }

    //public void SetisLightOn(GameObject LightToSet,bool isLightOn, bool updateOnlyOnMove)
    //{
    //    int RevealerIndex;
    //    if (RevealerIndexs.TryGetValue(LightToSet, out RevealerIndex))
    //    {
    //        if (isLightOn)
    //        {
    //            if(!(DarknessSystem._FogRevealers.Count > RevealerIndex && RevealerIndex > -1))
    //            {
                    
    //            }
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogFormat("Given Light's Index of {0} exceeds the RevealerIndexs' container range", RevealerIndex);
    //    }

    //}
}

