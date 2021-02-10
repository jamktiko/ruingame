using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using FMOD;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

public class DamageCollider : MonoBehaviour
{
     private Collider _damageCollider;
     [FormerlySerializedAs("_damage")] public  float damage = default;
     [FormerlySerializedAs("_kbStrength")] public float kbStrength = 100f;
     [FormerlySerializedAs("_targetTag")] public string targetTag = "Enemy";
     private GameObject _attackingEntity;

    private void Start()
    {
        _damageCollider = GetComponent<Collider>();
        _attackingEntity = gameObject.transform.parent.gameObject;
    }
    
    
    private void OnTriggerEnter(Collider collision)
    {
        //Do a sphere check, deal damage to everything and knockback?
        //Deal Damage
        //Knock back from attacker
        //STUN?
        if (collision.CompareTag(targetTag))
        {
            var tr = collision.gameObject;
            var targetHealth = tr.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.DealDamage(damage);
                var kbDirection = _attackingEntity.transform.position - tr.transform.position;
                kbDirection.y = 0; //Normalize y coordinates
                try
                {
                    //Apply Knockback
                    tr.GetComponent<Rigidbody>().AddForce(-kbDirection*kbStrength);
                }
                catch
                {
                    Debug.Log("KNOCKBACK Object has no Rigidbody");
                }
            }
        }
    }

    public void EnableDamage()
    {
        _damageCollider.enabled = true;
    }

    public void DisableDamage()
    {
        _damageCollider.enabled = false;
    }
}
