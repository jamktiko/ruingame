using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Instrumentation;
using DefaultNamespace;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

/*
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerMovement))]
*/
[RequireComponent(typeof(SkillUser))]


//Move player initialization to another script
//Have run time management in one class
public class PlayerManager : BaseManager
{
    public InputReader _playerInputReader;
    public RuntimeAnimatorController _playerAnimator;

    private PlayerAttack _playerAttack;
    private PlayerHealth _playerHealth;
    public MovementController _playerMovement;
    private SkillUser _playerSkills;

    public PlayerData _playerData;
    
    [HideInInspector]
    public UnityEvent pickUpEvent;
    
    
    private void OnEnable()
    {
        _playerInputReader.interactEvent += OnPickUp;
    }
    private void OnDisable()
    {
        _playerInputReader.interactEvent -= OnPickUp;
    }

    private void Awake()
    {
        _playerAttack = GetComponent<PlayerAttack>();
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
        InitializeScriptsOnPlayer();
        UpdateSkills();
        UpdatePlayerStats();
    }

    private void SetupPlayerInput()
    {
        _playerAttack._inputReader = _playerInputReader;
        _playerMovement._inputReader = _playerInputReader;
      //  _playerSkills._inputReader = _playerInputReader;
    }

    private void SetupPlayerCamera()
    {
        var cam = GameObject.FindGameObjectWithTag("Cameras").GetComponent<CameraManager>();
        _playerMovement.gameplayCameraTransform = cam._cameraTransformAnchor;
        cam.inputReader = _playerInputReader;
        cam.SetupProtagonistVirtualCamera(gameObject.transform);
        cam.playerTransform = gameObject.transform;
        cam.enabled = false;
        cam.enabled = true;
    }

    private void SetupCharacterController()
    {
        var CC = GetComponent<CharacterController>();
        
    }
    private void SetupPlayerAnimations()
    {
        var animator = GetComponentInChildren<Animator>();
        animator.runtimeAnimatorController = _playerAnimator;
        animator.applyRootMotion = true;
    }

    public void UpdateSkills()
    {
        //_playerSkills.Initialize();
    }
    public void InitializeScriptsOnPlayer()
    {
        //Enables every script on the player
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            MonoBehaviour[] mb = go.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour m in mb)
            {
                m.enabled = false;
                m.enabled = true;
            }
        }
    }

    public void UpdatePlayerStats()
    {
        _playerMovement._movementSpeed = _playerData._entityMovementSpeed;
        _playerMovement._jumpHeight = _playerData._entityjumpHeight;
    }
    public void OnPickUp()
    {
        pickUpEvent.Invoke();
    }

    public void AddArtifact(Artifact artifact)
    {
        //Take artifact stats and add as a string to the artifact list
        Destroy(artifact.gameObject, 0.1f);
        Invoke("UpdatePlayerStats", 0.2f);
    }
    public void ModifyMovementSpeed(float amount, int type)
    {
        //Should be in a single method (Give property to modify and perform modification based on type given)
        switch (type)
        {
            case 0:
                _playerData._entityMovementSpeed -= amount;
                break;
            case 1:
                _playerData._entityMovementSpeed += amount;
                break;
            default:
                Debug.Log("No type given for modification type");
                break;
        }
    }
    public void ModifyJump(float amount, int type)
    {
        switch (type)
        {
            case 0:
                _playerData._entityjumpHeight -= amount;
                break;
            case 1:
                _playerData._entityjumpHeight += amount;
                break;
            default:
                Debug.Log("No type given for modification type");
                break;
        }
    }

    public void ModifyDamage(float amount, int type)
    {
        switch (type)
        {
            case 0:
                _playerData._entityDamage -= amount;
                break;
            case 1:
                _playerData._entityDamage += amount;
                break;
            default:
                Debug.Log("No type given for modification type");
                break;
        }
    }

    public void ModifyAttackSpeed(float amount, int type)
    {
        switch (type)
        {
            case 0:
                _playerData._entityAttackSpeed -= amount;
                break;
            case 1:
                _playerData._entityAttackSpeed += amount;
                break;
            default:
                Debug.Log("No type given for modification type");
                break;
        }
    }
}
