using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class AnimationTriggerHandler : MonoBehaviour
{
    private PlayerAttackHandler _attackHandler;
    public void Start()
    {
        _attackHandler = GetComponentInParent<PlayerAttackHandler>();
    }
    
    public void HandleDamage()
    {
        _attackHandler.HandleAttack();
    }
}
