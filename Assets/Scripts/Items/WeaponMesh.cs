
using UnityEngine;


public class WeaponMesh : MonoBehaviour
{
    public MeshRenderer wm;
    void Awake()
    {
        wm = GetComponent<MeshRenderer>();
    }
}
