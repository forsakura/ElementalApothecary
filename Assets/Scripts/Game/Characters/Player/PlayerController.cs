using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    public CinemachineVirtualCamera cv;
    public PlayerInputActions inputActions;
    [HideInInspector]
    public Rigidbody2D rb;
    //[HideInInspector]
    public Vector2 mousePos;
    //[HideInInspector]
    public Vector2 mouseWorldPos;

    public Vector2 playerDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new PlayerInputActions();
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
}
