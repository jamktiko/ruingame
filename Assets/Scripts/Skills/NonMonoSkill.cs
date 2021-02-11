using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NonMonoSkill
{
    public string skillname;
    public string skillDescription;
    public float duration;
    public float skillCooldown = 5f;
    public float iFrameDuration = 1f;
    public bool onCooldown = false;
    public bool persistentEffect = false;
    public float persistentEffectTime = 2f;
    public SkillUser skillUser;
    public virtual void Execute()
    {
    }
    public virtual void ApplyPersistentEffect()
    {
        
    }

    public virtual void DeActivatePersistentEffect()
    {
        
    }
}