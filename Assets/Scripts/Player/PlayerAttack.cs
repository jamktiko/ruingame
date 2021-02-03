﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerAttack : Attack
    {
        [HideInInspector]
        public InputReader _inputReader;

        [SerializeField] protected Movement _movementControl;

        public int comboStep = 1;
        public int maximumCombo = 6;

        public override void OnEnable()
        {
            _movementControl = GetComponent<Movement>();
            _inputReader.attackEvent += AttemptAttack;
        }

        public override void OnDisable()
        {
            _inputReader.attackEvent -= AttemptAttack;
        }

        public override void AttemptAttack()
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
        public override void ExecuteAttackAnimation()
        {
            //Triggers animation event
            _entityAnimator.Play("Attack" + comboStep);

        }
        public override void ExecuteAttack()
        {
            //USED AS AN ANIMATION EVENT
            base.ExecuteAttack();
            _movementControl.enabled = false;
        }

        public override void EndAttack()
        {
            //USED AS AN ANIMATION EVENT
            _movementControl.enabled = true;
            base.EndAttack();
        }
    }
}