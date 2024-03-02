using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.XInput;

public class Sign : MonoBehaviour
{
    public Animator anim;
    public PlayerInputControl playerInputControl;
    public Transform playerTransform;
    public GameObject signSpirte;
    public bool canPress;

    private void Awake()
    {
        playerInputControl = new PlayerInputControl();
        anim = signSpirte.GetComponent<Animator>();
        playerInputControl.Enable();
    }

    private void OnEnable()
    {
        InputSystem.onActionChange += OnActionChange;
    }

    

    private void Update()
    {
        signSpirte.GetComponent<SpriteRenderer>().enabled = canPress;
        signSpirte.transform.localScale = playerTransform.localScale;
        
    }
    private void OnActionChange(object obj, InputActionChange actionChange)
    {
        if (actionChange == InputActionChange.ActionStarted)
        {
            var device = ((InputAction)obj).activeControl.device;
            switch (device)
            {
                case DualShockGamepad:
                    anim.Play("PS");
                    break;
                case XInputController:
                    anim.Play("Xbox");
                    break;
                case Keyboard:
                    anim.Play("Key");
                    break;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            canPress = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canPress = false;
    }
}
