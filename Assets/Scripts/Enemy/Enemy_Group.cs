
using DefaultNamespace;
using UnityEngine;

public class Enemy_Group : MonoBehaviour
{
    
    public Enemy_StateMachine[] enemyStateMachines;

    public GameObject playerTarget;

    public EnemyGroupManager EGM;

    public PatrolAreaSettings PAS;

    private PatrolArea _patrolArea;
    public Bounds patrolArea;
    
    //Serves as a blackboard for the enemies in this group to act together

    public void Awake()
    {
        _patrolArea = gameObject.AddComponent<PatrolArea>();
    }
    public void Start()
    {
        _patrolArea.SetPatrolAreaSettings(PAS);
        patrolArea = _patrolArea.CreateArea();
    }
    public void AlertManager()
    {
        EGM.Alert();
    }


    public void AlertEnemies()
    {
        foreach (Enemy_StateMachine esm in enemyStateMachines)
        {
            playerTarget = PlayerManager.Instance.gameObject;
            esm.playerTarget = playerTarget;
            esm.SetState(new MoveTowardsPlayerState(esm));
        }
    }

    public void DisableEnemies()
    {
        foreach (Enemy_StateMachine esm in enemyStateMachines)
        {
            esm.enabled = false;
        }
    }
}

