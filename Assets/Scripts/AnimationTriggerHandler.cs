using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class AnimationTriggerHandler : MonoBehaviour
{
    private Attack _playerAttack;
    private DamageCollider _damageCollider;

    public void Start()
    {
        _playerAttack = GetComponentInParent<Attack>();
        _damageCollider = GetComponentInChildren<DamageCollider>();
    }

    public void TriggerAttack()
    {
        _playerAttack.ExecuteAttack();
    }

    public void EndAttack()
    {
        _playerAttack.EndAttack();
    }
}
