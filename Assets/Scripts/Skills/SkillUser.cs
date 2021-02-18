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
    
    //STORE THIS IN SKILL
    public AnimationClip sprintAnimation;
    
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
        //MOVE THIS TO CHARACTER CREATION OR SOME SHIT
        skillList[3] = gameObject.AddComponent<SprintSkill>();
        skillList[3].animationClip = sprintAnimation;
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
                
                //SKILL SHOULD DETERMINE WHICH ANIMATION TO USE
                entityAnimator.Play(sk.animationClip.name);
                //Currently uses animation length to determine skill duration, probably should work other way around?
                sk.Execute(sk.animationClip.length);
                
                _playerManager.StopAttacking();
                AddInvulnerability(sk.iFrameDuration);
                StartCoroutine(GoOnCooldown(sk));
            }
        }
    }
    public void ResetAllSkills()
    {
        //USE WITH CARE
        this.StopAllCoroutines();
        foreach (var skill in skillList)
        {
            skill.onCooldown = false;
        }
    }
    public void AddInvulnerability(float duration)
    {
        entityHealth.AddIFrame(duration);
    }
    public virtual IEnumerator GoOnCooldown(SkillExecute sk)
    {
        sk.onCooldown = true;
        yield return new WaitForSeconds(sk.skillCooldown);
        sk.onCooldown = false;
    }

    public virtual IEnumerator UsePersistentEffect(SkillExecute sk)
    {
        yield return new WaitForSeconds(sk.duration);
        sk.DeActivatePersistentEffect();
    }
}
