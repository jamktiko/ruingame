using System.Runtime.CompilerServices;
using UnityEngine;
    
namespace DefaultNamespace
{
    public class MoveTowardsPlayerState : State
    {
        private Vector3 destination;

        public MoveTowardsPlayerState(BaseEnemy _enemy) : base(_enemy)
        {
        }

        public override void Tick()
        {
            destination = _enemy._playerTransform.position;
            _enemy.MoveToward(destination);
            if (ReachedPlayer())
            {
                _enemy.SetState(new AttackPlayerState(_enemy));
            }
        }

        public override void OnStateEnter()
        {
            name = "moving towards player";
        }

        private bool ReachedPlayer()
        {
            return Vector3.Distance(_enemy.transform.position, destination) < 2f;
        }
    }
}