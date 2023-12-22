using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("基本参数")]
    public int maxHealth;
    public int currentHealth;
    [Header("人物受伤状态")] 
    public float invulnerableDuration;

    public float invulnerableCount;

    public bool invulnerable;

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

    public void TakeDamge(Attack attacker)
    {
        if (invulnerable)
        {
            return;
        }
        currentHealth -= attacker.attackDamge;
        TriggerInvulnerable();
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
