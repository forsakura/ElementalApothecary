using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    //private PlayerController playerController;
    public Animator anim;

    public float direction;

    private void Start()
    {
        anim = GetComponent<Animator>();
        //playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        direction = PlayerController.Instance.mouseWorldPos.x - transform.position.x;
        anim.SetFloat("direction", direction);
        anim.SetFloat("speed", PlayerController.Instance.rb.velocity.magnitude);
    }

    public void ShootAnimation()
    {
        anim.SetTrigger("shoot");
    }

    public void ThrowAnimation()
    {
        anim.SetTrigger("throw");
    }
}
