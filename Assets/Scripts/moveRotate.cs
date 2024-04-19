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
        Transform cam = transform.GetChild(3).GetChild(0).GetChild(0).transform;
        Vector3 up = cam.transform.up;
        Vector3 forward = cam.transform.forward;
        Vector3 left = Vector3.Cross(up, forward);
        Vector3 right = Vector3.Cross(forward, up);

        Debug.Log(transform.GetChild(3).GetChild(0).name);

        float angleUp = Vector3.Angle(up, Vector3.up);
        Debug.Log(angleUp);

        float angleLeft = Vector3.Angle(left, Vector3.up);
        Debug.Log(angleLeft);

        float angleRight = Vector3.Angle(right, Vector3.up);
        Debug.Log(angleRight);

        if (angleUp > 20 && angleLeft < 70)
        {
            left.Set(left.x, 0, left.z);
            transform.Translate(left.normalized * 0.05f);
        }

        if (angleUp > 20 && angleRight < 70)
        {
            right.Set(right.x, 0, right.z);

            transform.Translate(right.normalized * 0.05f);
        }
    }
}
