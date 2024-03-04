using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeCanvas : MonoBehaviour
{
    public Image fadeImage;
    [Header("事件监听")] public FadeEventSO fadeEvent;

    private void OnEnable()
    {
        fadeEvent.onRisedEvent += OnFadeEvent;
    }

    private void OnDisable()
    {
        fadeEvent.onRisedEvent -= OnFadeEvent;
    }

    

    private void OnFadeEvent(Color target,float duration,bool fadeIn)
    {
        fadeImage.DOBlendableColor(target,duration);
    }
}
