using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Game/PlayerData")]
    public class PlayerData : EntityData
    {
        /* private float turnSmoothing = 15f;
         private float stepHeight = 0.3f;
         private float stepSmooth = 2f;
     
         [Header("Animator Values")]
         private float movementAnimatorDeceleration = 10f;
         private float movementAnimatorAcceleration = 10f;
        */
        [Header("Current Artifacts")] 
        public List<Artifact> artifactList = new List<Artifact>();

    }
}