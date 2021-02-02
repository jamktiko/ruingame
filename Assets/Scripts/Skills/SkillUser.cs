using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUser : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;

    [SerializeField] private SkillExecute[] _skillList;

    [SerializeField] private Health entityHealth;
    private void OnEnable()
    {
        _inputReader.activateSkill1 += OnSkill1;
        _inputReader.activateSkill2 += OnSkill2;
        _inputReader.activateSkill3 += OnSKill3;
        _inputReader.activateSprintSkill += OnSprint;
        Initialize();
        
    }
    
    private void OnDisable()
    {
        _inputReader.activateSkill1 -= OnSkill1;
        _inputReader.activateSkill2 -= OnSkill2;
        _inputReader.activateSkill3 -= OnSKill3;
        _inputReader.activateSprintSkill -= OnSprint;
    }

    private void Initialize()
    {
        _skillList = new SkillExecute[4];
        var skills = GetComponentsInChildren<SkillExecute>();
        for (int i = 0; i < skills.Length; i++)
        {
            _skillList[i] = skills[i];
            _skillList[i].skillUser = this;
        }
    }
    void OnSkill1()
    {
        try
        {
            _skillList[0].ActivateSkill();
        }
        catch
        {
            Debug.Log("No skill assigned!");
        }
    }

    void OnSkill2()
    {
        try
        {
            _skillList[1].ActivateSkill();
        }
        catch
        {
            Debug.Log("No skill assigned!");
        }
    }

    void OnSKill3()
    {
        try
        {
            _skillList[2].ActivateSkill();
        }
        catch
        {
            Debug.Log("No skill assigned!");
        }
    }

    void OnSprint()
    {
        try
        {
            _skillList[3].ActivateSkill();
        }
        catch
        {
            Debug.Log("No skill assigned!");
        }
    }

    public void ResetAllSkills()
    {
        foreach (var skill in _skillList)
        {
            skill.StopCoroutine(nameof(SkillExecute.GoOnCooldown));
            skill.onCooldown = false;
            
        }
        Debug.Log("Reset all skills!");
    }

    public void AddInvulnerability(float duration)
    {
        entityHealth.AddIFrame(duration);
    }
}
