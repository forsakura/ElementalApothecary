using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// OnCharacterInformationChange：当Character中的数据发生变换时需要Invoke  以Character为参数的方法可在其中执行
/// OnGetDamage：受伤时返回攻击者的位置，与角色受伤相关的方法可在其中执行
/// OnDie：与角色死亡相关的方法可在其中执行
/// </summary>

public class Character : MonoBehaviour,ISavable
{
    public ECharacterType characterType;
    public UnityEvent<Character> OnCharacterInformationChange;
    public UnityEvent OnDie;
    public UnityEvent<Transform> OnGetDamage;
    public CharacterRestriction characterRestriction;
    [HideInInspector]public Element element;

    [HideInInspector]public int currentLv = 0;
    [Header("血量")]
    public float currentHp;
    [Header("------")]
    [Tooltip("无敌时间")]public float invincibleTime;
    [HideInInspector]public float invincibleTimeCounter;
    [Tooltip("行走速度")]public float walkSpeed;
    [Tooltip("当前速度")]public float currentSpeed;
    [Header("------")]
    [Tooltip("投掷距离")]public float throwDistance;
    [Tooltip("爆炸范围")]public float explodeRadius;
    [Tooltip("射击速度")]public float shootSpeed;
    [Tooltip("使用时间间隔")]public float useTimeInterval;
    [Tooltip("加伤")] public float DamageModifier;


    [Header("------")]
    public bool isHurt;
    public bool isInvincible;
    public bool isDead;

    float _initialWalkSpeed;

    protected virtual void Awake() 
    {
        element = GetComponent<Element>();
        InitilalzeCharacter();

        OnCharacterInformationChange?.Invoke(this);
        _initialWalkSpeed = walkSpeed;
    }
    protected virtual void Update() 
    {
        TriggerInvulnerable();

        characterRestriction.RaiseEvent(this);
    }
    public void InitilalzeCharacter()
    {
        currentHp = characterRestriction.CharacterRestrictionEntity[currentLv].maxHp;
        invincibleTime = characterRestriction.CharacterRestrictionEntity[currentLv].maxInvincibleTime;
        walkSpeed = characterRestriction.CharacterRestrictionEntity[currentLv].maxWalkSpeed;
        throwDistance = characterRestriction.CharacterRestrictionEntity[currentLv].maxThrowDistance;
        explodeRadius = characterRestriction.CharacterRestrictionEntity[currentLv].maxExplodeRadius;
        shootSpeed = characterRestriction.CharacterRestrictionEntity[currentLv].maxShootSpeed;
        useTimeInterval = characterRestriction.CharacterRestrictionEntity[currentLv].maxUseTimeInterval;
        element.currentElementCount.x = characterRestriction.CharacterRestrictionEntity[currentLv].maxAerTerra;
        element.currentElementCount.y = characterRestriction.CharacterRestrictionEntity[currentLv].maxIgnisAqua;
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
    
    public virtual void GetHurt(Attack attacker)
    {
        if(isInvincible)
            return;

        float damage = attacker.currentDamage*(1+DamageModifier);

        if(currentHp - damage > 0)
        {
            currentHp -= damage;

            OnGetDamage?.Invoke(attacker.transform);
            
            isInvincible = true;

            isHurt = true;
        }
        else
        {
            currentHp = 0;

            OnDie?.Invoke();

            isDead = true;

            Debug.Log("角色死亡");
        }

        OnCharacterInformationChange?.Invoke(this);
        
        
        Debug.Log("攻击者名称:" + attacker.name + "---" +"攻击者位置:" + attacker.transform.position + "伤害：" + damage);
        
    }

    public virtual void GetHurt(float damage)
    {
        if (isInvincible)
            return;

        damage = damage * (1 + DamageModifier);

        if (currentHp - damage > 0)
        {
            currentHp -= damage;

            isInvincible = true;

            isHurt = true;
        }
        else
        {
            currentHp = 0;

            OnDie?.Invoke();

            isDead = true;

            Debug.Log("角色死亡");
        }

        OnCharacterInformationChange?.Invoke(this);

    }

    public void TriggerInvulnerable()
    {
       if(isInvincible)
       {
            invincibleTimeCounter -= Time.deltaTime; 
            if (invincibleTimeCounter <= 0)
            {
                isInvincible = false;
                isHurt = false;
                invincibleTimeCounter = invincibleTime;
            }
       }
    }
    //动画事件  在角色死亡动画的最后一帧调用
    public void DestroyObj()
    {
        Destroy(gameObject);
    }

 


    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,throwDistance);
    }


    public void SlowDownWalkSpeed(float percentage)
    {
        walkSpeed *= (1 - percentage);
    }

    public void ResetWalkSpeed()
    {
        walkSpeed = _initialWalkSpeed;
    }

    public void Heal(int amount)
    {
        if (currentHp + amount > characterRestriction.CharacterRestrictionEntity[currentLv].maxHp)
            currentHp = characterRestriction.CharacterRestrictionEntity[currentLv].maxHp;
        else
            currentHp += amount;
    }

    public void AddOnLeakTicHealing()
    {
        element.OnLeakTic += OnLeakTickHealing;
    }

    public void RemoveOnLeakTicHealing()
    {
        element.OnLeakTic -= OnLeakTickHealing;
    }

    public void AddOnLeakTicPosion()
    {
        element.OnLeakTic += OnLeakTickPoison;
    }

    public void RemoveOnLeakTicPosion()
    {
        element.OnLeakTic -= OnLeakTickPoison;
    }

    void OnLeakTickHealing()
    {
        Heal(1);
    }

    void OnLeakTickPoison()
    {
        GetHurt(5);
    }
