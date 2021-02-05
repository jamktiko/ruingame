using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class PlayerMovement : Movement
    {
        
        public TransformAnchor gameplayCameraTransform;
        
        public InputReader _inputReader = default;

        private Vector2 _previousMovementInput;
    
        public override void Update()
        {
            //Gravity Check
            _entityVelocity.y += gravityValue * Time.deltaTime;
            _characterController.Move(_entityVelocity * Time.deltaTime);
            if (_groundedEntity && _entityVelocity.y < 0)
            {
                _entityVelocity.y = -0.3f;
            }
            _groundedEntity = _characterController.isGrounded;
            //Movement
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
        
        public override void OnEnable()
        {
            try
            {
                _inputReader.moveEvent += OnMove;
                _inputReader.jumpEvent += OnJump;
            }
            catch{}
        }

        public override void OnDisable()
        {
            _inputReader.moveEvent -= OnMove;
            _inputReader.jumpEvent -= OnJump;
        }

        public override void OnJump()
        {
            if (_groundedEntity && _entityVelocity.y < 0)
            {
                _entityVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * gravityValue);
            }
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

        public override void OnMove(Vector2 movement)
        {
            _previousMovementInput = movement;
        }
    }
}