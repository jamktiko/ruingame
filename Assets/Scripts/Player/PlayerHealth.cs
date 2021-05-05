
using UnityEngine.Events;
public class PlayerHealth : EntityHealth
{
    //For FeatherOfPhoenix artifact
    public event UnityAction DiePlayerEvent = delegate { };
    public bool revivePlayer = false;
    //For RabbitsFoot artifact
    public event UnityAction DamagePlayerEvent = delegate { };
    public bool dodgeAttack = false;
    public override void Die()
    {
        PlayerManager pm = GetComponent<PlayerManager>();
        DiePlayerEvent.Invoke();
        if (!revivePlayer)
            pm.Die();
    }

    public override void DealDamage(IAttack attack, BaseAttackHandler attacker)
    {
        DamagePlayerEvent.Invoke();
        if (dodgeAttack)
        {
            dodgeAttack = false;
            return;
        }
        
        base.DealDamage(attack, attacker);
    }
}
