using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("事件监听")]
    public GameSceneSO currentLoadedScene;
    public SceneLoderEventSO loderEventSo;
    public GameSceneSO firstLoderScene;
    private GameSceneSO loactionToLand;
    private Vector3 posToGo;
    private bool fadeScreen;
    public float waitDuration;

    private void Awake()
    {
        //Addressables.LoadSceneAsync(firstLoderScene.assetReference,LoadSceneMode.Additive);
        currentLoadedScene = firstLoderScene;
        currentLoadedScene.assetReference.LoadSceneAsync(LoadSceneMode.Additive);
    }

    private void OnEnable()
    {
        loderEventSo.LoadRequestEvent += OnLoadRequestEvent;
    }

    private void OnDisable()
    {
        loderEventSo.LoadRequestEvent -= OnLoadRequestEvent;
    }

    private void OnLoadRequestEvent(GameSceneSO sceneToLand, Vector3 posistionToGo, bool fadeScreen)
    {
        loactionToLand=sceneToLand ;
        posToGo=posistionToGo;
        this.fadeScreen=fadeScreen;
        if (currentLoadedScene != null)
        {
            StartCoroutine(UnloadedPreviousScen());
        }

        Debug.Log("66");
    }

    private IEnumerator UnloadedPreviousScen()
    {
        if (fadeScreen)
        {
            //TODO实现渐入渐出
        }
        yield return new WaitForSeconds(waitDuration);
        //卸载当前场景
        yield return currentLoadedScene.assetReference.UnLoadScene();
        LoadNewScene();
    }

    private void LoadNewScene()
    {
        loactionToLand.assetReference.LoadSceneAsync(LoadSceneMode.Additive, true);
    }
}
