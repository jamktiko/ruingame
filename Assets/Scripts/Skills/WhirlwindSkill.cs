using System.Collections;
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

        private void Awake()
        {
            damage = 20f;
        }

        public override void Execute()
        {
            targeting.AttackEnemies(attackAllEnemies, attackRadius, attackDistance, damage);
        }
    }

}