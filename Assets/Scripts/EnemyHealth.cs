using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;


namespace DefaultNamespace
{
    public class EnemyHealth : Health
    {
        public BaseEnemy _enemyController;

        public override void Start()
        {
            base.Start();
            _enemyController = GetComponent<BaseEnemy>();
        }

        public override void ReactToDamage(float amount)
        {
            _enemyController.stunned = true;
            //base.ReactToDamage(amount);
        }
    }
}