using UnityEngine;

[CreateAssetMenu(fileName = "RangedAttack", menuName = "Game/Combat/RangedAttack")]
public class RangedAttack : BaseAttack
{
    public float shootForce;
    public GameObject projectilePrefab;
    public Transform firePoint;
        
    public override void AttackAllTargets(GameObject[] currentTargets, BaseAttackHandler attacker)
    {
        firePoint = attacker.firePoint;
        foreach (GameObject target in currentTargets)
        {
            var pb = Instantiate(projectilePrefab, firePoint);
            pb.transform.parent = null;
            ProjectileBehaviour p = pb.GetComponent<ProjectileBehaviour>();
            p.AllowedTargetTags = attacker.AllowedTargetTags;
            p.attacker = attacker;
            p.attack = this;
            var attackerTransform = attacker.gameObject.transform;
            var targetDirection = attackerTransform.forward;
            if (target != null)
            {
                targetDirection = -(attackerTransform.position - target.transform.position).normalized;
            }
            targetDirection.y = 0;
            try
            {
                p.GetComponent<Rigidbody>().AddForce(targetDirection * this.shootForce);
            }
            catch{}
        }
        KnockBackAllTargets(currentTargets, attacker);
    }

    public override void KnockBackAllTargets(GameObject[] targets, BaseAttackHandler attacker)
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

    public baseAttackType BaseAttackType { get; } = baseAttackType.RANGED;
}
