using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : Characters
{
    // 碰撞伤害从这里来
    HitInstance enemyHit;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<Characters>().GetHit(new HitInstance());
        }
    }
}
