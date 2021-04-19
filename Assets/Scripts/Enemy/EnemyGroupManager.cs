using UnityEngine;

public class EnemyGroupManager : MonoBehaviour
{
    private static EnemyGroupManager _instance;
    public static EnemyGroupManager Instance
    {
        get { return _instance; }
    }
    public Enemy_Group[] enemyGroups;

    public GameObject playerTarget;
    //Serves as a blackboard to alert all groups controlled by this manager
    
    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        enemyGroups = GetComponentsInChildren<Enemy_Group>();
        foreach (Enemy_Group EG in enemyGroups)
        {
           EG.EGM = this;
        }   
    }

    public void Alert()
    {
        playerTarget = GameObject.FindWithTag("Player");
        foreach (Enemy_Group eg in enemyGroups)
        {
           eg.playerTarget = playerTarget;
           eg.AlertEnemies();
        }
    }

    public void DisableAllEnemies()
    {        
        foreach (Enemy_Group eg in enemyGroups)
        {
           eg.DisableEnemies();
        }
        
    }
}