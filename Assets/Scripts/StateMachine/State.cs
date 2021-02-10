using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{

    public abstract class State
    {
        protected BaseEnemy Enemy;
        public string Name;

        public abstract void Tick();

        public virtual void OnStateEnter()
        {
        }

        public virtual void OnStateExit()
        {
        }

        public State(BaseEnemy enemy)
        {
            this.Enemy = enemy;
        }

    }
}