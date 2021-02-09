using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameManager GameManager;

    public void StartGame()
    {
        GameManager.StartGame();
        this.enabled = false;
    }

    void StopGame()
    {
        
    }

    private void Awake()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
}

