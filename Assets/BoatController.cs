using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float value = Input.GetAxis("Horizontal");
        
        rb.AddForce(new Vector3( value, 0.1f, 0) * moveSpeed * Time.deltaTime, ForceMode.Acceleration);
    }
}
