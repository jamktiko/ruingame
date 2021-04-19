
using System.Collections;
using UnityEngine;
public class Health : MonoBehaviour
{
    public float _flatResistance = 10f;
    public float _percentualResistance = 10f;
    public float _maximumHealth = 100f;
    private float _reDamageTimer = 0.4f;
    //private float _reDamageTimerDoT = 0.4f;
    
    public HealthUI _healthUI;
    public float CurrentHealth;
    public bool _damageable = true; 
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
    }

    public virtual void ReactToDamage(float amount)
    {
        var damagePassed = amount;
        damagePassed -= _flatResistance;
        damagePassed = damagePassed * ((100 - _percentualResistance) / 100);
        CurrentHealth -= damagePassed;
        AddIFrame(_reDamageTimer);
        CheckHealth();
        _healthUI.UpdateUIValue(CurrentHealth);
        
    }

    public void DealDamageOverTime(float amount, float time)
    {
        StartCoroutine(DamageOverTime(amount, time));
    }
    
    public IEnumerator DamageOverTime(float amount, float time)
    {
        var damagePassed = amount;
        damagePassed -= _flatResistance;
        damagePassed = damagePassed * ((100 - _percentualResistance) / 100);
        damagePassed *= (0.1f / time);
        for (float ft = time; ft >= 0; ft -= 0.1f)
        {
            CurrentHealth -= damagePassed;
            CheckHealth();
            _healthUI.UpdateUIValue(CurrentHealth);
            yield return new WaitForSeconds(.1f);
        }
        AddIFrame(_reDamageTimer);
    }

    public virtual void Die()
    {
        //Animation
        //EntityAnimator.Play("Death");
        //Particles
        
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
            CurrentHealth = 0;
            Die();
        }
    }
}
