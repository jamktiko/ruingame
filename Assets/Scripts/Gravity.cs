using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class Gravity : MonoBehaviour
{
    private Rigidbody _rb;
    [FormerlySerializedAs("GravityForce")] public float gravityForce = -9.81f;
    public void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {

        _rb.AddForce(Vector3.up*gravityForce,ForceMode.Acceleration);
    }
}
