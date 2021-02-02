using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillExecute : MonoBehaviour
{

    public string skillname;
    public string skillDescription;
    public float skillCooldown = 5f;

    public bool onCooldown = false;

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
            onCooldown = true;
            StartCoroutine(nameof(GoOnCooldown));
        }
        else
        {
            //ON COOLDOWN
        }
    }
}
