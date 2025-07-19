using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is intended to create a pulsing effect for an object's outline width
public class OutlinePulse : MonoBehaviour
{
    // Public variable to hold the current outline width
    public float outlineWidth;

    private void Update()
    {
        // This line currently resets the outlineWidth every frame to a static Lerp result
        outlineWidth = Mathf.Lerp(3f, 7f, 1 * Time.deltaTime);

        // Output the current width to the console (for debugging purposes)
        Debug.Log(outlineWidth);
    }
}
