using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Haptics : MonoBehaviour
{
    [SerializeField]
    XRBaseController right_controller;

    [SerializeField]
    XRBaseController left_controller;

    public float duration = 0.1f;
    public float amplitude = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void sendHaptics()
    {
        if(right_controller != null)
        {
            right_controller.SendHapticImpulse(amplitude, duration);
        }
        
        if (left_controller != null)
        {
            left_controller.SendHapticImpulse(amplitude, duration);
        }
    }
}
