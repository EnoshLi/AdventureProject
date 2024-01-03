using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("基本参数")]
    public int maxHealth;
    public int currentHealth;
    [Header("人物受伤状态")] 
    public float invulnerableDuration;

    public float invulnerableCount;

    public bool invulnerable;

    public bool isDead;
    [Header("事件")] 
    public UnityEvent<Transform> OnTakeDamage;

    public UnityEvent OnDie;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (invulnerable)
        {
            invulnerableCount -= Time.deltaTime;
            if (invulnerableCount <= 0)
            {
                invulnerable = false;
            }
        }
    }

    public void TakeDamage(Attack attacker)
    {
        if (invulnerable)
        {
            return;
        }

        if (currentHealth-attacker.attackDamage>0)
        {
            currentHealth -= attacker.attackDamage;
            TriggerInvulnerable();
            //执行受伤
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            currentHealth = 0;
            //执行死亡
            OnDie?.Invoke();
        }
    }

    public void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCount = invulnerableDuration;
        }
    }

}
