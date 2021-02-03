using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerAttack : Attack
    {
        [SerializeField] private InputReader _inputReader;

        [SerializeField] private Animator _playerAnimator;

        public override void Start()
        {
            _damageCollider = GetComponentInChildren<DamageCollider>();
            _playerAnimator = GetComponentInParent<Animator>();
        }

        public override void OnEnable()
        {
            _inputReader.attackEvent += ExecuteAttackAnimation;
        }

        public override void OnDisable()
        {
            _inputReader.attackEvent -= ExecuteAttackAnimation;
        }

        public void ExecuteAttackAnimation()
        {
            if (_movementControl._groundedEntity)
                _playerAnimator.Play("Attack");
        }
        public virtual void ExecuteAttack()
        {
            
            _movementControl.enabled = false;
            _damageCollider.EnableDamage();
            _weaponMesh.enabled = true;
        }

        public virtual void EndAttack()
        {
            _movementControl.enabled = true;
            _damageCollider.DisableDamage();
            _weaponMesh.enabled = false;
        }
    }
}