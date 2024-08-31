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

        MushroomAnimation anim;
        Rigidbody2D rb;
        Coroutine moveCoroutine;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<MushroomAnimation>();
            rb = GetComponent<Rigidbody2D>();
            CurrentSpeed = 0.0f;
            AfterGetHit += ThresholdCheck;
        }

        protected override void InitHit()
        {
            base.InitHit();
            enemyHit.ElementName[1] = EElement.Terra;
            enemyHit.ElementContain[1] = 10;
        }

        private void ThresholdCheck(Characters character, HitInstance hit)
        {
            if((1.0f * CurrentHealth / characterData.MaxHealth) < threshold)
            {
                // Start animation;
                anim.FSM.ChangeStateTo("CreateSpore");

                CreateSporeArea();
                anim.FinishCreateSpore();

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
                    mushroom.AssembleTo(gameObject);
                }
            }
        }

        public void AssembleTo(GameObject target)
        {
            this.target = target;
            CurrentSpeed = characterData.MoveSpeed;
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
                    rb.velocity = CurrentSpeed * ((Vector2)next - new Vector2(transform.position.x, transform.position.y)).normalized;
                }
                else
                {
                    rb.velocity = new Vector2();
                    moveCoroutine = null;
                    yield break;
                }
                yield return null;
            }
            rb.velocity = new Vector2();
            moveCoroutine = null;
        }
    }
} 

