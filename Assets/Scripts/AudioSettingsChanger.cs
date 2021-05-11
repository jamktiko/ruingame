
using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class AudioSettingsChanger : MonoBehaviour
{

    FMOD.Studio.Bus musicBus;
    FMOD.Studio.Bus effectsBus;

    public float musicVolume;
    public float effectVolume;
    private float defaultMusic = 0f;
    private float defaultEffect = 0f;
    private const string FILENAME = "AudioSettings.dat";
    private float DecibelToLinear(float dB)
    {
        float linear = Mathf.Pow(10.0f, dB / 20f);
        return linear;
    }

    private void Awake()
    {
        musicBus = FMODUnity.RuntimeManager.GetBus("bus:/MusicBus");
        effectsBus = FMODUnity.RuntimeManager.GetBus("bus:/VFXBus");
        
        LoadSettings();
    }
    
    private void OnEnable()
    {        
        var sliders = GetComponentsInChildren<Slider>();
        
        LoadSettings();

        sliders[0].value = musicVolume;
        sliders[1].value = effectVolume;
    }

    private void OnDisable()
    {
        try {SaveToFile();}
        catch{}
    }

    private void OnApplicationQuit()
    {
        try
        {
            SaveToFile();
            
        }
        catch{}
    }

    public void LoadSettings()
    {
        try
        {
            LoadDataFromFile();
        }
        catch
        {
            musicVolume = defaultMusic;
            effectVolume = defaultEffect;
           
        }
        musicBus.setVolume(DecibelToLinear(musicVolume));
        effectsBus.setVolume(DecibelToLinear(effectVolume));
    }

    public void UpdateMusicVolume(Slider slider)
    {
        musicVolume = slider.value;
        musicBus.setVolume(DecibelToLinear(musicVolume));
        musicBus.setMute(slider.value < -80f);
    }
    public void UpdateEffectVolume(Slider slider)
    {
        effectVolume = slider.value;
        effectsBus.setVolume(DecibelToLinear(effectVolume));
        effectsBus.setMute(slider.value < -80f);
    }
    public void SaveToFile()
    {
        var filePath = Path.Combine(Application.persistentDataPath, FILENAME);

        if(!File.Exists(filePath))
        {
            File.Create(filePath);
        }
        var json = JsonUtility.ToJson(this);
        File.WriteAllText(filePath, json);
    }
    public void LoadDataFromFile()
    {
        var filePath = Path.Combine(Application.persistentDataPath, FILENAME);

        if(!File.Exists(filePath))
        {
            Debug.LogWarning($"File \"{filePath}\" not found!", this);
            return;
        }

        var json = File.ReadAllText(filePath);
        JsonUtility.FromJsonOverwrite(json, this);
    }

}
