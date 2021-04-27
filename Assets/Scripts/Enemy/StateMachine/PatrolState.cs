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
                Enemy.GetNewDirection();
            }
            else
            {
                UseMovement(Enemy.DecidePathToPlayer());
            }
            if (Enemy.CheckForPlayer() && Enemy.CanSeePlayer())
            {
                Enemy.AlertAllEnemies();
                Enemy.SetState(new MoveTowardsPlayerState(Enemy));
            }
        }
        public override void UseMovement(Vector3 direction)
        {
            Enemy.movementController.Move(direction);
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