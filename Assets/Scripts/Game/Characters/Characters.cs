using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using CharacterDelegates;
using UnityEditor.TerrainTools;

public class Characters : MonoBehaviour
{
    public CharacterData characterData;
    public ECharacterType characterType;
    public int CurrentHealth;
    public float CurrentSpeed;

    // [水/火, 风/土]
    public int[] ElementContain = new int[2] { 0, 0 };
    public EElement[] ElementName = new EElement[2];

    public int remainingBullet;
    public GameObject bulletPrefab;
    public Vector3 bulletInitOffset = Vector3.zero;

    private float invincibleTimer;

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
        ElementContain = new int[2] { 0, 0 };
        ElementName = new EElement[2] {EElement.None, EElement.None};

        isInvincible = false;
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

        isInvincible = true;
        invincibleTimer = characterData.InvincibleTime;
        StartCoroutine(InvincibleCountDown());

        AfterGetHit?.Invoke(this, hit);

        return true;
    }
    
    protected virtual int CalculateElementDamage(HitInstance hit)
    {
        int dmg = 0;
        for (int i = 0; i < 2; i++)
        {
            EElement en = hit.ElementName[i];
            int ec = hit.ElementContain[i];
            if (en == EElement.None)
            {
                continue;
            }
            if (this.ElementName[i] == EElement.None)
            {
                this.ElementName[i] = en;
                this.ElementContain[i] = ec;
            }
            else if (this.ElementName[i] == en)
            {
                // 缺少限制上限的内容，设想是直接对自己造成dmg=9999伤害直接暴毙（
                this.ElementContain[i] += ec;
            }
            else
            {
                if (this.ElementContain[i] == ec)
                {
                    this.ElementName[i] = EElement.None;
                    this.ElementContain[i] = 0;
                    dmg += ec;
                }
                else if (this.ElementContain[i] > ec)
                {
                    dmg += ec;
                    this.ElementContain[i] -= ec;
                }
                else
                {
                    dmg += this.ElementContain[i];
                    this.ElementContain[i] = ec - this.ElementContain[i];
                    this.ElementName[i] = en;
                }
            }
        }
        return dmg;
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
