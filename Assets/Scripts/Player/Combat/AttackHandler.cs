using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(ComboHandler))]
public class AttackHandler : MonoBehaviour
{
    public bool attacking = false;
    private float _playerDamage = default;
    private float _playerAttackSpeed = default;
    public ComboHandler comboHandler;
    private Animator _characterAnimator;
    private InputReader _inputReader;
    private Rigidbody _characterRigidbody;
    private MovementController _movementControl;
    private DamageCollider _damageCollider;
    public void Awake()
    {
        comboHandler = GetComponent<ComboHandler>();
        _characterAnimator = GetComponentInChildren<Animator>();
        _inputReader = GameManager.Instance.playerInputReader;
        _movementControl = GetComponent<MovementController>();
        _characterRigidbody = GetComponent<Rigidbody>();
        _damageCollider = GetComponentInChildren<DamageCollider>();
    }
    public void OnEnable()
    {
        try
        {
            _inputReader.AttackEvent += OnAttack;
        }
        catch
        {
        }
    }

    public void OnDisable()
    {
        _inputReader.AttackEvent -= OnAttack;
    }
    private void OnAttack()
    {
        if (attacking)
            return;
        if (!_movementControl.GroundedEntity)
            return;
        attacking = true;
        ComboAttack comboToExecute = comboHandler.ProcessCombo();
        try
        {
            _movementControl.attacking = attacking;
            _characterAnimator.SetFloat("attackSpeed", _playerAttackSpeed);
            _characterAnimator.Play(comboToExecute.animationClip.name);
            StartCoroutine(EndAttackRoutine(comboToExecute.animationClip.length / _playerAttackSpeed));
        }
        catch
        {
            EndAttack();
        }
    }
    public void EndAttack()
    {
        StopAllCoroutines();
        attacking = false;
        _movementControl.attacking = attacking;
        _damageCollider._damageCollider.enabled = false;
    }
    public void SetDamage(float amount)
    {
        _playerDamage = amount;
    }
    public void SetAttackSpeed(float amount)
    {
        _playerAttackSpeed = amount;
    }
    public virtual IEnumerator AttackMovementEffect(float attackForce, float duration)
    {
        //This should apply a rigidbody momentum when attacking
        _movementControl.attackMovement += attackForce;
        yield return new WaitForSeconds(duration);
        _movementControl.attackMovement -= attackForce;
    }
    public virtual IEnumerator EndAttackRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        attacking = false;
        _movementControl.attacking = attacking;
        _damageCollider._damageCollider.enabled = false;
    }
    
}
