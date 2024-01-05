using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(Animator),typeof(PhyscisCheck))]
public class Enemy : MonoBehaviour
{
    [HideInInspector]public Animator animator;

    protected Rigidbody2D rigidbody2D;

    [HideInInspector]public PhyscisCheck physcisCheck;

    [Header("基本参数")]
    
    public float normalSpeed;
    //冲锋速度
    public float ChaseSpeed;
    //当前速度
    [HideInInspector]public float currentSpeed;
    //面朝方向
    public Vector3 faceDir;
    //攻击者
    public Transform attacker;
    public float hurtForce;
    [Header("状态机")] 
    //巡逻
    protected BaseState patrolState;
    //追击
    protected BaseState chaseState;
    private BaseState currentState;
    [Header("基本状态")] 
    public bool isHurt;
    public bool isDead;
    
    [Header("等待计时器")]
    public bool wait;
    public float waitTime;
    public float waitCount;
    [Header("丢失计时器")] 
    public float lostTime;
    public float lostCount;
    
    [Header("检测敌人")] 
    public Vector2 centerOffset;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask attackLayerMask;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        physcisCheck = GetComponent<PhyscisCheck>();
        physcisCheck.isGround = true;
        waitCount = waitTime;

    }

    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
       //Debug.Log(transform.localScale.x);
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
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("PreMove")) ;
        {
            rigidbody2D.velocity = new Vector2(faceDir.x*currentSpeed * Time.deltaTime, rigidbody2D.velocity.y);
        }
        
    }

    #endregion
    //检测敌人
    public bool FoundPalyer()
    {
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize, 0, faceDir, checkDistance,
            attackLayerMask);
    }
    //状态切换
    public void SwaitchState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Chase => chaseState,
            NPCState.Patrol => patrolState,
                _ => null
        };
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
        
    }

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

        if (!FoundPalyer() && lostCount >= 0)
        {
            lostCount -= Time.deltaTime;
        }
        else if (FoundPalyer())
        {
            lostCount = lostTime;
        }
        
    }
    
    #endregion
    /// <summary>
    /// 事件方法
    /// </summary>
    /// <param name="attackTransform"></param>
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset+new Vector3(checkDistance*-transform.localScale.x,0,0),0.2f);
    }
}
