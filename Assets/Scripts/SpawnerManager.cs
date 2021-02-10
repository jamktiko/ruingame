﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnerManager : MonoBehaviour
{

    //GET ALL POSSIBLE SPAWNING LOCATIONS IN THE ROOM
    public Spawner[] spawnerList;
    private int _totalEnemiesRemaining;
    private int _spawnersDone;
    
    void Start()
    {
        spawnerList = GetComponentsInChildren<Spawner>();
        StartSpawners();
    }

    public int EnemiesRemaining()
    {
        var enemies = 0;
        foreach (Spawner sp in spawnerList)
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
        foreach (Spawner sp in spawnerList)
        {
            sp.StartSpawning(1);
        }
    }

}
