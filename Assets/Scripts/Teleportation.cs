using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Handles the teleportation system in the game
public class Teleportation : MonoBehaviour
{
    // Reference to RightHandTele gameobject
    public GameObject RightHandTele;

    // Reference to RightHandAction gameobject
    public GameObject RightHandAction;

    // Input action reference for toggling teleport interaction
    public InputActionReference toggleTeleportReference = null;

    // Called every frame
    void Update()
    {
        // Reads the current value of the input action as a Vector2
        Vector2 value = toggleTeleportReference.action.ReadValue<Vector2>();
        print("arvo: " + value);
    }
}
