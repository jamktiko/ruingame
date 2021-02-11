using UnityEngine;
    
namespace DefaultNamespace
{
    public class AttackPlayerState : State
    {
        public AttackPlayerState(BaseEnemy enemy) : base(enemy)
        {
        }

        public override void OnStateEnter()
        {
            Name = "Attacking Player";
            Enemy.attack.AttemptAttack();
        }
        public override void Tick()
        {
            if (AttackedPlayer())
            {
                Enemy.SetState(new MoveTowardsPlayerState(Enemy));
            }
        }

        private bool AttackedPlayer()
        {
            return true;
        }
    }
}