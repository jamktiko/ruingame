using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerAttack : Attack
    {
        [SerializeField]
        private InputReader _inputReader;

        [SerializeField] private Animator _playerAnimator;
        public override void Start()
        {
            _damageCollider = GetComponentInChildren<DamageCollider>();
        }

        public override void OnEnable()
        {
            _inputReader.attackEvent += ExecuteAttack;
        }

        public override void OnDisable()
        {
            _inputReader.attackEvent -= ExecuteAttack;
        }

        public override void ExecuteAttack()
        {
            base.ExecuteAttack();
            _playerAnimator.SetTrigger("Attack");
        }
    }
}