using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Skills
{
    public class WhirlwindSkill : SkillExecute
    {

        [SerializeField] private float _attackRadius = 5f;
        [SerializeField] private float _knockbackForce = 10f;
        private float _attackDistance;
        private MeleeAttack whirlwind;
        LayerMask enemyLayer;
        BaseAttackHandler attackHandler;

        protected override void Start()
        {
            base.Start();
            damage = 50f;
            skillCooldown = 3f;
            _attackDistance = _attackRadius / 2f;
            enemyLayer = LayerMask.GetMask("EnemyLayer");
            attackHandler = GetComponent<BaseAttackHandler>();
            whirlwind = Whirlwind();
        }

        public override void Execute(float duration)
        {
            skillUser.usingSkill = true;
            iFrameDuration = duration;
            base.Execute(duration);
            try { ExecuteWhirlWind(); }
            catch { Debug.Log("whirlwind"); }
            skillUser.usingSkill = false;
        }
        protected override void Awake()
        {
            skillname = "Whirlwind";
            animationClip = Resources.Load<AnimationClip>("WhirlWind");
        }
        public MeleeAttack Whirlwind()
        {
            var _whirlwind = ScriptableObject.CreateInstance<MeleeAttack>();
            _whirlwind.TargetingType = basetargetingType.AOE;
            _whirlwind.Radius = _attackRadius;
            _whirlwind.DamageType = baseDamageType.DIRECT;
            _whirlwind.baseDamage = damage;
            _whirlwind.KnockBack = true;
            _whirlwind.KnockBackStrength = _knockbackForce;
            return _whirlwind;
        }



        private void ExecuteWhirlWind()
        {
            GameObject[] gos = FindEnemies();
            if (gos.Length > 0)
            {
                try
                {
                    whirlwind.AttackAllTargets(gos, attackHandler);
                    foreach (var enemyGo in gos)
                    {
                        Enemy_StateMachine enemy = enemyGo.GetComponent<Enemy_StateMachine>();
                        enemy.SetState(new StunnedState(enemy));
                    }
                }
                catch { Debug.Log("couldn't execute whirlwind"); }
            }
        }

        GameObject[] FindEnemies()
        {
            Vector3 p1 = transform.position + transform.forward * _attackDistance;
            Vector3 p2 = transform.position + transform.forward * _attackDistance + Vector3.up * 2f;
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