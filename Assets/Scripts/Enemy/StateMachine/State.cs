
    using UnityEngine;

    public abstract class State
    {
        protected Enemy_StateMachine Enemy;
        public string Name;

        public abstract void Tick();

        public virtual void OnStateEnter()
        {
        }

        public virtual void OnStateExit()
        {
        }

        public virtual void UseMovement()
        {
            
        }

        public State(Enemy_StateMachine enemy)
        {
            Enemy = enemy;
        }

    }
