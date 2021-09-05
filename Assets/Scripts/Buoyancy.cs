using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Buoyancy : MonoBehaviour
{
    private List<Rigidbody> buoyants;

    public float force = 1000f;
    // Start is called before the first frame update
    void Start()
    {
        buoyants = new List<Rigidbody>();
    }

    // Update is called once per frame
    
    
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb!= null && rb.CompareTag("Buoyant"))
        {
            // buoyants.Add(rb);
            rb.AddForce(Vector3.up * force + Random.insideUnitSphere * 0.1f, ForceMode.Acceleration);
            rb.useGravity = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb!= null && rb.CompareTag("Buoyant"))
        {
            // buoyants.Add(rb);
            rb.useGravity = true;

            rb.AddRelativeForce(Vector3.up * force + Random.insideUnitSphere * 0.1f, ForceMode.Acceleration);
        }
    }
}
