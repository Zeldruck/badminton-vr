using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [SerializeField] private string shuttlecockTag;
    [Space]
    [SerializeField] private Transform[] _colliderRoots;

    private void Start()
    {
        for (int i = 0; i < _colliderRoots.Length; i++)
        {
            FiledColliderEvent[] filedColliderEvents = _colliderRoots[i].GetComponentsInChildren<FiledColliderEvent>();

            foreach (var colliderEvent in filedColliderEvents)
            {
                colliderEvent.shuttlecockTag = shuttlecockTag;
                colliderEvent.onTriggerSucceed.AddListener(() => SCTouchGround(i + 1, colliderEvent.Type));
            }
        }
    }

    private void SCTouchGround(int side, FiledColliderEvent.EFieldType eventTriggered)
    {
        switch (eventTriggered)
        {
            case FiledColliderEvent.EFieldType.OffField: break;

            case FiledColliderEvent.EFieldType.InLeftField: break;

            case FiledColliderEvent.EFieldType.InRightField: break;

            case FiledColliderEvent.EFieldType.InFrontField: break;

            case FiledColliderEvent.EFieldType.InBackField: break;
        }
    }
}
