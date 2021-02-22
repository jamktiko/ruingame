

using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Data.Util;
using UnityEngine;

namespace DefaultNamespace.Skills
{
    public class SprintSkill : SkillExecute
    {
        public override void Execute()
        {
            ApplyPersistentEffect(this);
        }

        public override void ApplyPersistentEffect(SkillExecute sk)
        {
            if (!onCooldown)
            {
                PlayerManager.Instance.ModifyMovementSpeed(SprintSpeed, 1);
                base.ApplyPersistentEffect(sk);
            }
        }

        public override void DeActivatePersistentEffect()
        {
            PlayerManager.Instance.ModifyMovementSpeed(SprintSpeed, 0);
        }
    }
}