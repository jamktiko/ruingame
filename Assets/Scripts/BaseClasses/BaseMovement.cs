
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
        Ray ray = new Ray(transform.position, Vector3.down);
        GroundedEntity = Physics.SphereCast(ray, 0.5f,groundCheckRadius, layerMask);
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
        float capsuleMaxHeight = 2f;
        float capsuleMaxRadius = 0.5f;
        float maxDistance = 10f;
        CapsuleCollider[] colliders = GetComponents<CapsuleCollider>();
        Vector3 direction = DashDirection();
        LayerMask cameraCollision = LayerMask.GetMask("CameraCollision");

        if (!CheckIfPlayerTouchesWall(ref maxDistance, capsuleMaxHeight, capsuleMaxRadius, cameraCollision))
        {
            CheckDashHitOnWalls(ref maxDistance, capsuleMaxHeight, capsuleMaxRadius, direction, cameraCollision);
            CheckOverlaps(ref maxDistance, startPos, 2f, 0.8f);
        }
        FreezePosition(true);
        EneableColliders(false, colliders);

        _characterAnimator.SetBool("dashing", true);
        dashing = true;
        for (float ft = duration; ft >= 0; ft -= Time.deltaTime)
        {
            Move2(maxDistance, startPos, direction);
            yield return new WaitForFixedUpdate();
        }
        dashing = false;
        _characterAnimator.SetBool("dashing", false);
        EneableColliders(true, colliders);
        FreezePosition(false);
    }

    Vector3 DashDirection()
    {
        if (MovementInput == Vector3.zero)
        {
            _animatorVelocity = 0;
            return transform.forward;
        }
        return MovementInput;
    }

    private void Move2(float hitDistance, Vector3 startPos, Vector3 direction)
    {
        float dashDistance = Vector3.Distance(transform.position, startPos);
        float step = _movementSpeed * Time.deltaTime;

        if (dashDistance + step <= hitDistance)
        {
            transform.position += direction * step;
        }
    }

    private void FreezePosition(bool all)
    {
        if (all)
            _characterRigidBody.constraints = RigidbodyConstraints.FreezeAll;
        else
            _characterRigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    private void EneableColliders(bool enable, CapsuleCollider[] colliders)
    {
        foreach (var item in colliders)
        {
            item.enabled = enable;
        }
    }

    private bool CheckIfPlayerTouchesWall(ref float maxDistance, float capsuleMaxHeight, float capsuleMaxRadius, LayerMask layer)
    {
        Vector3 p1 = transform.position;
        Vector3 p2 = transform.position + Vector3.up * capsuleMaxHeight;
        Collider[] colliders = Physics.OverlapCapsule(p1, p2, capsuleMaxRadius, layer, QueryTriggerInteraction.Collide);

        foreach (var item in colliders)
        {
            if (item.transform.tag != "Ground")
            {
                maxDistance = 0f;
                return true;
            }
        }
        return false;
    }

    private void CheckDashHitOnWalls(ref float maxDistance,  float capsuleMaxHeight, float capsuleMaxRadius, Vector3 direction, LayerMask layer)
    {
        Vector3 p1 = transform.position;
        Vector3 p2 = transform.position + Vector3.up * capsuleMaxHeight;
        RaycastHit[] hits = Physics.CapsuleCastAll(p1, p2, capsuleMaxRadius, direction, maxDistance, layer, QueryTriggerInteraction.Collide);

        foreach (var item in hits)
        {
            if (item.transform.tag != "Ground")
            {
                maxDistance = item.distance;
                break;
            }
        }
    }

    private void CheckOverlaps(ref float maxDistance, Vector3 startPos, float capsuleMaxHeight, float capsuleMaxRadius)
    {
        LayerMask enemyLayer = LayerMask.GetMask("EnemyLayer");
        LayerMask collisionObject = LayerMask.GetMask("CollisionObject");
        Vector3 maxPos = startPos + transform.forward * maxDistance;
        while (CheckOverlapOnLayer(enemyLayer, maxPos, capsuleMaxHeight, capsuleMaxRadius) || CheckOverlapOnLayer(collisionObject, maxPos, capsuleMaxHeight, capsuleMaxRadius))
        {
            maxDistance -= 0.5f;
            maxPos = startPos + transform.forward * maxDistance;
        }
    }

    private bool CheckOverlapOnLayer(LayerMask layer, Vector3 maxPos, float capsuleMaxHeight, float capsuleMaxRadius)
    {
        bool overlap = Physics.CheckCapsule(maxPos, maxPos + Vector3.up * capsuleMaxHeight, capsuleMaxRadius, layer, QueryTriggerInteraction.Collide);
        return overlap;
    }
}
