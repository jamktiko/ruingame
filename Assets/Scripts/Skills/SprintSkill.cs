

using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Data.Util;
using UnityEngine;

namespace DefaultNamespace.Skills
{
    public class SprintSkill : SkillExecute
    {
        //Should contain UI IMAGE and Animation Clip
        public float SprintSpeed = 20f;
        public override void Execute(float duration)
        {
            // THIS LOCKS PLAYER MOVEMENT TO LAST INPUT OR FORCES MOVEMENT IF PLAYER IS NOT INPUTTING ANYTHING
            PlayerManager.Instance._playerMovement.OnDash(duration);
            
            //APPLIES ENHANCED MOVEMENT SPEED
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