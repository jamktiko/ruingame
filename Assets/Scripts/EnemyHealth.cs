using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;


namespace DefaultNamespace
{
    public class EnemyHealth : Health
    {
        public BaseEnemy enemyController;

        public override void Start()
        {
            base.Start();
            enemyController = GetComponent<BaseEnemy>();
        }

        public override void ReactToDamage(float amount)
        {
            enemyController.stunned = true;
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