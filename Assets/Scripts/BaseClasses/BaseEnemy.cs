using System.Collections;

using UnityEngine;


namespace DefaultNamespace
{
    [RequireComponent(typeof(EnemyHealth))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(BaseAttackHandler))]
    public class BaseEnemy : MonoBehaviour
    {
        public Movement _movementControl;
        
        private State _currentState;

        public bool alive = true;

        //private float _knockbackStrength = default;
        
        [HideInInspector]
        public BaseAttackHandler attack;

        public float attackRange;
        
        [HideInInspector]
        public bool stunned;   
        
        public Transform playerTransform;

        private bool findingPlayer;
        private void Start()
        {
            _movementControl = GetComponent<Movement>();
            attack = GetComponent<BaseAttackHandler>();
            SetState(new MoveTowardsPlayerState(this));
        }

        private void Update()
        {
            if (playerTransform != null) 
                _currentState.Tick();
            else if (!findingPlayer)
            {
                FindPlayer();
            }
        }

        public void FindPlayer()
        {
            findingPlayer = true;
            StartCoroutine("FindPlayerRoutine");
        }

        public IEnumerator FindPlayerRoutine()
        {
            while (playerTransform == null)
            {
                yield return new WaitForSeconds(1f);
                try {playerTransform = PlayerManager.Instance.transform;}
                catch{Debug.Log("No player found!");}
            }

            findingPlayer = false;
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