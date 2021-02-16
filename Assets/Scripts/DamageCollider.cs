using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using FMOD;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

public class DamageCollider : MonoBehaviour
{
     public Collider _damageCollider;
      public  float damage = 100f;
     public float kbStrength = 100f;
     public string targetTag = "Enemy";
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
