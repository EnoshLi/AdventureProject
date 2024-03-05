using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("广播")] 
    public VoidEventSO afterSceneLoadedEventSO;
    public SceneLoderEventSO sceneUnLoadedEvent;
    
    [Header("事件监听")] 
    public FadeEventSO fadeEvent;
    public VoidEventSO newGameEvent;
    
    [Header("场景")]
    [HideInInspector]public GameSceneSO currentLoadedScene;
    public SceneLoderEventSO loderEventSo;
    public GameSceneSO firstLoderScene;
    public GameSceneSO menuScence;
    private GameSceneSO loactionToLand;
    
    [Header("基本变量")]
    private Vector3 posToGo;
    private bool fadeScreen;
    public float waitDuration;
    public bool isLoading;
    public Vector3 firstPosition;
    public Vector3 menuPosition;
    public Transform playerTrans;

    private void Awake()
    {
        
    }

    private void Start()
    {
        //NewGame();
        loderEventSo.RaiseLoadRequestEvent(menuScence,menuPosition,true);
    }

    private void OnEnable()
    {
        loderEventSo.LoadRequestEvent += OnLoadRequestEvent;
        newGameEvent.OnEventRised += NewGame;
    }

    private void OnDisable()
    {
        loderEventSo.LoadRequestEvent -= OnLoadRequestEvent;
        newGameEvent.OnEventRised -= NewGame;
    }

    private void NewGame()
    {
        loactionToLand = firstLoderScene;
        //OnLoadRequestEvent(loactionToLand,firstPosition,true);
        loderEventSo.RaiseLoadRequestEvent(loactionToLand,firstPosition,true);
    }

    /// <summary>
/// 场景加载事件请求
/// </summary>
/// <param name="sceneToLand"></param>
/// <param name="posistionToGo"></param>
/// <param name="fadeScreen"></param>
    private void OnLoadRequestEvent(GameSceneSO sceneToLand, Vector3 posistionToGo, bool fadeScreen)
    {
        if (isLoading)
        {
            return;
        }

        isLoading = true;
        loactionToLand=sceneToLand ;
        posToGo=posistionToGo;
        this.fadeScreen=fadeScreen;
        if (currentLoadedScene != null)
        {
            StartCoroutine(UnloadedPreviousScen());
        }
        else
        {
            LoadNewScene();
        }

        //Debug.Log("66");
    }

    private IEnumerator UnloadedPreviousScen()
    {
        if (fadeScreen)
        {
            //变黑
            fadeEvent.FadeIn(waitDuration);
        }
        yield return new WaitForSeconds(waitDuration);
        //调整人物UI的显示
        sceneUnLoadedEvent.LoadRequestEvent(loactionToLand,posToGo,true);
        //卸载当前场景
        yield return currentLoadedScene.assetReference.UnLoadScene();
        //关闭人物
        playerTrans.gameObject.SetActive(false);
        LoadNewScene();
    }

    private void LoadNewScene()
    {
      var LoadingOperation=  loactionToLand.assetReference.LoadSceneAsync(LoadSceneMode.Additive, true);
      LoadingOperation.Completed += OnLoadCompleted;
    }
/// <summary>
/// 场景加载完后
/// </summary>
/// <param name="obj"></param>
    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        currentLoadedScene = loactionToLand;
        playerTrans.position = posToGo;
        playerTrans.gameObject.SetActive(true);
        if (fadeScreen)
        {
            //透明
            fadeEvent.FadeOut(waitDuration);
        }

        isLoading = false;
        //场景加载完成后的事件
        if (currentLoadedScene.sceneType==SceneType.Loaction)
        {
            afterSceneLoadedEventSO.EventRise();  
        }
        //afterSceneLoadedEventSO.EventRise();
    }
}
