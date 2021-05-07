using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class SkillSelection : MonoBehaviour
{
    public List<SkillExecute> _options;
    public SkillExecute[] _playerSkillList;
    public List<SkillExecute> _skillList;
    public GameObject buttons;
    public GameObject skillSelectionButton;
    public List<SkillExecute> _skilllistTemplate;
    private int currentSelection = 0;
    private MenuManager menu;
    private SelectionButton[] _buttons;
    
    void OnEnable()
    {
        _skillList = GetComponents<SkillExecute>().ToList();
        _playerSkillList = new SkillExecute[3];
       menu = GetComponentInParent<MenuManager>();
       for (int i = 0; i < 3; i++)
       {
           GenerateButton(i);
       }

       _buttons = buttons.GetComponentsInChildren<SelectionButton>();
    }

    public void  StartSkillSelection()
    {
        DisableButtons();
        try {_skillList.Remove(_skillList.Find(x => x.skillname == "Sprint"));}
        catch{}
        GenerateSkillSelection();
        EnableButtons();
        var newTarget = GetComponentInChildren<MultiButton>().gameObject;
        EventSystem.current.SetSelectedGameObject(newTarget);
        menu.MSH.UpdateSelection(newTarget);
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
            GenerateOption(i, _buttons[i]);
        }
    }
    public void GenerateOption(int option, SelectionButton button)
    {
        button.option = option;
        button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = _options[option].skillname;
        button.onClick.AddListener(delegate { SelectOption(button.option); });
    }

    public void GenerateButton(int option)
    {
        var selectionbutton = Instantiate(skillSelectionButton, buttons.transform);
        selectionbutton.gameObject.name = "option " + option;
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
        if (currentSelection > 1)
        {
            ClearExistingButtons();
            _playerSkillList[2] = GetComponent<SprintSkill>();
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
        foreach (MultiButton btn in _buttons)
        {
            btn.onClick.RemoveAllListeners();
            btn.enabled = true;
        }
    }
    private void EnableButtons()
    {
        foreach (MultiButton btn in _buttons)
        {
            btn.enabled = true;
        }
    }

    private void ClearExistingButtons()
    {
        foreach (MultiButton btn in _buttons)
        {
            btn.onClick.RemoveAllListeners();
            
        }
        DisableButtons();
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

