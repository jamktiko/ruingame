
using UnityEngine;
using System.Collections.Generic;

namespace DefaultNamespace
{
    public class SpawnArtifact : MonoBehaviour
    {
        public enum SpawnType {Random, DragAndDrop, ListIndex, NextFromList }

        public GameObject artifactPrefab;
        public Transform spawnPoint;
        
        [Header("Choose spawn type to spawn with button")]
        public SpawnType spawnType;
        public int listIndex = 0;
        public string spawnedArtifact;
        public List<GameObject> artifactPrefabList;
        GameObject prefab;
        public List<GameObject> Commons;
        public List<GameObject> Rares;
        public List<GameObject> Mythics;
        public void BuildObject()
        {
            Vector3 spawnPos = PlayerManager.Instance.transform.position + Vector3.up * 5f;

            if (prefab != null)
                Destroy(prefab);

            switch (spawnType)
            {
                case SpawnType.Random:
                    SpawnRandomArtifact(spawnPos);
                    break;
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

       private void SpawnRandomArtifact(Vector3 spawnPos)
        {
            float rnd = Random.Range(0f, 1f);
            float common = 0.45f;
            float rare = 0.35f + common;
            float mythic = 1f;

            if (rnd <= common)
            {
                SpawnRandomIndex(Commons, spawnPos);
            }
            else if (rnd <= rare && rnd > common)
            {
                SpawnRandomIndex(Rares, spawnPos);
            }
            else if (rnd <= mythic && rnd > rare)
            {
                SpawnRandomIndex(Mythics, spawnPos);
            }
            else
            {
                Debug.Log("random out of range");
            }
        }

        void SpawnRandomIndex(List<GameObject> goList, Vector3 spawnPos)
        {
            int index = Mathf.FloorToInt(Random.Range(0f, goList.Count));
            prefab = Instantiate(goList[index], spawnPos + Vector3.up * 5f, Quaternion.identity);
        }

    }
}