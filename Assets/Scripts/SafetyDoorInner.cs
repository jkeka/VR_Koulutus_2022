using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Safety door movement handling
public class SafetyDoorInner : MonoBehaviour
{
    // The transform this object will follow
    public Transform toFollow;

    // The initial offset between this object and the object it follows
    private Vector3 offset;

    // Called once at the start
    void Start()
    {
        // Calculate and store the initial positional offset between the follower and the target
        offset = toFollow.position - transform.position;
    }

    // Called once per frame
    void Update()
    {
        // Update this object's position to maintain the initial offset from the target
        transform.position = toFollow.position - offset;

        // Match this object's rotation to the target's rotation
        transform.rotation = toFollow.rotation;
    }
}
