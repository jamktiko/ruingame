
using DefaultNamespace;
using UnityEngine;
using System.Collections;

public class TheRuinedRing : ArtifactEffect
{
    private float cooldownModifier = 0.95f;
    private float firstCooldownTime;

    public override void AddEffect()
    {
       _playerReference = PlayerManager.Instance;
        firstCooldownTime = _playerReference._playerSkills.skillList[0].skillCooldown;

        foreach (var skill in _playerReference._playerSkills.skillList)
            skill.skillCooldown *= cooldownModifier;
    }

    private void OnDestroy()
    {
        if (firstCooldownTime > _playerReference._playerSkills.skillList[0].skillCooldown)
            foreach (var skill in _playerReference._playerSkills.skillList)
                skill.skillCooldown /= cooldownModifier;
    }
}
