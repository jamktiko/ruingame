using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[AddComponentMenu("Custom/UI/MultiButton")]
public class MultiButton : Button
{
    private MenuSelectionHandler _menuSelectionHandler;

    private new void Awake()
    {
        _menuSelectionHandler = transform.root.gameObject.GetComponentInChildren<MenuSelectionHandler>();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        _menuSelectionHandler.HandleMouseEnter(gameObject);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        _menuSelectionHandler.HandleMouseExit(gameObject);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        _menuSelectionHandler.UpdateSelection(gameObject);
        base.OnSelect(eventData);
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        if (_menuSelectionHandler.AllowsSubmit())
            base.OnSubmit(eventData);
    }

}