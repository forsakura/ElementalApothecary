using Enemy.Mushroom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAnimation : MonoBehaviour
{
    public StateMachine FSM;
    Animator animator;
    Rigidbody2D rb;
    Mushroom mushroom;

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
        
        FSM.Excute();
    }

    private void InitFSM()
    {
        FSM = new();
        FSM.AddState("Idle", new State()
            .SetStay(() =>
            {
                animator.SetFloat("Direction", rb.velocity.x);
                animator.SetFloat("Speed", rb.velocity.magnitude);
            })
            .AddLeavingCondition("Move", () =>
            {
                return rb.velocity.magnitude > 0.1;
            })
            .AddLeavingCondition("Death", () =>
            {
                return mushroom.CurrentHealth < 1;
            })
        );

        FSM.AddState("Move", new State()
            .SetStay(() =>
            {
                animator.SetFloat("Direction", rb.velocity.x);
                animator.SetFloat("Speed", rb.velocity.magnitude);
            })
            .AddLeavingCondition("Idle", () =>
            {
                return rb.velocity.magnitude < 0.1;
            })
            .AddLeavingCondition("Death", () =>
            {
                return mushroom.CurrentHealth < 1;
            })
        );

        FSM.AddState("Death", new State()
            .SetExit(() =>
            {
                Destroy(gameObject);
            })
        );

        FSM.AddState("CreateSpore", new State()
            .SetStay(() =>
            {
                animator.SetFloat("Direction", rb.velocity.x);
                animator.SetFloat("Speed", rb.velocity.magnitude);
            })
            .AddLeavingCondition("Idle", () =>
            {
                return finishCreateSpore && rb.velocity.magnitude < 0.1;
            })
            .AddLeavingCondition("Move", () =>
            {
                return finishCreateSpore && rb.velocity.magnitude > 0.1;
            })
            .AddLeavingCondition("Death", () =>
            {
                return mushroom.CurrentHealth < 1;
            })
        );

        FSM.SetInitState("Idle");
    }

    public void FinishCreateSpore()
    {
        finishCreateSpore = true;
    }
}
