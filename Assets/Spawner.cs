using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int enemiesToSpawn;
    public int currentSpawnedEnemies;
    public int remainingEnemies;
    public GameObject enemyprefab;
    
    public void EntityDeath()
    {
        currentSpawnedEnemies--;
        remainingEnemies--;
    }

    public void StartSpawning(int amount)
    {
        remainingEnemies = amount;
        enemiesToSpawn = amount;
        //Spawn amount of enemies
        
        //If currentSpawnedEnemies => max concurrent enemies, wait
        SpawnEnemy();
        //Wait a bit, spawn another enemy

        if (enemiesToSpawn == 0)
        {
            //ALL ENEMIES SPAWNED
            if (currentSpawnedEnemies == 0)
            {
                //ALL ENEMIES DEAD
                SendMessageUpwards("SpawnerDone");
            }
        }
    }

    private void SpawnEnemy()
    {
        //Spawns one enemy
        var enemy = Instantiate(enemyprefab);
        enemy.transform.SetParent(gameObject.transform);
        enemiesToSpawn--;
        currentSpawnedEnemies++;
    }
}
