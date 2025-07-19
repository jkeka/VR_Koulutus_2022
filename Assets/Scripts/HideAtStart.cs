using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Disables the GameObject this script is attached immediately when the scene loads
public class HideAtStart : MonoBehaviour
{
    // Called once when the script instance is being loaded.
    // Sets the GameObject inactive to hide it at the start.
    private void Awake()
    {
        gameObject.SetActive(false);
    }
}
