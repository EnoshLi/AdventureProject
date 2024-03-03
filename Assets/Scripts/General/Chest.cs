using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour,IIteractable
{
    private SpriteRenderer spriteRenderer;
    public Sprite openSprite;
    public Sprite closeSprite;
    public bool isDone;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.sprite = isDone ? openSprite : closeSprite;
    }

    public void TriggleAction()
    {
        if (!isDone)
        {
            Debug.Log("Open Chest");
            //播放音效
            GetComponent<AudioDefination>()?.PlayAudioClip();
            OpenChest();
        }
        else
        {
            Debug.Log("Close Chest");
            CloseChest();
        }
        
    }

    private void OpenChest()
    {
        spriteRenderer.sprite = openSprite;
        isDone = true;
        
    }
    private void CloseChest()
    {
        spriteRenderer.sprite = closeSprite;
        isDone = false;
        
    }
}
