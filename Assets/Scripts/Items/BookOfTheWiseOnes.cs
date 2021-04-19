
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
            maxHealth = _playerHealth.maximumHealth;
            _playerHealth.maximumHealth *= maxHealthModifier;
        }
    }

    private void OnDestroy()
    {
        PlayerHealth _playerHealth = PlayerManager.Instance.GetComponent<PlayerHealth>();
        if (_playerHealth.maximumHealth > maxHealth)
            _playerHealth.maximumHealth /= maxHealthModifier;
    }
}
