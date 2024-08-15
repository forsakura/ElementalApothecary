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
    // public PlayerInputActions inputActions;
    public EPlayerAttackState currentAttackState;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public PlayerAnimations anims;
    [HideInInspector]
    public Player player;
    //[HideInInspector]
    public Vector2 mousePos;
    //[HideInInspector]
    public Vector2 mouseWorldPos;

    public Vector2 playerDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anims = GetComponent<PlayerAnimations>();
        player = GetComponent<Player>();
        InitInputActions();
    }

    private void Update()
    {
        UpdateCamera();
        PlayerMove();
    }

    private void InitInputActions()
    {
        PlayerInputManager.Instance.GamePlay.Shift.started += OnShift;
        PlayerInputManager.Instance.GamePlay.Shoot.started += Shoot;
        PlayerInputManager.Instance.GamePlay.Drink.started += Drink;
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
        playerDirection = PlayerInputManager.Instance.GamePlay.Move.ReadValue<Vector2>();
        rb.velocity = playerDirection * player.characterData.MoveSpeed;
    }

    private void OnShift(InputAction.CallbackContext context)
    {
        CharacterActions.SwitchWeapon.Invoke();
        switch (currentAttackState)
        {
            case EPlayerAttackState.Throwing:
                PlayerInputManager.Instance.GamePlay.Shoot.started -= Throw;
                PlayerInputManager.Instance.GamePlay.Shoot.started += Shoot;
                currentAttackState = EPlayerAttackState.Shooting;
                break;
            case EPlayerAttackState.Shooting:
                PlayerInputManager.Instance.GamePlay.Shoot.started -= Shoot;
                PlayerInputManager.Instance.GamePlay.Shoot.started += Throw;
                currentAttackState = EPlayerAttackState.Throwing;
                break;
            case EPlayerAttackState.Drinking:
                break;
        }
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        CharacterActions.OnShoot.Invoke();
        player.Shoot(mouseWorldPos);
    }

    private void Throw(InputAction.CallbackContext context)
    {
        CharacterActions.OnThrow.Invoke();
        player.Throw(mouseWorldPos);
    }

    private void Drink(InputAction.CallbackContext context)
    {
        CharacterActions.OnDrink.Invoke();
        player.Drink();
    }
}
