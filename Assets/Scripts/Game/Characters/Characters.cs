using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Characters : MonoBehaviour
{
    public CharacterData characterData;
    public ECharacterType characterType = ECharacterType.Player;
    public int CurrentHealth;
    public float CurrentSpeed;
    public GameObject bulletPrefab;
    // [水/火, 风/土]
    public int[] ElementContain = new int[2] { 0, 0 };
    public EElement[] ElementName = new EElement[2];

    public bool isInvincible;
    public bool isDead = false;

    private void Awake()
    {
        CurrentHealth = characterData.MaxHealth;
        CurrentSpeed = characterData.MoveSpeed;
        ElementContain = new int[2] { 0, 0 };
        ElementName = new EElement[2] {EElement.None, EElement.None};

        isInvincible = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject go = collider.gameObject;
        Debug.Log(go.name);
        if (go.tag == "Bullet" && go.GetComponent<BulletControl>().hitInstance.Source != gameObject)
        {
            GetHit(go.GetComponent<BulletControl>().hitInstance);
            Destroy(go);
        }
        if (characterType == ECharacterType.Player && go.tag == "Enemy")
        {

        }
    }

    public virtual void GetHit(HitInstance hit)
    {
        if (hit == null)
        {
            return;
        }
        if(!hit.IgnoreInvincible && isInvincible)
        {
            return;
        }
        // PlayerActions.BeforeGetHit.Invoke(hit);
        int elemDmg = CalculateElementDamage(hit);
        CurrentHealth -= hit.Damage + elemDmg;
        Debug.Log("Received hit from " + hit.Source.name + ", damage is: " + hit.Damage);
        if (CurrentHealth <= 0)
        {
            isDead = true;
            Debug.Log(gameObject.name + " is dead.");
        }
        // PlayerActions.AfterGetHit.Invoke(hit);
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

    public void Shoot(Vector2 target)
    {
        GameObject bul = Instantiate(bulletPrefab, transform.position, new Quaternion());
        HitInstance hit = new();
        hit.Source = gameObject;
        bul.GetComponent<BulletControl>().SetBullet(target, hit, BulletType.Shoot);
    }

    public void Throw(Vector2 target)
    {
        GameObject bul = Instantiate(bulletPrefab, transform.position, new Quaternion());
        HitInstance hit = new();
        hit.Source = gameObject;
        bul.GetComponent<BulletControl>().SetBullet(target, hit, BulletType.Throw);
    }
}
