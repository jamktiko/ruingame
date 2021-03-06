﻿using UnityEngine;

    [CreateAssetMenu(fileName = "CameraSettings", menuName = "CameraSettings", order = 0)]
    public class CameraSettings : ScriptableObject
    {
        public float CameraSensitivity;
        public bool invertAxisX;
        public bool invertAxisY;
        public float XSmoothing;
        public float YSmoothing;
        public float XAcceleration;
        public float YAcceleration;
    }