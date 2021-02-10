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

    public void Awake()
    {
        comboHandler = GetComponent<ComboHandler>();
        _characterAnimator = GetComponentInChildren<Animator>();
        _inputReader = GameManager.Instance.playerInputReader;
        _movementControl = GetComponent<MovementController>();
        _characterRigidbody = GetComponent<Rigidbody>();
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
        attacking = true;
        ComboAttack comboToExecute = comboHandler.ProcessCombo();
        try
        {
            _movementControl.attacking = attacking;
            StartCoroutine(AttackMovementEffect(5f, comboToExecute.animationClip.length / _playerAttackSpeed / 2 ));
            _characterAnimator.SetFloat("attackSpeed", _playerAttackSpeed);
            _characterAnimator.Play(comboToExecute.animationClip.name);
            Invoke("EndAttack", comboToExecute.animationClip.length / _playerAttackSpeed);
            Debug.Log(comboToExecute.name);
            Debug.Log(comboToExecute.Damage * _playerDamage);
        }
        catch
        {
            Debug.Log("No animation clip assigned to " + comboToExecute.name);
            EndAttack();
        }
    }
    private void EndAttack()
    {
        attacking = false;
        _movementControl.attacking = attacking;
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
        _movementControl.attackMovement += attackForce;
        yield return new WaitForSeconds(duration);
        _movementControl.attackMovement -= attackForce;

    }
}
