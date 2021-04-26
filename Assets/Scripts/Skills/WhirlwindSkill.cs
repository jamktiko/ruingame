using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Skill description
/*
Whirlwind

Lore: By activating an enhanced wind tattoo in their forearm, the adventurer calls forth a strong wind to push enemies back.

Concept: 270 degree knockback with middle point at the direction the player is facing at the moment. Clear space for the player to perform actions or/and deal AOE damage by punching air.

Type: Utility spell

Dmg: Medium damage (TBD)

Effects: Knockback (range TBD)

Justification: Humans were not able to use magic on their own, so adventurers have to rely on magic items or imbued enchantments to perform spell and abilities.

Problems: 270 degree knockback arc
 */
#endregion
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
            skillname = "WhirlWind";
            whirlwind = Whirlwind();
            damage = 50f;
            skillCooldown = 1f;
            _attackDistance = _attackRadius / 2f;
            enemyLayer = LayerMask.GetMask("EnemyLayer");
            attackHandler = GetComponent<BaseAttackHandler>();
        }

        public override void Execute(float duration)
        {
            skillUser.usingSkill = true;
            iFrameDuration = duration;
            base.Execute(duration);
            ExecuteWhirlWind();
            skillUser.usingSkill = false;
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

        public MeleeAttack Whirlwind()
        {
            var _whirlwind = ScriptableObject.CreateInstance<MeleeAttack>();
            _whirlwind.DamageType = baseDamageType.DIRECT;
            _whirlwind.baseDamage = damage;
            _whirlwind.KnockBack = true;
            _whirlwind.KnockBackStrength = _knockbackForce;
            return _whirlwind;
        }

    }

}