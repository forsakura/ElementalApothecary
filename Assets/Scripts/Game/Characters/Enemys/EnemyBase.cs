using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public abstract class EnemyBase : Characters
    {
        [SerializeField]
        protected GameObject target;

        // ≈ˆ◊≤…À∫¶¥”’‚¿Ô¿¥
        HitInstance enemyHit;

        protected virtual void InitHit()
        {
            enemyHit = new HitInstance();
            enemyHit.Source = gameObject;
            enemyHit.Damage = characterData.Damage;
        }

        // ≈ˆ◊≤…À∫¶
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