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
                newDestination -= time * Time.deltaTime;
            }
            else
            {
                if (newDestination <= 0)
                {
                    UseMovement();
                    newDestination = 1f;
                }
            }
            if (Enemy.CheckForPlayer())
            {
                Enemy.AlertAllEnemies();
                Enemy.SetState(new MoveTowardsPlayerState(Enemy));
            }
        }

        public override void UseMovement()
        {
            Enemy.movementController.Move(Enemy.DecidePatrolDirection());
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