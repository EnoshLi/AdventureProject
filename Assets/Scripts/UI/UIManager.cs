using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerState playerState;
    [Header("事件监听")] 
    public CharacterEventSO CharacterEventSo;

    private void OnEnable()
    {
        CharacterEventSo.EventRaised += OnHealthEvent;
    }

    private void OnHealthEvent(Character character)
    {
        var persentage=character.currentHealth / character.maxHealth;
        Debug.Log(persentage);
        playerState.OnHealthChange(persentage);
    }


    private void OnDisable()
    {
        CharacterEventSo.EventRaised += OnHealthEvent;
    }
}
