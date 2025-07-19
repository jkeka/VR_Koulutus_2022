using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Keeps the menu canvas locked at a specific height and upright rotation
// This is useful in VR or 3D scenes to prevent unwanted movement or tilting of UI elements
public class MenuCanvasMover : MonoBehaviour
{
    // Called every frame
    private void Update()
    {
        // Lock the Y position of the canvas to 3.36 units
        transform.position = new Vector3(transform.position.x, 3.36f, transform.position.z);

        // Keep the canvas rotation level by locking X and Z rotations to 0
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
