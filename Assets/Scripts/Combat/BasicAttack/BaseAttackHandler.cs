
using System.Collections;
using Data.Util;
using UnityEngine;
[RequireComponent(typeof(AttackTargeting))]
    public class BaseAttackHandler : MonoBehaviour
    {
        public BaseAttack baseAttack;
        public bool attacking = false;
        public float criticalHitChance;
        
        public string[] AllowedTargetTags;
        public float _entityDamage { get; protected set; } = 10f;
        protected float _entityAttackSpeed = 2f;
        public Animator _characterAnimator;
        protected Rigidbody _characterRigidbody;
        
        protected AttackTargeting _attackTargeting;
        public Transform firePoint;
        public IAttack currentAttack;
        protected GameObject[] currentTargets;

        public virtual void Awake()
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
        public virtual void OnEnable()
        {

        }

        public virtual void OnDisable()
        {
            
        }

        protected virtual void OnAttack()
        {
            if (CheckAttackConditions())
            {
                AttemptAttack();
            }
        }
        public virtual void AttemptAttack()
        {
            StartAttack();
            try { currentAttack = GetAttack(); }
            catch {Debug.Log("No attack to execute!");}
            TurnTowardsNearest();
            ExecuteAttack();
            StartCoroutine(EndAttackRoutine(GetAttackEndDuration()));
        }

        protected virtual void TurnTowardsNearest()
        {
            
        }
        protected virtual float GetAttackEndDuration()
        {
            return 2f;
        }
        protected virtual IAttack GetAttack()
        {
            return baseAttack;
        }

        protected virtual GameObject[] TargetAttack(IAttack attack)
        {
           return _attackTargeting.HandleTargeting(attack);
        }

        public virtual void ExecuteAttack()
        {
            /*try {_characterAnimator.Play("Attack");}
            catch {Debug.Log("No animation found!");*/
            HandleAttack(baseAttack);
        }
        
        protected virtual bool CheckAttackConditions()
        {
            if (attacking)
                return false;
            return true;
        }
        public virtual void EndAttack()
        {
            attacking = false;
        }
        protected virtual void StartAttack()
        {
            attacking = true;
        }

        public void SetDamage(float amount)
        {
            _entityDamage = amount;
        }

        public void SetAttackSpeed(float amount)
        {
            _entityAttackSpeed = amount;
        }

        public virtual IEnumerator EndAttackRoutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            EndAttack();
        }
        
        public virtual void HandleAttack(IAttack AttackToExecute)
        {
            AttackToExecute.inflictedDamage = AttackDamageCalculation(AttackToExecute);
            AttackToExecute.AttackAllTargets(TargetAttack(AttackToExecute), this);
            ClearTargets();
        }

        public virtual void HandleSelfAttack(IAttack AttackToExecute)
        {
            AttackToExecute.inflictedDamage = AttackToExecute.baseDamage;
            AttackToExecute.AttackAllTargets(TargetAttack(AttackToExecute), this);
        }

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
        protected virtual float AttackDamageCalculation(IAttack AttackToCalculate)
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

        protected void ClearTargets()
        {

        }
    }
