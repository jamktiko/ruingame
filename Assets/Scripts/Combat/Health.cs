
using UnityEngine;

public abstract class Health : MonoBehaviour, IDamageable
{
    public float maximumHealth;
    public float MaximumHealth
    {
        get { return maximumHealth;}
        set { maximumHealth = value; }
    }
    public float currentHealth;
    public float CurrentHealth
    {
        get { return currentHealth;}
        set { currentHealth = value; }
    }
    public bool damageable { get; set; }
    public virtual void Start()
    {
    }

    public virtual void Awake()
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