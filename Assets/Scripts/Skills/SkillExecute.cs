using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillExecute : MonoBehaviour
{

    public string skillname;
    public string skillDescription;
    public float skillCooldown = 5f;

    public bool onCooldown = false;
    
    public virtual void Execute()
    {
    }
    public virtual IEnumerator GoOnCooldown()
    {
        Debug.Log("Skill going on cooldown!");
        yield return new WaitForSeconds(skillCooldown);
        Debug.Log("Skill ready!");
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
            Debug.Log("Skill on cooldown!");
        }
    }
}
