using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(ComboHandler))]
public class PlayerAttackHandler : ComboAttackHandler
{
    private InputReader _inputReader;
    private MovementController _movementControl;
    
    public override void Awake()
    {
        base.Awake();
        _inputReader = GameManager.Instance.playerInputReader;
        _movementControl = GetComponent<MovementController>();
    }
    public override void OnEnable()
    {
        try
        {
            _inputReader.AttackEvent += OnAttack;
        }
        catch
        {
        }
    }
    public override void OnDisable()
    {
        _inputReader.AttackEvent -= OnAttack;
    }
    protected override bool CheckAttackConditions()
    {
        if (!_movementControl.GroundedEntity)
            return false;
        if (_movementControl.dashing)
            return false;
        return base.CheckAttackConditions();
    }
    public override void EndAttack()
    {
        base.EndAttack();
        _movementControl.attacking = attacking;
    }

    protected override void StartAttack()
    {
        base.StartAttack();
        _movementControl.attacking = attacking;
    }

    //New and improved attack logic!
}
