using DefaultNamespace;
using UnityEngine;

public class ScytheOfTheGrimReaper : ArtifactEffect
{
    private PlayerAttackHandler _playerAttackHandler;

    public override void AddEffect()
    {
        _playerReference = PlayerManager.Instance;
        if (_playerReference.TryGetComponent(out _playerAttackHandler))
        {
            _playerAttackHandler.PlayerAttackEvent += TryCriticalHit;
        }
    }

    private void TryCriticalHit(float damage, Health enemyHealth)
    {

        float rnd = Random.Range(0, 11);
        if (rnd <= 1)
        {
            _playerAttackHandler.artifactDamageModifier = damage;

            if (_playerReference.TryGetComponent(out PlayerHealth playerHealth))
            {
                float addHealth = playerHealth.CurrentHealth + playerHealth._maximumHealth * 0.1f;
                if (addHealth > playerHealth._maximumHealth)
                    playerHealth.CurrentHealth = playerHealth._maximumHealth;
                else
                    playerHealth.CurrentHealth = addHealth;

            }
        }
    }

    private void OnDestroy()
    {
        _playerAttackHandler.PlayerAttackEvent -= TryCriticalHit;
    }
}