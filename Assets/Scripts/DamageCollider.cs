using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [SerializeField] private Collider _damageCollider;
    [SerializeField] private float _damage;
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision);
        if (collision.tag == "Enemy")
        {
            var _targetHealth = collision.gameObject.GetComponent<Health>();
            Debug.Log(_targetHealth);
            if (_targetHealth != null)
                _targetHealth.DealDamage(_damage);
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
