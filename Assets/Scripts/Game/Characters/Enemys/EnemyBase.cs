using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterDelegates;

namespace Enemy
{
    public abstract class EnemyBase : Characters
    {
        [SerializeField]
        public GameObject target;

        public event OnEnemyTargetSetEventHandler OnTargetSet;

        // ≈ˆ◊≤…À∫¶¥”’‚¿Ô¿¥
        public HitInstance enemyHit = new();

        public virtual void InitHit()
        {
            enemyHit.Source = gameObject;
            enemyHit.Damage = characterData.Damage;
        }

        public virtual void SetTarget(GameObject targetObj)
        {
            target = targetObj;
            OnTargetSet();
        }

        // ≈ˆ◊≤…À∫¶
        //protected virtual void OnTriggerStay2D(Collider2D collision)
        //{
        //    if (collision.transform.parent == null)
        //    {
        //        return;
        //    }
        //    if (collision.transform.parent.CompareTag("Player"))
        //    {
        //        InitHit();
        //        collision.gameObject.GetComponent<HitArea>().GetHit(enemyHit);
        //    }
        //}
    }
}