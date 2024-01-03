using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator animator;

    protected Rigidbody2D rigidbody2D;

    [Header("基本参数")]
    
    public float normalSpeed;
    //当前速度
    public float currentSpeed;
    //冲锋速度
    public float ChaseSpeed;
    //面朝方向
    public Vector3 faceDir;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentSpeed = normalSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
    }

    private void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {
        rigidbody2D.velocity = new Vector2(faceDir.x*currentSpeed * Time.deltaTime, rigidbody2D.velocity.y);
    }
}
