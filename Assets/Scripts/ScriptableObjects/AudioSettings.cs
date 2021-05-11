using UnityEngine;

    [CreateAssetMenu(fileName = "AudioSettings", menuName = "AudioSettings", order = 0)]
    public class AudioSettings : ScriptableObject
    {
        public float musicVolume;
        public float effectVolume;
    }