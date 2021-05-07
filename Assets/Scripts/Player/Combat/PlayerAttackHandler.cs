using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ComboHandler))]
public class PlayerAttackHandler : ComboAttackHandler
{
    /// <summary>
    /// For artifact effects
    /// </summary>
    public event UnityAction<float, Health> PlayerAttackEvent = delegate { };
    public float artifactDamageModifier = 0;

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
        Debug.Log("Ended Attack");
        base.EndAttack();
        _movementControl.attacking = false;
    }
    public override void AttemptAttack()
    {
        StartAttack();
        try { currentAttack = GetAttack(); }
        catch {Debug.Log("No attack to execute!");}
        TurnTowardsNearest();
        ExecuteAttack();
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
