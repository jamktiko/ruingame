using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private int currentRoom = 0;
    public List<GameObject> possibleRooms;
    //Get total enemies 
    private int totalEnemiesInRoom = 0;
    public GameObject currentRoomGO;
    public GameObject playerReference;
    private RoomExit _roomExit;
    private Transform _entryPoint;

    private bool creatingRoom = false;
    private void Awake()
    {
        //Created by Game Manager
        DontDestroyOnLoad(gameObject);
    }

    public bool AllEnemiesCleared()
    {
        totalEnemiesInRoom = currentRoomGO.GetComponentInChildren<SpawnerManager>().EnemiesRemaining();
        return (totalEnemiesInRoom <= 0);
    }

    public void GoToNextLevel()
    {
        CreateLevel();
        //Wait until player is loaded
        //Start Encounter.
    }

    public void CreateLevel()
    {
        if (creatingRoom)
            return;
        creatingRoom = true;
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
        var p = playerReference.GetComponent<CharacterController>();
        p.enabled = false;
        playerReference.transform.position = tr.position;
        p.enabled = true;
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
        currentRoomGO.GetComponentInChildren<RoomExit>()._roomManager = gameObject.GetComponent<RoomManager>();
        currentRoom++;
        _entryPoint = GameObject.FindGameObjectWithTag("Entry").transform;
        PlacePlayerAtEntry();
        yield return new WaitForSeconds(1f);
        //Deactivate loading screen
        creatingRoom = false;
    }
    public void StartRoomManager()
    {
        //Create a list of possible rooms from roomVariants
        CreateLevel();
        WaitUntilPlayerIsLoaded();
    }

    void WaitUntilPlayerIsLoaded()
    {
        Debug.Log("Player loaded!");
    }
}
