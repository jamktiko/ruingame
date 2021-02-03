using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    
    //HANDLES ROOM ORDERING

    private int currentRoom = 0;
    private GameObject[] possibleRooms;
    private int totalEnemiesInRoom = 0;
    public int startingIndex = 1;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }

    public bool AllEnemiesCleared()
    {
        return (totalEnemiesInRoom <= 0);
    }
}
