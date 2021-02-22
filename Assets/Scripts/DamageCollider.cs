
using UnityEngine;

using Debug = UnityEngine.Debug;

public class DamageCollider : MonoBehaviour
{
     public Collider _damageCollider;
     private float damage = 100f;
     private string targetTag = "Enemy";
     private GameObject _attackingEntity;
     private GameObject _currentTrail;
    private void Start()
    {
        _damageCollider = GetComponent<Collider>();
    }
    
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag(targetTag))
        {
            var tr = collision.gameObject;
            var targetHealth = tr.GetComponent<Health>();
            try {
                targetHealth.DealDamage(damage);
            }
            catch {Debug.Log("Target has no health!");}
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
