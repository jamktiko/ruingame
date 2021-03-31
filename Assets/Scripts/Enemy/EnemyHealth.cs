
using UnityEngine;
using UnityEngine.VFX;

namespace DefaultNamespace
{
    public class EnemyHealth : EntityHealth
    {
        public BaseEnemy enemyController;

        public override void Start()
        {
        }

        public override void ReactToDamage(float amount)
        {
            enemyController.SetState(new StunnedState(enemyController));
            base.ReactToDamage(amount);
        }

        public override void Die()
        {
            base.Die();
            enemyController.alive = false;
            Destroy(gameObject, 0.5f);
        }
    }
}