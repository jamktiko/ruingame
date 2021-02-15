using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillExecute : MonoBehaviour
{

    public string skillname;
    public string skillDescription;
    public float skillCooldown = 3f;
    public float iFrameDuration = 0.3f;
    public bool onCooldown = false;
    public SkillUser skillUser;
    public float duration = 0.5f;
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
