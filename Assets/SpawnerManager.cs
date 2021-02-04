using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    //GET ALL POSSIBLE SPAWNING LOCATIONS IN THE ROOM
    public Spawner[] spawners;
    private int _totalEnemiesRemaining;
    private int _spawnersDone;
    
    void Start()
    {
        spawners = GetComponentsInChildren<Spawner>();
        StartSpawners();
    }

    public int EnemiesRemaining()
    {
        var enemies = 0;
        foreach (Spawner sp in spawners)
        {
            enemies += sp.remainingEnemies;
        }
        return enemies;
    }

    public void SpawnersDone()
    {
        _spawnersDone++;
    }

    public void StartSpawners()
    {
        foreach (Spawner sp in spawners)
        {
            sp.StartSpawning(1);
        }
    }

}
