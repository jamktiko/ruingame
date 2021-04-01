
using DefaultNamespace;

public class KappasPlate : ArtifactEffect
{
    public float blockedDamage = 10f;
    private PlayerHealth _playerHealth;

    public override void AddEffect()
    {
       _playerReference = PlayerManager.Instance;

        if (_playerReference.TryGetComponent(out _playerHealth))
        {
            _playerHealth.flatResistance += blockedDamage;
        }
    }

    private void OnDestroy()
    {
        PlayerManager.Instance.GetComponent<PlayerHealth>().flatResistance -= blockedDamage;
    }
}
