
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public GameManager gameManager;
    public Canvas initialCanvas;
    public Canvas[] childCanvases;
    public Canvas currentCanvas;
    public MenuSelectionHandler MSH;
    public Canvas SkillSelectionCanvas;
    
    
    private bool loadingMenu;
    public virtual void StartGame()
    {
        
        SwitchCanvasB(SkillSelectionCanvas);
        SkillSelectionCanvas.GetComponent<SkillSelection>().StartSkillSelection();
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
        currentCanvas.gameObject.SetActive(false);
    }

    public virtual void Start()
    {
        MSH = GetComponent<MenuSelectionHandler>();
        LoadMenu();
    }
    public void LoadMenu()
    {
        loadingMenu = true;
        StartCoroutine("FindGMRoutine");
    }

    public IEnumerator FindGMRoutine()
    {
        while (gameManager == null)
        {
            yield return new WaitForSeconds(1f);
            try {gameManager = GameManager.Instance;}
            catch{Debug.Log("No Game Manager loaded!");}
        }
        loadingMenu = false;
        currentCanvas.gameObject.SetActive(true);
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
    public void SwitchCanvasB(Canvas newCanvas)
    {
        currentCanvas.gameObject.SetActive(false);
        newCanvas.gameObject.SetActive(true);
        currentCanvas = newCanvas;

    }
}

