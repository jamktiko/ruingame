
using DefaultNamespace;

public class CapeOfAgility : ArtifactEffect
{
    private SprintSkill sprintSkill;
    private float normalSpeed;
    private float speedModifier = 0.05f;

    private void Start()
    {
        _playerReference = PlayerManager.Instance;
        normalSpeed = _playerReference._playerData.entityMovementSpeed;
        speedModifier = normalSpeed * speedModifier;
    }

    public override void AddEffect()
    {
        ChangeModifier(ArtifactModifier.Type.Plus);
        base.AddEffect();
        if (_playerReference.TryGetComponent(out sprintSkill))
        {
            sprintSkill.EndSprintEvent += ModifyMovmentSpeed;
        }
    }

    private void ModifyMovmentSpeed()
    {
        ChangeModifier(ArtifactModifier.Type.Plus);
        base.AddEffect();
        Invoke("ReduceSpeed", 2f);

    }

    private void ReduceSpeed()
    {
        ChangeModifier(ArtifactModifier.Type.Minus);
        base.AddEffect();

    }

    private void ChangeModifier(ArtifactModifier.Type type)
    {
        artifactModifiers[0].type = type;
        artifactModifiers[0].modifier = speedModifier;
    }

    private void OnDestroy()
    {
        _playerReference = PlayerManager.Instance;
        if (_playerReference._playerData.entityMovementSpeed > normalSpeed)
            _playerReference._playerData.entityMovementSpeed = normalSpeed;
    }

}
