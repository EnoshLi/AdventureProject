using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransPosition : MonoBehaviour,IIteractable
{
    
    public SceneLoderEventSO loderEventSo;
    public GameSceneSO sceneToGo;
    public Vector3 positionToGo;
    public void TriggleAction()
    {
        Debug.Log("传送");
        loderEventSo.LoadRequestEvent(sceneToGo,positionToGo,true);
    }
}
