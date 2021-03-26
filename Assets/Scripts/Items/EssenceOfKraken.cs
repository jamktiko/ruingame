
using DefaultNamespace;
using UnityEngine;

public class EssenceOfKraken : ArtifactEffect
{
    private PlayerManager _playerReference;
    private SprintSkill sprintSkill;
    public override void AddEffect()
    {
        _playerReference = PlayerManager.Instance;
        base.AddEffect();
        _playerReference._playerSkills.skillList[3].skillCooldown *= 0.6f;
        //sprintSkill = _playerReference.GetComponent<SprintSkill>();
        //sprintSkill = _playerReference.GetComponent<SprintSkill>();
        //sprintSkill.skillCooldown *= 0.6f;
    }

    private void OnDestroy()
    {
        artifactModifiers[0].type = ArtifactModifier.Type.Minus;
        base.AddEffect();
        _playerReference._playerSkills.skillList[3].skillCooldown /= 0.6f;
        //sprintSkill.skillCooldown /= 0.6f;
    }
}
