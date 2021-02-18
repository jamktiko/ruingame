using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameEditorManager : Editor
{
    
    float ValueMultiplier = 1.0f;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameManager gm = (GameManager) target;
        EditorGUILayout.LabelField("Gameplay Settings", EditorStyles.centeredGreyMiniLabel);
        if (GUILayout.Button("Spawn Player"))
        {
            gm.ConstructPlayer();
            gm.InitializePlayer();
        }
        if (GUILayout.Button("Lock Cursor"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        EditorGUILayout.LabelField("Player Options", EditorStyles.centeredGreyMiniLabel);
        ValueMultiplier = EditorGUILayout.FloatField("Increase stat by:", ValueMultiplier);
        if (GUILayout.Button("Damage") && gm.currentPlayer != null)
        {
            PlayerManager.Instance.ModifyDamage(ValueMultiplier, 1);
        }
        if (GUILayout.Button("Attack Speed") && gm.currentPlayer != null)
        {
            PlayerManager.Instance.ModifyAttackSpeed(ValueMultiplier, 1);
        }
        if (GUILayout.Button("Movement Speed") && gm.currentPlayer != null)
        {
            PlayerManager.Instance.ModifyMovementSpeed(ValueMultiplier, 1);
        }
        if (GUILayout.Button("Jump") && gm.currentPlayer != null)
        {
            PlayerManager.Instance.ModifyJump(ValueMultiplier, 1);
        }
    }
}