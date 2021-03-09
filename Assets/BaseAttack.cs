using UnityEngine;

[CreateAssetMenu(fileName = "BaseAttack", menuName = "Game/BaseAttack")]
public class BaseAttack : ScriptableObject
{
    [System.Serializable]
    public enum basetargetingType 
    {
        AOE,
        NEAREST,
        CONE,
        //FRONT,
    }
    public basetargetingType targetingType;
    
    public float baseDamage;
    
    [Tooltip("Used to radius on AoE and radius x distance in Nearest targeting")]
    public float radius;

    public float knockBackStrength;
    
    [Tooltip("Used to calculate distance from player")]
    public float range;
    
    public enum baseAttackType
    {
        PHYSICAL,
        RANGED,
    }
    public baseAttackType attackType;

    public GameObject projectilePrefab;

    public float shootForce;
}
