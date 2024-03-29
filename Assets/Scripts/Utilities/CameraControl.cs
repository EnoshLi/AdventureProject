using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("事件的监听")] 
    public VoidEventSO afterLoadedEventSO;
    private CinemachineConfiner2D confiner2D;
    public CinemachineImpulseSource impulseSource;
    public VoidEventSO cameraShakeEvent;
    private void Awake()
    {
        confiner2D = GetComponent<CinemachineConfiner2D>();
    }

    private void OnEnable()
    {
        cameraShakeEvent.OnEventRised += OnCameraShakeEvent;
        afterLoadedEventSO.OnEventRised += OnAfterLoadedEvent;
    }

    private void OnDisable()
    {
        cameraShakeEvent.OnEventRised -= OnCameraShakeEvent;
        afterLoadedEventSO.OnEventRised -= OnAfterLoadedEvent;
    }

    private void OnAfterLoadedEvent()
    {
        GetNewCameraBounds();
    }

    private void OnCameraShakeEvent()
    {
        impulseSource.GenerateImpulse();
    }

    /// <summary>
    /// TODO
    /// </summary>
    /*private void Start()
    {
        GetNewCameraBounds();
    }*/

    public void GetNewCameraBounds()
    {
        var obj = GameObject.FindGameObjectWithTag("Bounds");
        if (obj==null)
        {
            return;
        }

        confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        
        //转换场景清掉之前的缓存
        confiner2D.InvalidateCache();
    }
}
