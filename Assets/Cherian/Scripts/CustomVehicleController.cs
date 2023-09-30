using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomVehicleController : MonoBehaviour
{
    public float forwardForce = 1000.0f; // Adjust the forward force as needed.
    private Rigidbody vehicleRigidbody;

    private void Start()
    {
        // Get the Rigidbody component attached to the vehicle mesh.
        vehicleRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Apply a forward force to move the vehicle.
        Vector3 forwardForceVector = transform.forward * forwardForce;
        vehicleRigidbody.AddForce(forwardForceVector);
    }
}
