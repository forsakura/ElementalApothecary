using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControll : MonoBehaviour
{
    public PlayerInputActions inputAction;
    [HideInInspector]public Rigidbody2D rb;
    [HideInInspector]public Character character;
    [HideInInspector]public Vector3 mousePosition;
    private PlayerAnimation playerAnimation;
    public Transform rightHand;
    
    [Space(10)]
    [Tooltip("玩家当前的状态")]public EPlayerBaseState currentBaseState;
    [Tooltip("玩家当前攻击状态")]public EPlayerAttackState currentAttackState;
    [Tooltip("元素失调")]public ElementalSickness elementalSickness;
    [Space(10)]
    [Tooltip("玩家输入方向")]public Vector2 inputDirection;
    [Space(10)]
    [Tooltip("点击时间间隔")]public float clickTimeInterval;
    private float lastClickTime;


    public bool useMedicine;
    public bool isFloting;
    private void Awake() 
    {
        inputAction = new PlayerInputActions();

        rb = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
        playerAnimation = GetComponent<PlayerAnimation>();

        inputAction.GamePlay.Shoot.started += OnFire;
        inputAction.GamePlay.Fill.started += OnFill;
        
        currentBaseState = EPlayerBaseState.Default;
    }

    

    private void OnEnable() 
    {
        inputAction.Enable();
    }
    private void OnDisable() 
    {
        inputAction.Disable();
    }

    private void Update() 
    {
        switch (currentAttackState)//喝药时无法移动
        {
            case EPlayerAttackState.Drinking:
                character.currentSpeed = 0;
                break;
        }

        SwitchState();

        Move();

        Density();
    }
    private void FixedUpdate() 
    {
        
    }

    public void Move()
    {
        inputDirection = inputAction.GamePlay.Move.ReadValue<Vector2>();

        if(inputDirection.magnitude > 0)
        {
            currentBaseState = EPlayerBaseState.Walking;

            if(isFloting)
            {
                currentBaseState = EPlayerBaseState.Floating;
            }
        }
        else
        {
            currentBaseState = EPlayerBaseState.Default;
        }
        rb.velocity = inputDirection*character.currentSpeed;   
    }

    public void SwitchState()//切换玩家当处于前的状态时的速度  须在Move函数之前执行
    {
        switch (currentBaseState)
        {
            case EPlayerBaseState.Default :
                character.currentSpeed = Mathf.Lerp(character.currentSpeed,0,5*Time.deltaTime);
                break;
            case EPlayerBaseState.Walking :
                character.currentSpeed = Mathf.Lerp(character.currentSpeed,character.walkSpeed,5*Time.deltaTime);
                break;
            case EPlayerBaseState.Floating :
                character.currentSpeed = Mathf.Lerp(character.currentSpeed,character.walkSpeed,5*Time.deltaTime);
                break;  
        }

    }
    
    public void Density()
    {
         float densityX = Mathf.Abs(character.element.currentElementCount.x) / character.characterRestriction.CharacterRestrictionEntity[character.currentLv].maxAerTerra;

        if(densityX <= 0.39f)
        {
            elementalSickness = ElementalSickness.Persist;
        }
        else if(densityX <= 0.69f)
        {
            elementalSickness = ElementalSickness.Concentrate;
        }
        else if(densityX <= 0.90f)
        {
            elementalSickness = ElementalSickness.Abundant;
        }
        else if(densityX <= 0.99f)
        {
            elementalSickness = ElementalSickness.Impulsive;
        }
        else
        {
            elementalSickness = ElementalSickness.Fatal;
            //character.OnDie?.Invoke();
        }
    }
#region 点击键盘相关
    
    private void OnFire(InputAction.CallbackContext context)
    {
        if(Time.time - lastClickTime >= clickTimeInterval && !useMedicine)
        {
            var pos = Mouse.current.position.ReadValue();
            mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, Camera.main.nearClipPlane));

            playerAnimation.PlayerAttack();
            
            lastClickTime = Time.time;
            
        }

        if(Time.time - lastClickTime >= clickTimeInterval && useMedicine)
        {
            UsePotion();
            lastClickTime = Time.time;
        }
    }

    public void InstencePotion()
    {
        GameObject go = GetComponent<AnnounceToUI>().UseOnePotion();
        if(go == null)
            return;

    
        var obj = Instantiate(go,rightHand.position,Quaternion.identity,null);
        obj.GetComponent<SpriteRenderer>().sprite = PortableBag.Instance.currentSlot.potionIcon.sprite;
        obj.GetComponent<LegacyItem>().startPosition = rightHand.position;
        obj.GetComponent<LegacyItem>().movePosition = mousePosition;
    }
    public void UsePotion()
    {
        GetComponent<AnnounceToUI>().UseOnePotion();

        playerAnimation.PlayerDrink();

        currentAttackState = EPlayerAttackState.Drinking;
    }
    private void OnShift(InputAction.CallbackContext context)
    {
        if(currentAttackState == EPlayerAttackState.Throwing)
        {
            currentAttackState = EPlayerAttackState.Shooting;
            SwitchWeapon.Instance.shootPotion.gameObject.SetActive(true);
            SwitchWeapon.Instance.throwPotion.gameObject.SetActive(false);
        }
        else
        {
            currentAttackState = EPlayerAttackState.Throwing;
            SwitchWeapon.Instance.shootPotion.gameObject.SetActive(false);
            SwitchWeapon.Instance.throwPotion.gameObject.SetActive(true);
        }
    }
    private void OnFill(InputAction.CallbackContext context)
    {
        //装填子弹
        GetComponent<AnnounceToUI>().FillTheGun();
    }
#endregion


#region 动画事件相关
    public void GetHurt(Transform attacker)
    {
        //TODO:受伤动画  击退效果
        playerAnimation.PlayerHurt();
    }
    public void Dead()
    {
        //TODO:死亡动画
        gameObject.layer = 2;//死亡后更改角色Layer防止角色死亡后碰撞盒仍然会造成伤害
        gameObject.tag = "Untagged";

        inputAction.Disable();//死亡后关闭输入
    }
#endregion
}
