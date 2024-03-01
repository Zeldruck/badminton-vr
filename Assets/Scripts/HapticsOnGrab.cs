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

    private XRGrabInteractable interactable;
    private bool grabbed = false;

    private void Awake()
    {
        interactable = GetComponent<XRGrabInteractable>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable != null && interactable.isSelected)
        {
            if (!grabbed)
            {
                grabbed = true;
                sendHaptics();
            }
        }
        else
            grabbed= false;
    }

    void sendHaptics()
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
