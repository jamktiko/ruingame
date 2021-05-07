
using System;
using System.Collections;
using DefaultNamespace;
using DefaultNamespace.Skills;
using UnityEngine;



public class SkillUser : MonoBehaviour
{
    public InputReader inputReader = default;

    public SkillExecute[] skillList;

    public Animator entityAnimator;

    [SerializeField] private EntityHealth entityHealth;

    public BaseAttackHandler attackHandler;

    public SkillsUI skillUI;

    private PlayerManager _playerManager;

    public bool usingSkill;

    public Transform VFXPoint;

    public delegate void SkillActivatedHandler(SkillActivatedEventArgs e);

    public event SkillActivatedHandler SkillActivated;

    //STORE THIS IN SKILL
    public AnimationClip sprintAnimation;
    public AnimationClip stanceChangeAnimation;
    public AnimationClip whirlWindAnimation;

    public GameObject defaultParticles;
    public Targeting skillTargeting { get; private set; }
    
    private void Awake()
    {

    }

    private void Start()
    {
        foreach (SkillExecute sk in skillList)
        {
            sk.skillUser = this;
            sk.playerHealth = PlayerManager.Instance._playerHealth;
            sk.playerRb = PlayerManager.Instance._playerMovement._characterRigidBody;
        }
    }
    public void InitializeSkills()
    {
        entityAnimator = GetComponentInChildren<Animator>();
        _playerManager = PlayerManager.Instance;
        inputReader = _playerManager.playerInputReader;
        entityHealth = GetComponent<EntityHealth>();
        attackHandler = GetComponent<BaseAttackHandler>();
        skillTargeting = gameObject.AddComponent<Targeting>();

        skillList = new SkillExecute[3];
        /*skillList[0] = gameObject.AddComponent<TeleportSkill>();
        skillList[0].animationClip = whirlWindAnimation;
        skillList[0].skillUser = this;
        skillList[1] = gameObject.AddComponent<WhirlwindSkill>();
        skillList[1].animationClip = whirlWindAnimation;
        skillList[1].skillUser = this;
        skillList[2] = gameObject.AddComponent<SprintSkill>();
        skillList[2].animationClip = sprintAnimation;
        
        
        skillList[2].skillUser = this;
        skillList2 = new ScriptableSkill[1];
        skillList2[0] = ScriptableObject.CreateInstance<ScriptableSprint>();
        skillList2[0].animationClip = whirlWindAnimation;
        skillList2[0].skillUser = this;
        */
        skillList = GameManager.Instance.playerSkillList;
    }
    private void OnEnable()
  
    {
        try
        {
            inputReader.ActivateSkill1 += OnSkill1;
            inputReader.ActivateSkill2 += OnSkill2;
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
            inputReader.ActivateSprintSkill -= OnSprint;
        }
        catch{}
    }
    void OnSkill1()
    {
        try
        {
            ActivateSkill(0);
        }
        catch
        {
            Debug.Log("Cant Execute Skill!");
        }
    }

    void OnSkill2()
    {
        try
        {
            ActivateSkill(1);
        }
        catch
        {
            Debug.Log("Cant Execute Skill!");
        }
    }
    void OnSprint()
    {
        try
        {
            ActivateSkill(2);
        }
        catch
        {
            Debug.Log("Cant Execute Skill!");
        }
    }

    public virtual void ActivateSkill(int index)
    {
        var sk = skillList[index];
        if (entityAnimator.GetFloat("attackCancelFloat") < 1f)
        {
            if (!sk.onCooldown)
            {
                if (!usingSkill)
                {
                    if (sk.CheckExecuteCondition())
                    {
                        try
                        {
                            sk.Execute(sk.animationClip.length);
                            SkillActivatedEventArgs e = new SkillActivatedEventArgs();
                            e.SkillIndex = index;
                            SkillActivated?.Invoke(e);
                        }
                        catch
                        {
                            sk.Execute();
                            Debug.Log("No skill animation!");
                        }
                        AddInvulnerability(sk.iFrameDuration);
                        StartCoroutine(GoOnCooldown(sk));
                    }

                    //PlayerManager.Instance.ZoomCameraInAndOut();
                    //SHOULD ONLY BE CALLED AFTER SKILL GOES ON COOLDOWN, EG. Stance change only goes on cooldown after the duration is over
                    // _playerManager.StopAttacking();
                    //SKILL SHOULD DETERMINE WHICH ANIMATION TO USE
                    //Currently uses animation length to determine skill duration, probably should work other way around?
                }
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
        sk.DeActivateSkillActive();
    }


    public void PlayAnimation(SkillExecute sk)
    {
        entityAnimator.Play(sk.animationClip.name);
    }

}
public class SkillActivatedEventArgs : EventArgs
{
    public int SkillIndex { get; set; }
}