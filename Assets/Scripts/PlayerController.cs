using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl playerInputControl;
    public Vector2 playerDirction;
    public Rigidbody2D rb;
    public PhyscisCheck physcisCheck;
    [Header("基本参数")]
    public float speed;
    public float jumpForce;
    private void Awake()
    {
        playerInputControl = new PlayerInputControl();
        rb = GetComponent<Rigidbody2D>();
        physcisCheck = GetComponent<PhyscisCheck>();
    }

    private void Update()
    {
        playerDirction = playerInputControl.Player.Move.ReadValue<Vector2>();
        playerInputControl.Player.Jump.started += Jump;
    }

    

    private void FixedUpdate()
    {
        Move();
    }

    private void OnEnable()
    {
        playerInputControl.Enable();
    }

    private void OnDisable()
    {
        playerInputControl.Disable();
    }
    /// <summary>
    /// 方法
    /// </summary>
    /// <param name="obj"></param>
    
    //跳跃
    private void Jump(InputAction.CallbackContext obj)
    {
        if (physcisCheck.isGround)
        {
            rb.AddForce(transform.up*jumpForce,ForceMode2D.Impulse);
        }
        
    }
    //移动
    private void Move()
    {
        rb.velocity = new Vector2(playerDirction.x*speed*Time.deltaTime,rb.velocity.y);
        //人物翻转
        int faceDir = (int)transform.localScale.x;
        if (playerDirction.x>0)
        {
            faceDir = 1;
        }

        if (playerDirction.x<0)
        {
            faceDir = -1;
        }

        transform.localScale = new Vector3(faceDir,1,1);
    }
    
}
