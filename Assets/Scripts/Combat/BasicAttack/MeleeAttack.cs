using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAttack", menuName = "Game/Combat/MeleeAttack")]
public class MeleeAttack : BaseAttack
{
    public override void AttackAllTargets(GameObject[] targets, BaseAttackHandler attacker)
    {
        foreach (GameObject target in targets)
        {
            Debug.Log(target.name);
            try
            {
                var targetHealth = target.GetComponent<EntityHealth>();
                targetHealth.DealDamage(this, attacker);
            }
            catch
            {
                Debug.Log("Target has no health!");
            }
        }
        KnockBackAllTargets(targets, attacker);
    }

    public override void KnockBackAllTargets(GameObject[] targets, BaseAttackHandler attacker)
    {
        if (knockBack)
        {
            foreach (GameObject target in targets)
            {
                try
                {
                    var kb = target.GetComponent<BaseMovement>();
                    kb.HandleKnockBack(attacker.gameObject.transform.position, knockBackStrength);
                }
                catch
                {
                    Debug.Log("Target has no knockback handling!");
                }
            }
        }
    }

    public baseAttackType BaseAttackType { get; } = baseAttackType.MELEE;
    
}