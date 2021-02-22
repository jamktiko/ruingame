
using UnityEngine;

public class SkillExecute : MonoBehaviour
{
    //STORE THIS DATA IN A PREFAB OR SCRIPTABLE
    public string skillname;
    public string skillDescription;
    public float skillCooldown = 3f;
    public float iFrameDuration = 0.3f;
    public bool onCooldown = false;
    public SkillUser skillUser;
    public float duration = 0.5f;
    public AnimationClip animationClip;
    public virtual void Execute()
    {
        Debug.Log("Skill base, create an implementation!");
    }
    public virtual void Execute(float duration)
    {
        
    }
    public virtual void WhileSkillActive()
    {
        
    }

    public virtual void DeActivateSkillActive()
    {
        
    }
}
