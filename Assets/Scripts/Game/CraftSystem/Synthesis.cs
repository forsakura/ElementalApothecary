using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synthesis
{
    [SerializeField]
    SOListForCraft SOList;

    Dictionary<int, IDataItem> Materials;
    IDataItem production;

    float Explosion;
    public bool isExpolsive;
    bool isExploded;

    public int GachaTimes = 1;//先默认为1

    bool AddStabilizers;//还没做相关

    bool isSuccess;
    void addMaterial(int order, IDataItem Item)
    {
        try
        {
            Materials.Add(order, Item);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("An element with the same key already exists.");
        }
    }

    void removeMaterial(int order)
    {
        if (Materials.Remove(order))
        {
            Console.WriteLine($"Material with order {order} has been removed.");
        }
        else
        {
            Console.WriteLine($"Material with order {order} does not exist.");
        }
    }

    IDataItem output()
    {
        init();
        production = new DataItem();

        production.currentElementCount = CalculateElement();
        production.BaseElement = CalculateBaseElement(production.currentElementCount);

        for (int i = GachaTimes; i > 0; i--)
        {
            int mainATTR = GachaATTR(production.BaseElement);
            if (mainATTR != -1) 
                production.AddATTRID(mainATTR);
        }

        checkSecceed();
        if (isSuccess)
        {
            production.initByTemplet();
            production.applyATTR();
        }

        return production;
    }

    void checkSecceed()
    {
        if (production.BaseElement != new EElement[2] { EElement.None, EElement.None } ||
            production.GetATTRID().Count == 0)
        {
            isSuccess = false;
            if (Explosion > 0)
            {
                isExploded = true;//待定
            }
        }
        else if (!AddStabilizers && Explosion > 0)
        {
            isSuccess = false;
            isExploded = true;
        }
        else
        {
            isSuccess = true;
        }
    }

    Vector2 CalculateElement()
    {
        Vector2 output = new Vector2();
        foreach (var material in Materials.Values)
        {
            var origin = output;
            output += material.currentElementCount;

            if (Math.Abs(output.x) < Math.Abs(origin.x)) 
            {
                Explosion += (Math.Abs(origin.x) - Math.Abs(output.x))*2;
            }
            if (Math.Abs(output.y) < Math.Abs(origin.y))
            {
                Explosion += (Math.Abs(origin.y) - Math.Abs(output.y))*2;
            }
        }
        return output;
    }

    EElement[] CalculateBaseElement(Vector2 ElementCount)
    {
        EElement[] output = new EElement[2] { EElement.None, EElement.None };
        if (ElementCount.x > 0) 
        {
            output[0] = EElement.Aer;
        }
        else if(ElementCount.x < 0)
        {
            output[0] = EElement.Terra;
        }

        if(ElementCount.y > 0)
        {
            output[1] = EElement.Ignis;
        }
        else if (ElementCount.y < 0)
        {
            output[1] = EElement.Aqua;
        }
        return output;
    }

    int GachaATTR(EElement[] baseElement)
    {
        List<int> pool = new();
        foreach (var material in Materials.Values)
        {
            foreach(var ATTR in material.GetATTRID())
            {
                EElement attrElement = SOList.AttributeList.GetAttributeById(ATTR).baseElement;
                if (attrElement == baseElement[1] || (attrElement == baseElement[0])) 
                {
                    pool.Add(ATTR);
                }
            }
        }

        if (pool == null || pool.Count == 0)
        {
            Debug.Log("List is null or empty");
            return -1;
        }

        int index = UnityEngine.Random.Range(0, pool.Count);
        return pool[index];
    }

    public void init()
    {
        Materials.Clear();
        production = null;

        Explosion = 0;
    }
}

//public class Material
//{
//    public DataItem data;
//    public int order;
//    public Material(int order,DataItem data)
//    {
//        this.order = order;
//        this.data = data;
//    } 
//}

