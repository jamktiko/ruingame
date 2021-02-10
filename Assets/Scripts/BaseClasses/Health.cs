﻿using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float _maximumHealth = 100f;
    private float _flatResistance = 10f;
    private float _percentualResistance = 10f;
    private float _reDamageTimer = 0.4f;
    //private float _reDamageTimerDoT = 0.4f;
    
    protected float CurrentHealth;
    private bool _damageable = true; 
    protected Animator EntityAnimator;

    
    
    public virtual void Start()
    {
        CurrentHealth = _maximumHealth;
        EntityAnimator = GetComponentInChildren<Animator>();
    }

    public virtual void DealDamage(float amount)
    {
        if (!_damageable)
            return;
        ReactToDamage(amount);
        CheckHealth();
    }

    public virtual void ReactToDamage(float amount)
    {
        var damagePassed = amount;
        damagePassed -= _flatResistance;
        damagePassed = damagePassed * ((100 - _percentualResistance) / 100);
        CurrentHealth -= damagePassed;
        AddIFrame(_reDamageTimer);
    }
    public void DealDamageOverTime(float amount, float time)
    {
        //Make this a coroutine which applies damage over time
    }

    public virtual void Die()
    {
        //Animation
        EntityAnimator.Play("Death");
        //Particles
        Destroy(gameObject, 0.6f);
        //Update UI or stats
        SendMessageUpwards("EntityDeath");
    }

    public void AddIFrame(float duration)
    {
        _damageable = false;
        Invoke("TurnDamageOn", duration);
    }

    public void TurnDamageOn()
    {
        _damageable = true;
    }

    public void CheckHealth()
    {
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
}
