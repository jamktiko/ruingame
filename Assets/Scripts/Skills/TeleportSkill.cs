using UnityEngine;
public class TeleportSkill : SkillExecute
{
    public float teleportDistance = 10f;
    
    public override void Execute(float duration)
    {
        var tr = skillUser.transform;
        tr.position += transform.forward * TeleportDistance();
    }

    float TeleportDistance()
    {
        LayerMask layer = LayerMask.GetMask("CameraCollision");
        if (Physics.CapsuleCast(transform.position, transform.position + Vector3.up * 1f, .4f, transform.forward, out RaycastHit hit, teleportDistance, layer, QueryTriggerInteraction.Collide))
        {
            return hit.distance - 0.7f;
        }
        return teleportDistance;
    }
}
