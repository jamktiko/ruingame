using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SkillSelection : MonoBehaviour
{
    public List<SkillExecute> _options;
    public SkillExecute[] _playerSkillList;
    public List<SkillExecute> _skillList;
    public GameObject buttons;
    public GameObject skillSelectionButton;
    public List<SkillExecute> _skilllistTemplate;
    private int currentSelection = 0;
    
    void OnEnable()
    {
        _skillList = GetComponents<SkillExecute>().ToList();
        _playerSkillList = new SkillExecute[3];
    }

    public void  StartSkillSelection()
    {
       try { ClearExistingButtons();}
       catch{}
        GenerateSkillSelection();
        EnableButtons();
    }
    public void GenerateSkillSelection()
    {
        _options = new List<SkillExecute>();
        _skilllistTemplate = new List<SkillExecute>(_skillList);
        
        var diceRoll = Random.Range(0, _skilllistTemplate.Count);

        _options.Add(_skilllistTemplate[diceRoll]);
        _skilllistTemplate.RemoveAt(diceRoll);
        
        diceRoll = Random.Range(0, _skilllistTemplate.Count);
        _options.Add(_skilllistTemplate[diceRoll]);
        _skilllistTemplate.RemoveAt(diceRoll);

        diceRoll = Random.Range(0, _skilllistTemplate.Count);
        _options.Add(_skilllistTemplate[diceRoll]);
        _skilllistTemplate.RemoveAt(diceRoll);

        _options.Shuffle();
        
        for (int i = 0; i < _options.Count; i++)
        {
            GenerateOption(i);
        }
        
        EnableButtons();
        var menu = GetComponentInParent<MenuManager>();
        var newTarget = GetComponentInChildren<MultiButton>().gameObject;
        EventSystem.current.SetSelectedGameObject(newTarget);
        menu.MSH.UpdateSelection(newTarget);
    }
    public void GenerateOption(int option)
    {
        var selectionbutton = Instantiate(skillSelectionButton, buttons.transform); 
        var op = selectionbutton.GetComponent<SelectionButton>();
        op.option = option;
        op.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = _options[option].skillname;
        op.onClick.AddListener(delegate { SelectOption(op.option); });
    }
    public void SelectOption(int option)
    {
        OnSkillSelect(option);
    }
    private void OnSkillSelect(int option)
    {
        DisableButtons();
        _playerSkillList[currentSelection] = _options[option];
        if (_skillList.Contains(_options[option]))
            _skillList.Remove(_options[option]);
        currentSelection++;
        if (currentSelection > 2)
        {
            ClearExistingButtons();
            GameManager.Instance.playerSkillList = _playerSkillList;
            GameManager.Instance.StartGameplayLoop();
        }
        else
        {
            StartSkillSelection();
        }
    }

    private void DisableButtons()
    {
       var _buttons = GetComponentsInChildren<MultiButton>();
        foreach (MultiButton btn in _buttons)
        {
            btn.enabled = true;
        }
    }
    private void EnableButtons()
    {
        var _buttons = GetComponentsInChildren<MultiButton>();
        foreach (MultiButton btn in _buttons)
        {
            btn.enabled = true;
        }
    }

    private void ClearExistingButtons()
    {
        var _buttons = GetComponentsInChildren<MultiButton>();
        foreach (MultiButton btn in _buttons)
        {
            btn.onClick.RemoveAllListeners();
            Destroy(btn.gameObject);
        }
    }
}
static class ExtensionsSkill
{
    private static Random rng = new Random();  

    public static void Shuffle<T>(this IList<T> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = Random.Range(0, n+1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }
}

