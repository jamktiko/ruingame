using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class AttackTargeting : MonoBehaviour
{
    public string[] AllowedTargetTags;
    public LayerMask LayerToCheck;
    public GameObject[] HandleTargeting(BaseAttack attack)
    {
        switch (attack.targetingType)
            {
                case BaseAttack.basetargetingType.AOE:
                    return GetAOETargets(attack.radius);
                case BaseAttack.basetargetingType.CONE:
                    return GetFrontalTargets(attack.radius, attack.range);
                case BaseAttack.basetargetingType.NEAREST:
                    return GetNearestTargets(attack.radius);
            }
        return new GameObject[1];
    }

    private GameObject[] GetNearestTargets(float radius)
    {
        GameObject[] gos = new GameObject[] { };
        foreach (string tag in AllowedTargetTags)
        {
            gos.Concat(GameObject.FindGameObjectsWithTag(tag));
        }
        GameObject closest = null;
        float distance = radius;
        Vector3 position = transform.position;
        foreach (GameObject go in gos) {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = go;
                distance = curDistance;
            }
        }
        gos = new GameObject[1];
        gos[0] = closest;
        return gos;
    }
    private GameObject[] GetAOETargets(float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, LayerToCheck);
        List<GameObject> retVal = new List<GameObject>();
        
        foreach (Collider collision in hitColliders)
        {
            if (AllowedTargetTags.Contains(collision.tag))
            {
                retVal.Add(collision.gameObject);
            }
        }
        return retVal.ToArray();
    }
    private GameObject[] GetFrontalTargets(float radius, float range)
    {
        List<GameObject> targetList = new List<GameObject>();
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, transform.forward, range, LayerToCheck);
        if (hits != null)
        {
            foreach (RaycastHit hit in hits)
            {
                targetList.Add(hit.transform.gameObject);
            }
        }
        return targetList.ToArray();
    }

    public Vector3 FindNearestTargetInRadius(float radius)
    {
        GameObject[] gos = new GameObject[] { };
        foreach (string tag in AllowedTargetTags)
        {
            gos.Concat(GameObject.FindGameObjectsWithTag(tag));
        }
        GameObject closest = null;
        float distance = radius;
        Vector3 position = transform.position;
        foreach (GameObject go in gos) {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = go;
                distance = curDistance;
            }
        }
        if (closest != null)
        {
            var target = (closest.transform.position - transform.position);
            target.y = 0;
            return target.normalized;
        }
        return transform.forward;

    }
}
