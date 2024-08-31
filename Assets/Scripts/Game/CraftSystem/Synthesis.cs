using ProjectBase.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static Attribute;

public class Synthesis : MonoBehaviour, ISynthesis
{
    [SerializeField]
    private SOListForCraft SOList;

    private Dictionary<int, IDataItem> Materials;
    private IDataItem production;

    [SerializeField]
    private int maxMaterialEnum;
    public int MaxMaterialEnum
    {
        get => maxMaterialEnum;
        set => maxMaterialEnum = value;
    }

    [SerializeField]
    private bool isExplosive;
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

    private List<int> mainATTR;
    private List<int> auxATTR;

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
            try
            {
                Materials.Add(order, Item);
            }
            catch (ArgumentException e)
            {
            Console.WriteLine("An element with the same key already exists.");
            }
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

    public IDataItem output()
    {
        production = new DataItem();

        production.currentElementCount = CalculateElement();
        production.BaseElement = CalculateBaseElement(production.currentElementCount);

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

        return production;
    }

    public async Task<IDataItem> OutputAsync()
    {
        production = new DataItem();

        production.currentElementCount = CalculateElement();
        production.BaseElement = CalculateBaseElement(production.currentElementCount);

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
        if (production.BaseElement != new EElement[2] { EElement.None, EElement.None } ||
            production.GetATTRID().Count == 0)
        {
            IsSuccess = false;
            if (Explosion > 0)
            {
                IsExploded = true;//待定
            }
        }
        else if (!AddStabilizers && Explosion > 0)
        {
            IsSuccess = false;
            IsExploded = true;
        }
        else
        {
            IsSuccess = true;
        }
    }

    public Vector2 CalculateElement()
    {
        Vector2 output = new Vector2();
        foreach (var material in Materials.Values)
        {
            var origin = output;
            output += material.currentElementCount;

            if (Math.Abs(output.x) < Math.Abs(origin.x))
            {
                Explosion += (Math.Abs(origin.x) - Math.Abs(output.x)) * 2;
            }
            if (Math.Abs(output.y) < Math.Abs(origin.y))
            {
                Explosion += (Math.Abs(origin.y) - Math.Abs(output.y)) * 2;
            }
        }
        return output;
    }

    public EElement[] CalculateBaseElement(Vector2 ElementCount)
    {
        EElement[] output = new EElement[2] { EElement.None, EElement.None };
        if (ElementCount.x > 0)
        {
            output[0] = EElement.Aer;
        }
        else if (ElementCount.x < 0)
        {
            output[0] = EElement.Terra;
        }

        if (ElementCount.y > 0)
        {
            output[1] = EElement.Ignis;
        }
        else if (ElementCount.y < 0)
        {
            output[1] = EElement.Aqua;
        }
        return output;
    }

    public void initATTRpool(EElement[] baseElement, AttributeType attributeType)
    {
        mainATTR = new();
        auxATTR = new();
        foreach (var material in Materials.Values)
        {
            foreach (var ATTR in material.GetATTRID())
            {
                Attribute currentAttribute = SOList.AttributeList.GetAttributeById(ATTR);
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
                    if (attrElement == baseElement[1] || attrElement == baseElement[0])
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

