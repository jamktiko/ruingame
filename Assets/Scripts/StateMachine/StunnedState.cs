
using UnityEngine;
    
namespace DefaultNamespace
{
    public class StunnedState : State
    {
        private float _time = 1f;
        private float initialMoveSpeed;
        public float StunTimer = 3f;
        public StunnedState(Enemy_StateMachine enemy) : base(enemy)
        {
        }

        public override void Tick()
        {
            if (StunTimer > 0)
            {
                StunTimer -= _time * Time.deltaTime;
            }
            else
            {
                Enemy.attackHandler.attacking = false;
                Enemy.stunned = false;
            }
            if (!Enemy.alive)
            {
                Enemy.SetState(new DeathState(Enemy));
            }
            if (!Enemy.stunned)
            {
                Enemy.movementController.OnStunEnd();
                Enemy.SetState(new MoveTowardsPlayerState(Enemy));
            }
        }

        public override void OnStateEnter()
        {
            initialMoveSpeed = Enemy.movementController._movementSpeed;
            //Stun animation
            Enemy.attackHandler.EndAttack();
            Enemy.stunned = true;
            Name = "stunned";
        }
    }
}