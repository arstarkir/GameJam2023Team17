using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform target; // The target to follow (the vehicle).
    public Vector3 offset = new Vector3(0, 2, -5); // The preset offset from the target.
    public float followSpeed = 5.0f; // The speed at which the camera follows the target.

    private void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }

        // Calculate the desired position based on the target and offset.
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate the camera's position towards the desired position.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
