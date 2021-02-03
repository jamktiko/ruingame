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
        
        [SerializeField] protected Movement _movementControl;

        public int comboStep = 1;
        public int maximumCombo;
        public bool currentlyAttacking;
        public override void Start()
        {
            _damageCollider = GetComponentInChildren<DamageCollider>();
            _playerAnimator = GetComponentInParent<Animator>();
        }

        public override void OnEnable()
        {
            _inputReader.attackEvent += AttemptAttack;
        }

        public override void OnDisable()
        {
            _inputReader.attackEvent -= AttemptAttack;
        }

        public void AttemptAttack()
        {
            //ComboTimer that resets the combo if too long passes between attacks
            
            if (_movementControl._groundedEntity && !currentlyAttacking)
            {
                currentlyAttacking = true;
                ExecuteAttackAnimation();
                comboStep++;
            }

            if (comboStep == maximumCombo)
            {
                comboStep = 1;
            }
        }
        public void ExecuteAttackAnimation()
        {
            //Triggers animation event
            _playerAnimator.Play("Attack" + comboStep);

        }
        public override void ExecuteAttack()
        {
            //USED AS AN ANIMATION EVENT
            _movementControl.enabled = false;
            _damageCollider.EnableDamage();
            _weaponMesh.enabled = true;
        }

        public override void EndAttack()
        {
            //USED AS AN ANIMATION EVENT
            _movementControl.enabled = true;
            _damageCollider.DisableDamage();
            _weaponMesh.enabled = false;
            currentlyAttacking = false;
        }
    }
}