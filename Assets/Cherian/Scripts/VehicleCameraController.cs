using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCameraController : MonoBehaviour
{
    public Transform target;               // The target to follow (the car).
    public Vector3 positionOffset = new Vector3(0, 2, -5); // Offset from the target's position.
    public float followDistance = 10.0f;   // The initial follow distance.
    public float maxDistance = 20.0f;      // The maximum follow distance.
    public float distanceMultiplier = 2.0f; // How much the distance increases with speed.
    public float smoothTime = 0.3f;        // Smoothing time for camera movement.
    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    private Rigidbody carRigidbody; // Assuming the car has a Rigidbody for velocity.

    void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("Target not assigned to FollowCar script.");
            return;
        }

        offset = positionOffset;
        carRigidbody = target.GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        float currentDistance = offset.magnitude;

        // Calculate the desired follow distance based on car speed.
        float desiredDistance = followDistance + (carRigidbody.velocity.magnitude * distanceMultiplier);

        // Gradually adjust the distance.
        float smoothedDistance = Mathf.Lerp(currentDistance, desiredDistance, smoothTime * Time.deltaTime);

        // Ensure the distance stays within the specified range.
        smoothedDistance = Mathf.Clamp(smoothedDistance, followDistance, maxDistance);

        // Calculate the desired position for the camera.
        Vector3 desiredPosition = target.position + offset.normalized * smoothedDistance;

        // Smoothly move the camera towards the desired position.
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

        // Make the camera look at the target (car).
        transform.LookAt(target);
    }
}
