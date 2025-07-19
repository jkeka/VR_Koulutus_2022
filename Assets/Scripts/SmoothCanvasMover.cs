using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Script to move the canvas smoothly to desired target
public class SmoothCanvasMover : MonoBehaviour
{
    // The target transform the canvas will follow
    public Transform target;

    // Time it takes to smooth the movement
    private float smoothTime = 1.0f;

    // Current velocity, used by SmoothDamp for smooth interpolation
    Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        // Calculate the desired position relative to the target
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 5, -10));

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Match the rotation of the target to keep the canvas facing the same way
        transform.eulerAngles = target.eulerAngles;
    }
}
