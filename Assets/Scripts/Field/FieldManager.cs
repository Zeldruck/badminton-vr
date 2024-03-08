using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FieldManager : MonoBehaviour
{
    [SerializeField] private string shuttlecockTag;
    [Space]
    [SerializeField] private Transform[] _colliderRoots;
    [SerializeField] private FiledColliderEvent _netColliderEvent;


    public Action<int> _offFieldEvent;
    public Action<int> _inLeftFieldEvent;
    public Action<int> _inRightFieldEvent;
    public Action<int> _inFrontFieldEvent;
    public Action<int> _inBackFieldEvent;
    public Action _netEvent;

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

        _netColliderEvent.shuttlecockTag = shuttlecockTag;
        _netColliderEvent.onTriggerSucceed.AddListener(() => SCTouchGround(-1, _netColliderEvent.Type));
    }

    private void SCTouchGround(int side, FiledColliderEvent.EFieldType eventTriggered)
    {
        switch (eventTriggered)
        {
            case FiledColliderEvent.EFieldType.OffField:
                _offFieldEvent?.Invoke(side);
                break;

            case FiledColliderEvent.EFieldType.InLeftField:
                _inLeftFieldEvent?.Invoke(side);
                break;

            case FiledColliderEvent.EFieldType.InRightField:
                _inRightFieldEvent?.Invoke(side);
                break;

            case FiledColliderEvent.EFieldType.InFrontField:
                _inFrontFieldEvent?.Invoke(side);
                break;

            case FiledColliderEvent.EFieldType.InBackField:
                _inBackFieldEvent?.Invoke(side);
                break;

            case FiledColliderEvent.EFieldType.Net: 
                _netEvent?.Invoke();
                break;
        }
    }
}
