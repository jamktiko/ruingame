using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour
    {
        public Vector3 movementInput;
        
        public Rigidbody _entityRigidbody;

        //Get from entity stats
        public bool attacking;
        public float movementSpeed = 1000f;
        [SerializeField]
        protected float turnSmoothing = 15f;

        public virtual void Update()
        {
            if(!attacking)
                _entityRigidbody.AddForce((movementInput * (Time.deltaTime * movementSpeed)));
        }

        public virtual void Start()
        {
            _entityRigidbody = GetComponent<Rigidbody>();
        }
    
        /*
        public virtual void OnJump()
        {
            if (groundedEntity && EntityVelocity.y < 0)
            {
                EntityVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * GravityValue);
            }

        }
        */
        public virtual void StopMoving()
        {
            
        }
        public virtual void OnMove(Vector2 movement)
        {
            movementInput = new Vector3(movement.x, 0, movement.y);
        } 
    }
}