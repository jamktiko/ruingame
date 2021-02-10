using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "EntityData", menuName = "Game/EntityData")]
public class EntityData : ScriptableObject
{
    [FormerlySerializedAs("_entityMovementSpeed")]
    [Header("Movement stats")] 
    [SerializeField]
    public float entityMovementSpeed = 10f;
    [FormerlySerializedAs("_entityjumpHeight")] public float entityjumpHeight = 350f;

    [FormerlySerializedAs("_entityDamage")] [Header("Combat stats")] 
    public float entityDamage = 100f;
    [FormerlySerializedAs("_entityAttackSpeed")] public float entityAttackSpeed = 10f;
    [FormerlySerializedAs("_entityEnemyTag")] public string entityEnemyTag = "Enemy";
}
