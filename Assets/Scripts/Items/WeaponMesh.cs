using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMesh : MonoBehaviour
{
    public MeshRenderer WM;
    void Awake()
    {
        WM = GetComponent<MeshRenderer>();
    }
}
