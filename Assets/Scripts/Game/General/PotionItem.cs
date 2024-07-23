using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class PotionItem : LegacyItem
{

    [SerializeField]
    PotionEntity _thisPotion;
    Attack _attack;
    PlayerControll _player;

    public LayerMask targetLayer;
    public override void Start()
    {
        _attack = GetComponent<Attack>();
        _player = FindAnyObjectByType<PlayerControll>();
        base.Start();
        InitialzePotion();

    }

    PotionEntity GetPotionFromList(int id)
    {
        foreach(var potion in InventoryManager.Instance.potions_SO.PotionEntities)
        {
            if (potion.id == id)
            {
                //Debug.Log(id);
                return potion;
            }
                
        }

        Debug.LogError("未找到药水");
        return null;
    }

    void InitialzePotion()
    {
        _thisPotion = GetPotionFromList(itemID);
        //if (_thisPotion != null)
        //{
        //    this.itemDetails.itemName = _thisPotion.potionName;
        //    this.itemDetails.itemDescription = _thisPotion.description;
        //}

        SetDamageHealingAmount();
    }

    void SetDamageHealingAmount()
    {
        if(_thisPotion.toEnemyEffectIds == 201)
        {
            InstantStatEntity ins = GetInstantEffect(201);
            //_attack.currentDamage = ins.shootEffectParam;
            // _attack.currentDamage = 20;
            return;
        }

        if (_thisPotion.toEnemyEffectIds == 202)
        {
            InstantStatEntity ins = GetInstantEffect(201);
            //_attack.currentDamage = ins.shootEffectParam;
            // _attack.currentDamage = -20;
            return;
        }
    }

    InstantStatEntity GetInstantEffect(int id)
    {
        foreach(var instant in InventoryManager.Instance.InstantEffectSO.InstantStatEntities)
        {
            if (instant.id == id)
                return instant;
        }

        Debug.LogError("û���ҵ���ʱ�˺�");
        return null;
    }

    public override void ThrowModelDestroyObj()
    {
        base.ThrowModelDestroyObj();
        float distance = Vector3.Distance(transform.position, movePosition);
        if (distance <= 0.1f) 
        {
            Destroy(gameObject);
            Instantiate(effectPre,transform.position,Quaternion.identity,null);
            if (_player.currentAttackState == EPlayerAttackState.Throwing)
                Explode();
        } 
    }


    void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, itemDetails.itemUseRadius, targetLayer);
        foreach (Collider2D col in colliders)
        {
            // Apply effect to the objects detected within the AOE
            Debug.Log("AOE hit: " + col.gameObject.name);
            // Add your effect logic here
            col.GetComponent<Character>()?.GetHurt(_attack);
        }
    }

    void ApplyPotionEffect(Character target)
    {
        switch (_thisPotion.toEnemyEffectIds)
        {
            case 7:
                target.SlowDownWalkSpeed(0.4f);
                break;
            case 9:
                target.AddOnLeakTicHealing();
                break;
            case 10:
                target.DamageModifier = 0.2f;
                break;
            case 19:
                target.AddOnLeakTicPosion();
                break;
            default:
                break;
        }
    }
}
