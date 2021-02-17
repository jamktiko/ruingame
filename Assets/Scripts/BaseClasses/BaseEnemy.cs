using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class BaseEnemy : MonoBehaviour
    {

        public Movement _movementControl;
        
        private State _currentState;

        public bool alive = true;

        //private float _knockbackStrength = default;
        
        [HideInInspector]
        public Attack attack;
        
        [HideInInspector]
        public bool stunned;   
        
        public Transform playerTransform;

        private void Start()
        {
            playerTransform = PlayerManager.Instance.transform;
            _movementControl = GetComponent<Movement>();
            attack = GetComponent<Attack>();
            SetState(new MoveTowardsPlayerState(this));
        }

        private void Update()
        {
            if (PlayerManager.Instance != null) 
                _currentState.Tick();
        }

        public void SetState(State state)
        {
            _currentState?.OnStateExit();

            _currentState = state;

            _currentState?.OnStateEnter();
        }

        public virtual void MoveToward(Vector3 destination)
        {
            var direction = GetDirection(playerTransform.position);
            direction.y = 0;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, 90, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
            var enemyMoveInput = new Vector2(direction.x, direction.z);
            _movementControl.OnMove(enemyMoveInput);
        }

        protected virtual Vector3 GetDirection(Vector3 destination)
        {
            destination.y = 0;
            return (destination - transform.position).normalized;
        }
    }
}