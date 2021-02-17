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
            Enemy.attack.currentlyAttacking = true;
        }
        public override void Tick()
        {
            if (!Enemy.alive)
            {
                Enemy.SetState(new DeathState(Enemy));
            }
            if (!Enemy.attack.currentlyAttacking)
            {
                Enemy.SetState(new MoveTowardsPlayerState(Enemy));
            }
        }
    }
}