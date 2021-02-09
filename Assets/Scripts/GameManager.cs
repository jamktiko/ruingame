using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Skills;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }
    
    
    //Creates singleton gamemanager
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    
    [Header("Player References")]
    public PlayerManager _playerManager;
    public GameObject currentPlayer;
    public GameObject _playerMasterPrefab;
    
    [Header("Player Control SO")] 
    public InputReader _playerInputReader;

    [Header("Player Character and Animation")]
    public GameObject playerCharacter;
    public RuntimeAnimatorController _playerAnimator;
    
    [Header("Room Management References")]
    public RoomManager roomManager;
    public Roomvariants _Roomvariants;
    
    private GameObject _createdPlayer;
    public void ConstructPlayer()
    {
        //Destroy previous player
        Destroy(currentPlayer);
        //Create PlayerMaster
        currentPlayer = Instantiate(_playerMasterPrefab);
        currentPlayer.name = "PlayerMaster";
        
        _playerManager = currentPlayer.GetComponent<PlayerManager>();
        _playerManager._playerInputReader = _playerInputReader;
        
        //Setup playerAnimator / Make playeranimator and playercharacter come from a source for character selection.
        _playerManager._playerAnimator = _playerAnimator;
            
        //ADD Skills holder object
        var sk = new GameObject("Skills");
        //Get list of selected skills 
        //ADD Selected skills to this gameobject
        sk.AddComponent<SprintSkill>();
        //Set skill holder parent to playermaster
        sk.transform.SetParent(currentPlayer.transform);
        //UPDATE Skills on playermanager
        //_playerManager.UpdateSkills();
        //Instantiate player model
        sk.tag = "Player";
        currentPlayer.tag = "Player";
    }

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("MovementRework");
        StartCoroutine("CreateGame");
    }
    public virtual IEnumerator CreateGame()
    {
        yield return new WaitForSeconds(0.5f);
        ConstructPlayer();
        InitializePlayer();
        yield return new WaitForSeconds(0.5f);
        CreateRoomManager();
        roomManager.StartRoomManager();
    }

    public void GameOver()
    {
        roomManager.enabled = false;
        Destroy(roomManager);
        currentPlayer.GetComponent<PlayerManager>().enabled = false;
        Destroy(currentPlayer);
        SceneManager.LoadScene("MainMenu");
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
}
