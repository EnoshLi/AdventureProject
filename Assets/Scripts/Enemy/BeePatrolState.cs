using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeePatrolState : BaseState
{
    private Vector3 target;
    //移动方向
    private Vector3 moveDir;
    
    
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed= currentEnemy.normalSpeed;
        target = enemy.GetNewPoint();
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.FoundPalyer())
        {
            currentEnemy.SwitchState(NPCState.Chase);
        }

        if (Mathf.Abs(target.x-currentEnemy.transform.position.x)<0.1f&&Mathf.Abs(target.y-currentEnemy.transform.position.y)<0.1f)
        {
            currentEnemy.wait = true;
            target = currentEnemy.GetNewPoint();
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
        if (!currentEnemy.wait && !currentEnemy.isHurt && !currentEnemy.isDead)
        {
            currentEnemy.rigidbody2D.velocity = moveDir * currentEnemy.currentSpeed * Time.deltaTime;
        }
        else
        {
            currentEnemy.rigidbody2D.velocity = Vector2.zero;
        }
    }

    public override void OnExit()
    {
        Debug.Log("Exit");
    }
}
