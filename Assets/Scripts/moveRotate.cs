using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;

public class moveRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform cam = transform.GetChild(0).GetChild(0);
        Vector3 up = cam.up;
        Vector3 forward = cam.forward;
        Vector3 left = Vector3.Cross(up, forward);
        Vector3 right = Vector3.Cross(forward, up);

        //Debug.Log(transform.GetChild(0).GetChild(0).name);

        float angleUp = Vector3.Angle(up, Vector3.up);
        //Debug.Log(angleUp);

        float angleLeft = Vector3.Angle(left, Vector3.up);
        //Debug.Log(angleLeft);

        float angleRight = Vector3.Angle(right, Vector3.up);
        //Debug.Log(angleRight);

        if (angleUp > 25 && angleLeft < 65)
        {
            transform.Translate(Vector3.left * 0.05f);
        }

        if (angleUp > 25 && angleRight < 65)
        {
            transform.Translate(Vector3.right * 0.05f);
        }
    }
}
