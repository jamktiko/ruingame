
using DefaultNamespace;

public class FeatherOfThePhoenix : ArtifactEffect
{
    public float burningDamage = 50f;
    public float burningRadius = 20f;
    public float burningTime = 3f;
    private float counter = 1;
    private PlayerHealth _playerHealth;

    public override void AddEffect()
    {
        _playerReference = PlayerManager.Instance;
        _playerHealth = _playerReference.GetComponent<PlayerHealth>();
        _playerHealth.DiePlayerEvent += RevivePlayer;
        _playerHealth.revivePlayer = true;
    }

    private void RevivePlayer()
    {
        if (counter == 0)
           _playerHealth.revivePlayer = false;
        else
        {
            _playerHealth.CurrentHealth = _playerHealth._maximumHealth;
            BurningDamage();
            counter--;
        }
    }

    void BurningDamage()
    {
        Targeting targeting = PlayerManager.Instance.GetComponent<Targeting>();
        foreach (var item in targeting.GetListOfEnemiesInRange(burningRadius, 0f))
        {
            if (item.TryGetComponent(out EnemyHealth enemyHealth))
            {
                enemyHealth.DealDamageOverTime(burningDamage, burningTime);
            }
        }
    }
}
