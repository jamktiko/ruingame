
using UnityEngine;

public class Enemy_Group : MonoBehaviour
{
    public Enemy_StateMachine[] enemyStateMachines;

    public PlayerManager playerTarget;

    public EnemyGroupManager EGM;
    
    //Serves as a blackboard for the enemies in this group to act together

    public void Awake()
    {
        enemyStateMachines = GetComponentsInChildren<Enemy_StateMachine>();
        foreach (Enemy_StateMachine esm in enemyStateMachines)
        {
            esm.enemyGroup = this;
        }   
    }

    public void AlertManager()
    {
        EGM.Alert();
    }

    public void AlertEnemies()
    {
        foreach (Enemy_StateMachine esm in enemyStateMachines)
        {
            esm.GoToState(Enemy_StateMachine.NPC_EnemyAction.APPROACH);
        }
    }
}