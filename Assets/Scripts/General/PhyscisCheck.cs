using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyscisCheck : MonoBehaviour
{
    [Header("基本参数")] 
    public bool isGround;

    public Vector2 bottomOffset;

    public float radius;

    public LayerMask layerMask;
    private void Update()
    {
        Check();
    }                                                

    public void Check()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position+bottomOffset,radius,layerMask);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position+bottomOffset,radius);
    }
}
