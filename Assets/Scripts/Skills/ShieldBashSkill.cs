using System.Collections;
using UnityEngine;

namespace DefaultNamespace.Skills
{
    public class ShieldBashSkill : SkillExecute
    {
        [SerializeField] private bool _stopPlayerAfterDash = true;
        [SerializeField] private float _sprintSpeed = 20f;
        private float _bashRadius = 1f;
        private MeleeAttack _shieldBash;
        private LayerMask _enemyLayer;

        protected override void Awake()
        {
            skillname = "Shield bash";
            animationClip = Resources.Load<AnimationClip>("P_Dash");
        }

        protected override void Start()
        {
            try
            {
                base.Start();
                skillCooldown = 3f;
                duration = animationClip.length;
                iFrameDuration = duration;
                _sprintSpeed *= 1.2f;
                _enemyLayer = LayerMask.GetMask("EnemyLayer");
                _shieldBash = ShieldBashAttack();
            }
            catch { Debug.Log("Shieldbash Start() error"); }
        }


        public override void Execute(float duration)
        {
            base.Execute(duration);
            PlayerManager.Instance.ModifyMovementSpeed(_sprintSpeed, 1);
            PlayerManager.Instance._playerMovement.OnDash(duration);
            WhileSkillActive();
            StartCoroutine(CheckBashAttack(duration));
        }

        public override void WhileSkillActive()
        {

            skillUser.usingSkill = true;
            IEnumerator coroutine = skillUser.UsePersistentEffect(this);
            skillUser.StartCoroutine(coroutine);
        }

        public override void DeActivateSkillActive()
        {
            skillUser.usingSkill = false;
            PlayerManager.Instance.ModifyMovementSpeed(_sprintSpeed, 0);

            if (_stopPlayerAfterDash)
                playerRb.velocity = Vector3.zero;
        }

        IEnumerator CheckBashAttack(float duration)
        {
            for (float i = 0; i < duration; i++)
            {
                Vector3 p1 = transform.position;
                Vector3 p2 = transform.position + Vector3.up * 2;
                try
                {
                    if (Physics.CheckCapsule(p1, p2, _bashRadius, _enemyLayer))
                        skillUser.attackHandler.HandleAttack(_shieldBash);
                }
                catch { Debug.Log("Shieldbash"); }

                i += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
        }

        private MeleeAttack ShieldBashAttack()
        {
            var sb = ScriptableObject.CreateInstance<MeleeAttack>();
            sb.baseDamage = 10f;
            sb.Radius = _bashRadius;
            sb.TargetingType = basetargetingType.NEAREST;
            return sb;
        }
    }
}