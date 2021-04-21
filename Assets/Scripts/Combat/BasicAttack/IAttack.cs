
using UnityEngine;

public interface IAttack
{
    basetargetingType targetingType { get; set; }
    
    baseDamageType damageType { get; set; }
    
    float baseDamage { get; set; }
    
    float inflictedDamage { get; set; }
    
    float radius { get; set; }

    float knockBackStrength { get; set; }
    bool knockBack { get; set; }

    float range { get; set; }

    void AttackAllTargets(GameObject[] targets, BaseAttackHandler attacker);
    void KnockBackAllTargets(GameObject[] targets, BaseAttackHandler attacker);
    

}
public enum basetargetingType 
{
    AOE,
    NEAREST,
    CONE,
    SELF
}

public enum baseAttackType
{
    MELEE,
    RANGED,
    CAST
}

public enum baseDamageType
{
    PURE,
    PHYSICAL,
    DIRECT
}