
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;



public class RoomManager : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public int _currentRoom = 0;
    public List<GameObject> possibleRooms;
    //Get total enemies 
    private int _totalEnemiesInRoom = 0;
    public GameObject currentRoomGO;
    private GameObject playerReference;
    private RoomExit _roomExit;
    private Transform _entryPoint;
    public bool EnemiesCleared = false;
    public SpawnerManager spawnerManager;
    private bool _creatingRoom = false;
    private int enemiesToSpawn = 4;
    private void Awake()
    {
        //Created by Game Manager
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
       canvasGroup = GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<CanvasGroup>();
    }

    public bool AllEnemiesCleared()
    {
        _totalEnemiesInRoom = spawnerManager.EnemiesRemaining();
        EnemiesCleared = _totalEnemiesInRoom <= 0;
        return (_totalEnemiesInRoom <= 0);
    }

    public void GoToNextLevel()
    {
        CreateLevel();
        //Wait until player is loaded
        //Start Encounter.
    }

    private bool bossRoom = false;
    public void CreateLevel()
    {
        if (_creatingRoom)
            return;
        _creatingRoom = true;
        _currentRoom += 1;
        if (_currentRoom > 5)
        {
            _currentRoom = 1;
        }
        if (_currentRoom == 5)
        {
            bossRoom = true;
        }
        else
        {
            bossRoom = false;
        }
        StartCoroutine(WaitCreation());
    }
    
    private GameObject GetRandomRoomFromList()
    {
        return possibleRooms[0];
    }

    public void PlacePlayerInLoading()
    {
        PlayerManager.Instance.DisablePlayerInput();
        MovePlayerToLocation(gameObject.transform);
    }
    public void PlacePlayerAtEntry()
    {
        MovePlayerToLocation(_entryPoint);
    }

    public void MovePlayerToLocation(Transform tr)
    {
        playerReference.transform.position = tr.position;
        playerReference.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    public virtual IEnumerator WaitCreation()
    {
        //Activate Loading Screen
        /*
        if (currentRoomGO != null)
        {
            Destroy(currentRoomGO);
        }
        currentRoomGO = Instantiate(GetRandomRoomFromList());
        currentRoomGO.GetComponentInChildren<RoomExit>().roomManager = gameObject.GetComponent<RoomManager>();
        _currentRoom++;
        */
        PlayerManager.Instance.DisablePlayerInput();
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(FadeLoadingScreenIn(0.5f));
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Room" + _currentRoom);
        yield return new WaitForSeconds(0.2f);
        PlacePlayerInLoading();
        yield return new WaitForSeconds(0.5f);
        spawnerManager = GameObject.FindGameObjectWithTag("SpawnerManager").GetComponent<SpawnerManager>();
        _entryPoint = GameObject.FindGameObjectWithTag("Entry").transform;

        PlacePlayerAtEntry();
        spawnerManager.enemiesToSpawn = enemiesToSpawn;
        enemiesToSpawn += 2;
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeLoadingScreen(1));
        yield return new WaitForSeconds(1f);
        //Deactivate loading screen
        PlayerManager.Instance.EnablePlayerInput();

        if (bossRoom)
        {
            spawnerManager.SpawnBoss();
        }
        else
        {
            spawnerManager.StartSpawners();
        }
        _creatingRoom = false;
    }
    IEnumerator FadeLoadingScreen(float duration)
    {
        float startValue = 0;
        float time = 0;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }
    IEnumerator FadeLoadingScreenIn(float duration)
    {
        float startValue = 0;
        float time = 0;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }
    public void StartRoomManager()
    {
        //Create a list of possible rooms from roomVariants
        playerReference = GameManager.Instance.currentPlayer;
        CreateLevel();
        WaitUntilPlayerIsLoaded();
    }

    void WaitUntilPlayerIsLoaded()
    {
    }
}
