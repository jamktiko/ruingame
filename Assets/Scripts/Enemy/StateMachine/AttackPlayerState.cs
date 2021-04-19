using UnityEngine;
    
namespace DefaultNamespace
{
    public class AttackPlayerState : State
    {
        public AttackPlayerState(Enemy_StateMachine enemy) : base(enemy)
        {
        }

        public override void OnStateEnter()
        {
            Name = "Attacking Player";
            Enemy.movementController.attacking = true;
            Enemy.movementController.Move(Vector3.zero);
            Enemy.attackHandler.AttemptAttack();
        }
        public override void Tick()
        {
            if (!Enemy.attackHandler.attacking)
            {
                Enemy.SetState(new MoveTowardsPlayerState(Enemy));
            }
        }
        public override void OnStateExit()
        {
            Enemy.movementController.attacking = false;
            Enemy.attackHandler.attacking = false;
        }
    }
}