
using DefaultNamespace;
using UnityEngine;
using System.Collections;

public class CapeOfAgility : ArtifactEffect
{
    //public string  description = "5% movement speed. After dashing double the stat change for 2 seconds";

    private SprintSkill sprintSkill;
    private float normalSpeed;
    private float speedModifier = 0.05f;

    private void Start()
    {
        normalSpeed = _playerReference._playerData.entityMovementSpeed;
    }

    public override void AddEffect()
    {
        artifactModifiers[0].modifier = normalSpeed * speedModifier;
        base.AddEffect();
        if (_playerReference.TryGetComponent(out sprintSkill))
        {
            sprintSkill.EndSprintEvent += ModifyMovmentSpeed;
        }
    }

    private void ModifyMovmentSpeed()
    {
        artifactModifiers[0].type = ArtifactModifier.Type.Plus;
        artifactModifiers[0].modifier = normalSpeed * speedModifier;
        base.AddEffect();
        Invoke("ReduceSpeed", 2f);
    }

    private void ReduceSpeed()
    {
        artifactModifiers[0].type = ArtifactModifier.Type.Minus;
        artifactModifiers[0].modifier = normalSpeed * speedModifier;
        base.AddEffect();
    }

    private void OnDestroy()
    {
        if (_playerReference._playerData.entityMovementSpeed > normalSpeed)
            _playerReference._playerData.entityMovementSpeed = normalSpeed;
    }

}
