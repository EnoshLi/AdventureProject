using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("监听")] 
    public SceneLoderEventSO loadEvent;
    public VoidEventSO afterLoadSceneEvent;
    
    [Header("基本组件")]
    public PlayerInputControl playerInputControl;
    public Vector2 playerDirction;
    public Rigidbody2D rb;
    public PhyscisCheck physcisCheck;
    private CapsuleCollider2D capsule2D;
    private PlayerAnimation playerAnimation;
    private Character character;
    
    [Header("基本参数")]
    public float speed;
    public float jumpForce;
    public float wallJumpFarce;
    public float hurtForce;
    public float slideDistance;
    public float slideSpeed;
    public float powerCost;
    private Vector2 offset;
    private Vector2 size;
    
    [Header("基本状态")]
    public bool isCrouch;
    public bool isHurt;
    public bool isDead;
    public bool isAttack;
    public bool wallJump;
    public bool isSlide;
    [Header("物理材质")] 
    public PhysicsMaterial2D normal;

    public PhysicsMaterial2D wall;
    
    private void Awake()
    {
        playerInputControl = new PlayerInputControl();
        rb = GetComponent<Rigidbody2D>();
        physcisCheck = GetComponent<PhyscisCheck>();
        capsule2D = GetComponent<CapsuleCollider2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        character = GetComponent<Character>();
        offset=capsule2D.offset;
        size = capsule2D.size;
    }

    private void Update()
    {
        playerDirction = playerInputControl.Player.Move.ReadValue<Vector2>();
        //人物跳跃功能
        playerInputControl.Player.Jump.started += Jump;
        //人物攻击功能
        playerInputControl.Player.Attack.started += Attack;
        //滑铲
        playerInputControl.Player.Slide.started += Slide;
        //人物某些状态改变
        //CheckState();
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
        loadEvent.LoadRequestEvent += OnLoadEvent;
        afterLoadSceneEvent.OnEventRised += OnAfterLoadSceneEvent;
    }
    
    private void OnDisable()
    {
        loadEvent.LoadRequestEvent -= OnLoadEvent;
        afterLoadSceneEvent.OnEventRised -= OnAfterLoadSceneEvent;
        playerInputControl.Disable();
    }
    /// <summary>
    /// 方法
    /// </summary>
    /// <param name="obj"></param>
    
    //在Menu界面人物不允许移动
    private void OnLoadEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        playerInputControl.Player.Disable();
    }
    //恢复人物移动
    private void OnAfterLoadSceneEvent()
    {
        playerInputControl.Player.Enable(); 
    }
    #region 移动
    private void Move()
    {
        //人物移动
        if (!isCrouch&&!wallJump)
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
    #endregion
   
    #region 跳跃
    private void Jump(InputAction.CallbackContext obj)
    {
        if (physcisCheck.isGround)
        {
            rb.AddForce(transform.up*jumpForce,ForceMode2D.Impulse);
            isSlide = false;
            StopAllCoroutines();
        }
        else if (physcisCheck.onWall)
        {
            rb.AddForce(new Vector2(-playerDirction.x,2f)*wallJumpFarce,ForceMode2D.Impulse);
            wallJump = true;
        }
        
    }

    #endregion
    
    //滑铲
    private void Slide(InputAction.CallbackContext obj)
    {

        if (!isSlide && physcisCheck.isGround && character.currentPower>=powerCost)
        {
            isSlide = true;
            var targetPos = new Vector3(transform.position.x + slideDistance * transform.localScale.x,
                transform.position.y);
            gameObject.layer = LayerMask.NameToLayer("Enemy");
            StartCoroutine(TriggleSlide(targetPos));
            character.OnSlide(powerCost);
        }
    }

    private IEnumerator TriggleSlide(Vector3 target)
    {
        do
        {
            yield return null;
            if (!physcisCheck.isGround)
            { 
                break;
            }

            if ((physcisCheck.touchLeftWall && transform.localScale.x<0f)|| (physcisCheck.touchRightWall&&transform.localScale.x>0f))
            {
                isSlide = false;
                break;
            }
            rb.MovePosition(new Vector2(transform.position.x+slideSpeed*transform.localScale.x,transform.position.y));
        } while (Mathf.Abs(target.x-transform.position.x)>=0.1f);
        isSlide = false;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    #region UnityEvent
    //受到伤害
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.transform.position.x), 0).normalized;
        rb.AddForce(hurtForce*dir,ForceMode2D.Impulse);
    }
    //角色死亡
    public void OnDie()
    {
        isDead = true;
        playerInputControl.Player.Disable();
    }
    //防止死亡后敌人仍然再攻击
    public void CheckState()
    {
        
        if (isDead)
        {
            gameObject.layer = LayerMask.NameToLayer("Enemy");
        }
        //改变人物摩擦因素
        capsule2D.sharedMaterial= physcisCheck.isGround ? normal : wall;
        //改变人物下滑的速度
        if (physcisCheck.onWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y  );
        }

        if (wallJump && rb.velocity.y<0)
        {
            wallJump = false; 
        }

    }

    #endregion

    #region 攻击功能

    private void Attack(InputAction.CallbackContext obj)
    {
        playerAnimation.AttackTriggle();
        isAttack = true;
    }

    #endregion
    
}
