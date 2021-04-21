using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovementController : BaseMovement
{
    private TransformAnchor _gameplayCameraTransform;
    private InputReader _inputReader = default;

    //Used to calculate movement vectors

    public override void OnEnable()
    {
        try
        {
            _inputReader.MoveEvent += OnMove;
            _inputReader.JumpEvent += OnJump;
            _inputReader.AttackEvent += OnAttack;
        }
        catch{}
    }

    public override void OnDisable()
    {
        _inputReader.AttackEvent -= OnAttack;
        _inputReader.MoveEvent -= OnMove;
        _inputReader.JumpEvent -= OnJump;
    }

    public override void OnAttack()
    {
        if (GroundedEntity && !dashing)
        {
            _animatorVelocity = 0;
            _characterRigidBody.velocity = Vector3.zero;
        }
    }
    public override void Start()
    {
        base.Start();
        _inputReader = PlayerManager.Instance.playerInputReader;
    }
    public override void OnMove(Vector2 movement)
    {
        _previousMovementInput = movement;
    }

    public override void Move(Vector3 MovementInput)
    {
        Vector3 movementVelocity = _movementSpeed * MovementInput;
       _characterRigidBody.velocity =
            new Vector3(movementVelocity.x, _characterRigidBody.velocity.y, movementVelocity.z);
    }

    public override void OnJump()
    {
        if (GroundedEntity && !attacking)
        {
            _characterRigidBody.AddForce(Vector3.up * _jumpHeight);
        }
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
    
    public override void CalculateMovement()
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
    
    public void SetPlayerCamera(TransformAnchor cam)
    {
        _gameplayCameraTransform = cam;
    }

    public override void SetMovementSpeed(float amount)
    {
        _movementSpeed = amount;
        _characterAnimator.SetFloat(MovementSpeed, _movementSpeed/10);
    }
    public override void SetJumpHeight(float amount)
    {
        _jumpHeight = amount;
    }
    public override void HandleKnockBack(Vector3 target, float force)
    {
        currentTarget = target;
        if (canKnockback)
        {
            canKnockback = false;
            //_characterRigidbody.velocity = Vector3.zero;
            Vector3 direction = (transform.position - target).normalized;
            _characterRigidBody.AddForce(direction * force, ForceMode.Impulse);
            StartCoroutine("KnockbackReset");
        }
    }
    public override IEnumerator KnockbackReset()
    {
        yield return new WaitForSeconds(1f);
        canKnockback = true;
    }
}
