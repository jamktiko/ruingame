using UnityEngine;
using System.Collections.Generic;

public class SkillExecute : MonoBehaviour
{
    public SkillUser skillUser;

    //STORE THIS DATA IN A PREFAB OR SCRIPTABLE
    public string skillname;
    public string skillDescription;
    public float skillCooldown = 3f;
    public float iFrameDuration = 0.3f;
    public bool onCooldown = false;
    public float duration = 4f;
    public AnimationClip animationClip;

    protected float damage = 10f;

    protected Targeting targeting;
    protected PlayerHealth playerHealth;
    protected Rigidbody playerRb;

    protected virtual void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        targeting = skillUser.skillTargeting;
        playerRb = gameObject.GetComponent<Rigidbody>();

    }
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
    public virtual void ModifyPlayerStats(int type)
    {

    }
}
