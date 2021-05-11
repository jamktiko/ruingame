
using System;
using System.Linq;
using UnityEngine;


public class SpawnerManager : MonoBehaviour
{

    //GET ALL POSSIBLE SPAWNING LOCATIONS IN THE ROOM
    public Spawner[] spawnerList;
    public int _totalEnemiesRemaining;
    private int _spawnersDone;
    public int enemiesToSpawn;
    void Start()
    {
        spawnerList = GetComponentsInChildren<Spawner>();
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

    public void SpawnBoss()
    {
        var bossSpawner = Array.Find(spawnerList, spawners => spawners.GetType() == typeof(BossSpawner));
        bossSpawner.StartSpawning(1);
    }
    public void StartSpawners()
    {
        
        var singleSpawner = Mathf.CeilToInt(enemiesToSpawn / spawnerList.Length);
        foreach (Spawner sp in spawnerList)
        {
            sp.StartSpawning(singleSpawner);
        }
    }
    public void SpawnAdds(int amount)
    {
        var spL = GetComponentsInChildren<Spawner>().ToList();
        spL.Remove(spL.Find(x => x.GetType() == typeof(BossSpawner)));
        
        var singleSpawner = Mathf.CeilToInt(enemiesToSpawn / spawnerList.Length);
        foreach (Spawner sp in spawnerList)
        {
            sp.StartSpawning(singleSpawner);
        }
    }

}
