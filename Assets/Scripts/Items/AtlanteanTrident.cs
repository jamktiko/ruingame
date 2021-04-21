
using DefaultNamespace;

public class AtlanteanTrident : ArtifactEffect
{
    public float slowDownTime = 2f;
    PlayerAttackHandler playerAttackHandler;
    private BaseMovement enemyMovement;
    private float normalEnemySpeed;

    private void Start()
    {
        _playerReference = PlayerManager.Instance;
        normalEnemySpeed = gameObject.AddComponent<BaseMovement>()._movementSpeed;
    }

    public override void AddEffect()
    {
        if (_playerReference.TryGetComponent(out playerAttackHandler))
        {
            playerAttackHandler.PlayerAttackEvent += SlowEnemyDown;
        }
    }

    private void SlowEnemyDown(float damage, Health enemyHealth)
    {
        if (enemyHealth.TryGetComponent(out enemyMovement))
        {
            enemyMovement._movementSpeed = normalEnemySpeed / 2f;
            float time = enemyHealth.GetComponent<StunnedState>().StunTimer + slowDownTime;
            Invoke("NormilizeMovementSpeed", time);
        }
    }

    void NormilizeMovementSpeed()
    {
        enemyMovement._movementSpeed = normalEnemySpeed;
    }
}
