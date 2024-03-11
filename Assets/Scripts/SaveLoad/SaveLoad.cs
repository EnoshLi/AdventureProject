using System.Collections;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;
using UnityEngine;

public class SaveLoad : MonoBehaviour,IIteractable
{
    [Header("广播")] 
    public VoidEventSO gameLoadEvent;
    [Header("变量参数 ")]
    public SpriteRenderer spriteRenderer;
    public Sprite darkSprite;
    public Sprite lightSprite;
    public bool isDone;
    public GameObject lightObj;
    

    private void OnEnable()
    {
        spriteRenderer.sprite = isDone ? lightSprite : darkSprite;
        lightObj.SetActive(isDone);
    }
    
    public void TriggleAction()
    {
        if (!isDone)
        {
            isDone = true;
            lightObj.SetActive(true);
            spriteRenderer.sprite = lightSprite;
            //TODO保存数据
            gameLoadEvent.EventRise();
            this.gameObject.tag = "Untagged";
        }
    }
}
