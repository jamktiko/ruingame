using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    [RequireComponent(typeof(AttackTargeting))]
    public class BaseAttackHandler : MonoBehaviour
    {
        public BaseAttack baseAttack;
        public bool attacking = false;
        
        protected float _entityDamage = 10f;
        protected float _entityAttackSpeed = default;
        public Animator _characterAnimator;
        protected Rigidbody _characterRigidbody;
        
        protected AttackTargeting _attackTargeting;
        protected BaseAttack currentAttack;
        protected GameObject[] currentTargets;

        public virtual void Awake()
        {
            _characterAnimator = GetComponentInChildren<Animator>();
            _characterRigidbody = GetComponent<Rigidbody>();
            _attackTargeting = GetComponent<AttackTargeting>();
        }

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
            try { currentTargets = TargetAttack(currentAttack); }
            catch {Debug.Log("No targets found!");}
            ExecuteAttack();
            StartCoroutine(EndAttackRoutine(GetAttackEndDuration()));
        }

        protected virtual float GetAttackEndDuration()
        {
            return 2f;
        }
        protected virtual BaseAttack GetAttack()
        {
            return baseAttack;
        }

        protected virtual GameObject[] TargetAttack(BaseAttack attack)
        {
           return _attackTargeting.HandleTargeting(attack);
        }

        protected virtual void ExecuteAttack()
        {
            /*try {_characterAnimator.Play("Attack");}
            catch {Debug.Log("No animation found!");*/
            HandleAttack();
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

        //New and improved attack logic!
        public void HandleAttack()
        {
            try
            {
                DamageAllCurrentTargets(currentTargets, currentAttack);
            }
            catch
            {
                Debug.Log("No current target or attack!");
            }
        }
        private void DamageAllCurrentTargets(GameObject[] targets, BaseAttack attack)
        {
            foreach (GameObject target in targets)
            {
                try
                {
                    var targetHealth = target.GetComponent<Health>();
                    targetHealth.DealDamage(attack.baseDamage * _entityDamage);
                }
                catch
                {
                    Debug.Log("Target has no health!");
                }
            }

            KnockBackAllTargets(targets, attack);
            currentAttack = null;
            currentTargets = null;
        }
        private void KnockBackAllTargets(GameObject[] targets, BaseAttack attack)
        {
            foreach (GameObject target in targets)
            {
                try
                {
                    var kb = target.GetComponent<KnockbackHandler>();
                    kb.HandleKnockBack(transform.position, attack.knockBackStrength);
                }
                catch
                {
                    Debug.Log("Target has no knockback handling!");
                }
            }
        }
        
    }
}
