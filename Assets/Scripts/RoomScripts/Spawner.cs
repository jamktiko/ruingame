using System.Collections;

using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int enemiesToSpawn;
    public int currentSpawnedEnemies;
    public int remainingEnemies;
    public GameObject enemyprefab;
    public Enemy_Group EG;
    public void EntityDeath()
    {
        currentSpawnedEnemies--;
        remainingEnemies--;
    }

    public virtual void StartSpawning(int amount)
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

    private void Start()
    {
        EG = GetComponent<Enemy_Group>();
    }
    protected IEnumerator SpawnEnemy()
    {
        //Spawns one enemy
        yield return new WaitForSeconds(1f);
        var enemy = Instantiate(enemyprefab, gameObject.transform);
        yield return new WaitForSeconds(0.2f);
        enemiesToSpawn--;
        currentSpawnedEnemies++;
        if (enemiesToSpawn > 0)
        {
            StartCoroutine(SpawnEnemy());
        }
        EG.enemyStateMachines = GetComponentsInChildren<Enemy_StateMachine>();
        foreach (Enemy_StateMachine esm in EG.enemyStateMachines)
        {
            esm.enemyGroup = EG;
        }
    }

    public void SpawnSingleEnemy()
    {
        Instantiate(enemyprefab, gameObject.transform);
    }
}
