
using System;
using UnityEngine;


public class RoomExit : DoorSwitch
    {
        //SWITCHES DOOR LOGIC FROM CLOSED TO OPEN WHEN ALL ENEMIES ARE KILLED
        //REQUIRES INTERACTION OR TRIGGER?
        public RoomManager roomManager;
        public bool CanExit = true;
        private void OnTriggerEnter(Collider other)
        {
            if (roomManager.AllEnemiesCleared())
            {
                CanExit = true;
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
                CanExit = false;
            }
        }

        private void Awake()
        {
            roomManager = GameManager.Instance.GetComponent<RoomManager>();
        }
    }