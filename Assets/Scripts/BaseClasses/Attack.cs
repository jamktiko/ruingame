using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    
public class Attack : MonoBehaviour
{
    protected DamageCollider DamageCollider;
    protected MeshRenderer WeaponMesh;
    [HideInInspector]
    public bool currentlyAttacking;
    protected Animator EntityAnimator;

    //Get Attack Input
        //Check if already attacking
        //Check Combo Step
            //Attack based on current Combo Step
                //Attack deals damage on hit
            //Trigger Animation, prevent attacking while an attack is in progress
                //Last Combo Step resets number

    public virtual void Start()
    {
        DamageCollider = GetComponentInChildren<DamageCollider>();
        EntityAnimator = GetComponentInChildren<Animator>();
        EntityAnimator.enabled = true;
        try
        {
            WeaponMesh = GetComponentInChildren<WeaponMesh>().wm;
        }
        catch {}
    }
    

    public virtual void OnEnable()
    {

    }
    
    public virtual void OnDisable()
    {

    }
    public virtual void ExecuteAttack()
    {
        DamageCollider.EnableDamage();
        //WeaponMesh.enabled = true;
    }

    public virtual void AttemptAttack()
    {
        if (!currentlyAttacking)
        {
            ExecuteAttackAnimation();
            currentlyAttacking = true;
        }
    }
    public virtual void ExecuteAttackAnimation()
    {
        //Triggers animation event
        EntityAnimator.Play("Attack1");
    }

    public virtual void EndAttack()
    {
        DamageCollider.DisableDamage();
        //WeaponMesh.enabled = false;
        currentlyAttacking = false;
    }
    
    //Attack Range visualization
    /*
   private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.forward * _attackDistance/2, new Vector3(3, transform.lossyScale.y / 2, _attackDistance));
        float maxDistance = 3f;
        RaycastHit hit;
        bool isHit = Physics.SphereCast(transform.position, transform.lossyScale.x / 2, transform.forward, out hit,
            maxDistance);
        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            Gizmos.DrawWireSphere(transform.position + transform.forward * hit.distance, transform.lossyScale.x / 2);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
        }
    }
    */
}
}