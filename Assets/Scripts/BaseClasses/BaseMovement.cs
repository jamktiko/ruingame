
    using UnityEngine;
    using System.Collections;
    
    [RequireComponent(typeof(Rigidbody))]
    public class BaseMovement : MonoBehaviour
    { 
        [Header("Ground Check")]
    [SerializeField] protected LayerMask layerMask;
    [SerializeField] protected float groundCheckRadius;
    public bool GroundedEntity;

    [Header("Animation parameters")]
    protected float animatorDeceleration = 10f;
    protected float animatorAcceleration = 10f;
    protected float _animatorVelocity = 0.0f;
    public Animator _characterAnimator;
    
    protected Rigidbody _characterRigidBody;
    //Used to calculate movement vectors
    protected Vector2 _previousMovementInput;
    [HideInInspector] public Vector3 MovementInput { get; protected set; }

    public float _movementSpeed = default;
    public float _jumpHeight = default;
    
    [Header("Turn Rotation")]
    [SerializeField]
    protected float turnSmoothing = 15f;
    
    [Header("Stair stepping")]
    [SerializeField] protected float stepHeight = 0.3f;
    [SerializeField] protected float stepSmooth = 0.3f;
    protected GameObject _stepRayUpper;
    protected GameObject _stepRayLower;

    public bool dashing { get; protected set; }
    public bool attacking;
    public bool canKnockback = true;
    protected Vector3 currentTarget;

    protected static readonly int MovementSpeed = Animator.StringToHash("movementSpeed");
    public virtual void OnEnable()
    {
    }

    public virtual void OnDisable()
    {
    }

    public virtual void OnAttack()
    {
        if (GroundedEntity && !dashing)
        {
            _animatorVelocity = 0;
            _characterRigidBody.velocity = Vector3.zero;
        }
    }

    public virtual void OnStun()
    {
        _animatorVelocity = 0;
        _movementSpeed = 0;
    }
    public virtual void Start()
    {
        _characterRigidBody = GetComponent<Rigidbody>();
        _characterAnimator = GetComponentInChildren<Animator>();
        CreateStepRays();
        _characterAnimator.SetFloat(MovementSpeed, _movementSpeed/10);
    }

    public virtual void CreateStepRays()
    {
        _stepRayLower = new GameObject("stepRayLower");
        _stepRayUpper = new GameObject("stepRayUpper");
        _stepRayLower.transform.parent = gameObject.transform;
        _stepRayUpper.transform.parent = gameObject.transform;
        _stepRayLower.transform.position = new Vector3(0, 0.08f, 0);
        var stp = _stepRayUpper.transform.position;
        _stepRayUpper.transform.position = new Vector3(0, stepHeight, 0);
    }
    public virtual void OnMove(Vector2 movementInput)
    {
        _previousMovementInput = movementInput;
    }

    public virtual void Update()
    {
        CalculateMovement();
        HandleMovementAnimation();
    }
    public virtual void FixedUpdate()
    {
        GroundCheck();
        StepClimb();
        if (!attacking && !dashing && MovementInput.magnitude > 0)
        {
            Move(MovementInput);
            RotateTowardsMovement(turnSmoothing);
        }
        else
        {
            _animatorVelocity = 0.0f;
        }
    }
    public virtual void Move(Vector3 MovementInput)
    {

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
    public virtual void RotateTowardsMovement(float amount)
    {

    }
    public virtual void RotateTowardsMovement(Vector3 direction, float amount)
    {

    }
    
    public virtual void CalculateMovement()
    {
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

    public virtual void OnDash(float duration)
    {
       RotateTowardsMovement(1000f);
       StartCoroutine(Dashing(duration));
    }
    public virtual IEnumerator Dashing(float duration)
    {
       // _characterAnimator.SetBool("dashing", true);
        dashing = true;
        var originalInput = MovementInput;
        if (originalInput == Vector3.zero)
        {
            _animatorVelocity = 0;
            originalInput = transform.forward;
        }
        for (float ft = duration-0.2f; ft >= 0; ft -= 0.1f)
        {
            Move(originalInput);
            yield return new WaitForSeconds(.1f);
        }
        dashing = false;
        //_characterAnimator.SetBool("dashing", false);
    }
    public virtual void SetMovementSpeed(float amount)
    {
    }
    public virtual void SetJumpHeight(float amount)
    {
        
    }
    public virtual void HandleKnockBack(Vector3 target, float force)
    {

    }
    public virtual IEnumerator KnockbackReset()
    {
        yield return new WaitForSeconds(1f);
    }

    /// <summary>
    /// Dash with translate on FixedUpdate. 
    /// Changes and reverts rigidbody properties, disables/enables colliders and checks if dash hit walls 
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    public void OnDash2(float duration)
    {
        RotateTowardsMovement(1000f);
        StartCoroutine(Dashing2(duration));
    }

    public virtual IEnumerator Dashing2(float duration)
    {
        Vector3 startPos = transform.position;
        float capsuleMaxHeight = 1.99f;
        float capsuleMaxRadius = 0.49f;
        RaycastHit hit;
        bool hitWalls = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * capsuleMaxHeight, capsuleMaxRadius, transform.forward, out hit, 20f, LayerMask.GetMask("CameraCollision"), QueryTriggerInteraction.Collide);
        float hitDistance = hit.distance - capsuleMaxRadius;
        CapsuleCollider[] colliders = GetComponents<CapsuleCollider>();

        ChangeRigidbodyToKinematic(true, RigidbodyInterpolation.Interpolate, CollisionDetectionMode.Discrete);
        EneableColliders(false, colliders);

        _characterAnimator.SetBool("dashing", true);
        dashing = true;
        for (float ft = duration; ft >= 0; ft -= Time.deltaTime)
        {
            Move2(hitWalls, hitDistance, startPos);
            yield return new WaitForFixedUpdate();
        }
        dashing = false;
        _characterAnimator.SetBool("dashing", false);

        EneableColliders(true, colliders);
        ChangeRigidbodyToKinematic(false, RigidbodyInterpolation.Interpolate, CollisionDetectionMode.ContinuousDynamic);
    }

    private void Move2(bool hitWall, float hitDistance, Vector3 startPos)
    {
        float dashDistance = Vector3.Distance(transform.position, startPos);
        float step = _movementSpeed * Time.deltaTime;

        if (!hitWall || dashDistance + step <= hitDistance)
        {
            transform.Translate(Vector3.forward * step);
        }
    }

    void ChangeRigidbodyToKinematic(bool isKinematic, RigidbodyInterpolation interpolation, CollisionDetectionMode collisionDetection)
    {
        //Camera may jitter without interpolation or if its changed back to None 
        _characterRigidBody.interpolation = interpolation;
        _characterRigidBody.collisionDetectionMode = collisionDetection;
        _characterRigidBody.isKinematic = isKinematic;
    }

    private void EneableColliders(bool enable, CapsuleCollider[] colliders)
    {
        foreach (var item in colliders)
        {
            item.enabled = enable;
        }
    }
}
