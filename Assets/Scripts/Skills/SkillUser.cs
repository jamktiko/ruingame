using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SkillUser : MonoBehaviour
{
    [FormerlySerializedAs("_inputReader")] public InputReader inputReader = default;

    [FormerlySerializedAs("_skillList")] [SerializeField] private NonMonoSkill[] skillList;

    [FormerlySerializedAs("_entityHealth")] [SerializeField] private Health entityHealth;

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

    public void Initialize()
    {
        skillList = new NonMonoSkill[4];
        var skills = GetComponentsInChildren<NonMonoSkill>();
        for (int i = 0; i < skills.Length; i++)
        {
            skillList[i] = skills[i];
            skillList[i].skillUser = this;
        }
        entityHealth = GetComponent<Health>();
    }
    void OnSkill1()
    {
        try
        {
            ActivateSkill(skillList[0]);
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
            ActivateSkill(skillList[1]);
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
            ActivateSkill(skillList[2]);
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
            ActivateSkill(skillList[3]);
        }
        catch
        {
            Debug.Log("No skill assigned!");
        }
    }
    
    public virtual void ActivateSkill(NonMonoSkill sk)
    {
        if (!sk.onCooldown)
        {
            sk.Execute();
            AddInvulnerability(sk.iFrameDuration);
            sk.onCooldown = true;
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
    public virtual IEnumerator GoOnCooldown(NonMonoSkill sk)
    {
        yield return new WaitForSeconds(sk.skillCooldown);
        sk.onCooldown = false;
    }

    public virtual IEnumerator UsePersistentEffect(NonMonoSkill sk)
    {
        yield return new WaitForSeconds(sk.duration);
        sk.DeActivatePersistentEffect();
    }
}
