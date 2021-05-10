using System.Collections;
using UnityEngine;

namespace DefaultNamespace.Skills
{
    public class BlizzardSkill : SkillExecute
    {

        [SerializeField] private float _attackRadius = 5f;
        [SerializeField] private float _knockbackForce = 10f;
        private float _attackDistance;
        private MeleeAttack blizzard;
        LayerMask enemyLayer;
        BaseAttackHandler attackHandler;

        protected override void Awake()
        {
            skillname = "Blizzard";
            animationClip = Resources.Load<AnimationClip>("Blizzard");
        }

        protected override void Start()
        {
            base.Start();
            damage = 50f;
            skillCooldown = 3f;
            _attackDistance = _attackRadius / 2f;
            enemyLayer = LayerMask.GetMask("EnemyLayer");
            attackHandler = GetComponent<BaseAttackHandler>();
            blizzard = Blizzard();
        }

        public override void Execute(float duration)
        {
            skillUser.usingSkill = true;
            iFrameDuration = duration;
            base.Execute(duration);
            try { ExecuteBlizzard(); }
            catch { Debug.Log("blizzard"); }
            skillUser.usingSkill = false;
        }

        public MeleeAttack Blizzard()
        {
            var _blizzard = ScriptableObject.CreateInstance<MeleeAttack>();
            _blizzard.Radius = _attackRadius;
            _blizzard.DamageType = baseDamageType.DIRECT;
            _blizzard.baseDamage = damage;
            _blizzard.KnockBack = true;
            _blizzard.KnockBackStrength = _knockbackForce;
            return _blizzard;
        }

        private void ExecuteBlizzard()
        {
            GameObject[] gos = FindEnemies();

            //foreach (var item in gos)
            //{
            //    Debug.Log(item.GetComponent<EnemyHealth>().currentHealth);
            //    Debug.Log(item.GetComponent<Enemy_StateMachine>().movementController._movementSpeed);
            //}

            if (gos.Length > 0)
            {
                blizzard.AttackAllTargets(gos, attackHandler);
                foreach (var enemyGo in gos)
                {
                    Enemy_StateMachine enemy = enemyGo.GetComponent<Enemy_StateMachine>();
                    StunnedState stunned = new StunnedState(enemy);
                    enemy.SetState(stunned);
                    float stunTime = stunned.StunTimer;
                    float movementSpeed = enemy.movementController._movementSpeed;
                    enemy.movementController._movementSpeed /= 2f;
                    //Debug.Log(enemyGo.GetComponent<EnemyHealth>().currentHealth);
                    //Debug.Log(enemy.movementController._movementSpeed);
                    StartCoroutine(NormalizeSpeed(enemy, movementSpeed, stunTime));
                }
            }
        }

        IEnumerator NormalizeSpeed(Enemy_StateMachine enemy, float initialSpeed, float stunTime)
        {
            yield return new WaitForSeconds(2f + stunTime);
            enemy.movementController._movementSpeed = initialSpeed;
            //Debug.Log(enemy.movementController._movementSpeed);
        }

        GameObject[] FindEnemies()
        {
            Vector3 p1 = transform.position;
            Vector3 p2 = transform.position + Vector3.up * 2f;
            Collider[] colliders = Physics.OverlapCapsule(p1, p2, _attackRadius, enemyLayer, QueryTriggerInteraction.Collide);
            GameObject[] enmyGos = new GameObject[colliders.Length];

            for (int i = 0; i < colliders.Length; i++)
            {
                enmyGos[i] = colliders[i].gameObject;
            }
            return enmyGos;
        }
    }

}