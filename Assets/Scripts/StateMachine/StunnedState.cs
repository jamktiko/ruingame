using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
namespace DefaultNamespace
{
    public class StunnedState : State
    {
        private float _time = 1f;
        private float initialMoveSpeed;
        public float StunTimer = 1f;
        public StunnedState(BaseEnemy enemy) : base(enemy)
        {
        }

        public override void Tick()
        {
            if (StunTimer > 0)
            {
                Enemy._movementControl.movementSpeed = 0f;
                
                Enemy.stunned = true;
                StunTimer -= _time * Time.deltaTime;
            }
            else
            {
                Enemy.stunned = false;
            }
            if (!Enemy.alive)
            {
                Enemy.SetState(new DeathState(Enemy));
            }
            if (!Enemy.stunned)
            {
                Enemy._movementControl.movementSpeed = initialMoveSpeed;
                Enemy.SetState(new MoveTowardsPlayerState(Enemy));
            }
        }

        public override void OnStateEnter()
        {
            //Enemy._movementControl._entityRigidbody.velocity = Vector3.zero;
            initialMoveSpeed = Enemy._movementControl.movementSpeed;
            //Enemy.attack.EndAttack();
            Enemy.stunned = true;
            Name = "stunned";
        }
    }
}