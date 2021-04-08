using UnityEngine;
public class TeleportSkill : SkillExecute
{
    public float teleportDistance = 10f;

    public override void Execute()
    {
        var tr = skillUser.transform;
        tr.position += transform.forward * TeleportDistance();
    }

    float TeleportDistance()
    {
        LayerMask layer = LayerMask.GetMask("CameraCollision");
        float capsuleRadius = 0.4f;
        float capsuleHeight = 1f;
        if (Physics.CapsuleCast(transform.position, transform.position + Vector3.up * capsuleHeight, capsuleRadius, transform.forward, out RaycastHit hit, teleportDistance, layer, QueryTriggerInteraction.Collide))
        {
            return hit.distance - 0.7f;
        }
        return teleportDistance;
    }
}
