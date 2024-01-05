using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.ChaseSpeed;
        currentEnemy.animator.SetBool("Run",true);
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.lostCount<=0)
        {
            currentEnemy.SwaitchState(NPCState.Patrol);
        }
        if (!currentEnemy.physcisCheck.isGround || (currentEnemy.physcisCheck.touchLeftWall && currentEnemy.faceDir.x < 0 )|| (currentEnemy.physcisCheck.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.transform.localScale = new Vector3(currentEnemy.faceDir.x, 1, 1);
        }
    }

    public override void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        currentEnemy.animator.SetBool("Run",false);
        
    }
}
