
using UnityEngine;

[CreateAssetMenu(fileName = "CasterAttack", menuName = "Game/Combat/CasterAttack")]
public class CasterAttack : BaseAttack
{
    public override void AttackAllTargets(GameObject[] targets, BaseAttackHandler attacker)
    {
        throw new System.NotImplementedException();
    }

    public override void KnockBackAllTargets(GameObject[] targets, BaseAttackHandler attacker)
    {
        throw new System.NotImplementedException();
    }

    public baseAttackType BaseAttackType { get; } = baseAttackType.CAST;
}
