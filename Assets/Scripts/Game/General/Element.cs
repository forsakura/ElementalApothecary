using System;
using Unity.Mathematics;
using UnityEngine;

public class Element : MonoBehaviour,ISavable
{
    //对于元素量（x,y），x为风，-x为土，y为火，-y为水
    public EElement currentElement;
    public Vector2 currentElementCount;

    [HideInInspector] public float elementDamage;//内部计算元素伤害

    [Header("------")]
    [Tooltip("元素流失间隔 /s")] public float leakInterval = 1f; // 控制元素流失的时间间隔
    [Tooltip("元素流失量 /1")] public float leakAmount = 1f; // 控制每次流失的数量
    [Header("------")]
    [Tooltip("元素积累间隔 /s")] public float accumlationInterval = 1f; // 控制元素积累的时间间隔
    [Tooltip("元素积累量 /1")] public float accumlationAmount = 1f; // 控制每次积累的数量
    private float lastAccumlationTime;
    private float lastLeakTime;
    private float addOnTime = 1;//内置附着CD
    private float addOnTimeCounter;


    [Header("------")]
    public bool isLeaking = false;

    public bool isAddOn;//附着  

    public bool isAccumlation;//元素累积

    public Action OnLeakTic;

    Vector2 _previousElementCount;

    Character _character;

    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    private void Update()
    {
        SwitchElement();

        TriggerAddOn();

        OnLeaking();

    }
    private void OnEnable() 
    {
        ISavable savable = this;
        savable.RegisterSaveData();
    }
    private void OnDisable() 
    {
        ISavable savable = this;
        savable.UnRegisterSaveData();
    }
    public EElement SwitchElement()
    {
        if (Mathf.Abs(currentElementCount.x) > Mathf.Abs(currentElementCount.y))
        {
            currentElement = currentElementCount.x > 0 ? EElement.Aer : EElement.Terra;
        }
        else
        {
            currentElement = currentElementCount.y > 0 ? EElement.Ignis : EElement.Aqua;
        }

        if (currentElementCount.x == 0 && currentElementCount.y == 0)
        {
            currentElement = EElement.None;
        }

        return currentElement;
    }

    public void ElementReaction(Element attacker, Element handel, Vector2 quant, bool isExplode)
    {
        if (isAddOn)
        {
            switch (attacker.currentElement)
            {
                case EElement.Aer:
                    HandleElement(handel, quant, isExplode);

                    if (handel.currentElement == EElement.Aer) return;

                    float a = attacker.currentElementCount.x - handel.currentElementCount.x;
                    attacker.elementDamage = Mathf.Abs(attacker.currentElementCount.x - a);
                    break;
                case EElement.Ignis:
                    HandleElement(handel, quant, isExplode);

                    if (handel.currentElement == EElement.Ignis) return;
                    float c = attacker.currentElementCount.y - handel.currentElementCount.y;
                    attacker.elementDamage = Mathf.Abs(attacker.currentElementCount.y - c);
                    break;
                case EElement.Terra:
                    HandleElement(handel, quant, isExplode);

                    if (handel.currentElement == EElement.Terra) return;
                    float b = attacker.currentElementCount.x - handel.currentElementCount.x;
                    attacker.elementDamage = Mathf.Abs(attacker.currentElementCount.x - b);
                    break;
                case EElement.Aqua:
                    HandleElement(handel, quant, isExplode);

                    if (handel.currentElement == EElement.Aqua) return;
                    float d = attacker.currentElementCount.y - handel.currentElementCount.y;
                    attacker.elementDamage = Mathf.Abs(attacker.currentElementCount.y - d);
                    break;
                case EElement.None:
                    HandleElement(handel, quant, isExplode);

                    elementDamage = 0;
                    break;
            }
        }

    }

    public void HandleElement(Element element, Vector2 quant, bool isExplode)
    {
        switch (element.currentElement)
        {
            case EElement.Aer:
                element.currentElementCount += quant;
                break;
            case EElement.Ignis:
                element.currentElementCount += quant;
                break;
            case EElement.Terra:
                element.currentElementCount += quant;
                break;
            case EElement.Aqua:
                element.currentElementCount += quant;
                break;
            case EElement.None:
                element.currentElementCount += quant;
                break;

        }
    }

    public void OnLeaking()
    {

        if (Time.time - lastLeakTime >= leakInterval)
        {
            lastLeakTime = Time.time;
            _previousElementCount = currentElementCount;
            ElementLeak(ref currentElementCount);

            if(_character)
                ClearBuff();

            OnLeakTic?.Invoke();
        }
    }

