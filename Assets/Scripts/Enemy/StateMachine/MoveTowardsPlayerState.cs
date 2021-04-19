using System.Collections;
using UnityEngine;
    
namespace DefaultNamespace
{
    public class MoveTowardsPlayerState : State
    {
        private Vector3 _destination;
        private float time = 1f;
        private float newDestination = 0.2f;

        public MoveTowardsPlayerState(Enemy_StateMachine enemy) : base(enemy)
        {
        }

        public override void Tick()
        {
            Enemy.currentTargetPos = Enemy.playerTarget.transform.position;

            if (newDestination > 0)
            {
                newDestination -= time * Time.deltaTime;
            }
            else
            {
                if (newDestination <= 0)
                {
                    UseMovement();
                    newDestination = 0.2f;
                }
            }
            if (Enemy.HasReachedAttackRange())
            {
                Enemy.SetState(new AttackPlayerState(Enemy));
            }
            
        }

        public override void UseMovement()
        {
            Enemy.movementController.Move(Enemy.DecidePathToPlayer());
        }

        public override void OnStateEnter()
        {
            Name = "moving towards player";
            
        }

        public override void OnStateExit()
        {
        }
    }
}