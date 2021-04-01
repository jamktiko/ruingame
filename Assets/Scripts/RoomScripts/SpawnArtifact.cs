
using UnityEngine;
using System.Collections.Generic;

namespace DefaultNamespace
{
    public class SpawnArtifact : MonoBehaviour
    {
        public enum SpawnType { DragAndDrop, ListIndex, NextFromList }

        public GameObject artifactPrefab;
        public Transform spawnPoint;
        
        [Header("Choose spawn type to spawn with button")]
        public SpawnType spawnType;
        public int listIndex = 0;
        public string spawnedArtifact;
        public List<GameObject> artifactPrefabList;
        GameObject prefab;
        public void BuildObject()
        {
            Vector3 spawnPos = PlayerManager.Instance.transform.position + Vector3.up * 5f;
            if (prefab != null)
                Destroy(prefab);

            switch (spawnType)
            {
                case SpawnType.DragAndDrop:
                    prefab = Instantiate(artifactPrefab, spawnPoint.position, Quaternion.identity);
                    break;
                case SpawnType.ListIndex:
                    prefab = Instantiate(artifactPrefabList[listIndex], spawnPos, Quaternion.identity);
                    break;
                case SpawnType.NextFromList:
                    if (listIndex == artifactPrefabList.Count - 1)
                        listIndex = 0;
                    else
                        listIndex++;
                    prefab = Instantiate(artifactPrefabList[listIndex], spawnPos, Quaternion.identity);
                    break;
                default:
                    break;
            }
            spawnedArtifact = prefab.name;
        }
    }
}