using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomExit : DoorSwitch
    {
        //SWITCHES DOOR LOGIC FROM CLOSED TO OPEN WHEN ALL ENEMIES ARE KILLED
        //REQUIRES INTERACTION OR TRIGGER?
        public RoomManager _roomManager;
        
        private void OnTriggerEnter(Collider other)
        {
            if (_roomManager.AllEnemiesCleared())
            {
                Debug.Log("Player Exited Level!");
                _roomManager.GoToNextLevel();
            }
            else
            {
                //ENEMIES STILL REMAINING
            }
        }
    }