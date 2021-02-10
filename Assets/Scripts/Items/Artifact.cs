using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Artifact : MonoBehaviour
{
    public PlayerManager PlayerReference;
    
    public ArtifactEffect ArtifactEffect;

    private int _pickupeventcount = 0;
    private void Start()
    {
        ArtifactEffect = GetComponent<ArtifactEffect>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _pickupeventcount++;
            if (_pickupeventcount == 2)
            {
                PlayerReference = other.gameObject.GetComponent<PlayerManager>();
                var pickup = PlayerReference.pickUpEvent;
                pickup.AddListener(OnPickup);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _pickupeventcount--;
            if (_pickupeventcount == 0)
            {
                PlayerReference = other.gameObject.GetComponent<PlayerManager>();
                var pickup = PlayerReference.pickUpEvent;
                pickup.RemoveAllListeners();
            }
        }
    }

    private void OnPickup()
    {
        try
        {
            ArtifactEffect.AddEffect(PlayerReference);
            PlayerReference.AddArtifact(gameObject.GetComponent<Artifact>());
        }
        catch
        {
            Debug.Log(this.name + " has no artifact effect applied!!");
        }
        PlayerReference.pickUpEvent.RemoveAllListeners();
    }
}
