using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]public Animator animator;

    protected Rigidbody2D rigidbody2D;

    [HideInInspector]public PhyscisCheck physcisCheck;

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
    [Header("状态机")] 
    private BaseState currentState;
    //巡逻
    protected BaseState patrolState;
    //追击
    protected BaseState chaseState;
    [Header("基本状态")] 
    public bool isHurt;
    public bool isDead;
    
    [Header("计时器")]
    public bool wait;
    public float waitTime;
    public float waitCount;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentSpeed = normalSpeed;
        physcisCheck = GetComponent<PhyscisCheck>();
    }

    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
    }

    // Update is called once per frame
    void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        currentState.LogicUpdate();
        TimeCount();
    }

    private void FixedUpdate()
    {
        if (!isHurt && !isDead && !wait)
        {
            Move();
        }
        
    }

    private void OnDisable()
    {
        currentState.OnExit();
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
                waitCount = waitTime;
                transform.localScale = new Vector3(faceDir.x, 1, 1);
                StartCoroutine(FaceCheck());
            }
        }
    }

    IEnumerator FaceCheck()
    {
        physcisCheck.bottomOffset.x *=  -transform.localScale.x;
        yield return new WaitForSeconds(0.2f);
        wait = false;
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
        Vector2 dir = new Vector2(transform.position.x-attackTransform.position.x,0);
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
