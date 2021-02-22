
using UnityEngine;

[CreateAssetMenu(fileName = "PhysicalAttack", menuName = "Game/PhysicalAttack")]
public class BaseAttack : ScriptableObject

{
    [System.Serializable]
    public enum basetargetingType 
    {
        AOE,
        NEAREST,
        FRONTAL,
    }

    public basetargetingType targetingType;
    
    public float baseDamage;
    
    [Tooltip("Used to radius on AoE and radius x distance in Nearest targeting")]
    public float radius;

    public float knockBackStrength;
    
    [Tooltip("Used to calculate distance from player")]
    public float range;

}
