using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class MovementController : MonoBehaviour
{
    [Header("Input and Camera")]
    public TransformAnchor gameplayCameraTransform;
    public InputReader _inputReader = default;
    
    [Header("Ground Check")]
    public LayerMask layerMask;
    [SerializeField] private float groundCheckRadius;
    public bool _groundedEntity;

    [Header("Animation parameters")]
    public float animatorDeceleration = 1f;
    public float animatorAcceleration = 1f;
    private float animatorVelocity = 0.0f;
    private Animator characterAnimator;
    
    protected Rigidbody _characterRigidBody;
    
    //Used to calculate movement vectors
    private Vector2 _previousMovementInput;
    [HideInInspector] public Vector3 movementInput;
    
    //Get from entity stats
    [Header("Movement values")]
    public float _movementSpeed = 10f;
    public float _jumpHeight = 1.0f;
    
    [Header("Turn Rotation")]
    [SerializeField]
    protected float turnSmoothing = 15f;
    
    [Header("Stair stepping")]
    [SerializeField] private GameObject stepRayUpper;
    [SerializeField] private GameObject stepRayLower;
    [SerializeField] private float stepHeight = 0.3f;
    [SerializeField] private float stepSmooth = 0.1f;
    public void OnEnable()
    {
        try
        {
            //Subscribe to movement events
            _inputReader.moveEvent += OnMove;
            _inputReader.jumpEvent += OnJump;
            _inputReader.attackEvent += OnAttack;
        }
        catch{}
    }

    public void OnDisable()
    {
        _inputReader.attackEvent -= OnAttack;
        _inputReader.moveEvent -= OnMove;
        _inputReader.jumpEvent -= OnJump;
    }

    public void OnAttack()
    {
        if (_groundedEntity)
        {
            _previousMovementInput = Vector3.zero;
            animatorVelocity = 0;
        }
    }
    public void Start()
    {
        _characterRigidBody = GetComponent<Rigidbody>();
        characterAnimator = GetComponentInChildren<Animator>();
        CreateStepRays();
    }

    private void CreateStepRays()
    {
        stepRayLower = new GameObject("stepRayLower");
        stepRayUpper = new GameObject("stepRayUpper");
        stepRayLower.transform.parent = gameObject.transform;
        stepRayUpper.transform.parent = gameObject.transform;
        stepRayLower.transform.position = new Vector3(0, 0.08f, 0);
        var stp = stepRayUpper.transform.position;
        stepRayUpper.transform.position = new Vector3(0, stepHeight, 0);
    }
    public void OnMove(Vector2 movement)
    {
        _previousMovementInput = movement;
    }

    public void Update()
    {
        CalculateMovement();
        
    }
    public void FixedUpdate()
    {
        GroundCheck();
        Move();
        StepClimb();
        RotateTowardsMovement();
        HandleMovementAnimation();
    }
    public void Move()
    {
        Vector3 movementVelocity =  _movementSpeed * movementInput;
        _characterRigidBody.velocity = new Vector3(movementVelocity.x, _characterRigidBody.velocity.y, movementVelocity.z);
    }

    public void GroundCheck()
    {
        _groundedEntity = Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z),groundCheckRadius, layerMask);
    }

    public void HandleMovementAnimation()
    {
        if (movementInput.magnitude > 0 && animatorVelocity < 1.0f)
        {
            animatorVelocity += Time.deltaTime * animatorAcceleration;
        }
        if (movementInput.magnitude == 0 && animatorVelocity > 0.0f)
        {
            animatorVelocity -= Time.deltaTime * animatorDeceleration;
        }
        if (movementInput.magnitude == 0 && animatorVelocity < 0.0f)
        {
            animatorVelocity = 0.0f;
        }
        characterAnimator.SetFloat("Blend", animatorVelocity);
    }

    public void OnJump()
    {
        //CHECK FOR ANIMATION FINISHED TOO
        if (_groundedEntity)
        {
            _characterRigidBody.AddForce(Vector3.up * _jumpHeight);
        }
    }
    private void RotateTowardsMovement()
    {
        float singleStep = turnSmoothing * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, movementInput, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

    }
    private void CalculateMovement()
    {
        if (gameplayCameraTransform.isSet)
        {
            Vector3 cameraForward = gameplayCameraTransform.Transform.forward;
            cameraForward.y = 0f;
            Vector3 cameraRight = gameplayCameraTransform.Transform.right;
            cameraRight.y = 0f;
            Vector3 adjustedMovement = cameraRight.normalized * _previousMovementInput.x + cameraForward.normalized * _previousMovementInput.y;
            movementInput = Vector3.ClampMagnitude(adjustedMovement, 1f);
            movementInput.Normalize();
        }
    }

    private void StepClimb()
    {
        RaycastHit hitLower;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.forward),
            out hitLower, 0.5f))
        {
            RaycastHit hitUpper;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward),
                out hitUpper, 0.5f))
            {
                _characterRigidBody.position -= new Vector3(0f, -stepSmooth*Time.deltaTime, 0f);
            }
                
        }
        RaycastHit hitLower45;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(1.5f, 0, 1),
            out hitLower45, 0.5f))
        {
           
            RaycastHit hitUpper45;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(1.5f, 0, 1),
                out hitUpper45, 0.5f))
            {
                _characterRigidBody.position -= new Vector3(0f, -stepSmooth*Time.deltaTime, 0f);
            }
                
        }
        RaycastHit hitLowerMinus45;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(-1.5f, 0, 1),
            out hitLowerMinus45, 0.5f))
        {
            RaycastHit hitUpperMinus45;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(-1.5f, 0, 1),
                out hitUpperMinus45, 0.5f))
            {
                _characterRigidBody.position -= new Vector3(0f, -stepSmooth*Time.deltaTime, 0f);
            }
                
        }
    }
    
}
