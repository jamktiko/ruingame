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
     public GameObject attackTrail;
     private GameObject _currentTrail;
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
            try {
                targetHealth.DealDamage(damage);
            }
            catch {Debug.Log("Target has no health!");}
            try
            {
                var rb = tr.GetComponent<Rigidbody>();
                rb.AddForce(gameObject.transform.forward * kbStrength * 100);
            }
            catch
            {
                Debug.Log("Target has no rigidbody");
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

    public void EnableAttackTrail()
    {
         _currentTrail = Instantiate(attackTrail, gameObject.transform);
    }
    public void DisableAttackTrail()
    {
        Destroy(_currentTrail);
    }
}
