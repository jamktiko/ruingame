using System.Collections;
using UnityEngine;

#region Skill description
/*
Stance Change

Lore: By drinking the blood of an animal of unknown origin, your reflexes improve significantly allowing you to focus on avoiding critical hits and dealing increased damage.Enter Aggro mode to clear away your enemies while dealing minimal damage to yourself.

Concept: Passively increases resistances and ATK, activating aggressive mode disables resistances while increasing damage significantly.Useful in defeating enemies faster before they are able to deal damage to you.Aggro mode cannot be used when under 40% HP.

Type: Run-permanent passive
Status: +Physical resistance, +Increased ATK, -Self-damage

Justification: The effects of the blood wear off once magical power of three skills are present.
*/
#endregion
namespace DefaultNamespace.Skills
{
    public class BlizzardSkill: SkillExecute
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
            try { skillUser.attackHandler.HandleAttack(whirlwind); }
            catch { Debug.Log("whirlwind"); }
            skillUser.usingSkill = false;
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

        protected override void Awake()
        {
            skillname = "Blizzard";
            animationClip = Resources.Load<AnimationClip>("Blizzard");
        }

    }
}