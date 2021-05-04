using DefaultNamespace;
using UnityEngine;

public class ScytheOfTheGrimReaper : ArtifactEffect
{
    private BaseAttackHandler _attackHandler;
    private float _newCriticalHitChance = 10f;
    private float _initialCriticalHitChance;
    private PlayerHealth _playerHealth;
    private float _healthModifer = 10f;
    protected void Start()
    {
        _playerHealth = PlayerManager.Instance.GetComponent<PlayerHealth>();
        _attackHandler = PlayerManager.Instance.GetComponent<BaseAttackHandler>();
        _initialCriticalHitChance = _attackHandler.criticalHitChance;
    }

    public override void AddEffect()
    {
        ModifyCriticalHitChance();
        _attackHandler.CriticalHit += AddHealth;
    }

    private void ModifyCriticalHitChance()
    {
        _attackHandler.criticalHitChance = _newCriticalHitChance;
    }

    private void AddHealth()
    {
        if (_playerHealth.currentHealth + _healthModifer > _playerHealth.maximumHealth)
            _playerHealth.currentHealth = _playerHealth.maximumHealth;
        else
            _playerHealth.currentHealth += _healthModifer;
    }
}