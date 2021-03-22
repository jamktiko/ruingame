using System.Linq;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public string[] AllowedTargetTags;
    public float damage;
    public Rigidbody rb;
    public float flighttime;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("ActivateGravity", flighttime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (AllowedTargetTags.Contains(other.tag))
        {
            try
            {
                var targetHealth = other.GetComponent<Health>();
                targetHealth.DealDamage(damage);
            }
            catch {Debug.Log("Target has no health!");}
            Destroy(gameObject);
        }
    }

    public void ActivateGravity()
    {
        rb.constraints = RigidbodyConstraints.None;
        Destroy(gameObject, 0.75f);
    }
}
