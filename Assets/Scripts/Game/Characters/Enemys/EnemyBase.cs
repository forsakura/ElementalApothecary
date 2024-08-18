using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : Characters
{
    // ��ײ�˺���������
    HitInstance enemyHit;

    private void Start()
    {
        InitHit();
    }

    protected virtual void InitHit()
    {
        enemyHit.Source = gameObject;
        enemyHit.Damage = characterData.Damage;
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<Characters>().GetHit(enemyHit);
        }
    }
}
