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
            Enemy._movementControl.attacking = true;
            Enemy._movementControl._entityRigidbody.velocity = Vector3.zero;
            Enemy.attack.AttemptAttack();
            
        }
        public override void Tick()
        {
            if (!Enemy.alive)
            {
                Enemy.SetState(new DeathState(Enemy));
            }
            if (!Enemy.attack.attacking)
            {
                Enemy.SetState(new MoveTowardsPlayerState(Enemy));
            }
        }
        public override void OnStateExit()
        {
            Enemy._movementControl.attacking = false;
        }
    }
}