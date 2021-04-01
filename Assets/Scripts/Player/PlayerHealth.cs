using UnityEngine.Events;
public class PlayerHealth : Health
{

    public float flatResistance;
    public float percentualResistance;
    
    /// <summary>
    /// for artifact effects
    /// </summary>
    /// 
    public event UnityAction DamagePlayerEvent = delegate { };
    public event UnityAction DiePlayerEvent = delegate { };
    public bool revivePlayer = false;
    public bool dodgeAttack = false;

    public override void Die()
    {
        PlayerManager pm = GetComponent<PlayerManager>();
        DiePlayerEvent.Invoke();
        if (!revivePlayer)
            pm.Die();
    }

    public override void DealDamage(float amount)
    {
        DamagePlayerEvent.Invoke();
        if (dodgeAttack)
            return;
        base.DealDamage(amount);
        dodgeAttack = false;
    }
}
