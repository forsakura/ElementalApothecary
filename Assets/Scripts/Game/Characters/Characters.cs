using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    public CharacterData characterData;
    public int CurrentHealth;
    public float CurrentSpeed;
    // [水/火, 风/土]
    public int[] ElementContain;
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
        PlayerActions.BeforeGetHit(hit);
        int elemDmg = CalculateElementDamage(hit);
        CurrentHealth -= hit.Damage + elemDmg;
        if (CurrentHealth <= 0)
        {
            isDead = true;
        }
        PlayerActions.AfterGetHit(hit);
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
}
