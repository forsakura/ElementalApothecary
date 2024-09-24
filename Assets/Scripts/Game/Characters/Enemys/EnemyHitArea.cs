using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyHitArea : HitArea
    {
        EnemyBase enemyParent;

        protected override void Start()
        {
            base.Start();
            enemyParent = parent as EnemyBase;
        }

        // Åö×²ÉËº¦
        protected virtual void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.transform.parent == null)
            {
                return;
            }
            if (collision.transform.parent.CompareTag("Player"))
            {
                enemyParent.InitHit();
                collision.gameObject.GetComponent<HitArea>().GetHit(enemyParent.enemyHit);
            }
        }

        public override bool GetHit(HitInstance hit)
        {
            Characters characters = hit.Source.GetComponent<Characters>();
            if (characters != null)
            {
                enemyParent.HaveTaunt(characters);
            }
            return base.GetHit(hit);
        }
    }
}
