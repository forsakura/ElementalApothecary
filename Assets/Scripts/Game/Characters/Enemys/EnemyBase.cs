using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterDelegates;

namespace Enemy
{
    public abstract class EnemyBase : Characters
    {
        public GameObject target;

        public event OnEnemyTargetSetEventHandler OnTargetSet;

        public bool getTaunt = false;
        public float tauntTime = 10.0f;

        Coroutine taunt;

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

        public virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                HaveTaunt(collision.GetComponent<Characters>());
            }
        }

        public virtual void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                LoseTaunt();
            }
        }

        public virtual void HaveTaunt(Characters character, HitInstance hitInstance)
        {
            if (character == null || !character.CompareTag("Player"))
            {
                return;
            }
            HaveTaunt(character);
        }

        public virtual void HaveTaunt(Characters character)
        {
            if (character != null)
            {
                getTaunt = true;
                target = character.gameObject;
                if (taunt != null)
                {
                    StopCoroutine(taunt);
                    taunt = null;
                }
            }
        }

        public virtual void LoseTaunt()
        {
            if (taunt != null)
            {
                StopCoroutine(taunt);
            }
            taunt = StartCoroutine(TauntCountDown());
        }

        public IEnumerator TauntCountDown()
        {
            float timer = tauntTime;
            while (timer > 0.0f)
            {
                timer -= Time.deltaTime;
                yield return null;
            }
            getTaunt = false;
            taunt = null;
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