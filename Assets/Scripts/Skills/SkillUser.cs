using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Skills;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class SkillUser : MonoBehaviour
{
    public InputReader inputReader = default;
    
    public SkillExecute[] skillList;

    public Animator entityAnimator;
    
    [SerializeField] private Health entityHealth;

    public SkillsUI skillUI;
    
    private PlayerManager _playerManager;

    private void Awake()
    {
        entityAnimator = GetComponentInChildren<Animator>();
        _playerManager = PlayerManager.Instance;
        inputReader = _playerManager.playerInputReader;
        skillList = new SkillExecute[4];
        var skills = GetComponentsInChildren<SkillExecute>();
        for (int i = 0; i < skills.Length; i++)
        {
            skillList[i] = skills[i];
            skillList[i].skillUser = this;
        }
        entityHealth = GetComponent<Health>();
        skillList[0] = gameObject.AddComponent<WhirlwindSkill>();
        skillList[0].skillUser = this;
        skillList[3] = gameObject.AddComponent<SprintSkill>();
        skillList[3].skillUser = this;
    }
    private void OnEnable()
   
    {
        try
        {
            inputReader.ActivateSkill1 += OnSkill1;
            inputReader.ActivateSkill2 += OnSkill2;
            inputReader.ActivateSkill3 += OnSKill3;
            inputReader.ActivateSprintSkill += OnSprint;
        }
        catch{}
    }
    
    private void OnDisable()
    {
        try
        {
            inputReader.ActivateSkill1 -= OnSkill1;
            inputReader.ActivateSkill2 -= OnSkill2;
            inputReader.ActivateSkill3 -= OnSKill3;
            inputReader.ActivateSprintSkill -= OnSprint;
        }
        catch{}
    }
    void OnSkill1()
    {
        try
        {
            ActivateSkill(skillList[0], 0);
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
            ActivateSkill(skillList[1], 1);
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
            ActivateSkill(skillList[2], 2);
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
            ActivateSkill(skillList[3], 3);
        }
        catch
        {
            Debug.Log("Cant Execute Skill!");
        }
    }
    
    public virtual void ActivateSkill(SkillExecute sk, int index)
    {
        if (entityAnimator.GetFloat("attackCancelFloat") < 1f)
        {
            if (!sk.onCooldown)
            {
                skillUI.OnSkillUse(index);
                sk.Execute();
                _playerManager.StopAttacking();
                entityAnimator.Play("Sprinting");
                AddInvulnerability(sk.iFrameDuration);
                sk.onCooldown = true;
                StartCoroutine(GoOnCooldown(sk));
            }
        }
    }
    public void ResetAllSkills()
    {
        foreach (var skill in skillList)
        {
            skill.onCooldown = false;
        }
        this.StopAllCoroutines();
    }
    public void AddInvulnerability(float duration)
    {
        entityHealth.AddIFrame(duration);
    }
    public virtual IEnumerator GoOnCooldown(SkillExecute sk)
    {
        yield return new WaitForSeconds(sk.skillCooldown);
        sk.onCooldown = false;
    }

    public virtual IEnumerator UsePersistentEffect(SkillExecute sk)
    {
        yield return new WaitForSeconds(sk.duration);
        sk.DeActivatePersistentEffect();
    }
}
