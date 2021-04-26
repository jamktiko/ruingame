
    using UnityEngine;
    using System.Collections;
    using DefaultNamespace;

    [RequireComponent(typeof(Rigidbody))]
    public class EnemyMovement : BaseMovement
    {
        public Enemy_StateMachine _characterController;
        private bool stunned = false;

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
            _characterController = GetComponent<Enemy_StateMachine>();
            canKnockback = true;
        }

        public override void OnStun()
    {
        stunned = true;
    }

    public void OnStunEnd()
    {
        stunned = false;
        attacking = false;
    }
    
    public override void Move(Vector3 movementInput)
    {
        MovementInput = movementInput;
        Vector3 movementVelocity = _movementSpeed * MovementInput;
        _characterRigidBody.velocity =
            new Vector3(movementVelocity.x, _characterRigidBody.velocity.y, movementVelocity.z);
    }
    public override void FixedUpdate()
    {
        RotateTowardsMovement(turnSmoothing);
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
    public override void HandleMovementAnimation()
    {
        _characterAnimator.SetFloat("movementSpeed", _movementSpeed);
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
            var dir = transform.position - target;
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
