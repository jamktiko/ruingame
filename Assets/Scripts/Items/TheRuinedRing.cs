
using DefaultNamespace;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheRuinedRing : ArtifactEffect
{
    private float cooldownModifier = 0.95f;
    public override void AddEffect()
    {
        _playerReference = PlayerManager.Instance;
        foreach (var skill in _playerReference._playerSkills.skillList)
        {
            skill.skillCooldown *= cooldownModifier;
        }

    }
}
