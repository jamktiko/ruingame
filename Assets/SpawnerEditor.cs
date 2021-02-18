using DefaultNamespace;
using UnityEngine;     
using UnityEditor;     

[CustomEditor(typeof(Spawner))]          
public class SpawnerEditor : Editor
{
    private int EnemiesToSpawn = 0;
    public override void OnInspectorGUI()    
    {                                        
        DrawDefaultInspector();

        Spawner spawner = (Spawner) target;
        if (GUILayout.Button("Spawn Single Enemy"))
        {                                    
              spawner.SpawnSingleEnemy();
        }     
        EnemiesToSpawn = EditorGUILayout.IntField("Enemies to spawn: ", EnemiesToSpawn);
        if (GUILayout.Button("Spawn Enemies"))
        {     
            spawner.StopAllCoroutines();
            spawner.remainingEnemies = 0;
            spawner.currentSpawnedEnemies = 0;
            spawner.enemiesToSpawn = 0;
            spawner.StartSpawning(EnemiesToSpawn);
        }    
    }                                        
}                                            