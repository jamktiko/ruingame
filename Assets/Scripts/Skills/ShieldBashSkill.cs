using System.Collections;
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
        [SerializeField] protected float SprintSpeed = 20f;
        bool onSkill = false;

        private void Awake()
        {
            skillname = "Shield Bash";
            SprintSpeed *= 3f;
            damage = 10f;
        }
        public override void Execute()
        {
            WhileSkillActive();
        }

        public override void WhileSkillActive()
        {
            if (!onCooldown)
            {
                Debug.Log("Starting ShieldBash");
                skillUser.usingSkill = true;
                PlayerManager.Instance.ModifyMovementSpeed(SprintSpeed, 1);
                Debug.Log(PlayerManager.Instance);
                onSkill = true;
                IEnumerator coroutine = skillUser.UsePersistentEffect(this);
                skillUser.StartCoroutine(coroutine);
            }
        }
        public override void DeActivateSkillActive()
        {
            Debug.Log("Disabling ShieldBash");
            PlayerManager.Instance.ModifyMovementSpeed(SprintSpeed, 0);
            onSkill = false;
            skillUser.usingSkill = false;
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