using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("事件监听")] 
    public PlayerAudioEventSO FxEventSo;
    public PlayerAudioEventSO BGMEventSo;
    public AudioSource BGMSource;
    public AudioSource FXSource;

    private void OnEnable()
    {
        FxEventSo.onEventRised += OnFxEvent;
        BGMEventSo.onEventRised += OnBGMEvent;
    }
    
    private void OnDisable()
    {
        FxEventSo.onEventRised -= OnFxEvent;
        BGMEventSo.onEventRised -= OnBGMEvent;
    }
    private void OnFxEvent(AudioClip audioClip)
    {
        FXSource.clip = audioClip;
        FXSource.Play();
    }
    private void OnBGMEvent(AudioClip audioClip)
    {
        BGMSource.clip = audioClip;
        BGMSource.Play();
    }
}
