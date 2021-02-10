
using UnityEngine;
using System.Collections;

namespace DefaultNamespace
{
    public class SpawnArtifact : MonoBehaviour
    {
        public GameObject artifactPrefab;
        public Transform spawnPoint;

        public void BuildObject()
        {
            Instantiate(artifactPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}