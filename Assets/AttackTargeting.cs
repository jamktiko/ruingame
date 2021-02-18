using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class AttackTargeting : MonoBehaviour
{
    public string[] AllowedTargetTags;
    public LayerMask layerToCheck;
    public GameObject[] HandleTargeting(BaseAttack attack)
    {
        try
        {
            switch (attack.targetingType)
            {
                case BaseAttack.basetargetingType.AOE:
                    return GetAOETargets(attack.radius);
                case BaseAttack.basetargetingType.FRONTAL:
                    return GetFrontalTargets();
                case BaseAttack.basetargetingType.NEAREST:
                    return GetNearestTargets();
            }
        }
        catch
        {
            Debug.Log("Unknown targeting type!");
        }
        return new GameObject[1];
    }

    private GameObject[] GetNearestTargets()
    {
        return new GameObject[1];
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
    private GameObject[] GetFrontalTargets()
    {
        return new GameObject[1];
    }
}
