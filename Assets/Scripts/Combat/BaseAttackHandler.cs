
using System.Collections;

using UnityEngine;


namespace DefaultNamespace
{
    [RequireComponent(typeof(AttackTargeting))]
    public class BaseAttackHandler : MonoBehaviour
    {
        public BaseAttack baseAttack;
        public bool attacking = false;
        public Transform firePoint;
        
        protected float _entityDamage = 10f;
        protected float _entityAttackSpeed = 2f;
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
            try { currentTargets = TargetAttack(currentAttack); }
            catch {Debug.Log("No targets found!");}

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
            if (_characterAnimator.GetFloat("attackCancelFloat") > 0)
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

        public void HandleDamage()
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
        //New and improved attack logic!
        public void HandleAttack()
        {
            switch (currentAttack.attackType)
            {
                case BaseAttack.baseAttackType.RANGED:
                    ShootProjectile();
                    break;
                case BaseAttack.baseAttackType.PHYSICAL:
                    HandleDamage();
                    break;
            }
        }

        public void ShootProjectile()
        {
            foreach (GameObject target in currentTargets)
            {
                var pb = Instantiate(currentAttack.projectilePrefab, firePoint);
                pb.transform.parent = null;
                ProjectileBehaviour p = pb.GetComponent<ProjectileBehaviour>();
                p.damage = (currentAttack.baseDamage * _entityDamage);
                p.AllowedTargetTags = _attackTargeting.AllowedTargetTags;
                var targetDirection = transform.forward;
                if (target != null)
                {
                    targetDirection = -(transform.position - target.transform.position).normalized;
                }
                targetDirection.y = 0;
                try
                {
                    p.GetComponent<Rigidbody>().AddForce(targetDirection * currentAttack.shootForce);
                }
                catch{}
            }
            ClearTargets();
        }
        protected virtual void DamageAllCurrentTargets(GameObject[] targets, BaseAttack attack)
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
            ClearTargets();
        }
        protected void KnockBackAllTargets(GameObject[] targets, BaseAttack attack)
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

        protected void ClearTargets()
        {
            currentAttack = null;
            currentTargets = null;
        }
    }
}
