
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    private int _currentRoom = 0;
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
    private int enemiesToSpawn = 2;
    private void Awake()
    {
        //Created by Game Manager
        DontDestroyOnLoad(gameObject);
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

    public void CreateLevel()
    {
        if (_creatingRoom)
            return;
        _creatingRoom = true;
        _currentRoom += 1;
        if (_currentRoom > 4)
        {
            GameManager.Instance.GameOver();
        }
        StartCoroutine(WaitCreation());
    }
    
    private GameObject GetRandomRoomFromList()
    {
        return possibleRooms[0];
    }

    public void PlacePlayerInLoading()
    {
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
        PlacePlayerInLoading();
        /*
        if (currentRoomGO != null)
        {
            Destroy(currentRoomGO);
        }
        currentRoomGO = Instantiate(GetRandomRoomFromList());
        currentRoomGO.GetComponentInChildren<RoomExit>().roomManager = gameObject.GetComponent<RoomManager>();
        _currentRoom++;
        */
        SceneManager.LoadScene("Room" + _currentRoom);
        yield return new WaitForSeconds(0.5f);
        currentRoomGO = GameObject.FindGameObjectWithTag("Room");
        spawnerManager = GameObject.FindGameObjectWithTag("SpawnerManager").GetComponent<SpawnerManager>();
        _entryPoint = GameObject.FindGameObjectWithTag("Entry").transform;
        
        PlacePlayerAtEntry();
        
        spawnerManager.enemiesToSpawn = enemiesToSpawn;
        enemiesToSpawn += 2;
        yield return new WaitForSeconds(1f);
        //Deactivate loading screen
        spawnerManager.StartSpawners();
        _creatingRoom = false;
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
