using UnityEngine;

    [CreateAssetMenu(fileName = "CameraSettings", menuName = "CameraSettings", order = 0)]
    public class CameraSettings : ScriptableObject
    {
        public float CameraSensitivity;
        public int invertAxisX = -1;
        public int invertAxisY = -1;
    }