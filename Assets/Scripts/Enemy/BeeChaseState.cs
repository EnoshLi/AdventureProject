using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeChaseState : BaseState
{
    private Vector3 target;
    private Vector3 moveDir;
    private Attack attack;
    private bool isAttack;
    private float attackRateCount = 0;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed= currentEnemy.ChaseSpeed;
        attack = enemy.GetComponent<Attack>();
        currentEnemy.lostCount = currentEnemy.lostTime;
        currentEnemy.animator.SetBool("Chase",true);
    }
    

    public override void LogicUpdate()
    {
        if (currentEnemy.lostCount<=0)
        {
            currentEnemy.SwitchState(NPCState.Patrol);
        }

        target = new Vector3(currentEnemy.attacker.position.x, currentEnemy.attacker.position.y + 1.5f, 0);
        //判断攻击距离
        if (Mathf.Abs(target.x - currentEnemy.transform.position.x) <= attack.attackRange && Mathf.Abs(target.y - currentEnemy.transform.position.y) <= attack.attackRange)
        {
            //攻击
            isAttack = true;
            if (!currentEnemy.isHurt)
            {
                currentEnemy.rigidbody2D.velocity = Vector2.zero;
            }
            
            //计时器
            attackRateCount -= Time.deltaTime;
            if (attackRateCount<=0)
            {
                currentEnemy.animator.SetTrigger("Attack");
                attackRateCount = attack.attackRange;
            }
        }
        else
        {
            isAttack = false;
        }
        moveDir = (target - currentEnemy.transform.position).normalized;
        if (moveDir.x>0)
        {
            currentEnemy.transform.localScale = new Vector3(-1, 1, 1);
        }else if (moveDir.x<0)
        {
            currentEnemy.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public override void PhysicsUpdate()
    {
        if ( !currentEnemy.isHurt && !currentEnemy.isDead && !isAttack)
        {
            currentEnemy.rigidbody2D.velocity = moveDir * currentEnemy.currentSpeed * Time.deltaTime;
        }
        
    }

    public override void OnExit()
    {
        currentEnemy.animator.SetBool("Chase",false);
    }
}
