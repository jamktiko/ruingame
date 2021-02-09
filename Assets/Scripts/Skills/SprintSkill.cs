

using UnityEngine;

namespace DefaultNamespace.Skills
{
  
    public class SprintSkill : SkillExecute
    {
        public float sprintSpeed;
        public override void Execute()
        {
            StartCoroutine("SkillPersistentEffect");
        }

        public override void ApplyPersistentEffect()
        {
            skillUser.gameObject.GetComponent<PlayerManager>().ModifyMovementSpeed(sprintSpeed, 1);
        }

        public override void DeActivatePersistentEffect()
        {
            skillUser.gameObject.GetComponent<PlayerManager>().ModifyMovementSpeed(sprintSpeed, 0);
        }
    }
}