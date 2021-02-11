using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class RoomExit : DoorSwitch
    {
        //SWITCHES DOOR LOGIC FROM CLOSED TO OPEN WHEN ALL ENEMIES ARE KILLED
        //REQUIRES INTERACTION OR TRIGGER?
        public RoomManager roomManager;
        
        private void OnTriggerEnter(Collider other)
        {
            if (roomManager.AllEnemiesCleared())
            {
                try
                {
                    roomManager.GoToNextLevel();
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