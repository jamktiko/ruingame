
using UnityEngine;

namespace DefaultNamespace
{
    
public class Attack : MonoBehaviour
{
    public DamageCollider DamageCollider;
    protected MeshRenderer WeaponMesh;
    [HideInInspector]
    public bool currentlyAttacking;
    protected Animator EntityAnimator;

    public virtual void Start()
    {
        DamageCollider = GetComponentInChildren<DamageCollider>();
        EntityAnimator = GetComponentInChildren<Animator>();
    }
    public virtual void ExecuteAttack()
    {
        DamageCollider.EnableDamage();
        //WeaponMesh.enabled = true;
    }

    public virtual void AttemptAttack()
    {
        try
        {
            ExecuteAttackAnimation();
        }
        catch
        {
            
        }
        DamageCollider.EnableDamage();
        currentlyAttacking = true;
        Invoke("EndAttack", 1f);
    }
    public virtual void ExecuteAttackAnimation()
    {
        EntityAnimator.Play("Attack1");
    }

    public virtual void EndAttack()
    {
        currentlyAttacking = false;
    }
}
}