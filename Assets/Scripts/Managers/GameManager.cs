using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Creates singleton gamemanager
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public GameObject pauseMenu;
    public GameObject currentPauseMenu;
    public PlayerManager playerManager { get; private set; }
    public GameObject currentPlayer { get; private set; }

    [Header("Player References")] 
    public GameObject playerMasterPrefab;
    
    [Header("Player Control SO")] 
    public InputReader playerInputReader;

    [Header("Player Character and Animation")]
    public RuntimeAnimatorController playerAnimator;
    public Combo weaponCombo;
    
    [Header("Room Management References")]
    public RoomManager roomManager;
    public Roomvariants roomvariants;
    
    private GameObject _createdPlayer;
    public void ConstructPlayer()
    {
        //Destroy previous player
        Destroy(currentPlayer);
        //Create PlayerMaster
        currentPlayer = Instantiate(playerMasterPrefab);
        currentPlayer.name = "PlayerMaster";
        playerManager = PlayerManager.Instance;

        //Setup playerAnimator / Make playeranimator and playercharacter come from a source for character selection.
        //ADD Skills holder object
        var sk = new GameObject("Skills");
        //Get list of selected skills 
        //ADD Selected skills to this gameobject
        //sk.AddComponent<SprintSkill>();
        //Set skill holder parent to playermaster
        sk.transform.SetParent(currentPlayer.transform);
        //UPDATE Skills on playermanager
        //_playerManager.UpdateSkills();
        
        sk.tag = "Player";
        currentPlayer.tag = "Player";
    }

    public void StartGameplayLoop()
    {
        StartCoroutine("CreateGame");
    }

    public void StopGameplayLoop()
    {
        GameOver();
        //Cleanup gameplay loop
        //Save XP gained etc
    }
    public virtual IEnumerator CreateGame()
    {
        yield return new WaitForSeconds(0.2f);
        ConstructPlayer();
        InitializePlayer();
        DontDestroyOnLoad(currentPlayer);
        yield return new WaitForSeconds(0.2f);
        CreateRoomManager();
        roomManager.StartRoomManager();
        yield return new WaitForSeconds(0.2f);
        CreateMenuManager();
        PlayerManager.Instance.playerInputReader.EnablePlayerInput();
    }

    private void CreateMenuManager()
    {
        currentPauseMenu = Instantiate(pauseMenu);
    }
    public void GameOver()
    {
        roomManager.enabled = false;
        Destroy(roomManager);
        Destroy(currentPauseMenu);
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
        roomManager.possibleRooms = roomvariants.possibleRooms;
    }

    private void AddSkills(GameObject sk)
    {
        sk.AddComponent<SprintSkill>();
    }

    public void SetTimeScale(float amount)
    {
        Time.timeScale = amount;
    }
}
