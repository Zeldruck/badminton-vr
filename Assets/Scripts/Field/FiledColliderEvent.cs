using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FiledColliderEvent : MonoBehaviour
{
    public enum EFieldType
    {
        OffField,
        InLeftField,
        InRightField,
        InFrontField,
        InBackField,
        Net
    }

    [SerializeField] private EFieldType _type;
    public EFieldType Type => _type;

    [HideInInspector] public UnityEvent onTriggerSucceed;

    [HideInInspector] public string shuttlecockTag;

    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(shuttlecockTag)) return;

        onTriggerSucceed?.Invoke();
    }
}
