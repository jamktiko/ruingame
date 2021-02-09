using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gravity : MonoBehaviour
{
    private Rigidbody rb;
    public float GravityForce = -9.81f;
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {

        rb.AddForce(Vector3.up*GravityForce,ForceMode.Acceleration);
    }
}
