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
            name = "attacking";
            _enemy.GetComponent<Renderer>().material.color = Color.red;
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
            _enemy._attack.ExecuteAttack();
            return true;
        }
    }
}