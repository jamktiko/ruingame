using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] public TransformAnchor gameplayCameraTransform;

        [SerializeField] private InputReader _playerInput = default;
        [HideInInspector] public Vector3 movementInput;
        [HideInInspector] public Vector3 movementVector; 
        private Vector2 _previousMovementInput;
        
        protected CharacterController _characterController;
    
        //Get from entity stats
        [SerializeField]
        private float _movementSpeed = 10f;
        [SerializeField]
        protected float turnSmoothing = 0.2f;

        private void Update()
        {
            CalculateMovement();
            _characterController.Move(movementInput * (Time.deltaTime * _movementSpeed));
            RotateTowardsMovement();
        }

        private void RotateTowardsMovement()
        {
            float singleStep = turnSmoothing * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, movementInput, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        void OnEnable()
        { 
            //Subscribe to events
            _playerInput.moveEvent += OnMove;
            
        }

        private void OnDisable()
        {
            _playerInput.moveEvent -= OnMove;
        }

        private void CalculateMovement()
        {
            if (gameplayCameraTransform.isSet)
            {
                
                Vector3 cameraForward = gameplayCameraTransform.Transform.forward;
                cameraForward.y = 0f;
                Vector3 cameraRight = gameplayCameraTransform.Transform.right;
                cameraRight.y = 0f;
                
                Vector3 adjustedMovement = cameraRight.normalized * _previousMovementInput.x +
                                           cameraForward.normalized * _previousMovementInput.y;
                movementInput = Vector3.ClampMagnitude(adjustedMovement, 1f);
                movementInput.Normalize();
            }
        }

        private void OnMove(Vector2 movement)
        {
            _previousMovementInput = movement;
        }
    }
}