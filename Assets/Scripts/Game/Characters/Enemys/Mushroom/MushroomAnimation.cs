using Enemy.Mushroom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Mushroom
{
    public class MushroomAnimation : MonoBehaviour
    {
        public StateMachine FSM;
        Animator animator;
        Rigidbody2D rb;
        Mushroom mushroom;

        float dir = 1.0f;
        bool finishCreateSpore = false;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            mushroom = GetComponent<Mushroom>();
            InitFSM();
        }

        // Update is called once per frame
        void Update()
        {
            animator.SetFloat("Speed", rb.velocity.magnitude);
            if (rb.velocity.x > 0.0f)
            {
                dir = 1.0f;
            }
            else if (rb.velocity.x < 0.0f)
            {
                dir = -1.0f;
            }
            animator.SetFloat("Direction", dir);
            FSM.Excute();
        }

        private void InitFSM()
        {
            FSM = new();
            FSM.AddState("Idle", new State()
                .AddLeavingCondition("Move", () =>
                {
                    return rb.velocity.magnitude > 0.01;
                })
                .AddLeavingCondition("Death", () =>
                {
                    return mushroom.currentHealth < 1;
                })
            );

            FSM.AddState("Move", new State()
                .AddLeavingCondition("Idle", () =>
                {
                    return rb.velocity.magnitude < 0.01;
                })
                .AddLeavingCondition("Death", () =>
                {
                    return mushroom.currentHealth < 1;
                })
            );

            FSM.AddState("Death", new State()
                .SetEnter(() =>
                {
                    mushroom.currentSpeed = 0;
                    animator.SetTrigger("Dead");
                })
            );

            FSM.AddState("CreateSpore", new State()
                .AddLeavingCondition("Idle", () =>
                {
                    return finishCreateSpore && rb.velocity.magnitude < 0.01;
                })
                .AddLeavingCondition("Move", () =>
                {
                    return finishCreateSpore && rb.velocity.magnitude > 0.01;
                })
                .AddLeavingCondition("Death", () =>
                {
                    return mushroom.currentHealth < 1;
                })
            );

            FSM.SetInitState("Idle");
            FSM.EnterStateMachine();
        }

        public void CreateArea()
        {
            animator.SetTrigger("CreateArea");
        }

        public void FinishCreateSpore()
        {
            finishCreateSpore = true;
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}

