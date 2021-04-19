
    public interface IDamageable
    {
        bool damageable
        {
            get;
            set;
        }

        float CurrentHealth
        {
            get;
            set;
        }

        float MaximumHealth
        {
            get;
            set;
        }
        void DealDamage(IAttack attack, BaseAttackHandler attacker);
        void ReactToDamage(float amount);
        void AddIFrame(float duration);
        void TurnDamageOn();
        void DealDamageOverTime(float amount, float duration);
        void Die();
    }
    