#region 存储数据
    public DataDefination GetDataID()
    {
        return GetComponent<DataDefination>();
    }

    public void GetSaveData(Data data)
    {
        if(data.characterPosDicData.ContainsKey(GetDataID().ID))
        {
            data.characterPosDicData[GetDataID().ID] = new SerializeVector3(this.transform.position);

            data.flotSaveData[GetDataID().ID + "hp"] = this.currentHp;
            data.flotSaveData[GetDataID().ID + "walkSpeed"] = this.walkSpeed;
            data.flotSaveData[GetDataID().ID + "invincibleTime"] = this.invincibleTime;
            data.flotSaveData[GetDataID().ID + "throwDistance"] = this.throwDistance;
            data.flotSaveData[GetDataID().ID + "explodeRadius"] = this.explodeRadius;
            
        }
        else
        {
            data.characterPosDicData.Add(GetDataID().ID,new SerializeVector3(this.transform.position));
            
            data.flotSaveData.Add(GetDataID().ID + "hp", this.currentHp);
            data.flotSaveData.Add(GetDataID().ID + "walkSpeed",this.walkSpeed); 
            data.flotSaveData.Add(GetDataID().ID + "invincibleTime",this.invincibleTime); 
            data.flotSaveData.Add(GetDataID().ID + "throwDistance",this.throwDistance); 
            data.flotSaveData.Add(GetDataID().ID + "explodeRadius",this.explodeRadius);
            
        }
    }

    public void LoadData(Data data)
    {
        if(data.characterPosDicData.ContainsKey(GetDataID().ID))
        {
            this.transform.position = data.characterPosDicData[GetDataID().ID].ToVector3();
            
            this.currentHp = data.flotSaveData[GetDataID().ID + "hp"];
            this.walkSpeed = data.flotSaveData[GetDataID().ID + "walkSpeed"];
            this.invincibleTime = data.flotSaveData[GetDataID().ID + "invincibleTime"];
            this.throwDistance = data.flotSaveData[GetDataID().ID + "throwDistance"];
            this.explodeRadius = data.flotSaveData[GetDataID().ID + "explodeRadius"];
            

            OnCharacterInformationChange?.Invoke(this);
        }
    }
#endregion
}
