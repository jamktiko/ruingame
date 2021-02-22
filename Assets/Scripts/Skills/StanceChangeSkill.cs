using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Data.Util;
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
    public class StanceChangeSkill : SkillExecute
    {
        PlayerHealth playerHealth;

        [SerializeField] private float resistance;
        [SerializeField] private float passiveAttackSpeed;
        [SerializeField] private float passiveResistance;

        private void Awake()
        {
            skillname = "Stance Change";
            damage = 20f;
            resistance = 5f;
            passiveAttackSpeed = 10f;
            passiveResistance = 10f;

            PlayerManager.Instance.ModifyAttackSpeed(passiveAttackSpeed, 1);
            PlayerManager.Instance.ModifyResistance(passiveResistance, 1);

            playerHealth = GetComponent<PlayerHealth>();
        }

        public override void Execute()
        {
            ApplyPersistentEffect(this);
        }

        public override void ApplyPersistentEffect(SkillExecute sk)
        {
            if (!onCooldown && playerHealth.CurrentHealth >= 40f)
            {
                base.ApplyPersistentEffect(sk);
                ModifyPlayerStats(1);
            }
        }

        public override void DeActivatePersistentEffect()
        {
            ModifyPlayerStats(0);
        }

        public override void ModifyPlayerStats(int type)
        {
            PlayerManager.Instance.ModifyDamage(damage, type);
            type = type == 1 ? 0 : 1;
            PlayerManager.Instance.ModifyResistance(resistance, type);
        }
    }
}