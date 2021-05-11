using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMixer : MonoBehaviour
{
    FMOD.Studio.Bus musicBus;
    FMOD.Studio.Bus effectsBus;

    [SerializeField] [Range(-80f, 10f)]
    private float musicBusVolume;

    [SerializeField] [Range(-80f, 10f)]
    private float effectsBusVolume;

    void Start()
    {
        musicBus = FMODUnity.RuntimeManager.GetBus("bus:/MusicBus");
        effectsBus = FMODUnity.RuntimeManager.GetBus("bus:/VFXBus");
    }

    
    void Update()
    {
        musicBus.setVolume(DecibelToLinear(musicBusVolume));
        effectsBus.setVolume(DecibelToLinear(effectsBusVolume));
    }

    private float DecibelToLinear(float dB)
    {
        float linear = Mathf.Pow(10.0f, dB / 20f);
        return linear;
    }

}
