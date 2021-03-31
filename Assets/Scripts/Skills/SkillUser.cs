
using System.Collections;
using DefaultNamespace;
using DefaultNamespace.Skills;
using UnityEngine;


public class SkillUser : MonoBehaviour
{
    public InputReader inputReader = default;
    
    public SkillExecute[] skillList;

    public Animator entityAnimator;
    
    [SerializeField] private Health entityHealth;

    public SkillsUI skillUI;
    
    private PlayerManager _playerManager;

    public bool usingSkill;

    public Transform VFXPoint;
    
    //STORE THIS IN SKILL
    public AnimationClip sprintAnimation;
    public AnimationClip stanceChangeAnimation;
    public AnimationClip whirlWindAnimation;

    public GameObject defaultParticles;
    public Targeting skillTargeting { get; private set; }
    private void Awake()
    {
        entityAnimator = GetComponentInChildren<Animator>();
        _playerManager = PlayerManager.Instance;
        inputReader = _playerManager.playerInputReader;
        entityHealth = GetComponent<Health>();
        
        skillTargeting = gameObject.AddComponent<Targeting>();
        
        skillList = new SkillExecute[4];
        skillList[0] = gameObject.AddComponent<WhirlwindSkill>();
        skillList[0].skillUser = this;
        skillList[0].animationClip = whirlWindAnimation;
        skillList[1] = gameObject.AddComponent<ShieldBashSkill>();
        skillList[1].skillUser = this;
        skillList[2] = gameObject.AddComponent<StanceChangeSkill>();
        skillList[2].animationClip = stanceChangeAnimation;
        skillList[2].skillUser = this;
        skillList[3] = gameObject.AddComponent<SprintSkill>();
        skillList[3].animationClip = sprintAnimation;
        skillList[3].skillUser = this;
        
        
    }
    private void OnEnable()
  
    {
        try
        {
            inputReader.ActivateSkill1 += OnSkill1;
            inputReader.ActivateSkill2 += OnSkill2;
            inputReader.ActivateSkill3 += OnSKill3;
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
            inputReader.ActivateSkill3 -= OnSKill3;
            inputReader.ActivateSprintSkill -= OnSprint;
        }
        catch{}
    }
    void OnSkill1()
    {
        try
        {
            ActivateSkill(skillList[0], 0);
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
            ActivateSkill(skillList[1], 1);
        }
        catch
        {
            Debug.Log("Cant Execute Skill!");
        }
    }

    void OnSKill3()
    {
        try
        {
            ActivateSkill(skillList[2], 2);
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
            ActivateSkill(skillList[3], 3);
        }
        catch
        {
            Debug.Log("Cant Execute Skill!");
        }
    }
    
    public virtual void ActivateSkill(SkillExecute sk, int index)
    {
        if (entityAnimator.GetFloat("attackCancelFloat") < 1f)
        {
            if (!sk.onCooldown)
            {
                if (!usingSkill)
                {
                    //PlayerManager.Instance.ZoomCameraInAndOut();
                    //SHOULD ONLY BE CALLED AFTER SKILL GOES ON COOLDOWN, EG. Stance change only goes on cooldown after the duration is over

                    try
                    {
                        skillUI.OnSkillUse(index);
                    }
                    catch
                    {
                        Debug.Log("skill UI not updating correctly");
                    }

                    try
                    {
                        _playerManager.StopAttacking();
                    }
                    catch
                    {
                        Debug.Log("Playermanager.StopAttacking");
                    }
                    //SKILL SHOULD DETERMINE WHICH ANIMATION TO USE
                    //Currently uses animation length to determine skill duration, probably should work other way around?
          
                    try
                    {
                        sk.Execute(sk.animationClip.length);
                    }
                    catch
                    {
                        try {sk.Execute();}
                        catch{Debug.Log("Skill Execute");}
                        Debug.Log("No skill animation!");
                    }

                    try
                    {
                        var ps =Instantiate(defaultParticles, VFXPoint.position, Quaternion.identity);
                        Destroy(ps, 0.5f);
                    }
                    catch{Debug.Log("Particles");}
                    try
                    {
                        AddInvulnerability(sk.iFrameDuration);
                    }
                    catch
                    {
                        Debug.Log("Iframe");
                    }
                    try
                    {
                        StartCoroutine(GoOnCooldown(sk));
                    }
                    catch
                    {
                        Debug.Log("Cooldown");
                    }
                }
                else
                {
                    Debug.Log("Using a skill!");
                }
            }
            else
            {
                Debug.Log("Skill on Cooldown!");
            }
        }
        else
        {
            Debug.Log("Animation in progress!");
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
        entityAnimator.Play(sk.skillname);
    }
}
