using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using CharacterDelegates;
using UnityEditor.TerrainTools;
using System;
using ProjectBase.UI;
using ProjectBase.Pool;
using static DG.Tweening.DOTweenModuleUtils;

public class Characters : MonoBehaviour
{
    public CharacterData characterData;
    public ECharacterType characterType;
    public int currentHealth;
    public float currentSpeed;
    public DataItem currentBulletValue;
    // [水/火, 风/土]
    //public int[] ElementContain = new int[2] { 0, 0 };
    //public Vector2 element;
    //public EElement[] ElementName = new EElement[2];

    public ElementVector elementState;

    public int remainingBullet;
    public GameObject bulletPrefab;
    public bool immunePhysical;

    public Vector3 bulletInitOffset = Vector3.zero;
    public float attackInterval;
    public float attackDistance;
    public float invincibleTime;

    private float invincibleTimer;
    private bool elementLossing;

    public bool isInvincible;
    public bool isDead = false;

    public event BeforeGetHitEventHandler BeforeGetHit;
    public event AfterGetHitEventHandler AfterGetHit;
    public event CharacterDeathEventHandler OnDeath;
    public event OnHealthChangeEventHandler OnHealthChange;

    public event ShootEventHandler OnShoot;
    public event ThrowEventHandler OnThrow;
    public event FillBulletEventHandler OnFill;

    private void Awake()
    {
        
        if (GetComponent<PlayerController>()  != null)
        {
            characterType = ECharacterType.Player;
            tag = "Player";
            
        }
        else
        {
            characterType = ECharacterType.Enemy;
            tag = "Enemy";
        }
        currentHealth = characterData.maxHealth;
        OnHealthChange?.Invoke(this, currentHealth);
        currentSpeed = characterData.moveSpeed;
        elementState = new ElementVector();
        //ElementContain = new int[2] { 0, 0 };
        //ElementName = new EElement[2] {EElement.None, EElement.None};

        isInvincible = false;
        elementLossing = false;
    }
    private void Update()
    {
        if (!elementLossing && elementState.GetEElements() !=new EElement[2]{EElement.None,EElement.None })
        {
            elementLossing = true;
            StartCoroutine(ElementLoss());
        }
    }

    IEnumerator ElementLoss()
    {
        float x=elementState.elementVector.x;
        float y= elementState.elementVector.y;
        if (x > 0.1)
            elementState.elementVector.x = Math.Max(x - GlobalValue.EnviormentLeak, 0);
        else if(x<-0.1)
            elementState.elementVector.x = Math.Min(x + GlobalValue.EnviormentLeak, 0);
        if (y > 0.1)
            elementState.elementVector.y = Math.Max(y - GlobalValue.EnviormentLeak, 0);
        else if(y<-0.1)
            elementState.elementVector.y = Math.Min(y - GlobalValue.EnviormentLeak, 0);
        yield return new WaitForSeconds(1f);
        elementLossing=false;
    }


    private void OnEnable()
    {
        CharacterManager.Instance.Register(this);
    }

    private void OnDisable()
    {
        CharacterManager.Instance.Unregister(this);
    }

