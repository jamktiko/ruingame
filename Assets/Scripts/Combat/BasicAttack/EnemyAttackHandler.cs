
using System.Collections;
using Data.Util;
using UnityEngine;
[RequireComponent(typeof(AttackTargeting))]
    public class EnemyAttackHandler : BaseAttackHandler
    {
        public override void Awake()
        {
            _characterAnimator = GetComponentInChildren<Animator>();
            _characterRigidbody = GetComponent<Rigidbody>();
            _attackTargeting = GetComponent<AttackTargeting>();
            AllowedTargetTags = _attackTargeting.AllowedTargetTags;
        }
        
        //ON ATTACK
        //CHECK ATTACK CONDITIONS
        //ATTEMPTATTACK
        //STARTATTACK
        //EXECUTE ATTACK
        //START END ATTACK ROUTINE
        
        private bool CheckForCritical()
        {
            bool retVal = false;
            var diceRoll = Random.Range(0, 100);
            if (diceRoll < criticalHitChance)
            {
                retVal = true;
            }
            return retVal;
        }
        protected override float AttackDamageCalculation(IAttack AttackToCalculate)
        {
            var retVal = 0f;
            retVal += AttackToCalculate.baseDamage;
            retVal += _entityDamage;
            if (CheckForCritical())
            {
                retVal *= 2;
            }

            return retVal;
        }

    }
