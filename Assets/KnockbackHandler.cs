using System.Collections;
using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class KnockbackHandler : MonoBehaviour
{
    private Rigidbody _characterRigidbody;
    private BaseEnemy _enemyController;
    public bool canKnockback = true;
    private Vector3 currentTarget;
    void Start()
    {
        _enemyController = gameObject.GetComponent<BaseEnemy>();
        _characterRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    public virtual void HandleKnockBack(Vector3 target, float force)
    {
        currentTarget = target;
        try {_enemyController.stunned = true;}
        catch {}
        if (canKnockback)
        {
            canKnockback = false;
            _characterRigidbody.velocity = Vector3.zero;
            Vector3 direction = (transform.position - target).normalized;
            _characterRigidbody.AddForce(direction * force, ForceMode.Impulse);
            StartCoroutine("KnockbackReset");
        }
    }
    public virtual IEnumerator KnockbackReset()
    {
        yield return new WaitForSeconds(1f);
        canKnockback = true;
    }
}
