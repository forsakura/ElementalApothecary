using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Enemy.JungleKid
{
    public class JKMain : EnemyBase
    {
        public Rigidbody2D rb;
        public JKAnimation anim;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<JKAnimation>();
        }

        private void Update()
        {
            
        }

        public void Attack()
        {
            Shoot(target.transform.position);
        }

        public override void OnShootHitTarget(BulletControl bullet, Characters go)
        {
            if (go.transform.parent == null || !go.transform.parent.CompareTag("Player"))
            {
                return;
            }
            InitHit();
            go.GetComponent<HitArea>().GetHit(enemyHit);
            Destroy(bullet.gameObject);
        }
    }
}