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
    private CapsuleCollider2D capsule2D;
    
    [Header("基本参数")]
    public float speed;
    public float jumpForce;
    public float hurtForce;
    public bool isCrouch;
    public bool isHurt;
    public bool isDead;
    private Vector2 offset;
    private Vector2 size;
    
    private void Awake()
    {
        playerInputControl = new PlayerInputControl();
        rb = GetComponent<Rigidbody2D>();
        physcisCheck = GetComponent<PhyscisCheck>();
        capsule2D = GetComponent<CapsuleCollider2D>();
        offset=capsule2D.offset;
        size = capsule2D.size;
    }

    private void Update()
    {
        playerDirction = playerInputControl.Player.Move.ReadValue<Vector2>();
        playerInputControl.Player.Jump.started += Jump;
    }

    

    private void FixedUpdate()
    {
        if (!isHurt)
        {
            Move();
        }
        
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
        //人物移动
        if (!isCrouch)
        {
            rb.velocity = new Vector2(playerDirction.x * speed * Time.deltaTime, rb.velocity.y);
        }

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
        //下蹲
        isCrouch = playerDirction.y < -0.5f && physcisCheck.isGround;
        if (isCrouch)
        {
            capsule2D.offset= new Vector2(-0.05f,0.8f);
            capsule2D.size = new Vector2(0.66f,1.6f);
        }
        else
        {
            capsule2D.offset = offset;
            capsule2D.size = size;
        }
    }
    //人物受到伤害
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.transform.position.x), 0).normalized;
        rb.AddForce(hurtForce*dir,ForceMode2D.Impulse);
    }
    //人物死亡
    public void OnDie()
    {
        isDead = true;
        playerInputControl.Player.Disable();
    }
    public void CheckState()
    {
        if (isDead)
            gameObject.layer = LayerMask.NameToLayer("Enemy");
        else
            gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
