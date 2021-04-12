
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEditor;

public enum DashType { DashWithVelocity, DashWithTranslate }
namespace DefaultNamespace
{
    public class SprintSkill : SkillExecute
    {
        public event UnityAction EndSprintEvent = delegate { };
        public DashType dashType;

        //Should contain UI IMAGE and Animation Clip
        [SerializeField] private bool _stopPlayerAfterDash = true;
        public float SprintSpeed = 20f;

        protected override void Start()
        {
            base.Start();
            duration = 0.5f;
        }

        public override void Execute(float duration)
        {
            // THIS LOCKS PLAYER MOVEMENT TO LAST INPUT OR FORCES MOVEMENT IF PLAYER IS NOT INPUTTING ANYTHING
            base.Execute();

            switch (dashType)
            {
                case DashType.DashWithVelocity:
                    SprintSpeed = 20f;
                    PlayerManager.Instance._playerMovement.OnDash(duration);
                    break;
                case DashType.DashWithTranslate:
                    SprintSpeed = 10f;
                    PlayerManager.Instance._playerMovement.OnDash2(duration);
                    break;
                default:
                    break;
            }
            //APPLIES ENHANCED MOVEMENT SPEED
            WhileSkillActive();
        }
        public override void WhileSkillActive()
        {
            skillUser.usingSkill = true;
            PlayerManager.Instance.ModifyMovementSpeed(SprintSpeed, 1);
            IEnumerator coroutine = skillUser.UsePersistentEffect(this);
            skillUser.StartCoroutine(coroutine);
        }

        public override void DeActivateSkillActive()
        {
            skillUser.usingSkill = false;
            PlayerManager.Instance.ModifyMovementSpeed(SprintSpeed, 0);

            if (_stopPlayerAfterDash)
                playerRb.velocity = Vector3.zero;

            EndSprintEvent.Invoke();
        }
    }
}