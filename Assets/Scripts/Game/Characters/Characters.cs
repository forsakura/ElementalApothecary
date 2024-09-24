using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using CharacterDelegates;
using UnityEditor.TerrainTools;
using System;

public class Characters : MonoBehaviour
{
    public CharacterData characterData;
    public ECharacterType characterType;
    public int CurrentHealth;
    public float CurrentSpeed;

    // [水/火, 风/土]
    //public int[] ElementContain = new int[2] { 0, 0 };
    //public Vector2 element;
    //public EElement[] ElementName = new EElement[2];

    public ElementVector elementState;

    public int remainingBullet;
    public GameObject bulletPrefab;
    public Vector3 bulletInitOffset = Vector3.zero;

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
        CurrentHealth = characterData.MaxHealth;
        OnHealthChange?.Invoke(this, CurrentHealth);
        CurrentSpeed = characterData.MoveSpeed;
        elementState = new ElementVector();
        //ElementContain = new int[2] { 0, 0 };
        //ElementName = new EElement[2] {EElement.None, EElement.None};

        isInvincible = false;
        elementLossing = false;
    }
    private void Update()
    {
        //元素相邻
        if (Vector2.Angle(elementState.elementVector,Vector2.right)%90 > 0.1f&&!elementLossing)
        {
            elementLossing = true;
            StartCoroutine(ElementLoss());
            
        }
    }

    IEnumerator ElementLoss()
    {
        float x=elementState.elementVector.x;
        float y=elementState.elementVector.y;
        if (x > 0)
            elementState.elementVector.x = Math.Max(x - GlobalValue.EnviormentLeak, 0);
        else elementState.elementVector.x = Math.Min(x + GlobalValue.EnviormentLeak, 0);
        if (y > 0)
            elementState.elementVector.y = Math.Max(y - GlobalValue.EnviormentLeak, 0);
        else elementState.elementVector.y = Math.Min(y - GlobalValue.EnviormentLeak, 0);
        //print(elementState.elementVector);
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
        CurrentHealth -= totalDmg;
        OnHealthChange?.Invoke(this, CurrentHealth);
        if (CurrentHealth <= 0)
        {
            isDead = true;
            OnDeath?.Invoke(this, hit);
        }

        print(elementState.elementVector);
        isInvincible = true;
        invincibleTimer = characterData.InvincibleTime;
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
            //dmg+=(int)Cause(res+res2);
        }

        elementState.elementVector += hit.elementState.elementVector;

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
        GameObject bul = Instantiate(bulletPrefab, transform.position + bulletInitOffset, new Quaternion());
        //HitInstance hit = new()
        //{
        //    Source = gameObject,
        //    Damage = characterData.Damage
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
        //    Damage = characterData.Damage
        //};
        BulletControl bulletControl = bul.GetComponent<BulletControl>();
        bulletControl.SetBullet(target, BulletType.Throw);
        bulletControl.OnThrowHitTarget += OnThrowHitTarget;
        OnThrow?.Invoke(this);
    }

    public virtual void Fill()
    {
        remainingBullet = characterData.MaxBulletCount;
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
