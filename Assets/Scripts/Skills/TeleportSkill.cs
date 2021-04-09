using UnityEngine;
public class TeleportSkill : SkillExecute
{
    public float teleportDistance = 10f;
    private float capsuleRadius;
    private float capsuleHeight;

    private void Start()
    {
        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        capsuleHeight = collider.height;
        capsuleRadius = collider.radius;
    }

    public override void Execute()
    {
        var tr = skillUser.transform;
        tr.position += tr.forward * TeleportDistance();
    }

    private float TeleportDistance()
    {
        LayerMask layer = LayerMask.GetMask("CameraCollision");
        if (Physics.CapsuleCast(transform.position, transform.position + Vector3.up * capsuleHeight, capsuleRadius, transform.forward, out RaycastHit hit, teleportDistance, layer, QueryTriggerInteraction.Collide))
        {
            return hit.distance;
        }
        return teleportDistance;
    }
}
