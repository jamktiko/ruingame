using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class PlayerAttack : Attack
    {
        private Rigidbody _characterRigidbody;
        private InputReader _inputReader;
        private float _playerDamage = default;
        private float _playerAttackSpeed = default;
        protected MovementController _movementControl;

        public float attackSwingForce;
        public int comboStep = 1;
        public int maximumCombo = 2;
        
        public override void Start()
        {
            base.Start();
            _inputReader = GameManager.Instance.playerInputReader;
            _movementControl = GetComponent<MovementController>();
            _characterRigidbody = GetComponent<Rigidbody>();
        }
        public override void OnEnable()
        {
            try
            {
                _inputReader.AttackEvent += AttemptAttack;
            }
            catch
            {
            }
        }

        public override void OnDisable()
        {
            _inputReader.AttackEvent -= AttemptAttack;
        }

        public override void AttemptAttack()
        {
            //ComboTimer that resets the combo if too long passes between attacks
            
            //Set damagecollider damage based on combo step?
            DamageCollider.damage = _playerDamage;
            if (_movementControl.GroundedEntity && !currentlyAttacking)
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
            EntityAnimator.Play("Attack" + comboStep);

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
        public void SetDamage(float amount)
        {
            _playerDamage = amount;
        }

        public void SetAttackSpeed(float amount)
        {
            _playerAttackSpeed = amount;
        }
    }
}