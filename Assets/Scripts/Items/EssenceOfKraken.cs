
using DefaultNamespace;

public class EssenceOfKraken : ArtifactEffect
{
    public override void AddEffect()
    {
        _playerReference = PlayerManager.Instance;
        base.AddEffect();
        SprintSkill sprintSkill = _playerReference.GetComponent<SprintSkill>();
        sprintSkill.skillCooldown *= 0.6f;
    }
}
