using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
public class SpawnedArtifacts : MonoBehaviour
{
    // Start is called before the first frame update
    public static SpawnedArtifacts Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void DestroyGO()
    {

        Destroy(this.gameObject);

    }
}
