using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
namespace DefaultNamespace
{
    public class StunnedState : State
    {
        private float _time = 1f;
        public float StunTimer = 1f;
        public StunnedState(BaseEnemy enemy) : base(enemy)
        {
        }

        public override void Tick()
        {
            if (StunTimer > 0)
            {
                Enemy.stunned = true;
                StunTimer -= _time * Time.deltaTime;
            }
            else
            {
                Enemy.stunned = false;
            }
            if (!Enemy.stunned)
                Enemy.SetState(new MoveTowardsPlayerState(Enemy));
        }

        public override void OnStateEnter()
        {
            Enemy.attack.EndAttack();
            Enemy.stunned = true;
            Name = "stunned";
        }
    }
}