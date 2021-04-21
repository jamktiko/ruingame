#if (UNITY_EDITOR)

using DefaultNamespace;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Enemy_Melee))]
public class EnemyDebugger : Editor
{
    
    public GameObject playerTarget;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Enemy_Melee em = (Enemy_Melee) target;
        if (GUILayout.Button("Switch state"))
        {
            switch (em.BaseStates)
            {
                case (Enemy_StateMachine.baseStates.MOVE):
                    em.playerTarget = GameObject.FindGameObjectWithTag("Player");
                    em.SetState(new MoveTowardsPlayerState(em));
                    break;
                case (Enemy_StateMachine.baseStates.DEATH):
                    em.SetState(new DeathState(em));
                    break;
                case (Enemy_StateMachine.baseStates.PATROL):
                    em.SetState(new PatrolState(em));
                    break;
                case (Enemy_StateMachine.baseStates.ATTACK):
                    em.SetState(new AttackPlayerState(em));
                    break;
            }
        }
    }
}
#endif