using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Game/EntityData")]
public class EntityData : ScriptableObject
{
    [Header("Movement stats")] 
    [SerializeField]
    public float _entityMovementSpeed = 10f;
    public float _entityjumpHeight = 350f;

    [Header("Combat stats")] 
    public float _entityDamage = 100f;
    public float _entityAttackSpeed = 10f;
    public string _entityEnemyTag = "Enemy";
}
