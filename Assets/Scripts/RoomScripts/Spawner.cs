﻿using System.Collections;

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
        StartCoroutine(SpawnEnemy());
        //Wait a bit, spawn another enemy

        if (enemiesToSpawn == 0)
        {
            //ALL ENEMIES SPAWNED
            if (currentSpawnedEnemies == 0)
            {
            }
        }
    }

    private IEnumerator SpawnEnemy()
    {
        //Spawns one enemy
        yield return new WaitForSeconds(1f);
        var enemy = Instantiate(enemyprefab, gameObject.transform);
        enemiesToSpawn--;
        currentSpawnedEnemies++;
        if (enemiesToSpawn > 0)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    public void SpawnSingleEnemy()
    {
        Instantiate(enemyprefab, gameObject.transform);
    }
}
