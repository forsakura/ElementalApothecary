using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : EnemyBase
{
    [SerializeField]
    float attackSpan;
    [SerializeField]
    [Tooltip("�ż���ʱԶ����ҵľ���")]
    float attackDistance;

    bool canAttack = true;
    bool finishAttack;
    bool hitAnimeFinish;
    float publicTimer;
    float getHitTimer;
    float deathTimer;

    Vector2 backDirection;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void InitStateMachine()
    {
        base.InitStateMachine();
        sm.GetState("Idle")
            // .AddLeavingCondition("Attack", () => reachTarget && canAttack)
            .AddLeavingCondition("GetHit", () => getHit)
            .AddLeavingCondition("Attack", () => (GetDistanceWithPlayer() < attackDistance) && canAttack);
        sm.GetState("Walk")
            .AddLeavingCondition("GetHit", () => getHit)
            .AddLeavingCondition("Attack", () => (GetDistanceWithPlayer() < attackDistance) && canAttack);
        sm.AddState("Attack", new State()
            .SetEnter(AttackEnter)
            .SetStay(AttackStay)
            .SetExit(AttackExit)
            .AddLeavingCondition("GetHit", () => getHit)
            .AddLeavingCondition("Idle", () => finishAttack)
        );
        sm.AddState("GetHit", new State()
            .SetEnter(GetHitEnter)
            .SetStay(GetHitCountDown)
            .AddLeavingCondition("Idle", () => hitAnimeFinish)
        );
    }

    protected override void WalkStay()
    {
        currentWalkTime += Time.deltaTime;
        if (!getTaunt)
        {
            aiPath.canMove = false;
            rb.velocity = walkDirection * character.walkSpeed;
            // transform.position = Vector2.Lerp(origin, target, currentWalkTime / enemyData.WalkDuration);

            if (currentWalkTime > enemyData.WalkDuration)
            {
                walkFinish = true;
            }
        }
        // ̫����Զ��
        else if (GetDistanceWithPlayer() < distanceWhenReach - 1.0f)
        {
            aiPath.canMove = false;
            backDirection = (transform.position - player.position).normalized;
            if (backDirection == Vector2.zero)
            {
                backDirection = Random.insideUnitCircle.normalized;
            }
            rb.velocity = backDirection * character.walkSpeed;
        }
        // ̫Զ�˿���
        else if (!reachTarget)
        {
            aiPath.canMove = true;
        }
        else
        {
            walkFinish = true;
        }
    }

    protected override void DeathEnter()
    {
        StopAllCoroutines();
        aiPath.canMove = false;
        rb.velocity = Vector2.zero;
        deathTimer = 0.0f;
        animator.SetTrigger("Death");
    }

    protected override void DeathStay()
    {
        deathTimer += Time.deltaTime;
        if (deathTimer > 1.7f)
        {
            Die();
        }
    }

    float timeRecording;
    bool haveRecording;

    void AttackEnter()
    {
        Debug.Log("AttackEnter");
        // ����Ҹ���ʱ��ҪԶ�����
        publicTimer = 0.0f;
        timeRecording = 0.0f;
        haveRecording = false;
        finishAttack = false;
        backDirection = (transform.position - player.position).normalized;
        if (backDirection == Vector2.zero)
        {
            backDirection = Random.insideUnitCircle.normalized;
        }
    }

    void AttackStay()
    {
        // ���ˣ�Ȼ����˿���޶������ʱ������ֹ���ϰ��ﵲסһֱ����
        publicTimer += Time.deltaTime;
        if (publicTimer < 3.0f && GetDistanceWithPlayer() < attackDistance && !haveRecording)
        {
            rb.velocity = backDirection * character.walkSpeed;
        }
        else if (!haveRecording)
        {
            timeRecording = publicTimer;
            haveRecording = true;
        }
        else if (publicTimer - timeRecording > 0.5f)
        {
            finishAttack = true;
        }
    }

    void AttackExit()
    {
        animator.SetTrigger("Attack");
        rb.velocity = Vector2.zero;
        float direction = (player.transform.position - transform.position).x > 0 ? -1 : 1;
        GameObject spike = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Enemys/projectile/Spike"), transform.position, Quaternion.Euler(0, 0, Vector3.Angle(Vector3.up, player.transform.position - transform.position) * direction));
        Debug.Log("Rotation:" + Vector3.Angle(Vector3.right, player.transform.position - transform.position));
        spike.GetComponent<FlyInLine>().SetDirection(player.position);
        canAttack = false;
        StartCoroutine(AttackTimer());
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
        if (getHitTimer > 0.5f)
        {
            hitAnimeFinish = true;
        }
    }

    IEnumerator AttackTimer()
    {
        float t = 0.0f;
        while (t < attackSpan)
        {
            t += Time.deltaTime;
            yield return null;
        }
        canAttack = true;
    }
}
