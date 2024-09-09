using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Frog
{
    public class Frog : EnemyBase
    {

        public bool getTaunt = false;
        public const float tauntTime = 10.0f;

        Coroutine taunt;

        // Start is called before the first frame update
        void Start()
        {
            AfterGetHit += HaveTaunt;
        }

        // Update is called once per frame
        void Update()
        {
            if (isDead)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                HaveTaunt(collision.GetComponent<Characters>());
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                LoseTaunt();
            }
        }

        private void HaveTaunt(Characters character, HitInstance hitInstance)
        {
            if (character == null || !character.CompareTag("Player"))
            {
                return;
            }
            HaveTaunt(character);
        }

        private void HaveTaunt(Characters character)
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

        private void LoseTaunt()
        {
            if (taunt != null)
            {
                StopCoroutine(taunt);
            }
            taunt = StartCoroutine(TauntCountDown());
        }

        private IEnumerator TauntCountDown()
        {
            float timer = 0.0f;
            if (timer < tauntTime)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            getTaunt = false;
            taunt = null;
        }
    }
}