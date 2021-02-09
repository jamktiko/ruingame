using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
namespace DefaultNamespace
{
    public class StunnedState : State
    {
        private float _time = 1f;
        public float _stunTimer = 1f;
        public StunnedState(BaseEnemy _enemy) : base(_enemy)
        {
        }

        public override void Tick()
        {
            if (_stunTimer > 0)
            {
                _enemy.stunned = true;
                _stunTimer -= _time * Time.deltaTime;
            }
            else
            {
                _enemy.stunned = false;
            }
            if (!_enemy.stunned)
                _enemy.SetState(new MoveTowardsPlayerState(_enemy));
        }

        public override void OnStateEnter()
        {
            _enemy._attack.EndAttack();
            _enemy.stunned = true;
            name = "stunned";
        }
    }
}