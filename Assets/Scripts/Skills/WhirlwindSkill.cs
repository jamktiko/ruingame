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

        protected override void Start()
        {
            base.Start();
            skillname = "WhirlWind";
            damage = 20f;
            skillCooldown = 5f;
            _attackDistance = _attackRadius / 2f;
        }

        public override void Execute(float duration)
        {
            skillUser.usingSkill = true;
            iFrameDuration = duration;
            base.Execute(duration);
            DamageAndKnockback();
            skillUser.usingSkill = false;
        }

        private void DamageAndKnockback()
        {
            Vector3 p1 = transform.position + transform.forward * _attackDistance;
            Vector3 p2 = transform.position + transform.forward * _attackDistance + Vector3.up * 2f;
            Collider[] colliders = targeting.FindColliderOverlaps(p1, p2, _attackRadius);
            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent(out BaseMovement kbh))
                {
                    kbh.HandleKnockBack(transform.position, _knockbackForce);
                }
                if (collider.gameObject.TryGetComponent(out Enemy_StateMachine enemy))
                {
                    enemy.SetState(new StunnedState(enemy));
                }
                targeting.DamageEnemy(collider.gameObject, damage);
            }
        }

    }

}