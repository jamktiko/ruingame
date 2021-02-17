using System.Runtime.CompilerServices;
using UnityEngine;
    
namespace DefaultNamespace
{
    public class MoveTowardsPlayerState : State
    {
        private Vector3 _destination;

        public MoveTowardsPlayerState(BaseEnemy enemy) : base(enemy)
        {
        }

        public override void Tick()
        {
            _destination = Enemy.playerTransform.position;
            Enemy.MoveToward(_destination);
            /*
            if (ReachedPlayer())
            {
                Enemy.SetState(new AttackPlayerState(Enemy));
            }
            */
            
            if (Enemy.stunned)
            {
                Enemy.SetState(new StunnedState(Enemy));
            }
            if (!Enemy.alive)
            {
                Enemy.SetState(new DeathState(Enemy));
            }
        }

        public override void OnStateEnter()
        {
            Name = "moving towards player";
        }

        private bool ReachedPlayer()
        {
            return Vector3.Distance(Enemy.transform.position, _destination) < 2f;
        }

        public override void OnStateExit()
        {
            _destination = Vector3.zero;
        }
    }
}