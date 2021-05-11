using System.Collections;
using System.IO;
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
    private const string AUDIOFILENAME = "AudioSettings.dat";
    FMOD.Studio.Bus musicBus;
    FMOD.Studio.Bus effectsBus;

    public float musicVolume;
    public float effectVolume;
    public float defaultMusic = 0f;
    public float defaultEffect = 0f;

    public int startingRoom = 0;
    public GameObject pauseMenu;
    public GameObject currentPauseMenu;
    public SkillExecute[] playerSkillList;
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

    private void Start()
    {
        LoadSettings();
    }
    public void ConstructPlayer()
    {
        //Destroy previous player
        try{Destroy(currentPlayer);}
        catch{}
        //Create PlayerMaster
        currentPlayer = Instantiate(playerMasterPrefab);
        currentPlayer.name = "PlayerMaster";
        playerManager = PlayerManager.Instance;

        var sk = new GameObject("Skills");
        foreach (SkillExecute sks in playerSkillList)
        {
            var skill = sk.AddComponent(sks.GetType());
        }
        sk.transform.SetParent(currentPlayer.transform);
        
        playerSkillList = sk.GetComponentsInChildren<SkillExecute>();
        
        currentPlayer.tag = "Player";
        PlayerManager.Instance.InitializeSkills();
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(0.2f);
        ConstructPlayer();
        InitializePlayer();
        DontDestroyOnLoad(currentPlayer);
        yield return new WaitForSeconds(0.2f);
        CreateRoomManager();
        roomManager._currentRoom = startingRoom;
        roomManager.StartRoomManager();
        yield return new WaitForSeconds(0.2f);
        CreateMenuManager();
    }

    private void CreateMenuManager()
    {
        currentPauseMenu = Instantiate(pauseMenu);
    }

    public void ResetPauseMenu()
    {
        StartCoroutine(ResetPause());
    }
    public virtual IEnumerator ResetPause()
    {
        currentPauseMenu.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        currentPauseMenu.gameObject.SetActive(true);
    }
    public void GameOver()
    {
        roomManager.enabled = false;
        Destroy(roomManager);
        Destroy(currentPauseMenu);
        currentPlayer.GetComponent<PlayerManager>().enabled = false;
        Destroy(currentPlayer);
        SpawnedArtifacts.Instance.DestroyGO();
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

    public void SetTimeScale(float amount)
    {
        Time.timeScale = amount;
    }
    public void LoadSettings()
    {
        musicBus = FMODUnity.RuntimeManager.GetBus("bus:/MusicBus");
        effectsBus = FMODUnity.RuntimeManager.GetBus("bus:/VFXBus");
        try
        {
            LoadDataFromFile();
        }
        catch
        {
            musicVolume = defaultMusic;
            effectVolume = defaultEffect;
            
        }
        musicBus.setVolume(DecibelToLinear(musicVolume));
        effectsBus.setVolume(DecibelToLinear(effectVolume));
    }
    public void LoadDataFromFile()
    {
        var filePath = Path.Combine(Application.persistentDataPath, AUDIOFILENAME);

        if(!File.Exists(filePath))
        {
            Debug.LogWarning($"File \"{filePath}\" not found!", this);
            return;
        }

        var json = File.ReadAllText(filePath);
        JsonUtility.FromJsonOverwrite(json, this);
    }
    private float DecibelToLinear(float dB)
    {
        float linear = Mathf.Pow(10.0f, dB / 20f);
        return linear;
    }


}

