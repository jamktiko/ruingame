using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maximumHealth = 100f;
    public float flatResistance = 10f;
    public float percentualResistance = 10f;
    public float reDamageTimer = 0.4f;
    public float reDamageTimerDoT = 0.4f;
    
    [SerializeField]
    private float _currentHealth;
    [SerializeField]
    private bool damageable = true;

    [SerializeField] protected Animator _entityAnimator;

    
    
    void Start()
    {
        _currentHealth = maximumHealth;
        _entityAnimator = GetComponentInChildren<Animator>();
    }

    public void DealDamage(float amount)
    {
        if (!damageable)
            return;
        ReactToDamage(amount);
        CheckHealth();
    }

    public void ReactToDamage(float amount)
    {
        var damagePassed = amount;
        damagePassed -= flatResistance;
        damagePassed = damagePassed * ((100 - percentualResistance) / 100);
        _currentHealth -= damagePassed;
        AddIFrame(reDamageTimer);
    }
    public void DealDamageOverTime(float amount, float time)
    {
        //Make this a coroutine which applies damage over time
    }

    public virtual void Die()
    {
        //Animation
        _entityAnimator.Play("Death");
        //Particles
        Destroy(gameObject, 0.6f);
        //Update UI or stats
        SendMessageUpwards("EntityDeath");
    }

    public void AddIFrame(float duration)
    {
        damageable = false;
        Invoke("TurnDamageOn", duration);
    }

    public void TurnDamageOn()
    {
        damageable = true;
    }

    public void CheckHealth()
    {
        if (_currentHealth <= 0)
        {
            Die();
        }
    }
}
