using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DangerArrow is a simple script that rotates an arrow to point towards a target
// Useful for warning indicators or directional guidance in 3D space

public class DangerArrow : MonoBehaviour
{
    // Target that the arrow should point to
    public Transform target;
    Renderer m_RendererTarget;
    // Rotation speed in degrees per second
    public float speed = 5.0f;

    // Updates the arrow's orientation to rotate toward the target
    void Update()
    {
        // Calculate direction vector from this object to the target
        Vector3 targetDirection = target.position - transform.position;
        // Determine the step size for this frame based on speed
        float singleStep = speed * Time.deltaTime;
        // Calculate the new direction vector by rotating toward the target
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        // Apply the new rotation to the arrow object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
