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

        [SerializeField] private float _attackRadius;
        [SerializeField] private float _knockbackForce = 20f;
        private float _attackDistance = 0f;

        protected override void Start()
        {
            base.Start();
            damage = 20f;
            _attackRadius = _knockbackForce / 2f;
        }

        public override void Execute()
        {
            base.Execute();
             skillUser.usingSkill = true;
             UpdateAttackRadius();
             DamageAndKnockbackBasedOnDistanceFromPlayer();
             skillUser.usingSkill = false;
        }
        private void DamageAndKnockbackBasedOnDistanceFromPlayer()
        {
            List<GameObject> enemyList = targeting.GetListOfEnemiesInRange(_attackRadius, _attackDistance);

            foreach (var go in enemyList)
            {
                float distance = Vector3.Distance(go.transform.position, transform.position);
                float kbForce = CalculateForce(distance);
                float damage = CalculateDamage(distance);

                if (go.TryGetComponent(out KnockbackHandler kbh))
                {
                    kbh.HandleKnockBack(transform.position, kbForce);
                }

                targeting.DamageEnemy(go, damage);
            }
        }

        //Damage and KnockBack are at the edge of AttackRadius 0
        float CalculateForce(float distance)
        {
            return _knockbackForce - distance * 2f;
        }

        float CalculateDamage(float distance)
        {
            float per = distance / _attackRadius;
            return damage * per;
        }

        private void UpdateAttackRadius() => _attackRadius = _knockbackForce / 2f;
    }

}