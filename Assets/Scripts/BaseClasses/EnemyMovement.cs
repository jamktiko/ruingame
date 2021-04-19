
    using UnityEngine;
    using System.Collections;
    
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyMovement : BaseMovement
    {
        
        [HideInInspector] public Vector3 MovementInput { get; protected set; }
        public Enemy_StateMachine _characterController;
        private bool stunned = false;
        protected static readonly int MovementSpeed = Animator.StringToHash("movementSpeed");
        
    public override void OnAttack()
    {
        if (GroundedEntity && !dashing)
        {
            _animatorVelocity = 0;
            _characterRigidBody.velocity = Vector3.zero;
        }
    }

    public void OnStun()
    {
        stunned = true;
    }

    public void OnStunEnd()
    {
        stunned = false;
    }
    public virtual void Start()
    {
        _characterRigidBody = GetComponent<Rigidbody>();
        _characterAnimator = GetComponentInChildren<Animator>();
        CreateStepRays();
        //_characterAnimator.SetFloat(MovementSpeed, _movementSpeed/10);
    }
    
    public virtual void OnMove(Vector2 movementInput)
    {
        _previousMovementInput = movementInput;
    }

    public virtual void Update()
    {
       CalculateMovement();
       // HandleMovementAnimation();
    }
    public virtual void FixedUpdate()
    {
       // GroundCheck();
       // StepClimb();
        if (!stunned && !attacking && !dashing && MovementInput.magnitude > 0)
        {
            RotateTowardsMovement(turnSmoothing);
        }
        else
        {
            _animatorVelocity = 0.0f;
        }
        
    }
    public override void Move(Vector3 movementInput)
    {
        MovementInput = movementInput;
        Vector3 movementVelocity = _movementSpeed * MovementInput;
        _characterRigidBody.velocity =
            new Vector3(movementVelocity.x, _characterRigidBody.velocity.y, movementVelocity.z);
    }

    public virtual void GroundCheck()
    {
        GroundedEntity = Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z),groundCheckRadius, layerMask);
    }

    public virtual void HandleMovementAnimation()
    {
        if (MovementInput.magnitude > 0 && _animatorVelocity < 1.0f)
        {
            _animatorVelocity += Time.deltaTime * animatorAcceleration;
        }
        if (MovementInput.magnitude == 0 && _animatorVelocity > 0.0f)
        {
            _animatorVelocity -= Time.deltaTime * animatorDeceleration;
        }
        if (MovementInput.magnitude == 0 && _animatorVelocity < 0.0f)
        {
            _animatorVelocity = 0.0f;
        }
        _characterAnimator.SetFloat("Blend", _animatorVelocity);
    }

    public virtual void OnJump()
    {
    }
    public override void RotateTowardsMovement(float amount)
    {
        float singleStep = amount * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, MovementInput, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

    }
    public override void RotateTowardsMovement(Vector3 direction, float amount)
    {
        float singleStep = amount * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public virtual void StepClimb()
    {
        if (GroundedEntity)
        {
            RaycastHit hitLower;
        if (Physics.Raycast(_stepRayLower.transform.position, transform.TransformDirection(Vector3.forward),
            out hitLower, 0.5f))
        {
            RaycastHit hitUpper;
            if (!Physics.Raycast(_stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward),
                out hitUpper, 0.5f))
            {
                _characterRigidBody.position -= new Vector3(0f, -stepSmooth*Time.deltaTime, 0f)  - transform.forward * Time.deltaTime * 0.5f;
            }
        }
         
        RaycastHit hitLower45;
        if (Physics.Raycast(_stepRayLower.transform.position, transform.TransformDirection(1.5f, 0, 1),
            out hitLower45, 0.5f))
        {
           
            RaycastHit hitUpper45;
            if (!Physics.Raycast(_stepRayUpper.transform.position, transform.TransformDirection(1.5f, 0, 1),
                out hitUpper45, 0.5f))
            {
                _characterRigidBody.position -= new Vector3(0f, -stepSmooth*Time.deltaTime, 0f)  - transform.forward * Time.deltaTime * 0.5f;
            }
                
        }
        RaycastHit hitLowerMinus45;
        if (Physics.Raycast(_stepRayLower.transform.position, transform.TransformDirection(-1.5f, 0, 1),
            out hitLowerMinus45, 0.5f))
        {
            RaycastHit hitUpperMinus45;
            if (!Physics.Raycast(_stepRayUpper.transform.position, transform.TransformDirection(-1.5f, 0, 1),
                out hitUpperMinus45, 0.5f))
            {
                _characterRigidBody.position -= new Vector3(0f, -stepSmooth*Time.deltaTime, 0f)   - transform.forward * Time.deltaTime * 0.5f;
            }
                
        }
        }
    }
    public override void SetMovementSpeed(float amount)
    {
        _movementSpeed = amount;
    }
    public override void SetJumpHeight(float amount)
    {
        _jumpHeight = amount;
    }
    public override void HandleKnockBack(Vector3 target, float force)
    {
        if (canKnockback)
        {
            stunned = true;
            var dir = target - transform.position;
            dir.y = 0;
            _characterRigidBody.AddForce(dir * force);
            canKnockback = false;
            StartCoroutine("KnockbackReset");
        }
    }
    public override IEnumerator KnockbackReset()
    {
        yield return new WaitForSeconds(1f);
        canKnockback = true;
    }
    
}
