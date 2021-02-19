
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class AttackTargeting : MonoBehaviour
{
    public string[] AllowedTargetTags;
    public LayerMask layerToCheck;
    public GameObject[] HandleTargeting(BaseAttack attack)
    {
        switch (attack.targetingType)
            {
                case BaseAttack.basetargetingType.AOE:
                    return GetAOETargets(attack.radius);
                case BaseAttack.basetargetingType.FRONTAL:
                    return GetFrontalTargets(attack.radius);
                case BaseAttack.basetargetingType.NEAREST:
                    return GetNearestTargets(attack.radius);
            }
        return new GameObject[1];
    }

    private GameObject[] GetNearestTargets(float radius)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
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
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, layerToCheck);
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
    private GameObject[] GetFrontalTargets(float radius)
    {
        return new GameObject[1];
    }

    public Vector3 FindNearestTargetInRadius(float radius)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
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
        else
        {
            return transform.forward;
        }
    }
}
