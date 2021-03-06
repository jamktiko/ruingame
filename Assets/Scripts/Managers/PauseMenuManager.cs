﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MenuManager
{
    private bool _gamePaused;
    public override void Awake()
    {
        DontDestroyOnLoad(this);
        gameManager = GameManager.Instance;
        currentCanvas = initialCanvas;
        childCanvases = GetComponentsInChildren<Canvas>();
        foreach (Canvas c in childCanvases)
        {
            c.gameObject.SetActive(false);
        }
        currentCanvas.gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        Cursor.visible = false;
        gameManager.playerManager.playerInputReader.PauseEvent += OnPause;
    }

    public void OnDisable()
    {
        gameManager.playerManager.playerInputReader.PauseEvent -= OnPause;
    }

    public override void Start()
    {
        MSH = GetComponent<MenuSelectionHandler>();
    }

    public void OnPause()
    {
        if (_gamePaused)
        {
            StopPause();
        }
        else
        {
            PlayerManager.Instance.playerInputReader.EnableMenuInput();
            _gamePaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gameManager.SetTimeScale(0f);
            currentCanvas.gameObject.SetActive(true);
        }
    }

    public void StopPause()
    {
        _gamePaused = false;
        PlayerManager.Instance.cameraManager.UpdateCameraSettings();
        PlayerManager.Instance.playerInputReader.EnablePlayerInput();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameManager.SetTimeScale(1f);
        currentCanvas.gameObject.SetActive(false);
    }

    public void ExitToMain()
    {
        StopPause();
        gameManager.playerManager.Die();
    }
}
