using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Handles the logic for reattaching bolts using a wrench
// during a specific stage of the game.
public class CoverBoltsAttach : MonoBehaviour
{
    // Material shown when the bolt is interactable
    public Material M_Int_Hover;
    
    // Default floor material
    public Material M_Floor;
    
    // Reference to the GameManager that controls global state
    public GameManager gameManager;

    // Input System reference to listen for interaction input
    public InputActionReference toggleReference = null;

    // Whether the player is in range to interact
    bool isOnPerimeter = false;

    // Initializes the object by assigning default material and subscribing to input
    void Awake()
    {
        toggleReference.action.started += Toggle;
        // Set initial material to floor material
        gameObject.GetComponent<Renderer>().material = M_Floor;
    }

    // Unsubscribes from the input event to avoid memory leaks
    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;
    }

    // Triggered when the player (by wrench) enters the bolt's interaction area
    // Highlights the bolt
    private void OnTriggerEnter(Collider other) //Toiminnallisuus, kun pelaaja menee sis��n 
    {
        if (other.tag == "Wrench" && gameManager.stageInt == 8)
        {
            gameManager.allBoltsFloorColor = false;
            gameObject.GetComponent<Renderer>().material = M_Int_Hover;
            isOnPerimeter = true;
        }
    }

    // Triggered when the player exits the bolt's interaction zone
    // Resets bolt material
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wrench" && gameManager.stageInt == 8)
        {
            gameObject.GetComponent<Renderer>().material = M_Floor;
            isOnPerimeter = false;
        }
    }
    
    // Called when the interaction input is triggered
    // If the bolt is interactable, attaches it
    private void Toggle(InputAction.CallbackContext context)
    {
        if (isOnPerimeter == true && gameManager.stageInt == 8)
        {
            bool isActive = !gameObject.activeSelf;
            AttachBolt();
            Debug.Log("Bolt clicked with wrench");
        }
    }
    
    // Handles the bolt attachment logic:
    // - Disables outline effect
    // - Increments counter
    // - Resets material and disables script
    public void AttachBolt()
    {
        gameManager.allBoltsLoose = false;
        Destroy(GetComponent<Outline>());
        gameObject.GetComponent<Renderer>().material = M_Floor;
        gameManager.PlayGranted();
        gameManager.attachedBolts++;
        Debug.Log("Bolt attached");
        Destroy(this);
    }
}
