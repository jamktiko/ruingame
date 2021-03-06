﻿
using DefaultNamespace;
using UnityEngine;


public class Artifact : MonoBehaviour
{
    public PlayerManager PlayerReference;
    public ArtifactInfo ArtifactInfo;
    public ArtifactEffect ArtifactEffect;

    private int _pickupeventcount = 0;
    private void Start()
    {
        ArtifactEffect = GetComponent<ArtifactEffect>();

    }

    private void Awake()
    {
        PlayerReference = PlayerManager.Instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _pickupeventcount++;
            if (_pickupeventcount >= 2)
            {
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
                var pickup = PlayerReference.pickUpEvent;
                pickup.RemoveAllListeners();
            }
        }
    }

    private void OnPickup()
    {
        try
        {
            ArtifactEffect.AddEffect();
            PlayerReference.AddArtifact(gameObject.GetComponent<Artifact>());
        }
        catch
        {
            Debug.Log(this.name + " has no artifact effect applied!!");
        }
        PlayerReference.pickUpEvent.RemoveAllListeners();
    }
}
