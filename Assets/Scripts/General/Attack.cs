using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("伤害属性")]
    //攻击伤害
    public int attackDamage;
    /// <summary>
    /// 攻击范围和频率是用于蜜蜂上的
    /// </summary>
    //攻击范围
    public int attackRange;
    //攻击频率
    public int attackRate;

    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<Character>().TakeDamage(this);
    }
}
