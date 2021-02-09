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
                try
                {
                    _roomManager.GoToNextLevel();
                }
                catch
                {
                    Debug.Log("No room manager in the scene!");
                }
            }
            else
            {
                //ENEMIES STILL REMAINING
            }
        }
    }