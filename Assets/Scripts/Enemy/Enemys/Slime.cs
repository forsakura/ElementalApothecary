using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyBase
{
    [Header("Skill Parameter")]
    [SerializeField]
    float dashSpeed;
    [SerializeField]
    float dashDuration;
    [SerializeField]
    float dashCoolDownTime;

    bool canDash = true;
    bool dashFinish;
    bool hitAnimeFinish;
    float dashTimer;
    float getHitTimer;
    float deathTimer = 0.0f;
    bool haveTauntThisWalk;

    Vector2 dashDirection;

    float previousX;
    float currentX;

    int dir;

    protected override void Awake()
    {
        base.Awake();
        currentX = transform.position.x;
    }

    protected override void Update()
    {
        base.Update();
        previousX = currentX;
        currentX = transform.position.x;
        if (Mathf.Abs(currentX - previousX) > 0.01f)
        {
            dir = (currentX - previousX) > 0 ? 1 : -1;
        }
        animator.SetFloat("Direction", dir);
        //Debug.Log(sm.GetCurrentState().Name);
    }


    public override void InitStateMachine()
    {
        sm.AddState("Idle", new State()
            .SetEnter(IdleEnter)
            .SetStay(() => { walkSpan -= Time.deltaTime; })
            .AddLeavingCondition("Death", () => character.isDead)
            .AddLeavingCondition("GetHit", () => getHit)
            .AddLeavingCondition("Walk", () => (walkSpan <= 0.0f) && !reachTarget) // ��Ŀ���Ա߾ͱ����ˣ�
            .AddLeavingCondition("Dash", () => getTaunt && canDash && GetDistanceWithPlayer() > 1.5f)
        );
        sm.AddState("Walk", new State()
            .SetEnter(WalkEnter)
            .SetStay(WalkStay)
            .SetExit(WalkExit)
            .AddLeavingCondition("Death", () => character.isDead)
            .AddLeavingCondition("GetHit", () => getHit)
            .AddLeavingCondition("Idle", () => walkFinish)
        );
        sm.AddState("Dash", new State()
            .SetEnter(DashEnter)
            .SetStay(DashStay)
            .SetExit(DashExit)
            .AddLeavingCondition("Death", () => character.isDead)
            .AddLeavingCondition("GetHit", () => getHit)
            .AddLeavingCondition("Idle", () => dashFinish)
        );
        sm.AddState("GetHit", new State()
            .SetEnter(GetHitEnter)
            .SetStay(GetHitCountDown)
            .AddLeavingCondition("Idle", () => hitAnimeFinish)
        );
        sm.AddState("Death", new State()
            .SetEnter(DeathEnter)
            .SetStay(DeathStay)
        );
        sm.SetInitState("Idle");
    }

    protected override void IdleEnter()
    {
        aiPath.canMove = false;
        aiPath.maxSpeed = 0.0f;
        if (getTaunt)
        {
            walkSpan = 0.5f;
        }
        else
        {
            walkSpan = enemyData.SpanBetweenWalk + Random.Range(-0.5f, 0.5f);
        }
    }

    protected override void WalkEnter()
    {
        Debug.Log("Start Walk.");
        walkFinish = false;
        currentWalkTime = 0.0f;
        origin = transform.position;
        if (getTaunt)
        {
            haveTauntThisWalk = true;
            // �ƶ��ٶ�
            aiPath.canMove = true;
            aiPath.maxSpeed = character.walkSpeed;
            // target = origin + (playerPosition - origin).normalized * enemyData.WalkMaxDistance;
        }
        else
        {
            haveTauntThisWalk = false;

            int targetNumbers;
            RaycastHit2D[] hit2D = new RaycastHit2D[5];
            do
            {
                walkDirection = Random.insideUnitCircle;
                // target = (Vector2)transform.position + Random.insideUnitCircle * enemyData.WalkMaxDistance;
                targetNumbers = Physics2D.Raycast(transform.position, walkDirection, contactFilter, hit2D, enemyData.WalkMaxDistance * walkDirection.magnitude);
                Debug.Log(targetNumbers);
            } while (targetNumbers != 0);
        }
        animator.SetTrigger("Walk");
    }

    protected override void WalkStay()
    {
        currentWalkTime += Time.deltaTime;
        if (!haveTauntThisWalk)
        {
            rb.velocity = walkDirection * character.walkSpeed;
            // transform.position = Vector2.Lerp(origin, target, currentWalkTime / enemyData.WalkDuration);
        }

        if (currentWalkTime > enemyData.WalkDuration)
        {
            walkFinish = true;
        }
    }

    protected override void WalkExit()
    {
        rb.velocity = Vector2.zero;
        aiPath.canMove = false;
        aiPath.maxSpeed = 0.0f;
    }

    void DashEnter()
    {
        dashFinish = false;
        dashTimer = 0.0f;
        origin = transform.position;
        dashDirection = ((Vector2)player.position - origin).normalized;
        // target = origin + ((Vector2)player.position - origin).normalized * dashDistance;
        // 水平速度快一点的跳跃
        animator.SetTrigger("Walk");
    }

    void DashStay()
    {
        Debug.Log("Start dash");
        dashTimer += Time.deltaTime;
        // preparation
        //if (dashTimer < 0.5f)
        //{
        //    // �˴�Ӧ��ѹ����������������
        //}
        //else if (dashTimer < 0.5f + dashDuration)
        if (dashTimer < dashDuration)
        {
            rb.velocity = dashDirection * dashSpeed;
            // transform.position = Vector2.Lerp(origin, target, (dashTimer - 0.5f) / dashDuration);
        }
        else
        {
            // ��̽����������оͲ��ţ�û�о�����
            // transform.position = target;
            dashFinish = true;
        }
    }

    void DashExit()
    {
        rb.velocity = Vector2.zero;
        canDash = false;
        StartCoroutine(DashCoolDown());
    }

    void GetHitEnter()
    {
        aiPath.canMove = false;
        rb.velocity = Vector2.zero;
        hitAnimeFinish = false;
        getHitTimer = 0.0f;
        getHit = false;
        animator.SetTrigger("GetHit");
    }

    void GetHitCountDown()
    {
        getHitTimer += Time.deltaTime;
        if (getHitTimer > 1.0f)
        {
            hitAnimeFinish = true;
        }
    }

    protected override void DeathEnter()
    {
        aiPath.canMove = false;
        rb.velocity = Vector2.zero;
        deathTimer = 0.0f;
        animator.SetTrigger("Death");
    }

    protected override void DeathStay()
    {
        deathTimer += Time.deltaTime;
        if (deathTimer > 2.0f)
        {
            Die();
        }
    }

    IEnumerator DashCoolDown()
    {
        float t = 0.0f;

        while (t < dashCoolDownTime)
        {
            t += Time.deltaTime;
            yield return null;
        }

        canDash = true;
    }
}
