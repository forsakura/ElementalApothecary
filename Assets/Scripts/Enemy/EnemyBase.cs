using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int EnemyID;
    public Transform player;
    [HideInInspector]public Character character;
    public Animator animator;
    [SerializeField]
    float tauntDiatance;
    [SerializeField]
    float timeToRemoveTaunt;

    [Header("Concat")]
    [SerializeField]
    public ContactFilter2D contactFilter;

    [Header("Drop Material After Death")]
    [SerializeField]
    int dropMaterialID;
    [SerializeField]
    int materialCountMin;
    [SerializeField]
    int materialCountMax;
    // 如史莱姆需要小于0.1，以便于对玩家造成碰撞伤害。
    // 如蜘蛛，可以近战攻击则只需要到达附近约1.0f的距离即可开始近战。
    [SerializeField]
    [Tooltip("判定为到达玩家身边时与玩家的距离")]
    public float distanceWhenReach;

    public EnemyData enemyData;

    [Header("Check State In Run")]
    float timer = 0.0f;
    public bool getTaunt = false;
    public bool reachTarget = false;
    public bool walkFinish;
    public bool getHit = false;
    public float walkSpan;
    public float currentWalkTime;
    public Vector2 walkDirection;

    [HideInInspector]
    public StateMachine sm;
    [HideInInspector]
    public Vector2 origin;
    [HideInInspector]
    public Vector2 target;
    [HideInInspector]
    public AIPath aiPath;
    [HideInInspector]
    public Rigidbody2D rb;

    protected virtual void Awake()
    {
        
        character = GetComponent<Character>();
        character.element = GetComponent<Element>();
        sm = new StateMachine();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        aiPath = GetComponent<AIPath>();
        character.OnGetDamage.AddListener(GetDamageFromPlayer);
        aiPath.maxSpeed = character.walkSpeed;
        InitStateMachine();
    }
    private void Start() 
    {
        // 受到伤害就选择玩家作为目标，超级背锅侠
        player = SceneManger.Instance.playerTrans;
        GetComponent<AIDestinationSetter>().target = player;
    }
    protected virtual void Update()
    {
        character.TriggerInvulnerable();
        // 在距离内，始终保持仇恨
        float dist = GetDistanceWithPlayer();
        if (dist < distanceWhenReach)
        {
            reachTarget = true;
        }
        else
        {
            reachTarget = false;
        }
        if (dist < tauntDiatance)
        {
            getTaunt = true;
            timer = 0.0f;
        }
        // 不在范围，如果有仇恨，计时器加时间，超过时间，去除仇恨，计时清零
        else if (getTaunt)
        {
            timer += Time.deltaTime;
            if (timer > timeToRemoveTaunt)
            {
                getTaunt = false;
                timer = 0.0f;
            }
        }
        sm.Excute();
    }

    public virtual void InitStateMachine()
    {
        sm.AddState("Idle", new State()
            .SetEnter(IdleEnter)
            .SetStay(() => { walkSpan -= Time.deltaTime; })
            .AddLeavingCondition("Walk", () => (walkSpan <= 0.0f) && !reachTarget)
            .AddLeavingCondition("Death", () => character.isDead)
        );
        sm.AddState("Walk", new State()
            .SetEnter(WalkEnter)
            .SetStay(WalkStay)
            .SetExit(WalkExit)
            .AddLeavingCondition("Idle", () => walkFinish)
            .AddLeavingCondition("Death", () => character.isDead)
        );
        sm.AddState("Death", new State()
            .SetEnter(DeathEnter)
            .SetStay(DeathStay)
        );
        sm.SetInitState("Idle");
    }

    protected virtual void IdleEnter()
    {
        walkSpan = 1.0f;
        aiPath.canMove = false;
        if (!getTaunt)
        {
            walkSpan = enemyData.SpanBetweenWalk + Random.Range(-0.5f, 0.5f);
        }
    }

    protected virtual void WalkEnter()
    {
        //Debug.Log("Start Walk.");
        walkFinish = false;
        currentWalkTime = 0.0f;
        origin = transform.position;
        if (getTaunt)
        {
            // �ƶ��ٶ�
            aiPath.canMove = true;
            // target = origin + (playerPosition - origin).normalized * enemyData.WalkMaxDistance;
        }
        else
        {
            // 把移动从Lerp改成方向，防止误触障碍物之后接着穿过去
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
    }

    protected virtual void WalkStay()
    {
        //Debug.Log("WalkStay");
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
        else
        {
            rb.velocity = Vector2.zero;
            aiPath.canMove = true;
            if (reachTarget)
            {
                walkFinish = true;
            }
        }
    }

    protected virtual void WalkExit()
    {
        rb.velocity = Vector2.zero;
        aiPath.canMove = false;
    }

    protected virtual void DeathEnter()
    {

    }

    protected virtual void DeathStay()
    {
        // 播放死亡动画

        // 死亡
        Die();
    }

    private void GetDamageFromPlayer(Transform attacker)
    {
        // 受到攻击刷新仇恨
        getTaunt = true;
        timer = 0.0f;
        getHit = true;
    }

    // 检查玩家是否在仇恨范围
    public float GetDistanceWithPlayer()
    {
        return (player.position - transform.position).magnitude;
    }

    public void Die()
    {
        GameObject material = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/GroundMaterial/MaterialOnGround"), (Vector2)transform.position + Random.insideUnitCircle, new Quaternion());
        material.GetComponent<MaterialOnGround>().InitMaterial(dropMaterialID, Random.Range(materialCountMin, materialCountMax));
        Destroy(gameObject);
    }
}
