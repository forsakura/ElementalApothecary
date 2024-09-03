using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.JungleKid
{
    public class DetectArea : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player") || collision.transform.parent != null)
            {
                return;
            }
            EnemyBase enemyParent = GetComponentInParent<EnemyBase>();
            enemyParent.SetTarget(collision.gameObject);
        }
    }
}
