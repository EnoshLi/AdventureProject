using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private PhyscisCheck physcisCheck;
    private PlayerController playerController;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physcisCheck = GetComponent<PhyscisCheck>();
        playerController = GetComponent<PlayerController>();
        
    }

    private void Update()
    {
        SetAnimation();
    }

    public void SetAnimation()
    {
        animator.SetFloat("velocityX",Mathf.Abs(rb.velocity.x));
        animator.SetFloat("velocityY",rb.velocity.y);
        animator.SetBool("isGround",physcisCheck.isGround);
        animator.SetBool("Crouch",playerController.isCrouch);
        animator.SetBool("isDead",playerController.isDead);
    }

    public void hurtTriggle()
    {
        animator.SetTrigger("hurt");
    }
}
