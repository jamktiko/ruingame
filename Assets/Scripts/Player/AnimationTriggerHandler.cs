using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class AnimationTriggerHandler : MonoBehaviour
{
    private DamageCollider _damageCollider;

    public void Start()
    {
        _damageCollider = GetComponentInChildren<DamageCollider>();
    }

    public void TriggerAttack()
    {
        _damageCollider._damageCollider.enabled = true;
    }

    public void EndAttack()
    {
        _damageCollider._damageCollider.enabled = false;
    }
}
