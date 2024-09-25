using Enemy.Frog;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

namespace Enemy.JungleKid
{
    public class JKAnimation : MonoBehaviour
    {
        public JKMain jk;

        public Rigidbody2D rb;
        public ContactFilter2D contactFilter;
        public Animator anime;

        float dir = 1.0f;
        private Vector2 speedDir = Vector2.zero;

        StateMachine currentFSM;
        StateMachine commonFSM;
        StateMachine followFSM;

        bool canAttack = true;
        float attackTimer = 0.0f;
        float timer = 0.0f;

        private void Start()
        {
            jk = GetComponent<JKMain>();
            rb = GetComponent<Rigidbody2D>();
            anime = GetComponent<Animator>();
            InitFSM();
        }

        private void Update()
        {
            anime.SetBool("GetTaunt", jk.getTaunt);
            if (jk.getTaunt)
            {
                if(currentFSM == commonFSM)
                {
                    currentFSM.ExitStateMachine();
                    currentFSM = followFSM;
                    currentFSM.EnterStateMachine();
                }
            }
            else
            {
                if (currentFSM == followFSM)
                {
                    currentFSM.ExitStateMachine();
                    currentFSM = commonFSM;
                    currentFSM.EnterStateMachine();
                }
            }
            anime.SetFloat("Speed", rb.velocity.magnitude);
            if (rb.velocity.x > 0.0f)
            {
                dir = 1.0f;
            }
            else if (rb.velocity.x < 0.0f)
            {
                dir = -1.0f;
            }
            anime.SetFloat("Direction", dir);

            currentFSM.Excute();
            Debug.Log(currentFSM.GetCurrentState().Name);
        }

        private void FixedUpdate()
        {
            rb.velocity = speedDir * jk.CurrentSpeed;
        }

        private void InitFSM()
        {
            commonFSM = new StateMachine();
            followFSM = new StateMachine();

            commonFSM.AddState("Idle", new State()
                .SetEnter(CommonIdleStart)
                .SetStay(CountDown)
                .AddLeavingCondition("Move", () => timer < 0)
            );
            commonFSM.AddState("Move", new State()
                .SetEnter(RandomMove)
                .SetStay(CountDown)
                .SetExit(MoveExit)
                .AddLeavingCondition("Idle", () => timer < 0)
            );
            commonFSM.SetInitState("Idle");

            followFSM.AddState("Idle", new State()
                .AddLeavingCondition("Attack", () => canAttack && (jk.target.transform.position - transform.position).magnitude < jk.characterData.AttackDistance)
                .AddLeavingCondition("Move", () => (jk.target.transform.position - transform.position).magnitude < jk.characterData.AttackDistance - 2 || (jk.target.transform.position - transform.position).magnitude > jk.characterData.AttackDistance)
            );
            followFSM.AddState("Move", new State()
                // .SetEnter(FollowMove)
                .SetEnter(() =>
                {
                    timer = 5.0f;
                })
                .SetStay(() => 
                {
                    FollowMove();
                    CountDown();
                })
                .SetExit(MoveExit)
                .AddLeavingCondition("Attack", () => canAttack && (jk.target.transform.position - transform.position).magnitude < jk.characterData.AttackDistance)
                .AddLeavingCondition("Idle", () => ((jk.target.transform.position - transform.position).magnitude > jk.characterData.AttackDistance - 2 && (jk.target.transform.position - transform.position).magnitude < jk.characterData.AttackDistance) || timer < 0)
            );
            followFSM.AddState("Attack", new State()
                .SetEnter(() => 
                { 
                    anime.SetTrigger("Attack");
                    canAttack = false;
                    StartCoroutine(AttackCountDown());
                })    
            );
            followFSM.SetInitState("Idle");

            currentFSM = commonFSM;
            currentFSM.EnterStateMachine();
        }

        private void CountDown()
        {
            timer -= Time.deltaTime;
        }

        // 放在移动结尾
        private void CommonIdleStart()
        {
            timer = Random.Range(3.0f, 5.0f);
        }

        private void FollowMove()
        {
            if ((jk.target.transform.position - transform.position).magnitude < jk.characterData.AttackDistance - 1)
            {
                Debug.Log("Close.");
                int targetNumbers;
                RaycastHit2D[] hit2D = new RaycastHit2D[5];
                int count = 0;
                Vector2 dir = (transform.position - jk.target.transform.position).normalized;
                do
                {
                    // target = (Vector2)transform.position + Random.insideUnitCircle * enemyData.WalkMaxDistance;
                    
                    targetNumbers = Physics2D.Raycast(transform.position, Quaternion.Euler(0, 0, 30 * count) * dir, contactFilter, hit2D, 1.5f * jk.CurrentSpeed);
                    // Debug.Log(targetNumbers);
                    count++;
                } while (targetNumbers != 0 && count < 12);
                if (count >= 12)
                {
                    speedDir = Vector2.zero;
                }
                else
                {
                    speedDir = (Quaternion.Euler(0, 0, 30 * count) * dir).normalized;
                }
            }
            else if ((jk.target.transform.position - transform.position).magnitude > jk.characterData.AttackDistance)
            {
                Debug.Log("Far.");
                Vector2? next = AStarPathFinding.AStarManager.Instance.GetNext(new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y)), new Vector2Int(Mathf.FloorToInt(jk.target.transform.position.x), Mathf.FloorToInt(jk.target.transform.position.y)));
                if (next != null)
                {
                    speedDir = ((Vector2)next - new Vector2(transform.position.x, transform.position.y)).normalized;
                }
                else
                {
                    speedDir = Vector2.zero;
                }
            }
        }

        private void RandomMove()
        {
            int targetNumbers;
            RaycastHit2D[] hit2D = new RaycastHit2D[5];
            speedDir = Vector2.zero;
            int count = 0;
            do
            {
                speedDir = Random.insideUnitCircle;
                // target = (Vector2)transform.position + Random.insideUnitCircle * enemyData.WalkMaxDistance;
                targetNumbers = Physics2D.Raycast(transform.position, speedDir, contactFilter, hit2D, 1.5f * jk.CurrentSpeed);
                // Debug.Log(targetNumbers);
                count++;
            } while (targetNumbers != 0 && count < 10);
            timer = 1.5f;
            //speedDir = Random.insideUnitCircle.normalized;
        }

        private void MoveExit()
        {
            speedDir = Vector2.zero;
        }

        private void LeaveAttackState()
        {
            currentFSM.ChangeStateTo("Idle");
        }

        IEnumerator AttackCountDown()
        {
            attackTimer = jk.characterData.AttackInterval;
            while(attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
                yield return null;
            }
            canAttack = true;
        }
    }
}
