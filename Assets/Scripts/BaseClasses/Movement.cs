using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        public Vector3 movementInput;
        public Vector3 movementVector; 

        protected Vector3 EntityVelocity;
        public bool groundedEntity;
        
        //Could refer from game manager?
        protected float GravityValue = -9.81f;
        
        protected CharacterController CharacterController;
        
        //Get from entity stats
        
        public float movementSpeed = 10f;
        protected float jumpHeight = 1.0f;
        [SerializeField]
        protected float turnSmoothing = 15f;

        public virtual void Update()
        {
            //Gravity check
            EntityVelocity.y += GravityValue * Time.deltaTime;
            groundedEntity = CharacterController.isGrounded;
            if (groundedEntity && EntityVelocity.y < 0)
            {
                EntityVelocity.y = -0.2f;
            }
            CharacterController.Move(EntityVelocity * Time.deltaTime);
            CharacterController.Move(movementInput * (Time.deltaTime * movementSpeed));
        }

        public virtual void Start()
        {
            CharacterController = GetComponent<CharacterController>();
        }
        
        public virtual void OnEnable()
        {

        }

        public virtual void OnDisable()
        {

        }

        public virtual void OnJump()
        {
            if (groundedEntity && EntityVelocity.y < 0)
            {
                EntityVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * GravityValue);
            }

        }
        
        public virtual void OnMove(Vector2 movement)
        {
            movementInput = new Vector3(movement.x, 0, movement.y);
        } 
    }
}