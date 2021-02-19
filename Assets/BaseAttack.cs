
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

    public float radius;

    public float knockBackStrength;

}
