using ProjectBase.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static BaseAttribute;

public class Synthesis : MonoBehaviour, ISynthesis
{
    [SerializeField]
    private SOListForCraft SOList;

    private Dictionary<int, IDataItem> Materials = new();
    private DataItem production;

    [SerializeField]
    private int maxMaterialEnum;
    public int MaxMaterialEnum
    {
        get => maxMaterialEnum;
        set => maxMaterialEnum = value;
    }

    [SerializeField]
    private bool isExplosive = true;
    public bool IsExplosive
    {
        get => isExplosive;
        set => isExplosive = value;
    }

    [SerializeField]
    private bool isExploded;
    public bool IsExploded
    {
        get => isExploded;
        set => isExploded = value;
    }

    [SerializeField]
    private float explosion;
    public float Explosion
    {
        get => explosion;
        set => explosion = value;
    }

    private List<int> mainATTR = new();
    private List<int> auxATTR = new();

    [SerializeField]
    private int mainGachaTimes = 1; // 默认值  
    public int MainGachaTimes
    {
        get => mainGachaTimes;
        set => mainGachaTimes = value;
    }

    [SerializeField]
    private int auxGachaTimes = 1; // 默认值  
    public int AuxGachaTimes
    {
        get => auxGachaTimes;
        set => auxGachaTimes = value;
    }

    [SerializeField]
    private float auxGachaProbability = 100; // 默认值  
    public float AuxGachaProbability
    {
        get => auxGachaProbability;
        set => auxGachaProbability = value;
    }

    [SerializeField]
    private bool addStabilizers;
    public bool AddStabilizers
    {
        get => addStabilizers;
        set => addStabilizers = value;
    }

    [SerializeField]
    private bool isSuccess;
    public bool IsSuccess
    {
        get => isSuccess;
        set => isSuccess = value;
    }

    /// <summary>
    /// 顺序从0开始
    /// </summary>
    /// <param name="order"></param>
    /// <param name="Item"></param>
    public void addMaterial(int order, IDataItem Item)
    {
        if(order < MaxMaterialEnum)
        {
            Materials.Add(order, Item);
        }
        else
        {
            Console.WriteLine($"the Material order {order} overrun the upper limit");
        }
    }

    public void removeMaterial(int order)
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

    public DataItem output()
    {
        production = new DataItem();

        production.currentElementCount = CalculateElement(out EElement outBaseElement);
        production.BaseElement = outBaseElement;

        initATTRpool(production.BaseElement, AttributeType.Main);
        initATTRpool(production.BaseElement, AttributeType.Aux);

        for (int i = MainGachaTimes; i > 0; i--)
        {
            int MainATTR = GachaATTR(mainATTR);
            if (MainATTR != -1)
                production.AddATTRID(MainATTR);
        }
        if (ProbabilityTool.Instance.CheckProbability(AuxGachaProbability))
        {
            for (int i = AuxGachaTimes; i > 0; i--)
            {
                int AuxATTR = GachaATTR(auxATTR);
                if (AuxATTR != -1)
                    production.AddATTRID(AuxATTR);
            }
        }


        checkSucceed();
        if (IsSuccess)
        {
            production.initByTemplet();
            production.applyATTR();
        }
        else
        {
            Debug.Log("Synthesis failed!");
            return null;
        }

        return production;
    }

    public async Task<DataItem> OutputAsync()
    {
        production = new DataItem();

        production.currentElementCount = CalculateElement(out EElement outBaseElement);
        production.BaseElement = outBaseElement;

        production.ID.BaseId = 0;//临时的

        initATTRpool(production.BaseElement, AttributeType.Main);
        initATTRpool(production.BaseElement, AttributeType.Aux);

        await Task.Run(() =>
        {
            for (int i = MainGachaTimes; i > 0; i--)
            {
                int MainATTR = GachaATTR(mainATTR);
                if (MainATTR != -1)
                    production.AddATTRID(MainATTR);
            }
            if (ProbabilityTool.Instance.CheckProbability(AuxGachaProbability))
            {
                for (int i = AuxGachaTimes; i > 0; i--)
                {
                    int AuxATTR = GachaATTR(auxATTR);
                    if (AuxATTR != -1)
                        production.AddATTRID(AuxATTR);
                }
            }
        });

        checkSucceed();
        if (IsSuccess)
        {
            production.initByTemplet();
            production.applyATTR();
        }

        return production;
    }

