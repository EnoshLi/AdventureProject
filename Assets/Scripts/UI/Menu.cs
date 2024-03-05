using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public GameObject newGameButton;
    // Start is called before the first frame update
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(newGameButton);
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
