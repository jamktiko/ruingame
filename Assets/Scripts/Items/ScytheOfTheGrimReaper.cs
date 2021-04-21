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
                float addHealth = playerHealth.currentHealth + playerHealth.maximumHealth * 0.1f;
                if (addHealth > playerHealth.maximumHealth)
                    playerHealth.currentHealth = playerHealth.maximumHealth;
                else
                    playerHealth.currentHealth = addHealth;

            }
        }
    }
}