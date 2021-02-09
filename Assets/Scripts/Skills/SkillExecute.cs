using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillExecute : MonoBehaviour
{

    public string skillname;
    public string skillDescription;
    public float skillCooldown = 5f;
    public float iFrameDuration = 1f;
    public bool onCooldown = false;
    public bool persistentEffect = false;
    public float persistentEffectTime = 2f;
    public SkillUser skillUser;
    public virtual void Execute()
    {
    }
    public virtual IEnumerator GoOnCooldown()
    {
        yield return new WaitForSeconds(skillCooldown);
        onCooldown = false;
    }

    public virtual void ActivateSkill()
    {
        if (!onCooldown)
        {
            Execute();
            skillUser.AddInvulnerability(iFrameDuration);
            onCooldown = true;
            StartCoroutine(nameof(GoOnCooldown));
        }
        else
        {
            //ON COOLDOWN
        }
    }

    public virtual IEnumerator SkillPersistentEffect()
    {
        ApplyPersistentEffect();
        yield return new WaitForSeconds(persistentEffectTime);
        DeActivatePersistentEffect();
    }

    public virtual void ApplyPersistentEffect()
    {
        
    }

    public virtual void DeActivatePersistentEffect()
    {
        
    }
}
