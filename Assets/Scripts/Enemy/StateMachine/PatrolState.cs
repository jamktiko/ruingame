using UnityEngine;
using UnityEngine.Rendering;

namespace DefaultNamespace
{
    public class PatrolState : State
    {
        public PatrolState(Enemy_StateMachine enemy) : base(enemy)
        {
        }

        public override void Tick()
        {
            if (Enemy.HasReachedTargetDestination())
            {
                GetNewDirection();
            }
            else
            {
                UseMovement();
            }
            
            if (Enemy.CheckForPlayer() && Enemy.CanSeePlayer())
            {
                Enemy.AlertAllEnemies();
                Enemy.SetState(new MoveTowardsPlayerState(Enemy));
            }
        }

        public void GetNewDirection()
        {
            Enemy.currentTargetPos = Enemy.DecidePatrolDirection();
        }
        public override void UseMovement()
        {
            Enemy.movementController.Move(Enemy.DecidePathToPlayer());
        }

        public override void OnStateEnter()
        {
            Name = "patrol";
        }
        
        public override void OnStateExit()
        {
        }
    }
}