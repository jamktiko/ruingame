using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DefaultNamespace;

[CustomEditor(typeof(SpawnArtifact))]
public class SpawnArtifactEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SpawnArtifact spawnArtifact = (SpawnArtifact) target;
        if (GUILayout.Button("Spawn Artifact"))
        {
            spawnArtifact.BuildObject();
        }
    }
}
