using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Makes this GameObject follow a target transform (player's left hand),
// maintaining an initial positional offset and matching rotation.
public class FollowLeftHand : MonoBehaviour
{
    // The transform of this object should follow (controller or hand)
    public Transform toFollow;

    // Initial positional offset between follower and target
    private Vector3 offset;

    // Called once in start
    // Calculates the initial offset from the target
    void Start()
    {
        offset = toFollow.position - transform.position;
    }

    // Updates every frame
    // Applies position offset and copies rotation from target
    void Update()
    {
        // Maintain initial offset relative to the target
        transform.position = toFollow.position - offset;
        
        // Match rotation exactly
        transform.rotation = toFollow.rotation;
    }
}
