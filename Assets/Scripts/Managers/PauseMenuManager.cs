using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MenuManager
{
    public bool _gamePaused = false;
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
       PlayerManager.Instance.playerInputReader.PauseEvent += OnPause;
       PlayerManager.Instance.playerInputReader.menuUnpauseEvent += OnUnPause;
    }

    public void OnDisable()
    {
        PlayerManager.Instance.playerInputReader.PauseEvent -= OnPause;
        PlayerManager.Instance.playerInputReader.menuUnpauseEvent -= OnUnPause;
    }

    public override void Start()
    {
        MSH = GetComponent<MenuSelectionHandler>();
        
        ResetPauseMenu();
    }
    
    public void OnPause()
    {
        _gamePaused = true;
        PlayerManager.Instance.playerInputReader.EnableMenuInput();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameManager.SetTimeScale(0f);
        currentCanvas.gameObject.SetActive(true);
    }

    public void OnUnPause()
    {
        _gamePaused = false;
        PlayerManager.Instance.cameraManager.UpdateCameraSettings();
        PlayerManager.Instance.playerInputReader.EnablePlayerInput();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameManager.SetTimeScale(1f);
        currentCanvas.gameObject.SetActive(false);
    }

    public void ResetPauseMenu()
    {
        GameManager.Instance.ResetPauseMenu();
    }

    public void ExitToMain()
    {
        OnUnPause();
        gameManager.playerManager.Die();
    }
}
