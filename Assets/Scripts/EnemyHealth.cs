using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;


namespace DefaultNamespace
{
    public class EnemyHealth : Health
    {
        public BaseEnemy enemyController;
        public void Awake()
        {
            //_enemyUI.GetComponentInChildren<EnemyUI>();
        }

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
    }
}