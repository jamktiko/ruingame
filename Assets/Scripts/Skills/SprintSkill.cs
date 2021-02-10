

using System.Dynamic;
using UnityEngine;

namespace DefaultNamespace.Skills
{
    public class SprintSkill : NonMonoSkill
    {
        public float SprintSpeed;
        public override void Execute()
        {
            ApplyPersistentEffect();
        }
        public override void ApplyPersistentEffect()
        {
            if (!onCooldown)
            {
                skillUser.gameObject.GetComponent<PlayerManager>().ModifyMovementSpeed(SprintSpeed, 1);
                skillUser.StartCoroutine("UsePersistentEffect");
            }
        }

        public override void DeActivatePersistentEffect()
        {
            skillUser.gameObject.GetComponent<PlayerManager>().ModifyMovementSpeed(SprintSpeed, 0);
        }
    }
}