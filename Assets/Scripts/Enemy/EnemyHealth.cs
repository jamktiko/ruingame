
namespace DefaultNamespace
{
    public class EnemyHealth : EntityHealth
    {
        public Enemy_StateMachine enemyController;

        public override void Start()
        {
            enemyController = GetComponent<Enemy_StateMachine>();
        }

        public override void ReactToDamage(float amount)
        {
            base.ReactToDamage(amount);
            enemyController.SetState(new StunnedState(enemyController));
        }

        public override void Die()
        {
            base.Die();
            enemyController.Die();
        }
    }
}