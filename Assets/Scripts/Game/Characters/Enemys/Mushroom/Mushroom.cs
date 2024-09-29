using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Mushroom
{
    public class Mushroom : EnemyBase
    {
        [Header("SpecialPropertyOfMushroom")]
        [SerializeField]
        private float threshold;
        [SerializeField]
        private float announceDistanceLimit;

        MushroomAnimation anim;
        Rigidbody2D rb;
        Coroutine moveCoroutine;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<MushroomAnimation>();
            rb = GetComponent<Rigidbody2D>();
            currentSpeed = 0.0f;
            AfterGetHit += ThresholdCheck;
        }

        public override void InitHit()
        {
            base.InitHit();
            enemyHit.elementState.elementVector = new Vector2(-10, 0);
            //enemyHit.ElementName[1] = EElement.Terra;
            //enemyHit.ElementContain[1] = 10;
        }

        private void ThresholdCheck(Characters character, HitInstance hit)
        {
            if((1.0f * currentHealth / characterData.maxHealth) < threshold)
            {
                // Start animation;
                anim.FSM.ChangeStateTo("CreateSpore");
                anim.CreateArea();
                if (moveCoroutine != null)
                {
                    StopCoroutine(moveCoroutine);
                    moveCoroutine = null;
                }
                rb.velocity = Vector2.zero;

                target = gameObject;
                AfterGetHit -= ThresholdCheck;
            }
        }

        public void CreateSporeArea()
        {
            // Create spore area.
            Debug.Log("Create spore area.");
            List<Characters> mushrooms = CharacterManager.Instance.FindCharacters(characterData.ID);
            foreach (Characters item in mushrooms)
            {
                Mushroom mushroom = item as Mushroom;
                if (mushroom != this)
                {
                    if((mushroom.transform.position - transform.position).magnitude > announceDistanceLimit)
                    {
                        continue;
                    }
                    mushroom.AssembleTo(gameObject);
                }
            }
        }

        public void AssembleTo(GameObject target)
        {
            this.target = target;
            currentSpeed = characterData.moveSpeed;
            // Start Move FSM
            Debug.Log(moveCoroutine == null);
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
                moveCoroutine = null;
            }
            moveCoroutine = StartCoroutine(Move(target.transform.position));
        }

        IEnumerator Move(Vector2 target)
        {
            while((target - (Vector2)transform.position).magnitude > 1.0f)
            {
                if (anim.FSM.GetCurrentState().Name == "CreateSpore")
                {
                    yield return null;
                    continue;
                }
                Vector2? next = AStarPathFinding.AStarManager.Instance.GetNext(new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y)), new Vector2Int(Mathf.FloorToInt(target.x), Mathf.FloorToInt(target.y)));
                if (next != null)
                {
                    rb.velocity = currentSpeed * ((Vector2)next - new Vector2(transform.position.x, transform.position.y)).normalized;
                }
                else
                {
                    rb.velocity = Vector2.zero;
                    moveCoroutine = null;
                    yield break;
                }
                yield return null;
            }
            rb.velocity = Vector2.zero;
            moveCoroutine = null;
        }
    }
} 

