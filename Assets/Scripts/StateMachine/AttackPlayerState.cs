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
        }
        public override void Tick()
        {

        }
        public override void OnStateExit()
        {
           
        }
    }
}