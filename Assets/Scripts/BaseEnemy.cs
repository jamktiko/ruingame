using UnityEngine;

namespace DefaultNamespace
{
    public class BaseEnemy : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 1f;

        [SerializeField] private CharacterController _characterController;
        
        private State currentState;
        
        public Attack _attack;
        
        [SerializeField]
        public Transform _playerTransform;

        private void Start()
        {
            _playerTransform = FindObjectOfType<PlayerMovement>().transform;
            _characterController = GetComponent<CharacterController>();
            SetState(new MoveTowardsPlayerState(this));
        }

        private void Update()
        {
            currentState.Tick();
        }

        public void SetState(State state)
        {
            currentState?.OnStateExit();

            currentState = state;

            currentState?.OnStateEnter();
        }

        public void MoveToward(Vector3 destination)
        {
            var direction = GetDirection(_playerTransform.position);
            direction.y = 0;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, 90, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
            _characterController.Move(direction * (Time.deltaTime * moveSpeed));
        }

        private Vector3 GetDirection(Vector3 destination)
        {
            return (destination - transform.position).normalized;
        }
    }
}