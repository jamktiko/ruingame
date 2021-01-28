using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Runtime Anchors/Transform")]
public class TransformAnchor : ScriptableObject
{
    [HideInInspector] public bool isSet = false;

    private Transform _transform;
    public Transform Transform
    {
        get { return _transform; }
        set
        {
            _transform = value;
            isSet = _transform != null;
        }
    }
    public void OnDisable()
    {
        _transform = null;
        isSet = false;
    }
}
