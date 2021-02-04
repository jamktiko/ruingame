using System.Collections;
using System.Collections.Generic;
using FMOD;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class DamageCollider : MonoBehaviour
{
    [SerializeField] private Collider _damageCollider;
    [SerializeField] private float _damage = 0f;
    [SerializeField] private float _kbStrength = 1f;
    [SerializeField] private string _targetTag;
    [SerializeField] private GameObject _attackingEntity;
    [SerializeField] private float _activationDuration;
    
    private void Start()
    {
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
                    tr.GetComponent<CharacterController>().Move(-kbDirection* _kbStrength);
                    
                }
                catch
                {
                    Debug.Log("KNOCKBACK Object has no character controller");
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
