
using System.Collections;
using Data.Util;
using DefaultNamespace;
using UnityEngine;
[RequireComponent(typeof(AttackTargeting))]
    public class EnemyAttackHandler : BaseAttackHandler
    {
        private Enemy_StateMachine _enemyController;
        public EnemyMovement movementController;
        public override void Awake()
        {
            _characterAnimator = GetComponentInChildren<Animator>();
            _characterRigidbody = GetComponent<Rigidbody>();
            _attackTargeting = GetComponent<AttackTargeting>();
            AllowedTargetTags = _attackTargeting.AllowedTargetTags;
            _enemyController = GetComponent<Enemy_StateMachine>();
            movementController = GetComponent<EnemyMovement>();

            currentAttack = baseAttack;
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
        public override void ExecuteAttack()
        {
            _characterAnimator.SetFloat("attackSpeed", _entityAttackSpeed);
            _characterAnimator.Play(_enemyController.enemyType + "Attack");
        }
        public override void AttemptAttack()
        {
            StartAttack();
            TurnTowardsNearest();
            ExecuteAttack();
        }
        protected override void TurnTowardsNearest()
        {
            var nearestTarget = _attackTargeting.FindNearestTargetInRadius(baseAttack.radius + 10f);
            movementController.RotateTowardsMovement(nearestTarget, 1000f);
        }
        protected override void StartAttack()
        {
            base.StartAttack(); 
            movementController.attacking = attacking;
        }
        public override void EndAttack()
        {
            base.EndAttack();
            movementController.attacking = attacking;
            Debug.Log("Stopped attacking!");
            _enemyController.SetState(new MoveTowardsPlayerState(_enemyController));
        }
        public override void HandleAttack(IAttack AttackToExecute)
        {
            AttackToExecute.inflictedDamage = AttackDamageCalculation(baseAttack);
            AttackToExecute.AttackAllTargets(TargetAttack(baseAttack), this);
        }

    }
