using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyscisCheck : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerController playerController;
    [Header("状态")]
    public bool isPlayer;
    public bool isGround;
    public bool touchRightWall;
    public bool touchLeftWall;
    public bool onWall;
    [Header("基本参数")] 
    public Vector2 bottomOffset;
    
    public Vector2 rightOffset;
    
    public Vector2 leftOffset;
    
    public float radius;

    public LayerMask layerMask;
    

    private void Awake()
    {
        if (isPlayer)
        {
            playerController = GetComponent<PlayerController>();
        }

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
        Check();
    }                                                

    public void Check()
    {
        //bottomOffset.x = bottomOffset.x * transform.localScale.x;
        //检测地面
        if (onWall)
        {
            isGround = Physics2D.OverlapCircle((Vector2)transform.position +new Vector2(bottomOffset.x * transform.localScale.x, bottomOffset.y), radius,layerMask);
        }
        else
        {
            isGround = Physics2D.OverlapCircle((Vector2)transform.position +new Vector2(bottomOffset.x * transform.localScale.x, 0), radius,layerMask);
        }
        
        //墙壁检测
        touchRightWall=Physics2D.OverlapCircle((Vector2)transform.position+rightOffset,radius,layerMask);
        touchLeftWall=Physics2D.OverlapCircle((Vector2)transform.position+leftOffset,radius,layerMask);
        //蹬墙跳判断
        if (isPlayer)
        {
            onWall = (touchRightWall&& playerController.playerDirction.x>0 || touchLeftWall&& playerController.playerDirction.x<0) && rb.velocity.y<0f;
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(bottomOffset.x * transform.localScale.x, bottomOffset.y),radius);
        Gizmos.DrawWireSphere((Vector2)transform.position+rightOffset,radius);
        Gizmos.DrawWireSphere((Vector2)transform.position+leftOffset,radius);
    }
}
