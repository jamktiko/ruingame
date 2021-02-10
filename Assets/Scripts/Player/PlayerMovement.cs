using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class PlayerMovement : Movement
    {
        public TransformAnchor gameplayCameraTransform;
        
        private InputReader _inputReader = default;
        private Vector2 _previousMovementInput;
    
        public override void Update()
        {
            //Gravity Check
            EntityVelocity.y += GravityValue * Time.deltaTime;
            CharacterController.Move(EntityVelocity * Time.deltaTime);
            if (groundedEntity && EntityVelocity.y < 0)
            {
                EntityVelocity.y = -0.3f;
            }
            groundedEntity = CharacterController.isGrounded;
            //Movement
            CalculateMovement();
            CharacterController.Move(movementInput * (Time.deltaTime * movementSpeed));
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
                _inputReader.MoveEvent += OnMove;
                _inputReader.JumpEvent += OnJump;
            }
            catch{}
        }

        public override void OnDisable()
        {
            _inputReader.MoveEvent -= OnMove;
            _inputReader.JumpEvent -= OnJump;
        }

        public override void OnJump()
        {
            if (groundedEntity && EntityVelocity.y < 0)
            {
                EntityVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * GravityValue);
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