using UnityEngine;
    
namespace DefaultNamespace
{
    public class AttackPlayerState : State
    {
        public AttackPlayerState(BaseEnemy _enemy) : base(_enemy)
        {
        }

        public override void OnStateEnter()
        {
            name = "Attacking Player";
            _enemy._attack.AttemptAttack();
        }
        public override void Tick()
        {
            if (AttackedPlayer())
            {
                _enemy.SetState(new MoveTowardsPlayerState(_enemy));
            }
        }

        private bool AttackedPlayer()
        {
            return true;
        }
    }
}