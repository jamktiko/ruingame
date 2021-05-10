
using DefaultNamespace;
using UnityEngine;
public class EssenceOfKraken : ArtifactEffect
{
    public override void AddEffect()
    {
        _playerReference = PlayerManager.Instance;
        base.AddEffect();
        try
        {
            SprintSkill sprintSkill = _playerReference.GetComponentInChildren<SprintSkill>();
            sprintSkill.skillCooldown *= 0.6f;
        }
        catch { Debug.Log("no sprint skill"); }
    }
}
