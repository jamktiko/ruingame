using UnityEngine;

public class EnemyGroupManager : MonoBehaviour
{
    //public Enemy_Group[] enemyGroups;

    public GameObject playerTarget;
    //Serves as a blackboard to alert all groups controlled by this manager
    
    public void Awake()
    {
        //enemyGroups = GetComponentsInChildren<Enemy_Group>();
        //foreach (Enemy_Group EG in enemyGroups)
        {
           // EG.EGM = this;
        }   
    }

    public void Alert()
    {
        //foreach (Enemy_Group eg in enemyGroups)
        {
            //eg.playerTarget = playerTarget;
            //eg.AlertEnemies();
        }
    }
}