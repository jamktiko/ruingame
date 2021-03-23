using System;
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
            c.enabled = false;
        }
    }

    public void OnEnable()
    {
        gameManager.playerManager.playerInputReader.PauseEvent += OnPause;
    }

    public void OnDisable()
    {
        gameManager.playerManager.playerInputReader.PauseEvent -= OnPause;
    }

    public void OnPause()
    {
        if (_gamePaused)
        {
            StopPause();
        }
        else
        {
            _gamePaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gameManager.SetTimeScale(0f);
            currentCanvas.enabled = true;
        }
    }

    public void StopPause()
    {
        _gamePaused = false;
        PlayerManager.Instance.cameraManager.UpdateCameraSettings();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameManager.SetTimeScale(1f);
        currentCanvas.enabled = false;
    }

    public void ExitToMain()
    {
        StopPause();
        gameManager.playerManager.Die();
        Destroy(gameObject);
    }
}
