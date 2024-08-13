using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : Characters
{
    // ��ײ�˺���������
    HitInstance enemyHit;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<Characters>().GetHit(new HitInstance());
        }
    }
}
