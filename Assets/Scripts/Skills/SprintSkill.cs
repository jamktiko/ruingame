

using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

namespace DefaultNamespace.Skills
{
    public class SprintSkill : SkillExecute
    {
        public float SprintSpeed = 50f;
        public override void Execute()
        {
            ApplyPersistentEffect();
        }
        public override void ApplyPersistentEffect()
        {
            if (!onCooldown)
            {
                PlayerManager.Instance.ModifyMovementSpeed(SprintSpeed, 1);
                IEnumerator coroutine = skillUser.UsePersistentEffect(this);
                skillUser.StartCoroutine(coroutine);
            }
        }

        public override void DeActivatePersistentEffect()
        {
            PlayerManager.Instance.ModifyMovementSpeed(SprintSpeed, 0);
        }
    }
}