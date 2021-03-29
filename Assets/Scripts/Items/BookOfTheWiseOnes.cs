
using DefaultNamespace;

public class BookOfTheWiseOnes : ArtifactEffect
{
    public float blockedDamage = 10f;
    private PlayerHealth _playerHealth;
    private float maxHealthModifier = 1.15f;
    private float maxHealth;

    public override void AddEffect()
    {
        _playerReference = PlayerManager.Instance;

        if (_playerReference.TryGetComponent(out _playerHealth))
        {
            maxHealth = _playerHealth._maximumHealth;
            _playerHealth._maximumHealth *= maxHealthModifier;
        }
    }

    private void OnDestroy()
    {
        if (_playerHealth._maximumHealth > maxHealth)
            _playerHealth._maximumHealth /= maxHealthModifier;
    }
}
