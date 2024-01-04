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
    //攻击者
    public Transform attacker;
    public float hurtForce;
    [Header("基本状态")] 
    public bool isHurt;

    public bool isDead;
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
        if (!isHurt && !isDead)
        {
            Move();
        }
        
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

    #region 受到伤害

    public void OnTakeDamge(Transform attackTransform)
    {
        attacker = attackTransform;
        if (attackTransform.position.x- transform.position.x>0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (attackTransform.position.x-transform.position.x<0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        isHurt = true;
        animator.SetTrigger("Hurt");
        Vector2 dir = new Vector2(transform.position.x-attacker.position.x,0);
        StartCoroutine(OnHurt(dir));
    }
    //协程
    IEnumerator OnHurt(Vector2 dir)
    {
        rigidbody2D.AddForce(dir*hurtForce,ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
    }

    #endregion

    #region 死亡

    public void OnDie()
    {
        gameObject.layer = 2;
        animator.SetBool("Dead",true);
        isDead = true;
    }

    public void AfterAnimation()
    {
        Destroy( gameObject);
    }



    #endregion
}
