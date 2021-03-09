using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

public class VFXSpawner : MonoBehaviour
{
    public VisualEffect visualEffect;
    
    public void SpawnVFX(Vector3 position, VisualEffectAsset effectToSpawn)
    {
        if (visualEffect = null)
        {
            visualEffect = gameObject.AddComponent<VisualEffect>();
        }
        visualEffect.visualEffectAsset = effectToSpawn;
        Destroy(visualEffect, 1f);
    }
}
/*
[CustomEditor(typeof(VFXSpawner))]
public class VfxSpawnerEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        VFXSpawner gm = (VFXSpawner) target;
        if (GUILayout.Button("Spawn VFX"))
        {
            gm.SpawnVFX(gm.gameObject.transform.position);
        }
    }
}
*/