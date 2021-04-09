
using UnityEngine;
using UnityEngine.VFX;

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
            enemyController.SetState(new StunnedState(enemyController));
            base.ReactToDamage(amount);
        }

        public override void Die()
        {
            base.Die();
            enemyController.Die();
        }
    }
}