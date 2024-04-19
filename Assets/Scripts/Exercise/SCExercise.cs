using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCExercise : MonoBehaviour
{
    public Rigidbody Rb { get; private set; }


    private void Start()
    {
        Rb = gameObject.GetComponent<hitVolant>().rb;
    }
}
