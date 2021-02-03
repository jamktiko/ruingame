using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{

    public abstract class State
    {
        protected BaseEnemy _enemy;
        public string name;

        public abstract void Tick();

        public virtual void OnStateEnter()
        {
        }

        public virtual void OnStateExit()
        {
        }

        public State(BaseEnemy _enemy)
        {
            this._enemy = _enemy;
        }

    }
}