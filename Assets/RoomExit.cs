using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomExit : DoorSwitch
    {
        //SWITCHES DOOR LOGIC FROM CLOSED TO OPEN WHEN ALL ENEMIES ARE KILLED
        //REQUIRES INTERACTION OR TRIGGER?
        [SerializeField]
        protected RoomManager _roomManager;
        private void OnTriggerEnter(Collider other)
        {
            if (_roomManager.AllEnemiesCleared())
            {
               
            }
            else
            {
                //ENEMIES REMAINING
            }
        }
    }