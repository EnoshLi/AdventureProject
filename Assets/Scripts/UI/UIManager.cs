using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerState playerState;
    [Header("事件监听")] 
    public CharacterEventSO CharacterEventSo;

    public SceneLoderEventSO loderEvent;

    

    private void OnEnable()
    {
        CharacterEventSo.EventRaised += OnHealthEvent;
        loderEvent.LoadRequestEvent += OnLoadRequestEvent;
    }

    

    private void OnHealthEvent(Character character)
    {
        var persentage=character.currentHealth / character.maxHealth;
       //Debug.Log(persentage);
        playerState.OnHealthChange(persentage);
        playerState.OnPowerChange(character);
    }


    private void OnDisable()
    {
        CharacterEventSo.EventRaised -= OnHealthEvent;
        loderEvent.LoadRequestEvent -= OnLoadRequestEvent;
    }
    //在Menu中关闭人物UI
    private void OnLoadRequestEvent(GameSceneSO sceneToLoad, Vector3 arg1, bool arg2)
    {
        var menu = sceneToLoad.sceneType == SceneType.Menu;
        playerState.gameObject.SetActive(!menu);
    }
}
