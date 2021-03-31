using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(ComboHandler))]
public class PlayerAttackHandler : ComboAttackHandler
{
    private InputReader _inputReader;
    private PlayerMovementController _movementControl;
    
    public override void Awake()
    {
        base.Awake();
        _inputReader = GameManager.Instance.playerInputReader;
        _movementControl = GetComponent<PlayerMovementController>();
    }
    public override void OnEnable()
    {
        try
        {
            _inputReader.AttackEvent += OnAttack;
        }
        catch
        {
        }
    }
    public override void OnDisable()
    {
        _inputReader.AttackEvent -= OnAttack;
    }
    protected override bool CheckAttackConditions()
    {
        if (!_movementControl.GroundedEntity)
            return false;
        if (_movementControl.dashing)
            return false;
        return base.CheckAttackConditions();
    }
    public override void EndAttack()
    {
        base.EndAttack();
        _movementControl.attacking = attacking;
    }

    protected override void StartAttack()
    {
        base.StartAttack();
        _movementControl.attacking = attacking;
    }
    protected override void TurnTowardsNearest()
    {
        var nearestTarget = _attackTargeting.FindNearestTargetInRadius(currentAttack.radius + 10f);
        _movementControl.RotateTowardsMovement(nearestTarget, 1000f);
    }

}
