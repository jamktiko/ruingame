using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSelector : MonoBehaviour
{
    public Canvas parentCanvas;
    public Canvas targetCanvas;
    public MenuManager menuManager;

    public void SelectCanvas()
    {
        menuManager.SwitchCanvas(targetCanvas);
    }

}
