using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{
    [Header("移动范围")]
    public float patrolRadius;

    protected override void Awake()
    {
        base.Awake();
        patrolState = new BeePatrolState();
    }

    public override bool FoundPalyer()
    {
        var obj = Physics2D.OverlapCircle(transform.position, checkDistance, attackLayerMask);
        if (obj)
        {
            attacker = obj.transform;
        }
        return obj;
    }

    public override void Move()
    {
       //patrolState.LogicUpdate();
    }

    protected override void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(spawnPoint,checkDistance);
        Gizmos.color=Color.green;
        Gizmos.DrawWireSphere(spawnPoint,patrolRadius);
    }

    public override Vector3 GetNewPoint()
    {
        var targetX = Random.Range(-patrolRadius, patrolRadius);
        var targetY = Random.Range(-patrolRadius, patrolRadius);
        return spawnPoint + new Vector3(targetX, targetY);
    }
}
