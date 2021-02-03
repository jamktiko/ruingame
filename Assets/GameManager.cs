using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor.Animations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    //Should be a separate player manager
    private GameObject createdPlayer;
    public RuntimeAnimatorController playerAnimator;
    public GameObject playerCharacter;
    public InputReader playerInput;
    public TransformAnchor playerAnchor;
    
    public CameraManager CameraManager;
    public void ConstructPlayer()
    {
        Destroy(createdPlayer);
        //Create PlayerMaster
        var cp = new GameObject("PlayerMaster");
        
        //Setup player movement
        var pm = cp.AddComponent<PlayerMovement>();
        pm._playerInput = playerInput;
        pm.gameplayCameraTransform = playerAnchor;
        
        //Setup player attacks
        var pa = cp.AddComponent<PlayerAttack>();
        pa._inputReader = playerInput;
        
        cp.AddComponent<PlayerHealth>();
        
        //Setup player skills
        var su = cp.AddComponent<SkillUser>();
        su._inputReader = playerInput;
        
        var animator = cp.AddComponent<Animator>();
        animator.runtimeAnimatorController = playerAnimator;

        var sk = new GameObject("Skills");
        //ADD Selected skills to this gameobject
        
        //Get list of selected skills 
        sk.AddComponent<TeleportSkill>();
        
        sk.transform.SetParent(cp.transform);
        
        var pc = Instantiate(playerCharacter);
        pc.transform.SetParent(cp.transform);
        cp.tag = "Player";
        
        createdPlayer = cp;
        Destroy(cp);
    }

    void Start()
    {
        ConstructPlayer();
        var player = Instantiate(createdPlayer);
        InitializePlayer();
        SetupPlayerCamera(player.transform);
    }

    public void InitializePlayer()
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

    public void SetupPlayerCamera(Transform tr)
    {
        CameraManager.inputReader = playerInput;
        CameraManager.SetupProtagonistVirtualCamera(tr);
    }
}
