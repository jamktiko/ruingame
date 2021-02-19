
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private int _currentRoom = 0;
    public List<GameObject> possibleRooms;
    //Get total enemies 
    private int _totalEnemiesInRoom = 0;
    private GameObject currentRoomGO;
    private GameObject playerReference;
    private RoomExit _roomExit;
    private Transform _entryPoint;

    private bool _creatingRoom = false;
    private void Awake()
    {
        //Created by Game Manager
        DontDestroyOnLoad(gameObject);
    }

    public bool AllEnemiesCleared()
    {
        _totalEnemiesInRoom = currentRoomGO.GetComponentInChildren<SpawnerManager>().EnemiesRemaining();
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
        if (currentRoomGO != null)
        {
            Destroy(currentRoomGO);
        }
        currentRoomGO = Instantiate(GetRandomRoomFromList());
        currentRoomGO.GetComponentInChildren<RoomExit>().roomManager = gameObject.GetComponent<RoomManager>();
        _currentRoom++;
        _entryPoint = GameObject.FindGameObjectWithTag("Entry").transform;
        PlacePlayerAtEntry();
        yield return new WaitForSeconds(1f);
        //Deactivate loading screen
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
