
using System;
using UnityEngine;


public class WeaponMesh : MonoBehaviour
{
    public MeshRenderer wm;
    public ParticleSystem ps;
    void Start()
    {
        wm = GetComponentInChildren<MeshRenderer>();
        ps = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            ps.Play();
        }
    }
}
