using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyscisCheck : MonoBehaviour
{
    [Header("状态")]
    public bool isGround;
    public bool touchRightWall;
    public bool touchLeftWall;
    [Header("基本参数")] 
    public Vector2 bottomOffset;
    
    public Vector2 rightOffset;
    
    public Vector2 leftOffset;
    public float radius;

    public LayerMask layerMask;
    private void Update()
    {
        Check();
    }                                                

    public void Check()
    {
        //地面检测
        isGround = Physics2D.OverlapCircle((Vector2)transform.position+bottomOffset,radius,layerMask);
        //墙壁检测
        touchRightWall=Physics2D.OverlapCircle((Vector2)transform.position+rightOffset,radius,layerMask);
        touchLeftWall=Physics2D.OverlapCircle((Vector2)transform.position+leftOffset,radius,layerMask);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position+bottomOffset,radius);
        Gizmos.DrawWireSphere((Vector2)transform.position+rightOffset,radius);
        Gizmos.DrawWireSphere((Vector2)transform.position+leftOffset,radius);
    }
}
