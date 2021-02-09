using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerAttack : Attack
    {
        public Rigidbody _characterRigidbody;
        public InputReader _inputReader;
        public float _playerDamage = 100f;
        public float _playerAttackSpeed = 1f;
        [SerializeField] protected MovementController _movementControl;

        public float attackSwingForce;
        public int comboStep = 1;
        public int maximumCombo = 2;
        
        public override void Start()
        {
            base.Start();
            _movementControl = GetComponent<MovementController>();
            _characterRigidbody = GetComponent<Rigidbody>();
        }
        public override void OnEnable()
        {
            try
            {
                _inputReader.attackEvent += AttemptAttack;
            }
            catch
            {
            }
        }

        public override void OnDisable()
        {
            _inputReader.attackEvent -= AttemptAttack;
        }

        public override void AttemptAttack()
        {
            //ComboTimer that resets the combo if too long passes between attacks
            
            //Set damagecollider damage based on combo step?
            _damageCollider._damage = _playerDamage;
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
        public override void ExecuteAttackAnimation()
        {
            //Triggers animation event
            _entityAnimator.Play("Attack" + comboStep);

        }
        public override void ExecuteAttack()
        {
            //USED AS AN ANIMATION EVENT
            base.ExecuteAttack();

        }

        public override void EndAttack()
        {
            //USED AS AN ANIMATION EVENT
            base.EndAttack();
        }
    }
}