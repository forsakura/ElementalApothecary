using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public abstract class EnemyBase : Characters
    {
        [SerializeField]
        protected GameObject target;

        // ��ײ�˺���������
        protected HitInstance enemyHit = new();

        protected virtual void InitHit()
        {
            enemyHit.Source = gameObject;
            enemyHit.Damage = characterData.Damage;
        }

        // ��ײ�˺�
        protected virtual void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag.Equals("Player"))
            {
                InitHit();
                collision.gameObject.GetComponent<Characters>().GetHit(enemyHit);
            }
        }
    }
}