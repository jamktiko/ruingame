using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(PlayerAttackHandler))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(SkillUser))]

//Move player initialization to another script
//Have run time management in separate class
public class PlayerManager : BaseManager
{
    private static PlayerManager _instance;
    public static PlayerManager Instance
    {
        get { return _instance; }
    }
    
    public InputReader playerInputReader { get; private set; }
    public RuntimeAnimatorController playerAnimator { get; private set; }
    
    private PlayerAttackHandler _playerAttack;
    private PlayerHealth _playerHealth;
    public MovementController _playerMovement { get; private set; }
    public SkillUser _playerSkills { get; private set; }

    public Combo _weaponData { get; private set; }
    public PlayerData _playerData;
    
    [HideInInspector]
    public UnityEvent pickUpEvent;
    
    private void OnEnable()
    {
        playerInputReader.InteractEvent += OnPickUp;
    }
    private void OnDisable()
    {
        playerInputReader.InteractEvent -= OnPickUp;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        playerInputReader = GameManager.Instance.playerInputReader;
        _playerAttack = GetComponent<PlayerAttackHandler>();
        _weaponData = GameManager.Instance.weaponCombo;
        _playerHealth = GetComponent<PlayerHealth>();
        _playerMovement = GetComponent<MovementController>();
        _playerSkills = GetComponent<SkillUser>();
        _playerData = ScriptableObject.CreateInstance<PlayerData>();
    }

    private void Start()
    {
        SetupPlayerInput();
        SetupPlayerCamera();
        SetupPlayerAnimations();
        DisableScriptsOnPlayer();
        EnableScriptsOnPlayer();
        UpdatePlayerStats();
        LockCursorToGame();
    }

    private void LockCursorToGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void SetupPlayerInput()
    {
        playerAnimator = GameManager.Instance.playerAnimator;
    }

    private void SetupPlayerCamera()
    {
        var cam = GameObject.FindGameObjectWithTag("Cameras").GetComponent<CameraManager>();
        _playerMovement.SetPlayerCamera(cam.cameraTransformAnchor);
        cam.SetupProtagonistVirtualCamera(gameObject.transform);
        cam.playerTransform = gameObject.transform;
        cam.enabled = false;
        cam.enabled = true;
    }

    private void SetupPlayerAnimations()
    {
        var animator = GetComponentInChildren<Animator>();
        animator.runtimeAnimatorController = playerAnimator;
        _playerAttack.comboHandler.comboData = _weaponData;
    }

    public void UpdateSkills()
    {
       // _playerSkills.Initialize();
    }
    public void DisableScriptsOnPlayer()
    {
        _playerAttack.enabled = false;
        _playerHealth.enabled = false;
        _playerMovement.enabled = false;
        _playerSkills.enabled = false;
    }
    public void EnableScriptsOnPlayer()
    {
        _playerAttack.enabled = true;
        _playerHealth.enabled = true;
        _playerMovement.enabled = true;
        _playerSkills.enabled = true;
    }
    public void UpdatePlayerStats()
    {
        _playerMovement.SetMovementSpeed(_playerData.entityMovementSpeed);
        _playerMovement.SetJumpHeight((_playerData.entityjumpHeight));
        _playerAttack.SetDamage(_playerData.entityDamage);
        _playerAttack.SetAttackSpeed(_playerData.entityAttackSpeed);
    }
    public void OnPickUp()
    {
        pickUpEvent.Invoke();
    }

    public void AddArtifact(Artifact artifact)
    {
        //Give artifact stats and add as a string to the artifact list
        Destroy(artifact.gameObject, 0.1f);
    }
    public void ModifyMovementSpeed(float amount, int type)
    {
        //Should be in a single method (Give property to modify and perform modification based on type given)
        switch (type)
        {
            case 0:
                _playerData.entityMovementSpeed -= amount;
                break;
            case 1:
                _playerData.entityMovementSpeed += amount;
                break;
            default:
                Debug.Log("No type given for modification type");
                break;
        }
        _playerMovement.SetMovementSpeed(_playerData.entityMovementSpeed);
    }
    public void ModifyJump(float amount, int type)
    {
        switch (type)
        {
            case 0:
                _playerData.entityjumpHeight -= amount;
                break;
            case 1:
                _playerData.entityjumpHeight += amount;
                break;
            default:
                Debug.Log("No type given for modification type");
                break;
        }
        _playerMovement.SetJumpHeight((_playerData.entityjumpHeight));
    }

    public void ModifyDamage(float amount, int type)
    {
        switch (type)
        {
            case 0:
                _playerData.entityDamage -= amount;
                break;
            case 1:
                _playerData.entityDamage += amount;
                break;
            default:
                Debug.Log("No type given for modification type");
                break;
        }
        _playerAttack.SetDamage(_playerData.entityDamage);
    }

    public void ModifyAttackSpeed(float amount, int type)
    {
        switch (type)
        {
            case 0:
                _playerData.entityAttackSpeed -= amount;
                break;
            case 1:
                _playerData.entityAttackSpeed += amount;
                break;
            default:
                Debug.Log("No type given for modification type");
                break;
        }
        _playerAttack.SetAttackSpeed(_playerData.entityAttackSpeed);
    }

    public void Die()
    {
        DisableScriptsOnPlayer();
        GameManager.Instance.GameOver();
    }

    public void StopAttacking()
    {
        _playerAttack.EndAttack();
    }
}
