using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Handles interaction with bolts during the tutorial phase
// Allows the user to attach bolts using a wrench tool when in the correct tutorial stage
public class TutorialBolts : MonoBehaviour
{
    // Materials for visual feedback
    public Material M_Int_Hover;
    public Material M_Floor;

    // Recefence to Tutorial Manager
    public TutorialManager tutorialManager;

    // Input reference for triggering the interaction
    public InputActionReference toggleReference = null;

    // Tracks whether the player is within interaction range
    bool isOnPerimeter = false;

    // Called in beginning
    void Awake()
    {
        // Subscribe to input toggle event
        toggleReference.action.started += Toggle;
        
        // Set initial material appearance
        gameObject.GetComponent<Renderer>().material = M_Floor;
    }

    // Unsubscribe when object is destroyed to prevent memory leak
    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;
    }

    // Enable interaction only when the wrench touches and it's the correct stage
    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Wrench" && tutorialManager.tutStageInt == 5)
        {
            gameObject.GetComponent<Renderer>().material = M_Int_Hover;
            isOnPerimeter = true;
        }
    }

    // Reset visual feedback when wrench leaves the bolt
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wrench" && tutorialManager.tutStageInt == 5)
        {
            gameObject.GetComponent<Renderer>().material = M_Floor;
            isOnPerimeter = false;
        }
    }

    // If player is in range and it's the correct stage, interact
    private void Toggle(InputAction.CallbackContext context)
    {
        if (isOnPerimeter == true && tutorialManager.tutStageInt == 5)
        {
            bool isActive = !gameObject.activeSelf;
            AttachBolt();
            Debug.Log("Bolt clicked with wrench");
        }
    }

    // Handles the logic for attaching the bolt
    public void AttachBolt()
    {
        Destroy(GetComponent<Outline>());    // Remove visual highlight
        gameObject.GetComponent<Renderer>().material = M_Floor; // Change material
        tutorialManager.PlayGranted();    // Play confirmation audio
        tutorialManager.attachedBolts++;    // Track progress by adding 1 to attachedBolts integer
        Debug.Log("Bolt attached");
        Destroy(this);    // Disable further interactions
    }
}
