using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(SkillUser))]
[RequireComponent(typeof(Animator))]
public class PlayerManager : BaseManager
{
    public InputReader _playerInputReader;
    public RuntimeAnimatorController _playerAnimator;

    private PlayerAttack _playerAttack;
    private PlayerHealth _playerHealth;
    public PlayerMovement _playerMovement;
    private SkillUser _playerSkills;
    private void Awake()
    {
        _playerAttack = GetComponent<PlayerAttack>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerSkills = GetComponent<SkillUser>();

    }

    private void Start()
    {
        SetupPlayerInput();
        SetupPlayerCamera();
        SetupPlayerAnimations();
        
        InitializeScriptsOnPlayer();
    }

    private void SetupPlayerInput()
    {
        _playerAttack._inputReader = _playerInputReader;
        _playerMovement._inputReader = _playerInputReader;
        _playerSkills._inputReader = _playerInputReader;
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

    private void SetupPlayerAnimations()
    {
        var animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = _playerAnimator;
        animator.applyRootMotion = true;
    }

    public void UpdateSkills()
    {
        _playerSkills.Initialize();
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
        Debug.Log("Player loaded!");
    }

}
