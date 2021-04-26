using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSelectionHandler : MonoBehaviour
{
	[SerializeField] private InputReader _inputReader;
	[SerializeField] private GameObject _defaultSelection;
	public GameObject currentSelection;
	public GameObject mouseSelection;

	private void OnEnable()
	{
		_inputReader.menuMouseMoveEvent += HandleMoveCursor;
		_inputReader.moveSelectionEvent += HandleMoveSelection;

		StartCoroutine(SelectDefault());
	}

	private void OnDisable()
	{
		_inputReader.menuMouseMoveEvent -= HandleMoveCursor;
		_inputReader.moveSelectionEvent -= HandleMoveSelection;
	}
	private IEnumerator SelectDefault()
	{
		yield return new WaitForSeconds(.03f); // Necessary wait otherwise the highlight won't show up

		if (_defaultSelection != null)
			EventSystem.current.SetSelectedGameObject(_defaultSelection);
	}

	private void HandleMoveSelection()
	{
		Cursor.visible = false;
		// Handle case where no UI element is selected because mouse left selectable bounds
		if (EventSystem.current.currentSelectedGameObject == null)
			EventSystem.current.SetSelectedGameObject(currentSelection);
	}

	private void HandleMoveCursor()
	{
		if (mouseSelection != null)
		{
			EventSystem.current.SetSelectedGameObject(mouseSelection);
		}

		Cursor.visible = true;
	}

	public void HandleMouseEnter(GameObject UIElement)
	{
		mouseSelection = UIElement;
		EventSystem.current.SetSelectedGameObject(UIElement);
	}

	public void HandleMouseExit(GameObject UIElement)
	{
		if (EventSystem.current.currentSelectedGameObject != UIElement)
			return;

		// deselect UI element if mouse moves away from it
		mouseSelection = null;
		EventSystem.current.SetSelectedGameObject(null);
	}

	public bool AllowsSubmit()
	{
		
		return !_inputReader.LeftMouseDown()
			   //mouse & keyboard are on different elements, do not allow interaction to continue
			   || mouseSelection != null && mouseSelection == currentSelection;
	}
	public void UpdateSelection(GameObject UIElement) => currentSelection = UIElement;

}
