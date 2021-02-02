using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private InputReader _inputReader;

    [SerializeField] private DamageCollider _damageCollider;

    [SerializeField] private float _damage = 100f;
    
    
    //Get Attack Input
        //Check if already attacking
        //Check Combo Step
            //Attack based on current Combo Step
                //Attack deals damage on hit
            //Trigger Animation, prevent attacking while an attack is in progress
                //Last Combo Step resets number

    private void Start()
    {
        _damageCollider = GetComponentInChildren<DamageCollider>();
    }

    private void OnEnable()
    {
        _inputReader.attackEvent += ExecuteAttack;
    }

    private void OnDisable()
    {
        _inputReader.attackEvent -= ExecuteAttack;
    }

    private void ExecuteAttack()
    {
        _damageCollider.EnableDamage(_damage);
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
