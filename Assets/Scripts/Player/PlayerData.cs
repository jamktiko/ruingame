using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerData : EntityData
    {
        [Header("Movement Tuning")]
        public float turnSmoothing = 15f;
        public float stepHeight = 0.3f;
        public float stepSmooth = 2f;
    
        [Header("Animator Values")]
        public float movementAnimatorDeceleration = 10f;
        public float movementAnimatorAcceleration = 10f;

        [Header("Current Artifacts")] public List<Artifact> artifactList = new List<Artifact>();

    }
}