    public void checkSucceed()
    {
        if (production.BaseElement == EElement.None ||
            production.GetATTRID().Count == 0)
        {
            IsSuccess = false;
            Debug.Log($"it's about production!! {production.GetATTRID().Count} {production.BaseElement}");
            if (Explosion > 0)
            {
                IsExploded = true;//待定
            }
        }
        else if (!AddStabilizers && Explosion > 0)
        {
            Debug.Log("it's about Explosion!!");
            IsSuccess = false;
            IsExploded = true;
        }
        else
        {
            IsSuccess = true;
        }
    }

    public float CalculateElement(out EElement baseElement)
    {
        Vector2 elementCount = new Vector2();
        foreach (var material in Materials.Values)
        {
            var origin = elementCount;
            switch(material.BaseElement)
            {
                case EElement.Aer:
                    elementCount.x += material.currentElementCount;
                    break;
                case EElement.Terra:
                    elementCount.x -= material.currentElementCount;
                    break;
                case EElement.Ignis:
                    elementCount.y += material.currentElementCount;
                    break;
                case EElement.Aqua:
                    elementCount.y -= material.currentElementCount;
                    break;
            }

            if (Math.Abs(elementCount.x) < Math.Abs(origin.x))
            {
                Explosion += (Math.Abs(origin.x) - Math.Abs(elementCount.x)) * 2;
            }
            if (Math.Abs(elementCount.y) < Math.Abs(origin.y))
            {
                Explosion += (Math.Abs(origin.y) - Math.Abs(elementCount.y)) * 2;
            }
        }

        if (Math.Abs(elementCount.x) > Math.Abs(elementCount.y))
        {
            if (elementCount.x >= 0) //其实是不可能等于的
                baseElement = EElement.Aer;
            else 
                baseElement = EElement.Terra;

            return Math.Abs(elementCount.x) - Math.Abs(elementCount.y);
        }
        else if (Math.Abs(elementCount.x) < Math.Abs(elementCount.y))
        {
            if (elementCount.y >= 0)
                baseElement = EElement.Ignis;
            else
                baseElement = EElement.Aqua;

            return Math.Abs(elementCount.y) - Math.Abs(elementCount.x);
        }
        else
        {
            baseElement = EElement.None;
            return 0;
        }
    }

    public void initATTRpool(EElement baseElement, AttributeType attributeType)
    {
        foreach (var material in Materials.Values)
        {
            foreach (var ATTR in material.GetATTRID())
            {
                BaseAttribute currentAttribute = SOList.AttributeList.GetAttributeById(ATTR);
                if (currentAttribute == null)
                {
                    Debug.LogError($"ATTRId {ATTR} is invalid!");
                }
                AttributeType type = currentAttribute.Type;
                if (type != attributeType)
                {
                    continue;
                }

                if (attributeType == AttributeType.Main)
                {
                    EElement attrElement = currentAttribute.baseElement;
                    if (attrElement == baseElement)
                    {
                        mainATTR.Add(ATTR);
                    }
                    
                }
                else if (attributeType == AttributeType.Aux)
                {
                    auxATTR.Add(ATTR);
                }
            }
        }
    }

    public int GachaATTR(List<int> pool)
    {
        if (pool == null || pool.Count == 0)
        {
            Debug.Log("List is null or empty");
            return -1;
        }

        int index = UnityEngine.Random.Range(0, pool.Count);
        int output = pool[index];
        pool.Remove(index);
        return output;
    }

    public void init()
    {
        Materials.Clear();
        mainATTR = new();
        auxATTR = new();
        production = null;

        Explosion = 0;
        IsSuccess = false;
        IsExploded = false;
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

