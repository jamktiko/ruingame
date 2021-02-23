using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillExecute : MonoBehaviour
{

    public SkillUser skillUser;
    public string skillname;
    public string skillDescription;
    public float skillCooldown = 3f;
    public float iFrameDuration = 0.3f;
    public bool onCooldown = false;
    public float duration = 0.5f;

    [SerializeField] protected bool attackAllEnemies = true;
    [SerializeField] protected float attackRadius = 3f;
    [SerializeField] protected float attackDistance = 0f;
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float SprintSpeed = 20f;

    protected Targeting targeting;
    protected PlayerHealth playerHealth;



    private void Start()
    {
        targeting = gameObject.AddComponent<Targeting>();
        playerHealth = GetComponent<PlayerHealth>();


    }
    public virtual void Execute()
    {
    }

    public virtual void ApplyPersistentEffect(SkillExecute sk)
    {
        IEnumerator coroutine = skillUser.UsePersistentEffect(sk);
        skillUser.StartCoroutine(coroutine);
        IEnumerator coroutine2 = skillUser.GoOnCooldown(sk);
        skillUser.StartCoroutine(coroutine2);
    }

    public virtual void DeActivatePersistentEffect()
    {

    }
    public virtual void ModifyPlayerStats(int type)
    {

    }
}
