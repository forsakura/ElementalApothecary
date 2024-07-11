using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : EnemyBase
{
    [HideInInspector]
    float attackDuration;
    [HideInInspector]
    float attackSpan;
    [SerializeField]
    float silkingSpan;
    [SerializeField]
    [Tooltip("�ż���ʱԶ����ҵľ���")]
    float silkingDistance;

    bool canAttack = true;
    bool finishAttack;
    bool canSilking = true;
    bool finishSilking;
    float publicTimer;

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
            .AddLeavingCondition("Silking", () => (GetDistanceWithPlayer() < silkingDistance) && canSilking);
        sm.GetState("Walk")
            .AddLeavingCondition("Silking", () => (GetDistanceWithPlayer() < silkingDistance) && canSilking);
        // ���������ײ�˺��Ļ��Ͳ�����������ˣ���ô����Ԫ�ز�֪��������Attack�ű�
        //sm.AddState("Attack", new State()
        //    .SetEnter(AttackEnter)
        //    .SetStay(AttackStay)
        //    .SetExit(AttackExit)
        //    .AddLeavingCondition("Idle", () => finishAttack)
        //);
        sm.AddState("Silking", new State()
            .SetEnter(SilkingEnter)
            .SetStay(SilkingStay)
            .SetExit(SilkingExit)
            .AddLeavingCondition("Idle", () => finishSilking)
        );
    }

    void AttackEnter()
    {
        finishAttack = false;
        publicTimer = 0.0f;
    }

    void AttackStay()
    {
        publicTimer += Time.deltaTime;
        if (publicTimer > attackDuration)
        {
            finishAttack = true;
        }
    }

    void AttackExit()
    {
        canAttack = false;
        StartCoroutine(AttackTimer());
    }

    float timeRecording;
    bool haveRecording;

    void SilkingEnter()
    {
        Debug.Log("SilkingEnter");
        // ����Ҹ���ʱ��ҪԶ�����
        publicTimer = 0.0f;
        timeRecording = 0.0f;
        haveRecording = false;
        finishSilking = false;
        backDirection = (transform.position - player.position).normalized;
        if (backDirection == Vector2.zero)
        {
            backDirection = Random.insideUnitCircle.normalized;
        }
    }

    void SilkingStay()
    {
        // ���ˣ�Ȼ����˿���޶������ʱ������ֹ���ϰ��ﵲסһֱ����
        publicTimer += Time.deltaTime;
        if (publicTimer < 3.0f && GetDistanceWithPlayer() < silkingDistance)
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
            finishSilking = true;
        }
    }

    void SilkingExit()
    {
        rb.velocity = Vector2.zero;
        GameObject gossamer = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Enemys/projectile/gossamer"), transform.position, new Quaternion());
        gossamer.GetComponent<FlyInLine>().SetDirection(player.position);
        canSilking = false;
        StartCoroutine(SilkingTimer());
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

    IEnumerator SilkingTimer()
    {
        float t = 0.0f;
        while (t < silkingSpan)
        {
            t += Time.deltaTime;
            yield return null;
        }
        canSilking = true;
    }
}
