using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailHideState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.ChaseSpeed;
        currentEnemy.animator.SetBool("Walk",false);
        currentEnemy.animator.SetBool("Hide",true);
        currentEnemy.animator.SetTrigger("Skill");
        currentEnemy.lostCount = currentEnemy.lostTime;
        currentEnemy.GetComponent<Character>().invulnerable = true;
        currentEnemy.GetComponent<Character>().invulnerableCount = currentEnemy.lostCount;
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.lostCount<=0)
        {
            currentEnemy.SwaitchState(NPCState.Patrol);
        }
        currentEnemy.GetComponent<Character>().invulnerableCount = currentEnemy.lostCount;
    }

    public override void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        currentEnemy.animator.SetBool("Hide",false);
    }
}
