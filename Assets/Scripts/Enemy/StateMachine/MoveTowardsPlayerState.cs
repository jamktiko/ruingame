using System.Collections;
using UnityEngine;
    
namespace DefaultNamespace
{
    public class MoveTowardsPlayerState : State
    {
        private Vector3 _destination;
        private float time = 1f;
        private float newDestination = 0.1f;

        public MoveTowardsPlayerState(Enemy_StateMachine enemy) : base(enemy)
        {
        }

        public override void Tick()
        {
        
            if (newDestination > 0)
            {
                newDestination -= time * Time.deltaTime;
            }
            else
            {
                if (newDestination <= 0)
                {
                    if (Enemy.HasReachedAttackRange() && Enemy.CanSeePlayer())
                    {
                        Enemy.SetState(new AttackPlayerState(Enemy));
                    }
                    Enemy.currentTargetPos = PlayerManager.Instance.gameObject.transform.position;
                    UseMovement();
                    newDestination = 0.1f;
                }
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