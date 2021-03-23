
using UnityEngine;


public class MenuManager : MonoBehaviour
{
    public GameManager gameManager;
    public Canvas initialCanvas;
    public Canvas[] childCanvases;
    public Canvas currentCanvas;
    public virtual void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameManager.StartGameplayLoop();
    }
    
    public virtual void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameManager = GameManager.Instance;
        currentCanvas = initialCanvas;
        childCanvases = GetComponentsInChildren<Canvas>();
        foreach (Canvas c in childCanvases)
        {
            c.enabled = false;
        }
        currentCanvas.enabled = true;
    }
    public void StopGame()
    {
        Application.Quit();
    }

    public void SwitchCanvas(Canvas newCanvas)
    {
        currentCanvas.enabled = false;
        newCanvas.enabled = true;
        currentCanvas = newCanvas;
    }
}

