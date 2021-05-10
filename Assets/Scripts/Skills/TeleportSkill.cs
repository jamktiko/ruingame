using UnityEngine;
using System.Collections;

public class TeleportSkill : SkillExecute
{
    public float teleportDistance = 10f;
    private float capsuleRadius = 0.49f;
    private float capsuleHeight = 1.99f;
    LayerMask enemyLayer;
    LayerMask collisionObject;
   
    protected override void Awake()
    {
        skillname = "Teleport";
        animationClip = Resources.Load<AnimationClip>("P_Dash");
        duration = 0.2f;
        enemyLayer = LayerMask.GetMask("EnemyLayer");
        collisionObject = LayerMask.GetMask("CollisionObject");
    }

    public override void Execute(float duration)
    {
        iFrameDuration = duration;
        base.Execute(duration);
        try { Invoke("Teleport", .2f); }
        catch { Debug.Log("Teleport"); }
        WhileSkillActive();
    }

    void Teleport()
    {
        var tr = skillUser.transform;
        Vector3 maxDistance = tr.position + tr.forward * TeleportDistance();
        CheckOverlaps(ref maxDistance);
        tr.position = maxDistance;
    }

    public override void WhileSkillActive()
    {
        skillUser.usingSkill = true;
        IEnumerator coroutine = skillUser.UsePersistentEffect(this);
        skillUser.StartCoroutine(coroutine);
    }

    public override void DeActivateSkillActive()
    {
        skillUser.usingSkill = false;
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


    private void CheckOverlaps(ref Vector3 maxDistance)
    {

        Vector3 dir = (transform.position - maxDistance).normalized;
        while (CheckOverlapOnLayer(enemyLayer, maxDistance) || CheckOverlapOnLayer(collisionObject, maxDistance))
        {
            maxDistance += dir;
        }
    }

    private bool CheckOverlapOnLayer(LayerMask layer, Vector3 maxDistance)
    {
        bool overlap = Physics.CheckCapsule(maxDistance, maxDistance + Vector3.up * capsuleHeight, capsuleRadius, layer, QueryTriggerInteraction.Collide);
        return overlap;
    }
}
