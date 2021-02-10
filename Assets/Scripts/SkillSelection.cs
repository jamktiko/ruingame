using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillSelection : MonoBehaviour
{
    //Creates singleton gamemanager
    private static SkillSelection _instance;
    public static SkillSelection Instance
    {
        get { return _instance; }
    }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    private SkillSelection _skillSelection;
    List<NonMonoSkill> _options = new List<NonMonoSkill>();
    private GameManager _gameManager;
    private SkillData _generatedSkillData;
    public MenuManager menuManager = default;
    
    void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void StartSkillSelection()
    {
        _generatedSkillData = gameObject.AddComponent<SkillData>();
        //GENERATE SELECTION OF SKILLS
        //ENABLE BUTTONS
        //ON BUTTON CLICK SELECT SKILL
    }

    public void GenerateSkillSelection()
    {
        _options.Add(_gameManager.baseSkillData.GetRandomSkillExecute());
        _options.Add(_gameManager.baseSkillData.GetRandomSkillExecute());
        _options.Add(_gameManager.baseSkillData.GetRandomSkillExecute());
    }

    public void SelectOption1()
    {
        _generatedSkillData.skillList.skill.Add(_options.ElementAt(0));
        _options.RemoveAt(0);
        OnSkillSelect(0);
    }
    public void SelectOption2()
    {
        _generatedSkillData.skillList.skill.Add(_options.ElementAt(1));
        _options.RemoveAt(1);
        OnSkillSelect(1);
    }
    public void SelectOption3()
    {
        _generatedSkillData.skillList.skill.Add(_options.ElementAt(2));
        _options.RemoveAt(2);
        OnSkillSelect(2);
    }

    private void OnSkillSelect(int option)
    {
        //Disable buttons
        //Add options that werent selected back to base
        if (_generatedSkillData.skillList.skill.Count == 4)
        {
            _gameManager.givenSkillData = _generatedSkillData;
            menuManager.StartGame();
        }
        else
        {
            _options = new List<NonMonoSkill>();
            GenerateSkillSelection();
        }
    }
}
