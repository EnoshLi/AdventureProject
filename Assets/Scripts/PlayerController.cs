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
    public float speed;
    public Rigidbody2D rb;

    private void Awake()
    {
        playerInputControl = new PlayerInputControl();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        playerDirction = playerInputControl.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
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

    private void OnEnable()
    {
        playerInputControl.Enable();
    }

    private void OnDisable()
    {
        playerInputControl.Disable();
    }
    
}
