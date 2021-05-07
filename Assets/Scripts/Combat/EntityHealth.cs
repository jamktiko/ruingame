using UnityEngine;
public abstract class EntityHealth : Health
{
    public float _flatResistance = 10f;
    public float _percentualResistance = 10f;
    private float _reDamageTimer = 0.3f;
    //private float _reDamageTimerDoT = 0.4f;

    public HealthUI _healthUI;

    protected Animator EntityAnimator;
    
    private Rigidbody _characterRigidbody;
    public override void Start()
    {
        _healthUI.UpdateUIValue(currentHealth);
    }

    public override void Awake()
    {
        maximumHealth = 100f;
        currentHealth = 100f;
        _characterRigidbody = gameObject.GetComponent<Rigidbody>();
        EntityAnimator = GetComponentInChildren<Animator>();
    }
    public override void DealDamage(IAttack attack, BaseAttackHandler attacker)
    {
        var incomingDamage = DamageCalculation(attack);
        currentHealth -= incomingDamage;
        AddIFrame(_reDamageTimer);
        ReactToDamage(incomingDamage);
    }
    public virtual void ReactToDamage(float amount)
    {

        _healthUI.UpdateUIValue(currentHealth);
        CheckHealth();
    }
    public override void DealDamageOverTime(float amount, float time)
    {
        //Make this a coroutine which applies damage over time
    }

    public override void Die()
    {
        //Animation
        //EntityAnimator.Play("Death");
        //Particles
        //Update UI or stats
    }

    public override void AddIFrame(float duration)
    {
        //Effect for Iframe?
        damageable = false;
        Invoke("TurnDamageOn", duration);
    }
    public virtual float DamageCalculation(IAttack attack)
    {
        switch (attack.damageType)
        {
            case baseDamageType.PURE:
                if (!damageable)
                    return 0;
                return attack.inflictedDamage;
            case baseDamageType.PHYSICAL:
                if (!damageable)
                    return 0;
                var calculatedDamage = attack.inflictedDamage;
                return calculatedDamage;
            case baseDamageType.DIRECT:
                return attack.baseDamage;
        }
        return 0;
    }
}

