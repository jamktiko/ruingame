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
    public class StanceChangeSkill : SkillExecute
    {

        [SerializeField] private float _damageResistance;
        [SerializeField] private float _passiveAttackSpeed;
        [SerializeField] private float _passiveResistance;
        [SerializeField] private float _selfDamage;

        protected override void Start()
        {
            base.Start();
            skillname = "Stance Change";
            damage = 50f;
            _damageResistance = 5f;
            _passiveAttackSpeed = 0.5f;
            _passiveResistance = 10f;
            _selfDamage = 20f;
            PlayerManager.Instance.ModifyAttackSpeed(_passiveAttackSpeed, 1);
            PlayerManager.Instance.ModifyResistance(_passiveResistance, 1);
        }

        public override void Execute()
        {
            if (playerHealth.CurrentHealth >= 40f)
            {
                base.Execute();
                WhileSkillActive();
            }
        }

        public override void WhileSkillActive()
        {
            skillUser.usingSkill = true;
                IEnumerator coroutine = skillUser.UsePersistentEffect(this);
                skillUser.StartCoroutine(coroutine);
                ModifyPlayerStats(1);

                //Might deal negative-negative damage to player. Regfactoring in progress
                playerHealth.DealDamage(_selfDamage);
        }
        public override void DeActivateSkillActive()
        {
            skillUser.usingSkill = false;
            ModifyPlayerStats(0);
        }

        public override void ModifyPlayerStats(int type)
        {
            PlayerManager.Instance.ModifyDamage(damage, type);
            PlayerManager.Instance.ModifyResistance(-_damageResistance, type);
        }
    }
}