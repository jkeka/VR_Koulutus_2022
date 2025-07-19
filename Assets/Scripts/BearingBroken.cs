using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles the behavior of a broken bearing in the game.
/// Allows interaction when the player enters a specific area
/// and activates the bearing with an input action.
/// </summary>

public class BearingBroken : MonoBehaviour
{
    // Material displayed when the bearing is highlighted (e.g. player nearby)
    public Material M_Int_Hover;
    
    // Default floor material when not highlighted
    public Material M_Floor;
    
    // Reference to the GameManager controlling game state
    public GameManager gameManager;

    // Input action reference for toggling interaction (button press)
    public InputActionReference toggleReference = null;

    // Tracks whether the player is currently inside the interaction area
    bool isOnPerimeter = false;

    void Awake()
    {
        toggleReference.action.started += Toggle;
    }

    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;

    }

    // Triggered when the player enters the interaction area 
    //Higlights the bearing if game is at correct state
    private void OnTriggerEnter(Collider other) //Toiminnallisuus, kun pelaaja menee sis��n 
    {
        if (other.tag == "BearingExtractor" && gameManager.stageInt == 5)
        {
            gameObject.GetComponent<Renderer>().material = M_Int_Hover;
            isOnPerimeter = true;
        }
    }

    // Triggered when the player exits the interaction area
    // Removes highlight from the bearing
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BearingExtractor")
        {
            gameObject.GetComponent<Renderer>().material = M_Floor;
            isOnPerimeter = false;
        }
    }

    // Called when the player attempts to interact via input
    // Extracts the bearing if within interaction area
    private void Toggle(InputAction.CallbackContext context)
    {
        if (isOnPerimeter == true)
        {
            bool isActive = !gameObject.activeSelf;
            ExtractBearing();
            Debug.Log("Bearing clicked with extractor");
        }
    }

    // Handles the extraction logic:
    // - Plays feedback
    // - Disables the bearing
    // - Advances game state
    public void ExtractBearing()
    {
        gameManager.PlayGranted();
        Debug.Log("Bearing extracted");
        gameObject.SetActive(false);
        gameManager.stageInt = 6;
        Destroy(this);
    }
}
