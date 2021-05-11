
namespace DefaultNamespace
{
    public class BossHealth : EnemyHealth
    {
        public float limit = 75;
        public int currentLimit = 1;
        public override void ReactToDamage(float amount)
        {
            base.ReactToDamage(amount);
            enemyController.SetState(new StunnedState(enemyController));
            if (currentHealth < limit && limit > 25)
            {
                LimitPassed();
                limit -= 25;
                currentLimit++;
            }
        }

        public override void Die()
        {
            base.Die();
            enemyController.Die();
        }
        public override void Awake()
        {
            base.Awake();
            maximumHealth = 200f;
            currentHealth = 200f;
        }
        
        public void LimitPassed()
        {
            GameManager.Instance.roomManager.spawnerManager.SpawnAdds(currentLimit * 2);
        }
    }
}