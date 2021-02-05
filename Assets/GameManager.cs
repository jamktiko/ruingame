using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor.Animations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [Header("Player References")]
    public PlayerManager _playerManager;
    public GameObject currentPlayer;
    
    [Header("Player Character and Animation")]
    public GameObject playerCharacter;
    public RuntimeAnimatorController _playerAnimator;

    [Header("Player Control SO")] 
    public InputReader _playerInputReader;
    
    [Header("Room Management References")]
    public RoomManager roomManager;
    public Roomvariants _Roomvariants;
    
    private GameObject _createdPlayer;
    void Start()
    {
        StartGame();
    }
    public void ConstructPlayer()
    {
        //Destroy previous player
        Destroy(currentPlayer);
        //Create PlayerMaster
        currentPlayer = new GameObject("PlayerMaster");
        //Setup player manager
        _playerManager = currentPlayer.AddComponent<PlayerManager>();
        _playerManager._playerInputReader = _playerInputReader;
        //Setup playerAnimator / Make playeranimator and playercharacter come from a source for character selection.
        _playerManager._playerAnimator = _playerAnimator;
            
        //ADD Skills holder object
        var sk = new GameObject("Skills");
        //Get list of selected skills 
        //ADD Selected skills to this gameobject
        sk.AddComponent<TeleportSkill>();
        //Set skill holder parent to playermaster
        sk.transform.SetParent(currentPlayer.transform);
        //UPDATE Skills on playermanager
        _playerManager.UpdateSkills();
        
        //Instantiate player model
        var pc = Instantiate(playerCharacter);
        
        pc.name = "PlayerCharacter";
        pc.transform.SetParent(currentPlayer.transform);
        sk.tag = "Player";
        pc.tag = "Player";
        currentPlayer.tag = "Player";
    }

    void StartGame()
    {
        ConstructPlayer();
        InitializePlayer();
        CreateRoomManager();
        roomManager.StartRoomManager();
    }

    public void InitializePlayer()
    {
        currentPlayer.transform.position = gameObject.transform.position;
    }
    

    public void CreateRoomManager()
    {
        roomManager = gameObject.AddComponent<RoomManager>();
        roomManager.playerReference = currentPlayer;
        roomManager.possibleRooms = _Roomvariants.possibleRooms;
    }

    [RuntimeInitializeOnLoadMethod]
    static void GameLoaded()
    {
        Debug.Log("Game Loaded");
    }

}
