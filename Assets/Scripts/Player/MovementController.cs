using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class MovementController : MonoBehaviour
{
    private TransformAnchor _gameplayCameraTransform;
    private InputReader _inputReader = default;
    
    [Header("Ground Check")]
    public LayerMask layerMask;
    [SerializeField] private float groundCheckRadius;
    public bool GroundedEntity { get; private set; }

    [Header("Animation parameters")]
    [SerializeField]
    private float animatorDeceleration = 1f;
    [SerializeField]
    private float animatorAcceleration = 1f;
    private float _animatorVelocity = 0.0f;
    private Animator _characterAnimator;
    
    private Rigidbody _characterRigidBody;
    private bool _attacking;
    
    //Used to calculate movement vectors
    private Vector2 _previousMovementInput;
    [HideInInspector] public Vector3 MovementInput { get; private set; }

    private float _movementSpeed = default;
    private float _jumpHeight = default;
    
    [Header("Turn Rotation")]
    [SerializeField]
    protected float turnSmoothing = 15f;
    
    [Header("Stair stepping")]
    [SerializeField] private float stepHeight = 0.3f;
    [SerializeField] private float stepSmooth = 0.1f;
    private GameObject _stepRayUpper;
    private GameObject _stepRayLower;

    public float attackMovement;
    public bool attacking;
    private void OnEnable()
    {
        try
        {
            _inputReader.MoveEvent += OnMove;
            _inputReader.JumpEvent += OnJump;
            _inputReader.AttackEvent += OnAttack;
        }
        catch{}
    }

    private void OnDisable()
    {
        _inputReader.AttackEvent -= OnAttack;
        _inputReader.MoveEvent -= OnMove;
        _inputReader.JumpEvent -= OnJump;
    }

    private void OnAttack()
    {
        if (GroundedEntity)
        {
            _previousMovementInput = Vector3.zero;
            _animatorVelocity = 0;
        }
    }
    private void Start()
    {
        _characterRigidBody = GetComponent<Rigidbody>();
        _characterAnimator = GetComponentInChildren<Animator>();
        _inputReader = PlayerManager.Instance.playerInputReader;
        CreateStepRays();
    }

    private void CreateStepRays()
    {
        _stepRayLower = new GameObject("stepRayLower");
        _stepRayUpper = new GameObject("stepRayUpper");
        _stepRayLower.transform.parent = gameObject.transform;
        _stepRayUpper.transform.parent = gameObject.transform;
        _stepRayLower.transform.position = new Vector3(0, 0.08f, 0);
        var stp = _stepRayUpper.transform.position;
        _stepRayUpper.transform.position = new Vector3(0, stepHeight, 0);
    }
    private void OnMove(Vector2 movement)
    {
        _previousMovementInput = movement;
    }

    private void Update()
    {
        CalculateMovement();
        
    }
    private void FixedUpdate()
    {
        GroundCheck();
        Move();
        StepClimb();
        RotateTowardsMovement();
        HandleMovementAnimation();
    }
    private void Move()
    {
        Vector3 movementVelocity = _movementSpeed * MovementInput;
        Vector3 attackVelocity = transform.forward * attackMovement;
        _characterRigidBody.velocity = new Vector3(movementVelocity.x, _characterRigidBody.velocity.y, movementVelocity.z) + attackVelocity;
    }

    private void GroundCheck()
    {
        GroundedEntity = Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z),groundCheckRadius, layerMask);
    }

    private void HandleMovementAnimation()
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

    private void OnJump()
    {
        //CHECK FOR ANIMATION FINISHED TOO
        if (GroundedEntity)
        {
            _characterRigidBody.AddForce(Vector3.up * _jumpHeight);
        }
    }
    private void RotateTowardsMovement()
    {
        float singleStep = turnSmoothing * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, MovementInput, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

    }
    private void CalculateMovement()
    {
        if (_gameplayCameraTransform.isSet)
        {
            Vector3 cameraForward = _gameplayCameraTransform.Transform.forward;
            cameraForward.y = 0f;
            Vector3 cameraRight = _gameplayCameraTransform.Transform.right;
            cameraRight.y = 0f;
            Vector3 adjustedMovement = cameraRight.normalized * _previousMovementInput.x + cameraForward.normalized * _previousMovementInput.y;
            MovementInput = Vector3.ClampMagnitude(adjustedMovement, 1f);
            MovementInput.Normalize();
        }
    }

    private void StepClimb()
    {
        RaycastHit hitLower;
        if (Physics.Raycast(_stepRayLower.transform.position, transform.TransformDirection(Vector3.forward),
            out hitLower, 0.5f))
        {
            RaycastHit hitUpper;
            if (!Physics.Raycast(_stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward),
                out hitUpper, 0.5f))
            {
                _characterRigidBody.position -= new Vector3(0f, -stepSmooth*Time.deltaTime, 0f);
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
                _characterRigidBody.position -= new Vector3(0f, -stepSmooth*Time.deltaTime, 0f);
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
                _characterRigidBody.position -= new Vector3(0f, -stepSmooth*Time.deltaTime, 0f);
            }
                
        }
    }

    public void SetPlayerCamera(TransformAnchor cam)
    {
        _gameplayCameraTransform = cam;
    }

    public void SetMovementSpeed(float amount)
    {
        _movementSpeed = amount;
    }
    public void SetJumpHeight(float amount)
    {
        _jumpHeight = amount;
    }
}
