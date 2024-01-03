using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator animator;

    protected Rigidbody2D rigidbody2D;

    public PhyscisCheck physcisCheck;

    [Header("基本参数")]
    
    public float normalSpeed;
    //当前速度
    public float currentSpeed;
    //冲锋速度
    public float ChaseSpeed;
    //面朝方向
    public Vector3 faceDir;
    [Header("计时器")]
    public bool wait;
    public float waitTime;
    public float waitCount;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentSpeed = normalSpeed;
        physcisCheck = GetComponent<PhyscisCheck>();
    }
    // Update is called once per frame
    void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        if ((physcisCheck.touchLeftWall && faceDir.x < 0 )|| (physcisCheck.touchRightWall && faceDir.x > 0))
        {
            wait = true;
            animator.SetBool("Walk" ,false);
        }
        TimeCount();
    }

    private void FixedUpdate()
    {
        Move();
    }

    #region 移动

    public virtual void Move()
    {
        rigidbody2D.velocity = new Vector2(faceDir.x*currentSpeed * Time.deltaTime, rigidbody2D.velocity.y);
    }

    #endregion

    #region 计时器
    public void TimeCount()
    {
        if (wait)
        {
            waitCount -= Time.deltaTime;
            if (waitCount<=0)
            {
                wait = false;
                waitCount = waitTime;
                transform.localScale = new Vector3(faceDir.x, 1, 1);
            }
        }

        
    }
    #endregion
    
}
