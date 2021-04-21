
using UnityEngine;

public abstract class BaseAttack : ScriptableObject, IAttack
{
    public basetargetingType TargetingType;
    public basetargetingType targetingType
    {
        get { return TargetingType;}
        set { TargetingType = value; }
    }
    public baseDamageType DamageType;

    public baseDamageType damageType
    {
        get { return DamageType;}
        set { DamageType = value; }
    }
    public float BaseDamage;
    public float baseDamage
    {
        get { return BaseDamage;}
        set { BaseDamage = value; }
    }
    public float InflictedDamage;
    public float inflictedDamage
    {
        get { return InflictedDamage;}
        set { InflictedDamage = value; }
    }

    public float Radius;
    public float radius     
    {
        get { return Radius;}
        set { Radius = value; }
    }

    public float KnockBackStrength;
    public float knockBackStrength    
    {
        get { return KnockBackStrength;}
        set { KnockBackStrength = value; }
    }
    public bool KnockBack;
    public bool knockBack    
    {
        get { return KnockBack;}
        set { KnockBack = value; }
    }


    public float Range;
    public float range   
    {
        get { return Range;}
        set { Range = value; }
    }

    public abstract void AttackAllTargets(GameObject[] targets, BaseAttackHandler attacker);

    public abstract void KnockBackAllTargets(GameObject[] targets, BaseAttackHandler attacker);

}
