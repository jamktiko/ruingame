﻿using System.Collections;
using UnityEngine;

#region Skill description
/*
Shield Bash

Lore: Push forward with all your might and summon a spirit to protect you in the process.

Concept: Dash 1.5x the regular amount, stop if it hits an enemy collider dealing slight damage. Gain temporary shield to avoid damage for a short time.
Type: Defensive spell
Effects: Temporary shield

Justification: By utilizing rituals Humans learned from other races, they gained ability to enclose and control spirits within objects.
*/
#endregion
namespace DefaultNamespace.Skills
{
    public class ShieldBashSkill : SkillExecute
    {
        [SerializeField] private bool _stopPlayerAfterDash = true;
        [SerializeField] private bool _stopPlayerOnEnemyCollision = true;
        [SerializeField] private float _knockbackForce = 15f;
        [SerializeField] private float _sprintSpeed = 30f;
        private bool _onSkill = false;

        private MeleeAttack _shieldBash;
        protected override void Start()
        {
            base.Start();
            _sprintSpeed *= 1.2f;
            damage = 10f;
            duration = 0.5f;
            iFrameDuration = 1f;
            _shieldBash = ShieldBashAttack();
        }

        public override void Execute()
        {
            base.Execute();
            PlayerManager.Instance._playerMovement.OnDash(duration);
            WhileSkillActive();
        }
        protected override void Awake()
        {
            skillname = "Shield bash";
            animationClip = Resources.Load<AnimationClip>("P_Dash");
        }
        public override void WhileSkillActive()
        {
            if (!onCooldown)
            {
                skillUser.usingSkill = true;
                _onSkill = true;
                PlayerManager.Instance.ModifyMovementSpeed(_sprintSpeed, 1);
                IEnumerator coroutine = skillUser.UsePersistentEffect(this);
                skillUser.StartCoroutine(coroutine);
            }
        }

        public override void DeActivateSkillActive()
        {
            skillUser.usingSkill = false;
            _onSkill = false;
            PlayerManager.Instance.ModifyMovementSpeed(_sprintSpeed, 0);

            if (_stopPlayerAfterDash)
                playerRb.velocity = Vector3.zero;
        }


        private void OnCollisionEnter(Collision collision)
        {
            var go = collision.gameObject;
            if (_onSkill && go.CompareTag("Enemy"))
            {
                EnemyHealth eh = go.GetComponent<EnemyHealth>();
                BaseMovement kbh = go.GetComponent<BaseMovement>();
                kbh.HandleKnockBack(transform.position, _knockbackForce);
                eh.DealDamage(_shieldBash, skillUser.attackHandler);

                if (_stopPlayerOnEnemyCollision)
                    playerRb.velocity = Vector3.zero;
            }
        }

        private MeleeAttack ShieldBashAttack()
        {
            var sb = ScriptableObject.CreateInstance<MeleeAttack>();
            sb.baseDamage = 10f;
            sb.TargetingType = basetargetingType.NEAREST;
            return sb;
        }
    }
}