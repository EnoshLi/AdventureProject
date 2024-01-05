using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
    }

    public override void LogicUpdate()
    {
        if (!currentEnemy.physcisCheck.isGround || (currentEnemy.physcisCheck.touchLeftWall && currentEnemy.faceDir.x < 0 )|| (currentEnemy.physcisCheck.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.wait = true;
            currentEnemy.animator.SetBool("Walk" ,false);
        }
        else
        {
            currentEnemy.animator.SetBool("Walk" ,true);
        }
    }

    public override void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }
}
