using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;


public class CombatMusicPlayer : MonoBehaviour
{
    private static FMOD.Studio.EventInstance Music;
    float roomNumber = 1.0f;
    

    void Start()
    {
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Combat_music");
        Music.start();
        Music.release();
    }
    private void OnDestroy()
    {
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    void Update()
    {
        Scene currentRoom = SceneManager.GetActiveScene();
        string roomName = currentRoom.name;

        float playerHealthParameter = PlayerManager.Instance._playerHealth.CurrentHealth;

        Music.setParameterByName("RoomName", roomNumber);
        Music.setParameterByName("PlayerHealth", playerHealthParameter);

        if (roomName == "Room1")
        {
            roomNumber = 1.0f;
        }
        else if (roomName == "Room2")
        {
            roomNumber = 2.0f;
        }
        else if (roomName == "Room3")
        {
            roomNumber = 3.0f;
        }
        else if (roomName == "Room4")
        {
            roomNumber = 4.0f;
        }

    }
}
