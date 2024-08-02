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
        //production = new DataItem();
        //production.currentElementCount = 
        return production;
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
                Explosion += Math.Abs(origin.x) - Math.Abs(output.x);
            }
            if (Math.Abs(output.y) < Math.Abs(origin.y))
            {
                Explosion += Math.Abs(origin.y) - Math.Abs(output.y);
            }
        }
        return output;
    }

    //int GachaATTR(EElement baseElement)
    //{
    //    List<int> pool = new ();
    //    foreach (var material in Materials.Values)
    //    {
    //        pool.AddRange(material.GetATTRID());
    //    }
        
    //}
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

