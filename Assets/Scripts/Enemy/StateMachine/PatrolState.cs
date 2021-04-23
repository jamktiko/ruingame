using UnityEngine;
using UnityEngine.Rendering;

namespace DefaultNamespace
{
    public class PatrolState : State
    {
        private Vector3 _destination;
        private float time = 1f;
        private float newDestination = 3f;
        public PatrolState(Enemy_StateMachine enemy) : base(enemy)
        {
        }

        public override void Tick()
        {
            
            if (newDestination > 0)
            {
                UseMovement();
                newDestination -= time * Time.deltaTime;
            }
            else
            {
                if (newDestination <= 0)
                {
                    GetNewDirection();
                    newDestination = 1f;
                }
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
            Enemy.entityAnimator.SetBool("Stunned", false);
            //Enemy.movementController.Move(Enemy.currentTargetDirection);
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