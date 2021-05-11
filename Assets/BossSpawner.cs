using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : Spawner
{
    private bool bossSpawned = false;
    public override void StartSpawning(int amount)
    {
        if (bossSpawned)
        {
            return;
        }

        bossSpawned = true;
        remainingEnemies = 1;
        enemiesToSpawn = 1;
        //Spawn amount of enemies
        
        //If currentSpawnedEnemies => max concurrent enemies, wait
        StartCoroutine(base.SpawnEnemy());
        //Wait a bit, spawn another enemy

        if (enemiesToSpawn == 0)
        {
            //ALL ENEMIES SPAWNED
            if (currentSpawnedEnemies == 0)
            {
            }
        }
    }
}
