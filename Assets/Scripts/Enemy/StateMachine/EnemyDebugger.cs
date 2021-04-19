#if (UNITY_EDITOR) 

using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Enemy_Melee))]
public class EnemyDebugger : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Enemy_Melee em = (Enemy_Melee) target;
        if (GUILayout.Button("Spawn Artifact"))
        {
            em.DecidePathToPlayer();
        }
    }
}
#endif