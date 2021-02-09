using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using FMOD;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class DamageCollider : MonoBehaviour
{
     private Collider _damageCollider;
     public  float _damage = default;
     public float _kbStrength = 100f;
     public string _targetTag = "Enemy";
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
        if (collision.CompareTag(_targetTag))
        {
            var tr = collision.gameObject;
            var _targetHealth = tr.GetComponent<Health>();
            if (_targetHealth != null)
            {
                _targetHealth.DealDamage(_damage);
                var kbDirection = _attackingEntity.transform.position - tr.transform.position;
                kbDirection.y = 0; //Normalize y coordinates
                try
                {
                    //Apply Knockback
                    tr.GetComponent<Rigidbody>().AddForce(-kbDirection*_kbStrength);
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
