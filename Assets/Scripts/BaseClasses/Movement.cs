using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        [HideInInspector] public Vector3 movementInput;
        [HideInInspector] public Vector3 movementVector; 

        protected Vector3 _entityVelocity;
        
        public bool _groundedEntity;
        
        //Could refer from game manager?
        protected float gravityValue = -9.81f;
        
        protected CharacterController _characterController;
        
        //Get from entity stats
        
        public float _movementSpeed = 10f;
        [SerializeField]
        protected float _jumpHeight = 1.0f;
        [SerializeField]
        protected float turnSmoothing = 15f;

        public virtual void Update()
        {
            //Gravity check
            _entityVelocity.y += gravityValue * Time.deltaTime;
            _characterController.Move(_entityVelocity * Time.deltaTime);
            _groundedEntity = _characterController.isGrounded;
            if (_groundedEntity && _entityVelocity.y < 0)
            {
                _entityVelocity.y = -0.2f;
            }
            _characterController.Move(movementInput * (Time.deltaTime * _movementSpeed));
        }

        public virtual void Start()
        {
            _characterController = GetComponent<CharacterController>();
        }
        
        public virtual void OnEnable()
        {

        }

        public virtual void OnDisable()
        {

        }

        public virtual void OnJump()
        {
            if (_groundedEntity && _entityVelocity.y < 0)
            {
                _entityVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * gravityValue);
            }

        }
        
        public virtual void OnMove(Vector2 movement)
        {
            movementInput = movement;
        } 
    }
}