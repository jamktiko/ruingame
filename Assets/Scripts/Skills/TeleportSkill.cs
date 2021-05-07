using UnityEngine;
public class TeleportSkill : SkillExecute
{
    public float teleportDistance = 10f;
    private float capsuleRadius = 0.49f;
    private float capsuleHeight = 1.99f;

    public override void Execute()
    {
        var tr = skillUser.transform;
        Vector3 maxDistance = tr.position + tr.forward * TeleportDistance();
        CheckOverlaps(ref maxDistance);
        tr.position = maxDistance;
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

    protected override void Awake()
    {
        skillname = "Teleport";
        animationClip = Resources.Load<AnimationClip>("P_Dash");
    }
    private void CheckOverlaps(ref Vector3 maxDistance)
    {
        LayerMask enemyLayer = LayerMask.GetMask("EnemyLayer");
        LayerMask collisionObject = LayerMask.GetMask("CollisionObject");
        Vector3 dir = (transform.position - maxDistance).normalized;
        while (CheckOverlapOnLayer(enemyLayer, maxDistance) || CheckOverlapOnLayer(collisionObject, maxDistance))
        {
            maxDistance -= -dir * 0.1f; 
        }
    }

    private bool CheckOverlapOnLayer(LayerMask layer, Vector3 maxDistance)
    {
        bool overlap = Physics.CheckCapsule(maxDistance, maxDistance + Vector3.up * capsuleHeight, capsuleRadius, layer, QueryTriggerInteraction.Collide);
        return overlap;
    }
}
