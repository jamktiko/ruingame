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
    private MovementController _movementControl;
    
    public override void Awake()
    {
        base.Awake();
        _inputReader = GameManager.Instance.playerInputReader;
        _movementControl = GetComponent<MovementController>();
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

    /// <summary>
    /// For artifact events
    /// </summary>
    /// <param name="targets"></param>
    /// <param name="attack"></param>
    protected override void DamageAllCurrentTargets(GameObject[] targets, BaseAttack attack)
    {
        foreach (GameObject target in targets)
        {
            try
            {
                var targetHealth = target.GetComponent<Health>();
                float damage = attack.baseDamage * _entityDamage;
                PlayerAttackEvent.Invoke(damage, targetHealth); 
                targetHealth.DealDamage(damage + artifactDamageModifier);
            }
            catch
            {
                Debug.Log("Target has no health!");
            }
        }

        KnockBackAllTargets(targets, attack);
        ClearTargets();

        artifactDamageModifier = 0;
    }
}
