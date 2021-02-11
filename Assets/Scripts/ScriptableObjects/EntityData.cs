using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "EntityData", menuName = "Game/EntityData")]
public class EntityData : ScriptableObject
{
    
    [Header("Movement stats")] 
    [SerializeField]
    public float entityMovementSpeed = 10f;
    public float entityjumpHeight = 350f;

    [Header("Combat stats")]
    public float entityDamage = 100f;
    public float entityAttackSpeed = 1f;
    public string entityEnemyTag = "Enemy";
}
