using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUser : MonoBehaviour
{
    [SerializeField] private InputReader _playerInput = default;
    [SerializeField] private ScriptableSkill[] _skillList = new ScriptableSkill[4];

    private void OnEnable()
    {
        _playerInput.activateSkill1 += OnSkill1;
        _playerInput.activateSkill2 += OnSkill2;
        _playerInput.activateSkill3 += OnSKill3;
        _playerInput.activateSprintSkill += OnSprint;
    }
    
    private void OnDisable()
    {
        _playerInput.activateSkill1 -= OnSkill1;
        _playerInput.activateSkill2 -= OnSkill2;
        _playerInput.activateSkill3 -= OnSKill3;
        _playerInput.activateSprintSkill -= OnSprint;
    }
    void OnSkill1()
    {
        try
        {
            _skillList[0]._skillExecute.Execute();
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
            _skillList[1]._skillExecute.Execute();
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
            _skillList[2]._skillExecute.Execute();
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
            _skillList[3]._skillExecute.Execute();
        }
        catch
        {
            Debug.Log("No skill assigned!");
        }
    }

    void OnSkillClean()
    {
        
    }
}