    public virtual bool GetHit(HitInstance hit)
    {
        if (hit == null)
        {
            return false;
        }
        if(!hit.IgnoreInvincible && isInvincible)
        {
            return false;
        }
        BeforeGetHit?.Invoke(this, hit);

        int totalDmg = hit.Damage + CalculateElementDamage(hit);
        currentHealth -= totalDmg;
        OnHealthChange?.Invoke(this, currentHealth);
        if (currentHealth <= 0)
        {
            isDead = true;
            OnDeath?.Invoke(this, hit);
        }

        //print(elementState.elementVector);
        isInvincible = true;
        invincibleTimer = invincibleTime;
        StartCoroutine(InvincibleCountDown());

        AfterGetHit?.Invoke(this, hit);

        return true;
    }
    /// <summary>
    /// 元素伤害计算相关
    /// </summary>
    /// <param name="hit"></param>
    /// <returns></returns>
    protected virtual int CalculateElementDamage(HitInstance hit)
    {
        int dmg = 0;
        Vector2 hitVector = hit.elementState.elementVector;
        Vector2 currentVector = elementState.elementVector;

        if (!hit.IgnoreInvincible)
        {
            //额外造成抵消元素的伤害
            float res = GetCausedDamage(currentVector.x, hitVector.x);
            float res2 = GetCausedDamage(currentVector.y, hitVector.y);
            dmg+=(int)/*Cause*/(res+res2);
        }

        elementState.elementVector += hit.elementState.elementVector;
        elementState.elementVector = new Vector2(
            Mathf.Clamp(elementState.elementVector.x, -100, 100), 
            Mathf.Clamp(elementState.elementVector.y, -100, 100));
        //for (int i = 0; i < 2; i++)
        //{
        //    EElement en = hit.ElementName[i];
        //    int ec = hit.ElementContain[i];
        //    if (en == EElement.None)
        //    {
        //        continue;
        //    }
        //    if (this.ElementName[i] == EElement.None)
        //    {
        //        this.ElementName[i] = en;
        //        this.ElementContain[i] = ec;
        //    }
        //    else if (this.ElementName[i] == en)
        //    {
        //        // 缺少限制上限的内容，设想是直接对自己造成dmg=9999伤害直接暴毙（
        //        this.ElementContain[i] += ec;
        //    }
        //    else
        //    {
        //        if (this.ElementContain[i] == ec)
        //        {
        //            this.ElementName[i] = EElement.None;
        //            this.ElementContain[i] = 0;
        //            dmg += ec;
        //        }
        //        else if (this.ElementContain[i] > ec)
        //        {
        //            dmg += ec;
        //            this.ElementContain[i] -= ec;
        //        }
        //        else
        //        {
        //            dmg += this.ElementContain[i];
        //            this.ElementContain[i] = ec - this.ElementContain[i];
        //            this.ElementName[i] = en;
        //        }
        //    }
        //}
        return dmg;
    }

    private float GetCausedDamage(float x, float y)
    {

        if (x * (x + y) < 0)
        {
            return Math.Abs(x);
        }
        else
        {
            if (Math.Abs(x + y) - Math.Abs(x) < 0)
            {
                return Math.Abs(y);
            }
            else return 0f;
        }
    }
    public virtual void Shoot(Vector2 target)
    {
        if (remainingBullet == 0 && characterType == ECharacterType.Player)
        {
            return;
        }
        //PoolManager.Instance.AddPoolDic(20, "Prefab/Bullets/Bullet");
        GameObject bul = Instantiate(bulletPrefab, transform.position + bulletInitOffset, new Quaternion());
        //HitInstance hit = new()
        //{
        //    Source = gameObject,
        //    damage = characterData.damage
        //};
        BulletControl bulletControl = bul.GetComponent<BulletControl>();
        bulletControl.SetBullet(target, BulletType.Shoot);
        bulletControl.OnShootHitTarget += OnShootHitTarget;
        remainingBullet -= 1;
        OnShoot?.Invoke(this, 1);
    }

    public virtual void Throw(Vector2 target)
    {
        GameObject bul = Instantiate(bulletPrefab, transform.position + bulletInitOffset, new Quaternion());
        //HitInstance hit = new()
        //{
        //    Source = gameObject,
        //    damage = characterData.damage
        //};
        ItemEntityCtrl itemEntity = bul.GetComponent<ItemEntityCtrl>();
        BulletControl bulletControl = itemEntity.BulletComponent;

        itemEntity.Data = currentBulletValue;
        bulletControl.SetBullet(target, BulletType.Throw);
        //bulletControl.OnThrowHitTarget += OnThrowHitTarget;//先去了，不由角色决定效果
        OnThrow?.Invoke(this);
    }

    public virtual void Fill()
    {
        currentBulletValue = UIManager.Instance.GetPanel<FightingUIPanel>("FightingUI").GetCurrentBullet();
        if (currentBulletValue == null) return;

        remainingBullet = characterData.maxBulletCount;
        OnFill?.Invoke(this, remainingBullet);
    }

    public virtual void OnShootHitTarget(BulletControl bullet, Collider2D go)
    {

    }

    public virtual void OnThrowHitTarget(BulletControl bullet, Collider2D go)
    {

    }

    private IEnumerator InvincibleCountDown()
    {
        while(invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
            yield return null;
        }
        isInvincible = false;
    }
}
