using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private PlayerControll playerControll;
    private float moveX,moveY;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerControll = GetComponent<PlayerControll>();
    }

    private void Update() 
    {
        SwitchAnimation();
    }

    public void SwitchAnimation()
    {
       
        if(playerControll.inputDirection.magnitude > 0)
        {
            Vector2 input = new Vector2(playerControll.inputDirection.x, playerControll.inputDirection.y).normalized;
            moveX = input.x;
            moveY = input.y;
        }
        anim.SetFloat("inputX", moveX);
        anim.SetFloat("inputY", moveY);

        anim.SetFloat("inputMagnitude", playerControll.inputDirection.magnitude);

        anim.SetFloat("speed", playerControll.character.currentSpeed);

        anim.SetBool("isFlot", playerControll.isFloting);

        anim.SetBool("isDead",playerControll.character.isDead);
    }

    public void PlayerAttack()
    {
        anim.SetTrigger("attack");
    }

    public void PlayerDrink()
    {
        anim.SetTrigger("drink");
    }

    public void PlayerHurt()
    {
        anim.SetTrigger("hurt");
    }
}
