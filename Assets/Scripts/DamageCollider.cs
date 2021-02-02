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
    private bool _canAttack = true;
    private void OnTriggerEnter(Collider collision)
    {
        //Needs better detection, only works for one tag at the moment.
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
                    Debug.Log("Object has no character controller");
                }
            }
        }
    }

    public void EnableDamage(float amount)
    {
        _damage = amount;
        if (_canAttack)
        {
            _damageCollider.enabled = true;
            _canAttack = false;
            Invoke(nameof(DisableDamage), _activationDuration);
        }
    }

    public void DisableDamage()
    {
        _damageCollider.enabled = false;
        _damage = 0;
        _canAttack = true;
        
    }
}
