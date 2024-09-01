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

        public override void OnBulletHitTarget(BulletControl bullet, Collider2D collision)
        {
            if (collision.transform.parent == null || !collision.transform.parent.CompareTag("Player"))
            {
                return;
            }
            InitHit();
            collision.GetComponent<HitArea>().GetHit(enemyHit);
            Destroy(bullet.gameObject);
        }
    }
}