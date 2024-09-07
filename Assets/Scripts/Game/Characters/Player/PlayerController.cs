using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CharacterDelegates;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public CinemachineVirtualCamera cv => GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
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
        PlayerInputManager.Instance.GamePlay.Shoot.started += Shoot;
        PlayerInputManager.Instance.GamePlay.UsePotion.started += UsePotion;
        PlayerInputManager.Instance.GamePlay.Fill.started += Fill;
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

    private void Shoot(InputAction.CallbackContext context)
    {
        player.Shoot(mouseWorldPos);
    }

    private void UsePotion(InputAction.CallbackContext context)
    {
        Throw(context);
        // Drink(context);
    }

    private void Throw(InputAction.CallbackContext context)
    {
        player.Throw(mouseWorldPos);
    }

    private void Drink(InputAction.CallbackContext context)
    {
        player.Drink();
    }

    private void Fill(InputAction.CallbackContext context)
    {
        player.Fill();
    }
}
