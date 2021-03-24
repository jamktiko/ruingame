
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public GameManager gameManager;
    public Canvas initialCanvas;
    public Canvas[] childCanvases;
    public Canvas currentCanvas;
    public MenuSelectionHandler MSH;
    
    public virtual void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameManager.StartGameplayLoop();
    }
    
    public virtual void Awake()
    {
        MSH = GetComponent<MenuSelectionHandler>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        currentCanvas = initialCanvas;
        childCanvases = GetComponentsInChildren<Canvas>();
        foreach (Canvas c in childCanvases)
        {
            c.gameObject.SetActive(false);
        }
        currentCanvas.gameObject.SetActive(true);
    }

    public virtual void Start()
    {
        gameManager = GameManager.Instance;
    }
    public void StopGame()
    {
        Application.Quit();
    }

    public void SwitchCanvas(Canvas newCanvas)
    {
        currentCanvas.gameObject.SetActive(false);
        newCanvas.gameObject.SetActive(true);
        currentCanvas = newCanvas;
        var newTarget = currentCanvas.GetComponentInChildren<MultiButton>().gameObject;
        EventSystem.current.SetSelectedGameObject(newTarget);
        MSH.UpdateSelection(newTarget);
    }
}

