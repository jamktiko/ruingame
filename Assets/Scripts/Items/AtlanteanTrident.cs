
using DefaultNamespace;

public class AtlanteanTrident : ArtifactEffect
{
    public float slowDownTime = 2f;
    PlayerAttackHandler playerAttackHandler;
    private Movement enemyMovement;
    private float normalEnemySpeed;

    private void Start()
    {
        _playerReference = PlayerManager.Instance;
        normalEnemySpeed = gameObject.AddComponent<Movement>().movementSpeed;
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
            enemyMovement.movementSpeed = normalEnemySpeed / 2f;
            float time = enemyHealth.GetComponent<StunnedState>().StunTimer + slowDownTime;
            Invoke("NormilizeMovementSpeed", time);
        }
    }

    void NormilizeMovementSpeed()
    {
        enemyMovement.movementSpeed = normalEnemySpeed;
    }

    private void OnDestroy()
    {
        playerAttackHandler.PlayerAttackEvent -= SlowEnemyDown;
    }
}
