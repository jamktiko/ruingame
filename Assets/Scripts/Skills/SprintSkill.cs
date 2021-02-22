

using System.Collections;


namespace DefaultNamespace
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
            WhileSkillActive();
        }
        public override void WhileSkillActive()
        {
            if (!onCooldown)
            {
                skillUser.usingSkill = true;
                PlayerManager.Instance.ModifyMovementSpeed(SprintSpeed, 1);
                IEnumerator coroutine = skillUser.UsePersistentEffect(this);
                skillUser.StartCoroutine(coroutine);
            }
        }

        public override void DeActivateSkillActive()
        {
            skillUser.usingSkill = false;
            PlayerManager.Instance.ModifyMovementSpeed(SprintSpeed, 0);
        }
    }
}