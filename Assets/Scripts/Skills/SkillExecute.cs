using UnityEngine;

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

    [SerializeField] protected bool attackAllEnemies = true;
    [SerializeField] protected float attackRadius = 3f;
    [SerializeField] protected float attackDistance = 0f;
    [SerializeField] protected float damage = 10f;


    protected Targeting targeting;

    protected virtual void Start()
    {
        targeting = skillUser.skillTargeting;
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
