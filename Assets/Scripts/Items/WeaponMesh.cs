using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponMesh : MonoBehaviour
{
    public MeshRenderer wm;
    void Awake()
    {
        wm = GetComponent<MeshRenderer>();
    }
}
