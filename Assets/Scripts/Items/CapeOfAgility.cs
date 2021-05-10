
using DefaultNamespace;
using UnityEngine;
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
        try
        {
            sprintSkill = _playerReference.GetComponentInChildren<SprintSkill>();
            sprintSkill.EndSprintEvent += ModifyMovmentSpeed;
        }
        catch { Debug.Log("no sprint skill"); }
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

}
