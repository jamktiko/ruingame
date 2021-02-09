using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class Artifact : MonoBehaviour
{
    public PlayerManager _playerReference;
    
    public ArtifactEffect _ArtifactEffect;

    private int pickupeventcount = 0;
    private void Start()
    {
        _ArtifactEffect = GetComponent<ArtifactEffect>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickupeventcount++;
            if (pickupeventcount == 2)
            {
                _playerReference = other.gameObject.GetComponent<PlayerManager>();
                var pickup = _playerReference.pickUpEvent;
                pickup.AddListener(OnPickup);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickupeventcount--;
            if (pickupeventcount == 0)
            {
                _playerReference = other.gameObject.GetComponent<PlayerManager>();
                var pickup = _playerReference.pickUpEvent;
                pickup.RemoveAllListeners();
            }
        }
    }

    private void OnPickup()
    {
        try
        {
            _ArtifactEffect.AddEffect(_playerReference);
            _playerReference.AddArtifact(gameObject.GetComponent<Artifact>());
        }
        catch
        {
            Debug.Log(this.name + " has no artifact effect applied!!");
        }
        _playerReference.pickUpEvent.RemoveAllListeners();
    }
}
