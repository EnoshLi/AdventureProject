using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    private PlayerController playerController;
    [Header("基本参数")]
    public float maxHealth;
    public float currentHealth;
    public float maxPower;
    public float currentPower;
    public float powerRecoverSpeed;
    [Header("人物受伤状态")] 
    public float invulnerableDuration;

    public float invulnerableCount;

    public bool invulnerable;

    public bool isDead;
    [Header("事件")] 
    public UnityEvent<Character> OnHealthChange;
    public UnityEvent<Transform> OnTakeDamage;

    public UnityEvent OnDie;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        currentHealth = maxHealth;
        currentPower = maxPower;
        OnHealthChange?.Invoke(this);
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

        if (currentPower<maxPower && !playerController.isSlide)
        {
            currentPower += Time.deltaTime * powerRecoverSpeed;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            currentHealth = 0;
            OnHealthChange?.Invoke(this);
            OnDie?.Invoke();
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
        OnHealthChange?.Invoke(this);
    }

    public void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCount = invulnerableDuration;
        }
    }

    public void OnSlide(float cost)
    {
        currentPower -= cost;
        OnHealthChange?.Invoke(this);
    }

}