    void ClearBuff()
    {
        if ((_previousElementCount.x < 0 && currentElementCount.x >= 0) || (_previousElementCount.y < 0 && currentElementCount.y >= 0))
        {
            _character.ResetWalkSpeed();
            if (IsBuffSubscribed("OnLeakTickHealing"))
                _character.RemoveOnLeakTicHealing();
        }

        if ((_previousElementCount.x > 0 && currentElementCount.x <= 0) || (_previousElementCount.y > 0 && currentElementCount.y <= 0))
            _character.DamageModifier = 0;

        if ((_previousElementCount.x < 0 && currentElementCount.x >= 0) || (_previousElementCount.y > 0 && currentElementCount.y <= 0))
        {
            if (IsBuffSubscribed("OnLeakTickPoison"))
                _character.RemoveOnLeakTicPosion();
        }
    }

    bool IsBuffSubscribed(string name)
    {
        if (OnLeakTic == null)
            return false;
        
        Delegate[] subscribers = OnLeakTic.GetInvocationList();
        foreach(Delegate sub in subscribers)
            if(sub.Method.Name == name)
                return true;
        return false;
    }
    /// <summary>
    /// 元素需要留存在怪物的身上，不能直接逸散 当存在临相元素时会触发逸散  逸散量 = 少的元素
    /// </summary>
    public void ElementLeak(ref Vector2 alpa )
    {
        if (MathF.Abs(alpa.x) > 0 && Mathf.Abs(alpa.y) > 0) 
        {
            alpa.x = alpa.x > 0 ? alpa.x -= leakAmount : alpa.x += leakAmount;
            alpa.y = alpa.y > 0 ? alpa.y -= leakAmount : alpa.y += leakAmount;
            isLeaking = true;
        }
        else
        {
            isLeaking = false;
        }

        // if(alpa.magnitude <= 1)
        // {
        //     isLeaking = false;
        // }
    }
    public void OnAccumlationAmount(Element attacker,Element handel)//trigger时执行
    {
        if (Time.time - lastAccumlationTime >= accumlationInterval)
        {
            lastAccumlationTime = Time.time;

            if(isAccumlation)
            {
                if(handel.currentElementCount.x != 0)
                    attacker.currentElementCount.x = handel.currentElementCount.x>0 ? attacker.currentElementCount.x += accumlationAmount : attacker.currentElementCount.x -= accumlationAmount;
                if(handel.currentElementCount.y != 0)
                    attacker.currentElementCount.y = handel.currentElementCount.y > 0 ? attacker.currentElementCount.y+= accumlationAmount : attacker.currentElementCount.y-= accumlationAmount;
            }
        }    
    }

    public void TriggerAddOn()
    {
       if(isAddOn)
       {
            addOnTimeCounter -= Time.deltaTime; 
            if (addOnTimeCounter <= 0)
            {
                isAddOn = false;
                addOnTimeCounter = addOnTime;
            }
       }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        Element element = other.GetComponent<Element>();

        if(element != null)
        {
            isAccumlation = true;
            OnAccumlationAmount(this, element);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        isAccumlation = false;
    }
    public DataDefination GetDataID()
    {
        return GetComponent<DataDefination>();
    }

    public void GetSaveData(Data data)
    {
        if(data.flotSaveData.ContainsKey(GetDataID().ID + "elementX"))
        {
            currentElementCount.x = data.flotSaveData[GetDataID().ID + "elementX"];
            currentElementCount.y = data.flotSaveData[GetDataID().ID + "elementY"];
        }
        else
        {
            data.flotSaveData.Add(GetDataID().ID + "elementX",currentElementCount.x);
            data.flotSaveData.Add(GetDataID().ID + "elementY",currentElementCount.y);
        }
        
    }

    public void LoadData(Data data)
    {
        if(data.flotSaveData.ContainsKey(GetDataID().ID + "elementX"))
        {
            currentElementCount.x = data.flotSaveData[GetDataID().ID + "elementX"];
            currentElementCount.y = data.flotSaveData[GetDataID().ID + "elementY"];
        }
            
    }

#if UNITY_EDITOR
    public void ReSetElement()
    {
        // currentElement = EElement.None;
        // AerCount = 0;
        // IgnisCount = 0;
        // TerraCount = 0;
        // AquaCount = 0; 
    }

    
#endif
}



