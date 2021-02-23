

using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Data.Util;
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
        bool onSkill = false;

        private void Awake()
        {
            skillname = "Shield Bash";
            SprintSpeed *= 1.5f;
            damage = 10f;
        }
        public override void Execute()
        {
            ApplyPersistentEffect(this);
        }

        public override void ApplyPersistentEffect(SkillExecute sk)
        {
            if (!onCooldown)
            {
                base.ApplyPersistentEffect(sk);
                PlayerManager.Instance.ModifyMovementSpeed(SprintSpeed, 1);
                playerHealth.AddIFrame(duration);
                onSkill = true;
            }
        }

        public override void DeActivatePersistentEffect()
        {
            PlayerManager.Instance.ModifyMovementSpeed(SprintSpeed, 0);
            onSkill = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var enemyGO = collision.gameObject;
            if (onSkill && enemyGO.CompareTag("Enemy"))
            {
                var tr = enemyGO.GetComponent<EnemyHealth>();
                tr.DealDamage(damage);
            }
        }
    }
}