using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Event/SceneLoderEventSO")]
public class SceneLoderEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector3, bool> LoadRequestEvent;
/// <summary>
/// 加载的场景，人物去的地址，是否渐入渐出
/// </summary>
/// <param name="loactionToLand"></param>
/// <param name="posToGo"></param>
/// <param name="fadeScreen"></param>
    public void RaiseLoadRequestEvent(GameSceneSO loactionToLand, Vector3 posToGo, bool fadeScreen)
    {
        LoadRequestEvent?.Invoke(loactionToLand,posToGo,fadeScreen);
    }
}
