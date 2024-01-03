using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Boar : Enemy
{
    
    public override void Move()
    {
        base.Move();
        animator.SetBool("Walk",true);
    }
}
