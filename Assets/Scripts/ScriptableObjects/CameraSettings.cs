using UnityEngine;

    [CreateAssetMenu(fileName = "CameraSettings", menuName = "CameraSettings", order = 0)]
    public class CameraSettings : ScriptableObject
    {
        public float CameraSensitivity;
        public bool invertAxisX;
        public bool invertAxisY;
    }