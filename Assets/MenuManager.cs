using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MenuManager : MonoBehaviour
{
    [FormerlySerializedAs("GameManager")] public GameManager gameManager;
    public SkillSelection skillSelectionManager;
    [FormerlySerializedAs("_PlayCanvas")] public Canvas playCanvas;
    [FormerlySerializedAs("_SkillSelectionCanvas")] public Canvas skillSelectionCanvas;
    public void StartGame()
    {
        gameManager.StartGameplayLoop();
        this.enabled = false;
    }

    void StopGame()
    {
        //Leave game or smth
    }

    public void StartSkillSelection()
    {
        playCanvas.enabled = false;
        skillSelectionCanvas.enabled = true;
        skillSelectionManager = SkillSelection.Instance;
        skillSelectionManager.StartSkillSelection();
    }
    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
}

