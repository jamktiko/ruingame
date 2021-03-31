
using UnityEngine;

public abstract class Health : MonoBehaviour, IDamageable
{
    public float maximumHealth { get; set; }
    public float currentHealth { get; set; }
    public bool damageable { get; set; }
    public virtual void Start()
    {
    }

    public virtual void DealDamage(IAttack attack, BaseAttackHandler attacker)
    {
        currentHealth -= attack.baseDamage;
    }
    
    public virtual void ReactToDamage(float amount)
    {
        CheckHealth();
    }
    public virtual void DealDamageOverTime(float amount, float time)
    {
        //Make this a coroutine which applies damage over time
    }

    public virtual void Die()
    {
        //Animation
        //EntityAnimator.Play("Death");
        //Particles
        
        //Update UI or stats
        Destroy(this, 1f);
    }

    public virtual void AddIFrame(float duration)
    {
        damageable = false;
        Invoke("TurnDamageOn", duration);
    }

    public virtual void TurnDamageOn()
    {
        damageable = true;
    }

    public virtual void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }
}