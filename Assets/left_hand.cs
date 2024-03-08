using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class left_hand : MonoBehaviour
{
    public InputActionReference action = null;
    
    private bool disabled = false;

    private void Awake()
    {
        action.action.started += Callback;
    }

    private void OnDestroy()
    {
        action.action.started -= Callback;
    }

    private void Callback(InputAction.CallbackContext context)
    {
        Debug.Log("Bruh");
        GetComponent<TeleportationProvider>().enabled = false;
    }
}
