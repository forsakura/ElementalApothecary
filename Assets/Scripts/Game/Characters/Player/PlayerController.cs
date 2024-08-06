using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public CinemachineVirtualCamera cv;
    public PlayerInputActions inputActions;
    public EPlayerAttackState currentAttackState;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public PlayerAnimations anims;
    [HideInInspector]
    public Characters characters;
    //[HideInInspector]
    public Vector2 mousePos;
    //[HideInInspector]
    public Vector2 mouseWorldPos;

    public Vector2 playerDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anims = GetComponent<PlayerAnimations>();
        characters = GetComponent<Characters>();
        InitInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        UpdateCamera();
        PlayerMove();
    }

    private void InitInputActions()
    {
        inputActions = new PlayerInputActions();
        inputActions.GamePlay.Shift.started += OnShift;
        inputActions.GamePlay.Fire.started += Shoot;
        currentAttackState = EPlayerAttackState.Shooting;
    }

    private void UpdateCamera()
    {
        mousePos = Mouse.current.position.ReadValue();
        mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
        CinemachineTransposer ct = cv.GetCinemachineComponent<CinemachineTransposer>();
        ct.m_FollowOffset = new Vector3(2 * mousePos.x / Screen.width - 1, 2 * mousePos.y / Screen.height - 1, ct.m_FollowOffset.z);
    }

    private void PlayerMove()
    {
        playerDirection = inputActions.GamePlay.Move.ReadValue<Vector2>();
        rb.velocity = playerDirection * 5.0f;
    }

    private void OnShift(InputAction.CallbackContext context)
    {
        // PlayerActions.SwitchWeapon.Invoke();
        switch (currentAttackState)
        {
            case EPlayerAttackState.Throwing:
                inputActions.GamePlay.Fire.started -= Throw;
                inputActions.GamePlay.Fire.started += Shoot;
                currentAttackState = EPlayerAttackState.Shooting;
                break;
            case EPlayerAttackState.Shooting:
                inputActions.GamePlay.Fire.started -= Shoot;
                inputActions.GamePlay.Fire.started += Throw;
                currentAttackState = EPlayerAttackState.Throwing;
                break;
            case EPlayerAttackState.Drinking:
                break;
        }
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        // PlayerActions.OnShoot.Invoke();
        characters.Shoot(mouseWorldPos);
    }

    private void Throw(InputAction.CallbackContext context)
    {
        // PlayerActions.OnThrow.Invoke();
        characters.Throw(mouseWorldPos);
    }
}
