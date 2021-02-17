using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;

public class DeathState : State
{
    public DeathState(BaseEnemy enemy) : base(enemy)
    {
    }

    public override void OnStateEnter()
    {
        Name = "Dying";
        //Enemy.attack.EndAttack();
        Enemy.alive = false;
    }
    public override void Tick()
    {
        if (Enemy.alive)
            Enemy.SetState(new MoveTowardsPlayerState(Enemy));
    }
}