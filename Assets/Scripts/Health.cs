using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maximumHealth = 100f;
    public float flatResistance = 10f;
    public float percentualResistance = 10f;

    [SerializeField]
    private float _currentHealth;
    private bool damageable = true;

    void Start()
    {
        _currentHealth = maximumHealth;
    }

    public void DealDamage(float amount)
    {
        if (!damageable)
            return;
        var damagePassed = amount;
        damagePassed -= flatResistance;
        damagePassed = damagePassed * ((100 - percentualResistance) / 100);
        _currentHealth -= damagePassed;

    }

    public void DealDamageOverTime(float amount, float time)
    {
        
    }

    public void Die()
    {
        Debug.Log(this +  " dies");
    }
    
}
